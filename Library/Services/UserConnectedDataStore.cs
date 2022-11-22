using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserConnectedDataStore : DataStoreService<UsersConnected>, IUserConnectedDataStore
    {
        public async Task<UsersConnected> GetUserConnectedID(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserConnectedID)}/{userId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            UsersConnected deserializeObject = JsonConvert.DeserializeObject<UsersConnected>(response);

            return deserializeObject;
        }

        public async Task<bool> ModifyOldConnections(UsersConnected userConnected)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(ModifyOldConnections)}");

            var serializeObj = JsonConvert
               .SerializeObject(userConnected, Formatting.Indented, new JsonSerializerSettings
               {
                    //ContractResolver = new JsonPrivateResolver(),
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                   NullValueHandling = NullValueHandling.Include
               });

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObj);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var response = await HttpClient.PostAsync(FullAPIUri,byteContent);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                     bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);

                    return deserializeObject;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }    

        }

        public async Task<bool> SendOrdersToEmployees(string storeId, string orderId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(SendOrdersToEmployees)}/{storeId}/{orderId}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            bool deserializeObject = JsonConvert.DeserializeObject<bool>(response);

            return deserializeObject;
        }
    }
}
