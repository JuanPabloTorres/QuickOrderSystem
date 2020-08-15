using Library.DTO;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class StripeServiceDS : IStripeServiceDS
    {
        protected readonly HttpClient HttpClient;
        protected readonly Uri BaseAPIUri;
        //protected readonly INetworkService NetworkService;

        public static string LocalBackendUrl = "http://192.168.56.1:5000/api";
        //public static string LocalBackendUrl = "https://192.168.1.132:5001/api";

        protected Uri FullAPIUri { get; set; }
        public StripeServiceDS()
        {
            HttpClient = new HttpClient();
            BaseAPIUri = new Uri($"{LocalBackendUrl}/Stripe/");
            FullAPIUri = BaseAPIUri;

        }
        public async Task<string> CreateStripeCustomer(UserDTO userDTO)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CreateStripeCustomer)}");

            var serializeObj = JsonConvert
             .SerializeObject(userDTO, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response =await HttpClient.PostAsync(FullAPIUri,byteContent);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }else
            {
                return string.Empty;
            }

           
        }

        public async Task<string> InsertStripeCardToCustomer(PaymentCard paymentCard, string stripeuserId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(InsertStripeCardToCustomer)}/{stripeuserId}");

            var serializeObj = JsonConvert
             .SerializeObject(paymentCard, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await HttpClient.PostAsync(FullAPIUri, byteContent);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return string.Empty;
            }

        }

        public async Task<string> GetCustomerCardId(string customerId, string customercardId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCustomerCardId)}/{customerId}/{customercardId}");

          
            var response = await HttpClient.GetAsync(FullAPIUri);


            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<bool> MakePayment(Guid storeId, double total, string customerId, string orderId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(MakePayment)}/{storeId}/{total}/{customerId}/{orderId}");


            var response =  HttpClient.GetStringAsync(FullAPIUri);

            
            bool deserialized = JsonConvert.DeserializeObject<bool>(response.Result);

            return deserialized;

        }
    }
}
