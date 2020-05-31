using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
    public interface IStoreDataStore:IDataStore<Store>
    {
         IEnumerable<Store> GetStoresFromUser(Guid userid);
    }
}
