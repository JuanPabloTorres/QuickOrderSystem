using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
    public interface IUserDataStore:IDataStore<User>
    {
        User CheckUserCredential(string username, string password);
        bool ForgotCodeSend(string email);
        bool ConfirmCode(string code);
    }
}
