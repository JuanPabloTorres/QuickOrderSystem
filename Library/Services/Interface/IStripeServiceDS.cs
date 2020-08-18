using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IStripeServiceDS
    {
        Task<string> CreateStripeCustomer(UserDTO userDTO);

        Task<string> InsertStripeCardToCustomer(PaymentCard paymentCard, string stripeuserId);
        Task<string> GetCustomerCardId(string customerId, string customercardId);

        Task<bool> MakePayment(Guid storeId, double total, string customerId, string orderId);

        Task<StripePaymentCardResult> CreateCardToken(PaymentCard paymentCard);

         Task<string>  CreateACustomerSubcription(string customerId);

        Task<bool> MakePayment2(Guid storeId, double total, string customerId, string orderId, string customerCardId);
        Task<StripePaymentCardResult> InsertStripeCardToStripeUser(PaymentCard paymentCard, string stripeuserId);

        Task<bool> MakePaymentWithCard(Guid storeId, double total, Guid paymentCardId, string orderId);

        Task<bool> UpdateStripeCustomer(UserDTO updateinformation);
        Task<string> TransferQuickOrderFeeFromStore(string storestripeAccId, string quickOrderFee,string storeId);

        //Task<string> CreateStripeAccount(UserDTO userDTO);


    }
}
