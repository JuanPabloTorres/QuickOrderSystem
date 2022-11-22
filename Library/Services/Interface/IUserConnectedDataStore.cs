using Library.Models;
using System;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IUserConnectedDataStore : IDataStore<UsersConnected>
    {
        Task<UsersConnected> GetUserConnectedID (Guid userId);

        Task<bool> ModifyOldConnections (UsersConnected usersConnected);

        Task<bool> SendOrdersToEmployees (string storeId, string orderId);
    }
}