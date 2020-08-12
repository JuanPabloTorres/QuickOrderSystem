using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ProductDataStore : DataStoreService<Product>, IProductDataStore
    {
        public IEnumerable<Product> GetProductFromStore(Guid StoreId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetProductFromStore)}/{StoreId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Product> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Product>>(response.Result);
            return deserializeObject;
        }

        public IEnumerable<Product> GetProductWithLowQuantity(Guid storeid, int lowquantity)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetProductWithLowQuantity)}/{storeid}/{lowquantity}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Product> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Product>>(response.Result);
            return deserializeObject;
        }

        public async Task<IEnumerable<Product>> GetSpecificProductTypeFromStore(Guid storeId, ProductType type)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetSpecificProductTypeFromStore)}/{storeId}/{type}");
            var response =  HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<Product> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Product>>(response.Result);
            return deserializeObject;
        }
    }
}
