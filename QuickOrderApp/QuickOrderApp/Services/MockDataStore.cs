using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickOrderApp.Models;

namespace QuickOrderApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;
        protected readonly HttpClient HttpClient;
        protected readonly Uri BaseAPIUri;
        protected Uri FullAPIUri { get; set; }
        public MockDataStore()
        {

            HttpClient = new HttpClient();

            var lower = typeof(Item).Name.ToLower();
            BaseAPIUri = new Uri($"{App.LocalBackendUrl}/{lower}/");
            //BaseAPIUri = new Uri($"{App.LocalBackendUrl}/{typeof(T).Name}/");
            FullAPIUri = BaseAPIUri;
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            //return await Task.FromResult(items);

            //string uriString = $"{typeof(T).Name}";
            var response = HttpClient.GetStringAsync(BaseAPIUri);
            IEnumerable<Item> deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<Item>>(response.Result);
            return deserializeObjects;

        }

        //public bool AddItem(T item)
        //{
        //    var serializeObj = JsonConvert
        //        .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
        //        {
        //            ContractResolver = new JsonPrivateResolver(),
        //            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        //            NullValueHandling = NullValueHandling.Include
        //        });

        //    var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response = HttpClient.PostAsync(BaseAPIUri, byteContent);

        //    if (response.Result.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public bool DeleteItem(string id)
        //{
        //    FullAPIUri = new Uri(BaseAPIUri, $"{id}");
        //    var response = HttpClient.DeleteAsync(FullAPIUri);

        //    if (response.Result.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        //public T GetItem(string id)
        //{
        //    FullAPIUri = new Uri(BaseAPIUri, $"{id}");
        //    var response = HttpClient.GetStringAsync(FullAPIUri);
        //    T deserializeObject = JsonConvert.DeserializeObject<T>(response.Result);
        //    return deserializeObject;
        //}

        //public IEnumerable<T> GetAll(bool forceRefresh = false)
        //{
        //    string uriString = $"{typeof(T).Name}";
        //    var response = HttpClient.GetStringAsync(BaseAPIUri);
        //    IEnumerable<T> deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Result);
        //    return deserializeObjects;
        //}

        //public bool UpdateItem(T item)
        //{
        //    FullAPIUri = new Uri(BaseAPIUri, "");

        //    var serializeObj = JsonConvert
        //     .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
        //     {
        //         ContractResolver = new JsonPrivateResolver(),
        //         ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        //         NullValueHandling = NullValueHandling.Include
        //     });

        //    var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        //    var response = HttpClient.PutAsync(BaseAPIUri, byteContent);

        //    if (response.Result.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}