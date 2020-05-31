using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class StoreDataStore : DataStoreService<Store>, IStoreDataStore
    {
        public IEnumerable<Store> GetStoresFromUser(Guid userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoresFromUser)}/{userid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result);
            return deserializeObject;
        }
    }
}
