using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
    public interface IProductDataStore:IDataStore<Product>
    {
        IEnumerable<Product> GetProductFromStore(Guid StoreId);
    }
}
