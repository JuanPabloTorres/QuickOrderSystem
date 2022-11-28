using Library.ApiResponses;
using Library.DTO;
using Library.Factories;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserDataStore : DataStoreService<AppUser>, IUserDataStore
    {
        public async Task<Response<Credential>> CheckIfUsernameAndPasswordExist (string username, string password)
        {
            ResponseFactory<Credential> responseFactory = new ResponseFactory<Credential> ();

            try
            {
                FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckIfUsernameAndPasswordExist)}/{username}/{password}");

                var response = await HttpClient.GetAsync(FullAPIUri);

                if (response.IsSuccessStatusCode)
                {
                    var deserializeObject = JsonConvert.DeserializeObject<Response<Credential>>(await response.Content.ReadAsStringAsync());

                    return deserializeObject;
                }
                else
                {
                    return responseFactory.FailResponse("Request Fail");
                }
            }
            catch (Exception e)
            {
                return responseFactory.FailResponse(e.Message);
            }

           

    
        }

        public AppUser CheckUserCredential (string username, string password)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CheckUserCredential)}/{username}/{password}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            AppUser deserializeObject = JsonConvert.DeserializeObject<AppUser>(response.Result);

            return deserializeObject;
        }

        public bool ConfirmCode (string code)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ConfirmCode)}/{code}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);

            return deserializeObject;
        }

        public async Task<bool> EmailExist (string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(EmailExist)}/{email}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }

        public bool ForgotCodeSend (string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ForgotCodeSend)}/{email}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public async Task<IEnumerable<UserDTO>> GetUserWithName (string name)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserWithName)}/{name}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<UserDTO> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(response);

            return deserializeObject;
        }

        public async Task<Response<TokenDTO>> LoginCredential (string username, string password)
        {
            try
            {
                FullAPIUri = new Uri(BaseAPIUri, $"{nameof(LoginCredential)}/{username}/{password}");

                var response = await HttpClient.GetAsync(FullAPIUri);

                if (response.IsSuccessStatusCode)
                {
                    Response<TokenDTO> _apiResponse = JsonConvert.DeserializeObject<Response<TokenDTO>>(await response.Content.ReadAsStringAsync());

                    return _apiResponse;
                }
                else
                {
                    ResponseFactory<TokenDTO> _failRequest = new ResponseFactory<TokenDTO>();

                    return _failRequest.ApiRequestFail();
                }

               
            }
            catch (Exception e)
            {
                ResponseFactory<TokenDTO> _failResponse = new ResponseFactory<TokenDTO>();

                return _failResponse.FailResponse(e.Message);

            }

          
        }

        public async Task<bool> ResendCode (string userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ResendCode)}/{userId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }

        public async Task<bool> ValidateEmail (string code, string userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ValidateEmail)}/{code}/{userid}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }
    }
}