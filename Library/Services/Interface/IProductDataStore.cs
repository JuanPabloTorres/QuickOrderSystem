using Library.Models;
using System;
using System.Collections.Generic;

namespace Library.Services.Interface
{
    public interface IProductDataStore : IDataStore<Product>
    {
        IEnumerable<Product> GetProductFromStore(Guid StoreId);
        IEnumerable<Product> GetProductWithLowQuantity(Guid storeid, int lowquantity);
    }
}
