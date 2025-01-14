﻿using Library.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.Managers
{
    public class TokenExpManger
    {
        private DateTime tokenexp;

        public TokenExpManger (DateTime exp)
        {
            this.TokenExp = exp;
        }

        public DateTime TokenExp
        {
            get { return tokenexp; }
            set { tokenexp = value; }
        }

        public async Task CloseSession ()
        {
            await Shell.Current.DisplayAlert("Notification", "Token has expired...!", "OK");

            await App.ComunicationService.Disconnect();

            UserConnectedDataStore userConnectedDataStore = new UserConnectedDataStore();

            if( App.UsersConnected != null )
            {
                App.UsersConnected.IsDisable = true;

                var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

                if( result )
                {
                    App.UsersConnected = null;
                }
            }
            App.Current.MainPage = new AppShell();

            await Shell.Current.GoToAsync("LoginRoute");
        }

        public bool IsExpired ()
        {
            if( DateTime.Now >= this.TokenExp )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}