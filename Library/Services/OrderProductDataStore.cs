using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class OrderProductDataStore : DataStoreService<OrderProduct>, IOrderProductDataStore
    {
        public bool OrderProductOfUserExist(Guid userid, string productname)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExist)}/{userid}/{productname}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public OrderProduct OrderProductOfUserExistWith(string productname)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(OrderProductOfUserExistWith)}/{productname}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            OrderProduct deserializeObject = JsonConvert.DeserializeObject<OrderProduct>(response.Result);
            return deserializeObject;
        }
    }
}
