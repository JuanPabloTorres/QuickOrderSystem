using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class EmployeeDataStore : DataStoreService<Employee>, IEmployeeDataStore
    {
        public async Task<IEnumerable<Employee>> GetEmployeesOfStore (Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetEmployeesOfStore)}/{storeId}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Employee> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Result);

            return deserializeObject;
        }

        public async Task<Employee> GetSpecificStoreEmployee (Guid userId, Guid StoreId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetSpecificStoreEmployee)}/{userId}/{StoreId}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            Employee deserializeObject = JsonConvert.DeserializeObject<Employee>(response.Result);

            return deserializeObject;
        }

        public async Task<IEnumerable<Employee>> GetUserEmployees (string userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserEmployees)}/{userId}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<Employee> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Result);

            return deserializeObject;
        }

        public async Task<bool> IsEmployeeFromStore (Guid storeId, Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(IsEmployeeFromStore)}/{storeId}/{userId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }
    }
}