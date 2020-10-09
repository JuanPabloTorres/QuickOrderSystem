using Library.DTO;
using Library.Models;
using Library.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuickOrderApp.LoginBuilder
{
   public abstract class LoginTokenBuilder
    {
        protected  TokenDTO UserLoginToken;
        protected IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();
        protected IUserConnectedDataStore userConnectedDataStore => DependencyService.Get<IUserConnectedDataStore>();

        protected IStripeServiceDS stripeServiceDS => DependencyService.Get<IStripeServiceDS>();
        public  void CreateLoginToken(string username,string password)
        {
            UserLoginToken = userDataStore.LoginCredential(username, password);
        }

        public TokenDTO GetLogin()
        {
            return UserLoginToken;
        }

        public abstract void VerifyLogin();
        public abstract void MakeHubConnection();

        public abstract void GoQuickOrderHome();

    }
}
