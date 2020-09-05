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
        Task<bool> SendEmailVerificationCode(string email);

        Task<ResponseDto> VerifyAuthCode(string code);
    }

    public class AuthService : IAuthService
	{
        readonly string _controllerName = "api/Auth";
        public Uri? FullAPIUri { get; private set; }
        public UriBuilder? UriBuilder { get; set; }
        private readonly HttpClient httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("MyHttpClient");
        }

        public async Task<bool> SendEmailVerificationCode(string email)
        {
            HttpResponseMessage result;
            FullAPIUri = new UriBuilder { 
                Port=5000,
                Path = $"{this._controllerName}/{nameof(SendEmailVerificationCode)}" ,
                Query = $"{nameof(email)}={email}"
            }.Uri;
            result = await httpClient.GetAsync(FullAPIUri);
             return result.IsSuccessStatusCode;
        }

        public async Task<ResponseDto> VerifyAuthCode(string code)
        {
            ResponseDto responseMessage;
            FullAPIUri = new UriBuilder
            {
                Port = 5000,
                Path = $"{this._controllerName}/{nameof(VerifyAuthCode)}",
                Query = $"{nameof(code)}={code}",
                Host = httpClient.BaseAddress.Host
            }.Uri;
            responseMessage = JsonConvert.DeserializeObject<ResponseDto>(await httpClient.GetStringAsync(FullAPIUri));
            return responseMessage;
        }
    }
}