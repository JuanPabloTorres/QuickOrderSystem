﻿using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class StoreDataStore : DataStoreService<Store>, IStoreDataStore
    {
        public async Task<IEnumerable<Store>> GetSpecificStoreCategory(string category)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetSpecificStoreCategory)}/{category}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<Store> GetStoresFromUser(Guid userid)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoresFromUser)}/{userid}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result);
            return deserializeObject;
        }

        public async Task<IEnumerable<Store>> SearchStore(string searchStore)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(SearchStore)}/{searchStore}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result);
            return deserializeObject;
        }
    }
}
