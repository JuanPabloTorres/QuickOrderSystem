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
    }
}
