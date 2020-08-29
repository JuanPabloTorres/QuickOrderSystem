using Library.DTO;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickOrderApp.Services
{
    public interface IAuthService
    {
        Task SendEmailVerificationCode(string email);

        Task<ResponseDto> VerifyAuthCode(string code);
    }

    public class AuthService : IAuthService
	{
        readonly string _controllerName = "Auth";
        public Uri? FullAPIUri { get; private set; }
        public UriBuilder? UriBuilder { get; set; }
        private readonly HttpClient httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("MyHttpClient");
        }

        public async Task SendEmailVerificationCode(string email)
        {
            HttpResponseMessage result;
            FullAPIUri = new UriBuilder { 
                Path = $"{this._controllerName}" ,
                Query = $"{nameof(email)}={email}"
            }.Uri;
            result = await httpClient.GetAsync(FullAPIUri);
        }

        public async Task<ResponseDto> VerifyAuthCode(string code)
        {
            ResponseDto responseMessage;
            FullAPIUri = new UriBuilder
            {
                Path = $"{this._controllerName}/{nameof(VerifyAuthCode)}",
                Query = $"{nameof(code)}={code}"
            }.Uri;
            responseMessage = JsonConvert.DeserializeObject<ResponseDto>(await httpClient.GetStringAsync(FullAPIUri));
            return responseMessage;
        }
    }
}