﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickOrderApp.Services.Interface
{
    public class DataStoreService<T> : IDataStore<T> where T : class
    {
        protected readonly Uri BaseAPIUri;

        protected readonly HttpClient HttpClient;

        public DataStoreService ()
        {
            HttpClient = new HttpClient();

            BaseAPIUri = new Uri($"{App.LocalBackendUrl}/{typeof(T).Name}/");

            FullAPIUri = BaseAPIUri;
        }

        //protected readonly INetworkService NetworkService;
        protected Uri FullAPIUri { get; set; }

        public async Task<bool> AddItemAsync (T item)
        {
            var serializeObj = JsonConvert
                .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
                {
                    //ContractResolver = new JsonPrivateResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                    NullValueHandling = NullValueHandling.Include
                });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PostAsync(BaseAPIUri, byteContent);

            if( response.Result.IsSuccessStatusCode )
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteItemAsync (string id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{id}");

            var response = HttpClient.DeleteAsync(FullAPIUri);

            if( response.Result.IsSuccessStatusCode )
            {
                return true;
            }
            else return false;
        }

        public async Task<T> GetItemAsync (string id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{id}");

            var response = HttpClient.GetStringAsync(FullAPIUri);

            T deserializeObject = JsonConvert.DeserializeObject<T>(response.Result);

            return deserializeObject;
        }

        public async Task<IEnumerable<T>> GetItemsAsync (bool forceRefresh = false)
        {
            string uriString = $"{typeof(T).Name}";

            var response = HttpClient.GetStringAsync(BaseAPIUri);

            IEnumerable<T> deserializeObjects = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Result);

            return deserializeObjects;
        }

        public async Task<bool> UpdateItemAsync (T item)
        {
            FullAPIUri = new Uri(BaseAPIUri, "");

            var serializeObj = JsonConvert
             .SerializeObject(item, Formatting.Indented, new JsonSerializerSettings
             {
                 //ContractResolver = new JsonPrivateResolver(),
                 ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                 NullValueHandling = NullValueHandling.Include
             });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = HttpClient.PutAsync(BaseAPIUri, byteContent);

            if( response.Result.IsSuccessStatusCode )
            {
                return true;
            }

            return false;
        }
    }
}