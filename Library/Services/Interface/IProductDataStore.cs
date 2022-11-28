using Library.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IProductDataStore : IDataStore<Product>
    {
        Task<IEnumerable<Product>> GetDifferentProductFromStore (IEnumerable<Product> productsAdded, Guid storeId);

        IEnumerable<Product> GetProductFromStore (Guid StoreId);

        IEnumerable<Product> GetProductWithLowQuantity (Guid storeid, int lowquantity);

        Task<IEnumerable<Product>> GetSpecificProductTypeFromStore(Guid storeId, ProductType type);

        Task<Product> SearchItemOfStore (string storeId, string item);
    }
}