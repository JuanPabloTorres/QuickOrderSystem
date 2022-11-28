using Library.Helpers;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Library.Services
{
    public class OrderDataStore : DataStoreService<Order>, IOrderDataStore
    {
        public async Task<bool> DisableOrder (Guid orderId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(DisableOrder)}/{orderId}");

            //HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }

        public async Task<IEnumerable<Order>> GetOrdersOfStoreOfUserWithSpecifiStatus (Guid userid, Guid storeid, Status status)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetOrdersOfStoreOfUserWithSpecifiStatus)}/{userid}/{storeid}/{status}");

            //HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response);

            return deserializeObject;
        }

        public async Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatus (Guid userid, Status status, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetOrdersOfUserWithSpecificStatus)}/{userid}/{status}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response);

            return deserializeObject;
        }

        public async Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatusDifferent (IEnumerable<Order> ordersAdded, Status status, Guid userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetOrdersOfUserWithSpecificStatusDifferent)}/{status}/{userid}");

            var serializeObj = JsonConvert
             .SerializeObject(ordersAdded, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync(FullAPIUri, byteContent);

            if( response.IsSuccessStatusCode )
            {
                IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(await response.Content.ReadAsStringAsync());

                return deserializeObject;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Order>> GetStoreOrders (Guid storeId, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreOrders)}/{storeId}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response);

            return deserializeObject;
        }

        public IEnumerable<Order> GetUserOrders (Guid userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserOrders)}/{userid}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);

            return deserializeObject;
        }

        public IEnumerable<Order> GetUserOrdersOfStore (Guid userid, Guid storeid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserOrdersOfStore)}/{userid}/{storeid}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);

            return deserializeObject;
        }

        public IEnumerable<Order> GetUserOrdersWithToken (Guid userid, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserOrders)}/{userid}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);

            return deserializeObject;
        }

        public Order HaveOrderOfSpecificStore (Guid userid, Guid storeid, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(HaveOrderOfSpecificStore)}/{userid}/{storeid}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = HttpClient.GetStringAsync(FullAPIUri);

            Order deserializeObject = JsonConvert.DeserializeObject<Order>(response.Result);

            return deserializeObject;
        }
    }
}