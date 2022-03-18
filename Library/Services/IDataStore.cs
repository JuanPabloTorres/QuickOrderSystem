using Library.ApiResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IDataStore<T> where T : class
    {
        Task<BaseResponse> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
