using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IEmployeeWorkHourDataStore : IDataStore<EmployeeWorkHour>
    {
        Task<IEnumerable<EmployeeWorkHour>> GetEmployeeWorkHours (string empId);
    }
}