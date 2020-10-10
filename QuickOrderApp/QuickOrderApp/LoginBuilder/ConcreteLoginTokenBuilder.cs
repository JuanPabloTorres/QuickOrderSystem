using Library.Services.Interface;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using QuickOrderApp.Views.Login;
using System.Threading.Tasks;
using System.Linq;
using Library.Models;
using Library.Services;
using QuickOrderApp.Views.Store.EmployeeStoreControlPanel;

namespace QuickOrderApp.LoginBuilder
{
    public class ConcreteLoginTokenBuilder:LoginTokenBuilder
    {
        public async override void ErrorMessage()
        {
            await App.Current.MainPage.DisplayAlert("Notification", "Incorrect login...!", "OK");
        }

        public async override void GoQuickOrderHome()
        {
            App.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("//RouteName");
        }

        public  async override void GoEmployeeHome()
        {
            App.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("EmployeeControlPanelRoute");

        }

        public async override void MakeHubConnection()
        {
            Task.Run(async () =>
            {
                await App.ComunicationService.Connect();
            }).Wait();

            if (!String.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId))
            {

                App.UsersConnected = new UsersConnected()
                {
                    HubConnectionID = App.ComunicationService.hubConnection.ConnectionId,
                    UserID = App.LogUser.UserId,
                    IsDisable = false,
                    ConnecteDate = DateTime.Now
                };

                //var result = await userConnectedDataStore.ModifyOldConnections(App.UsersConnected);

                var hub_connected_Result = await userConnectedDataStore.AddItemAsync(App.UsersConnected);
            }
        }

        public async override void VerifyLogin()
        {
           

                var loginresult = UserLoginToken.UserDetail;

                if (!loginresult.IsValidUser)
                {
                    App.LogUser = loginresult;

                    await PopupNavigation.PushAsync(new ValidateEmailCode());
                 

                }
                else
                {
                   

                    App.LogUser = loginresult;

                    bool hasPaymentCard = App.LogUser.PaymentCards.Count() > 0 ? true : false;

                    //Verfico si hay tarjetas registradas con el usuario
                    if (hasPaymentCard)
                    {
                        var data = App.LogUser.PaymentCards;
                        var card = new List<PaymentCard>(data);


                        var userCardTokenId = await stripeServiceDS.GetCustomerCardId(App.LogUser.StripeUserId, card[0].StripeCardId);


                        App.CardPaymentToken.CardTokenId = userCardTokenId;

                    }
                    
                }

           
        }
    }
}
