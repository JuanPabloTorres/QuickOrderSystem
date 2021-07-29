using Library.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using QuickOrderApp.ViewModels.LoginVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static string Route = "LoginRoute";

        protected IUserConnectedDataStore userConnectedDataStore => Startup.ServiceProvider.GetRequiredService<IUserConnectedDataStore>();





        public LoginPage()
        {
            InitializeComponent();


            BindingContext = new LoginViewModel();
        }




        protected async override void OnAppearing()
        {
            base.OnAppearing();


            if (!string.IsNullOrEmpty(App.ComunicationService.hubConnection.ConnectionId))
            {
                await App.ComunicationService.Disconnect();



                if (App.UsersConnected != null)
                {
                    App.UsersConnected.IsDisable = true;
                    var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

                    if (result)
                    {
                        App.UsersConnected = null;
                    }

                }
            }



        }

    }
}