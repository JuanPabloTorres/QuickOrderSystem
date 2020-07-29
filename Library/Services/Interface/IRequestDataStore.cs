using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IRequestDataStore : IDataStore<UserRequest>
    {
        Task<IEnumerable<UserRequest>> GetRequestOfUser(Guid userId);
        bool UserRequestExists(Guid userid, Guid storeId);

        Task<IEnumerable<UserRequest>> GetRequestAcceptedOfStore(Guid storeId);
    }
}
