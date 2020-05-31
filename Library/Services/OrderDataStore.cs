using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Order HaveOrder(Guid userid, Guid storeid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(HaveOrder)}/{userid}/{storeid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            Order deserializeObject = JsonConvert.DeserializeObject<Order>(response.Result);
            return deserializeObject;
        }
    }
}
