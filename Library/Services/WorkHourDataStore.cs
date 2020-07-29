using Library.Interface;
using Library.Models;
using Library.Services.Interface;

namespace Library.Services
{
    public class WorkHourDataStore : DataStoreService<WorkHour>, IWorkHourDataStore
    {
    }
}
