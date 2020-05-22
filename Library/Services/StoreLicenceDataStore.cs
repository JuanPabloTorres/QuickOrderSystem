using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Library.Services
{
    public class StoreLicenceDataStore : DataStoreService<StoreLicense>, IStoreLicenseDataStore
    {
        

        public bool StoreLicenseExists(Guid id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(StoreLicenseExists)}/{id}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Result);
            return deserializeObject;
        }
    }
}
