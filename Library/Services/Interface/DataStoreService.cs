﻿using Library.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.Interface
{
    public class DataStoreService<T> : IDataStore<T> where T : class
    {
        public static string LocalBackendUrl = "http://juantorres9-001-site1.etempurl.com/api";

        protected readonly Uri BaseAPIUri;

        protected readonly HttpClient HttpClient;

        //protected readonly INetworkService NetworkService;

        //public static string LocalBackendUrl = "http://192.168.1.144:5000/api";
        //public static string LocalBackendUrl = "http://192.168.1.133:5000/api";
        //public static string LocalBackendUrl = "http://192.168.56.1:5000/api";
        //public static string LocalBackendUrl = "https://192.168.1.132:5001/api";

        public DataStoreService ()
        {
            HttpClient = new HttpClient();

            BaseAPIUri = new Uri($"{LocalBackendUrl}/{typeof(T).Name}/");

            FullAPIUri = BaseAPIUri;
        }

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

            var response = await HttpClient.PostAsync(BaseAPIUri, byteContent);

            if( response.IsSuccessStatusCode )
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

            var response = await HttpClient.PutAsync(BaseAPIUri, byteContent);

            if( response.IsSuccessStatusCode )
            {
                return true;
            }

            return false;
        }
    }
}