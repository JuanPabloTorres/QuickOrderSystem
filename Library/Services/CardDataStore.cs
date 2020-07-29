using Library.Interface;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public class CardDataStore : DataStoreService<PaymentCard>, ICardDataStore
    {
        public async Task<IEnumerable<PaymentCard>> GetCardFromUser(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCardFromUser)}/{userId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<PaymentCard> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<PaymentCard>>(response.Result);
            return deserializeObject;
        }
    }
}
