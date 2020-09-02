using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Services
{
    public class StoreDataStore : DataStoreService<Store>, IStoreDataStore
    {
        public async Task<bool> DisableStore(Store store)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(DisableStore)}");

            var serializeObj = JsonConvert
             .SerializeObject(store, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response =await HttpClient.PutAsync(BaseAPIUri, byteContent);

            if (response.IsSuccessStatusCode)
            {
               bool deserializeObject = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                return deserializeObject;
            }

            return false;
        }

        public async Task<IEnumerable<Store>> GetAvailableStore()
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetAvailableStore)}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response);
            return deserializeObject;
        }

        public async Task<Store> GetAvailableStoreInformation(Guid id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetAvailableStoreInformation)}/{id}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            Store deserializeObject = JsonConvert.DeserializeObject<Store>(response);
            return deserializeObject;
        }

        public async Task<IEnumerable<Store>> GetSpecificStoreCategory(string category)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetSpecificStoreCategory)}/{category}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Store> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Store>>(response.Result);
            return deserializeObject;
        }

        public async Task<string> GetStoreDestinationPaymentKey(Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreDestinationPaymentKey)}/{storeId}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            string deserializeObject = response;
            return deserializeObject;
        }

        public Task<string> GetStoreDestinationPublicPaymentKey(Guid storeId)
        {
            throw new NotImplementedException();
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
