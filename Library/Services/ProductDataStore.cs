using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Services
{
    public class ProductDataStore : DataStoreService<Product>, IProductDataStore
    {
        public async Task<IEnumerable<Product>> GetDifferentProductFromStore(IEnumerable<Product> productsAdded, Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetDifferentProductFromStore)}/{storeId}");

            var serializeObj = JsonConvert
             .SerializeObject(productsAdded, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await HttpClient.PostAsync(FullAPIUri, byteContent);

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Product> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());
                return deserializeObject;
            }
            else
            {
                return null;
            }
        }

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

        public async Task<Product> SearchItemOfStore(string storeId, string item)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(SearchItemOfStore)}/{storeId}/{item}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            Product deserializeObject = JsonConvert.DeserializeObject<Product>(response);
            return deserializeObject;
        }
    }
}
