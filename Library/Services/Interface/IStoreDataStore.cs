using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IStoreDataStore : IDataStore<Store>
    {
        IEnumerable<Store> GetStoresFromUser(Guid userid);
        Task<IEnumerable<Store>> SearchStore(string searchStore);
        Task<IEnumerable<Store>> GetSpecificStoreCategory(string category);
        Task<string> GetStoreDestinationPaymentKey(Guid storeId);

        Task<string> GetStoreDestinationPublicPaymentKey(Guid storeId);

        Task<bool> DisableStore(Store store);

        Task<IEnumerable<Store>> GetAvailableStore();

        Task<Store> GetAvailableStoreInformation(Guid id);
    }
}
