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
    public class EmployeeWorkHourDataStore : DataStoreService<EmployeeWorkHour>, IEmployeeWorkHourDataStore
    {
        public EmployeeWorkHourDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<IEnumerable<EmployeeWorkHour>> GetEmployeeWorkHours(string empId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetEmployeeWorkHours)}/{empId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<EmployeeWorkHour> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<EmployeeWorkHour>>(response.Result);
            return deserializeObject;
        }
    }
}
