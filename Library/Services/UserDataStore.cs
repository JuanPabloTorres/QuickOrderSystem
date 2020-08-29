using Library.DTO;
using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserDataStore : DataStoreService<User>, IUserDataStore
    {
        public async Task<bool> CheckIfUsernameAndPasswordExist(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckIfUsernameAndPasswordExist)}/{username}/{password}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }

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

        public async Task<IEnumerable<UserDTO>> GetUserWithName(string name)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserWithName)}/{name}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<UserDTO> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(response);
            return deserializeObject;
        }

        public TokenDTO LoginCredential(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(LoginCredential)}/{username}/{password}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            TokenDTO deserializeObject = JsonConvert.DeserializeObject<TokenDTO>(response.Result);
            return deserializeObject;
        }
    }
}