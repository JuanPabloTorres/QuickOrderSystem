using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IWorkHourDataStore : IDataStore<WorkHour>
    {
        Task<IEnumerable<WorkHour>> GetStoreWorkHours (string storeId);
    }
}