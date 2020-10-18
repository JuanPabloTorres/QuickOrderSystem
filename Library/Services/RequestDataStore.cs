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
    public class RequestDataStore : DataStoreService<UserRequest>, IRequestDataStore
    {
        public RequestDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<UserRequest>> GetRequestAcceptedOfStore(Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetRequestAcceptedOfStore)}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<UserRequest> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<UserRequest>>(response.Result);
            return deserializeObject;
        }

        public async Task<IEnumerable<UserRequest>> GetRequestOfUser(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetRequestOfUser)}/{userId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<UserRequest> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<UserRequest>>(response.Result);
            return deserializeObject;
        }

        public bool UserRequestExists(Guid userid, Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UserRequestExists)}/{userid}/{storeId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }
    }
}
