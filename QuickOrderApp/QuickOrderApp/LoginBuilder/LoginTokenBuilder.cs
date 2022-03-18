using Library.ApiResponses;
using Library.DTO;
using Library.Models;
using Library.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QuickOrderApp.LoginBuilder
{
    public abstract class LoginTokenBuilder
    {
        protected LoginResponse UserLoginToken;
        protected IUserDataStore userDataStore => Startup.ServiceProvider.GetRequiredService<IUserDataStore>();
        protected IUserConnectedDataStore userConnectedDataStore => Startup.ServiceProvider.GetRequiredService<IUserConnectedDataStore>();

        protected IStripeServiceDS stripeServiceDS => DependencyService.Get<IStripeServiceDS>();
        public void CreateLoginToken(string username, string password)
        {
            var result = userDataStore.LoginCredential(username, password);

            if (result != null)
            {
                UserLoginToken = result;
            }
            else
            {
                UserLoginToken = null;
            }

        }

        public LoginResponse GetLogin()
        {
            return UserLoginToken;
        }

        public abstract void VerifyLogin();
        public abstract void MakeHubConnection();

        public abstract void GoQuickOrderHome();

        public abstract void ErrorMessage();
        public abstract void GoEmployeeHome();

    }
}
