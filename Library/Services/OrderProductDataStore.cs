using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;

namespace Library.Services
{
    public class OrderProductDataStore : DataStoreService<OrderProduct>, IOrderProductDataStore
    {
        public bool OrderProductOfUserExistInOrder(Guid userid, string productname, Guid orderid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExistInOrder)}/{userid}/{productname}/{orderid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public OrderProduct OrderProductOfUserExistOnOrder(string productname, Guid orderId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExistOnOrder)}/{productname}/{orderId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            OrderProduct deserializeObject = JsonConvert.DeserializeObject<OrderProduct>(response.Result);
            return deserializeObject;
        }
    }
}
