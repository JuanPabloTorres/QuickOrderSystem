using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IUserConnectedDataStore : IDataStore<UsersConnected>
    {

        Task<UsersConnected> GetUserConnectedID(Guid userId);
    }
       
}
