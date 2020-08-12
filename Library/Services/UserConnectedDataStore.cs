using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserConnectedDataStore : DataStoreService<UsersConnected>, IUserConnectedDataStore
    {
        public async Task<UsersConnected> GetUserConnectedID(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserConnectedID)}/{userId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            UsersConnected deserializeObject = JsonConvert.DeserializeObject<UsersConnected>(response.Result);
            return deserializeObject;
        }
    }
}
