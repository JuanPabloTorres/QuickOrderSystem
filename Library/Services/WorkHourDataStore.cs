using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class WorkHourDataStore : DataStoreService<WorkHour>, IWorkHourDataStore
    {
        public async Task<IEnumerable<WorkHour>> GetStoreWorkHours (string storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetStoreWorkHours)}/{storeId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<WorkHour> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<WorkHour>>(response);

            return deserializeObject;
        }
    }
}