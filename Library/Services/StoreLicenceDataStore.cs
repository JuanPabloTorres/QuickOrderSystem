using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Services
{
    public class StoreLicenceDataStore : DataStoreService<StoreLicense>, IStoreLicenseDataStore
    {
        public StoreLicenceDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<bool> IsLicenseInUsed(string license)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(IsLicenseInUsed)}/{license}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }

        public async Task<bool> PostStoreLicense(string email, string username)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(PostStoreLicense)}/{email}/{username}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }


        public bool StoreLicenseExists(Guid id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(StoreLicenseExists)}/{id}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }

        public async  Task<bool> UpdateLicenceInCode(Guid id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateLicenceInCode)}/{id}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);
            return deserializeObject;
        }
    }
}
