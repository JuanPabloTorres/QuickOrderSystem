using Library.DTO;
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

        public async Task<IEnumerable<PaymentCardDTO>> GetCardDTOFromUser(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCardDTOFromUser)}/{userId}");
            var response = await HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<PaymentCardDTO> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<PaymentCardDTO>>(response);
            return deserializeObject;
        }

        public async Task<IEnumerable<PaymentCard>> GetCardFromUser(Guid userId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCardFromUser)}/{userId}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            IEnumerable<PaymentCard> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<PaymentCard>>(response.Result);
            return deserializeObject;
        }

        public async Task<bool> DeletePaymentCard(string cardId)
        {
            //BaseAPIUri = new Uri($"{LocalBackendUrl}/{typeof(PaymentCard)}/");
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(DeletePaymentCard)}/{cardId}");

            var response = await HttpClient.DeleteAsync(FullAPIUri);

            if (response.IsSuccessStatusCode)
            {
                bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                return deserializeObject;
            }
            else return false;
        }

    }
}
