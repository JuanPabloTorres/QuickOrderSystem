﻿using Library.ApiResponses;
using Library.DTO;
using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserDataStore : DataStoreService<User>, IUserDataStore
    {
        public UserDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<bool> CheckIfUsernameAndPasswordExist(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckIfUsernameAndPasswordExist)}/{username}/{password}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }

        public LoginResponse CheckUserCredential(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckUserCredential)}/{username}/{password}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            LoginResponse deserializeObject = JsonConvert.DeserializeObject<LoginResponse>(response.Result);

            return deserializeObject;
        }

        public bool ConfirmCode(string code)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ConfirmCode)}/{code}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public async Task<bool> EmailExist(string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(EmailExist)}/{email}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
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

        public LoginResponse LoginCredential(string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(LoginCredential)}/{username}/{password}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            LoginResponse deserializeObject = JsonConvert.DeserializeObject<LoginResponse>(response.Result);

            return deserializeObject;
        }

        public async Task<bool> ResendCode(string userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ResendCode)}/{userId}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }

        public async Task<bool> ValidateEmail(string code, string userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ValidateEmail)}/{code}/{userid}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }
    }
}
