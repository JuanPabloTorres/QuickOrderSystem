using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Library.Services
{
    public class OrderDataStore : DataStoreService<Order>, IOrderDataStore
    {
        public IEnumerable<Order> GetStoreOrders(Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreOrders)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);

            
            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<Order> GetUserOrders(Guid userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserOrders)}/{userid}");

           
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<Order> GetUserOrdersWithToken(Guid userid,string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserOrders)}/{userid}");

            //HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer"+ token);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Order> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Order>>(response.Result);
            return deserializeObject;
        }

        public Order HaveOrderOfSpecificStore(Guid userid, Guid storeid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(HaveOrderOfSpecificStore)}/{userid}/{storeid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            Order deserializeObject = JsonConvert.DeserializeObject<Order>(response.Result);
            return deserializeObject;
        }
    }
}
