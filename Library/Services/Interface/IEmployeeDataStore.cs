using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IEmployeeDataStore : IDataStore<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesOfStore(Guid storeId);
        Task<IEnumerable<Employee>> GetUserEmployees(string userId);
        Task<Employee> GetSpecificStoreEmployee(Guid userId, Guid StoreId);
    }
}
