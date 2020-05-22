using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class ProductDataStore : DataStoreService<Product>, IProductDataStore
    {
        public IEnumerable<Product> GetProductFromStore(Guid StoreId)
        {
            throw new NotImplementedException();
        }
    }
}
