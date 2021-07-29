using Library.Services;
using QuickOrderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.Managers
{
    public class TokenExpManger : BaseViewModel
    {
        //readonly UserConnectedDataStore userConnectedDataStore;
        //public TokenExpManger(UserConnectedDataStore userConnectedDataStore)
        //{
        //    this.userConnectedDataStore = userConnectedDataStore;
        //}
        private DateTime tokenexp;

        public DateTime TokenExp
        {
            get { return tokenexp; }
            set { tokenexp = value; }
        }

        public TokenExpManger(DateTime exp)
        {
            this.TokenExp = exp;
        }

        public bool IsExpired()
        {

            if (DateTime.Now >= this.TokenExp)
            {
                return true;

            }
            else
            {
                return false;
            }

        }


        public async Task CloseSession()
        {
            await Shell.Current.DisplayAlert("Notification", "Token has expired...!", "OK");

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
            App.Current.MainPage = new AppShell();

            await Shell.Current.GoToAsync("LoginRoute");
        }


    }
}
