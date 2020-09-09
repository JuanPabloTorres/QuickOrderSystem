using Library.Models;
using Library.Services.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuickOrderApp.Components
{
    public class LoginComponent
    {
        public IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }

        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }



        public LoginComponent(string _username,string _password)
        {
            this.Username = _username;
            this.Password = _password;



        }


        //public bool OnLogin()
        //{

        //}




    }
}
