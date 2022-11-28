using Library.DTO;
using Library.Models;
using Library.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Library.Services
{
    public class CardDataStore : DataStoreService<PaymentCard>, ICardDataStore
    {
        public async Task<bool> DeletePaymentCard (string cardId)
        {
            //BaseAPIUri = new Uri($"{LocalBackendUrl}/{typeof(PaymentCard)}/");
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(DeletePaymentCard)}/{cardId}");

            var response = await HttpClient.DeleteAsync(FullAPIUri);

            if( response.IsSuccessStatusCode )
            {
                bool deserializeObject = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                return deserializeObject;
            }
            else return false;
        }

        public async Task<IEnumerable<PaymentCardDTO>> GetCardDTOFromUser (Guid userId, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCardDTOFromUser)}/{userId}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<PaymentCardDTO> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<PaymentCardDTO>>(response);

            return deserializeObject;
        }

        public async Task<IEnumerable<PaymentCard>> GetCardFromUser (Guid userId, string token)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetCardFromUser)}/{userId}");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await HttpClient.GetStringAsync(FullAPIUri);

            IEnumerable<PaymentCard> deserializeObject = JsonConvert.DeserializeObject<IEnumerable<PaymentCard>>(response);

            return deserializeObject;
        }
    }
}