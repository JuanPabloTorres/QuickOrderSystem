using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface ICardDataStore : IDataStore<PaymentCard>
    {
        Task<IEnumerable<PaymentCard>> GetCardFromUser(Guid userId);

        Task<IEnumerable<PaymentCardDTO>> GetCardDTOFromUser(Guid userId);

        Task<bool> DeletePaymentCard(string cardId);
    }
}
