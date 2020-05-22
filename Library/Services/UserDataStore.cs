using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class UserDataStore : DataStoreService<User>, IUserDataStore
    {
        public User CheckUserCredential(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckUserCredential)}/{username}/{password}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            User deserializeObject = JsonConvert.DeserializeObject<User>(response.Result);
            return deserializeObject;
        }

        public bool ConfirmCode(string code)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ConfirmCode)}/{code}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public bool ForgotCodeSend(string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ForgotCodeSend)}/{email}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }
    }
}
