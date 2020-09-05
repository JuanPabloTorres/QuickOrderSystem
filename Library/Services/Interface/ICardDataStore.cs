using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface ICardDataStore : IDataStore<PaymentCard>
    {
        Task<IEnumerable<PaymentCard>> GetCardFromUser(Guid userId,string token);

        Task<IEnumerable<PaymentCardDTO>> GetCardDTOFromUser(Guid userId,string token);

        Task<bool> DeletePaymentCard(string cardId);
    }
}
