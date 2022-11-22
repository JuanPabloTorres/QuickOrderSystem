using Library.DTO;
using Library.Models;
using System;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IStripeServiceDS
    {
        Task<bool> CancelSubcription (string customerId);

        Task<string> CreateACustomerSubcription (string customerId);

        Task<StripePaymentCardResult> CreateCardToken (PaymentCard paymentCard);

        Task<string> CreateStripeCustomer (UserDTO userDTO);

        Task<bool> DeleteCardFromCustomer (string customerId, string cardId);

        Task<string> GetCustomerCardId (string customerId, string customercardId);

        Task<string> InsertStripeCardToCustomer (PaymentCard paymentCard, string stripeuserId);

        Task<StripePaymentCardResult> InsertStripeCardToStripeUser (PaymentCard paymentCard, string stripeuserId);

        Task<bool> MakePayment (Guid storeId, double total, string customerId, string orderId);

        Task<bool> MakePayment2 (Guid storeId, double total, string customerId, string orderId, string customerCardId);

        Task<bool> MakePaymentWithCard (Guid storeId, double total, Guid paymentCardId, string orderId);

        Task<string> TransferQuickOrderFeeFromStore (string storestripeAccId, string quickOrderFee, string storeId);

        Task<bool> UpdateStripeCustomer (UserDTO updateinformation);

        //Task<string> CreateStripeAccount(UserDTO userDTO);
    }
}