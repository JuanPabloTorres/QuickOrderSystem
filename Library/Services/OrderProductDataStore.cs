using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Library.Services
{
    public class OrderProductDataStore : DataStoreService<OrderProduct>, IOrderProductDataStore
    {
        public OrderProductDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public bool OrderProductOfUserExistInOrder(Guid userid, Guid productId, Guid orderid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExistInOrder)}/{userid}/{productId}/{orderid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public OrderProduct OrderProductOfUserExistOnOrder(Guid productId, Guid orderId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExistOnOrder)}/{productId}/{orderId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            OrderProduct deserializeObject = JsonConvert.DeserializeObject<OrderProduct>(response.Result);
            return deserializeObject;
        }
    }
}
