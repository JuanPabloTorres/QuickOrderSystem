using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using WebApiQuickOrder.Context;

namespace WebApiQuickOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {

        IConfiguration _configuration;
        private readonly QOContext _context;

        public StripeController(IConfiguration configuration , QOContext context)
        {
            this._configuration = configuration;
            _context = context;
        }


        [HttpPost("[action]")]
        public async Task<string> CreateStripeCustomer(UserDTO userDTO)
        {

            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];
            var optionsCustomers = new CustomerCreateOptions
            {
                Description = "New Customer From Quick Order",
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Address = new AddressOptions() { Line1 = userDTO.Address }


            };

            var customerservice = new CustomerService();
            var customertoken = await customerservice.CreateAsync(optionsCustomers);

            if (!string.IsNullOrEmpty(customertoken.Id))
            {
                return customertoken.Id;
            }
            else
            {
                return string.Empty;
            }
        }

        [HttpPost("[action]/{stripeuserId}")]
        public async Task<string> InsertStripeCardToCustomer(PaymentCard paymentCard,string stripeuserId)
        {

            try
            {
                StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];
                var tokenoptions = new TokenCreateOptions()
                {
                    Card = new CreditCardOptions()
                    {
                        Number = paymentCard.CardNumber,
                        ExpYear = long.Parse(paymentCard.Year),
                        ExpMonth = long.Parse(paymentCard.Month),
                        Cvc = paymentCard.Cvc,
                        Name = paymentCard.HolderName,

                    },
                };


                var tokenService = new TokenService();

                var stripeToken = await tokenService.CreateAsync(tokenoptions);


                var CardCreateoptions = new CardCreateOptions
                {
                    Source = stripeToken.Id,
                };



                var cardservice = new CardService();
                var cardserviceToken = await cardservice.CreateAsync(stripeuserId, CardCreateoptions);

                if (!string.IsNullOrEmpty(cardserviceToken.Id))
                {
                    return cardserviceToken.Id;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
           
        }

        [HttpGet("[action]/{customerId}/{customercardId}")]
        public async Task<string> GetCustomerCardId(string customerId,string customercardId)
        {
            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];


            var customerService = new CustomerService();

            var cardService = new CardService();

            var service = new CardService();

            var customerToken = await customerService.GetAsync(customerId);
            var UserCardToken = await service.GetAsync(customerToken.Id,customercardId);

            if (!string.IsNullOrEmpty(UserCardToken.Id))
            {
                return UserCardToken.Id;
            }
            else
            {
                return string.Empty;
            }


        }

        [HttpGet("[action]/{storeId}/{total}/{customerId}/{orderId}")]
        public async Task<bool> MakePayment(Guid storeId,double total, string customerId,string orderId )
        {

            try
            {
                var key = await _context.Stores.Where(s => s.StoreId == storeId).FirstOrDefaultAsync();

                StripeConfiguration.ApiKey = key.SKKey;

                string valuetotal = total.ToString("0.00").Replace(".", "");

                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = long.Parse(valuetotal),
                    Currency = "USD",
                    ReceiptEmail = "est.juanpablotorres@gmail.com",
                    StatementDescriptor = "Order Id:" + orderId.Substring(0,8),
                    Capture = true,
                    Customer = customerId,
                    Description = "Payment of Order" + orderId,


                };

                var service = new ChargeService();

                Charge charge = await service.CreateAsync(chargeoptions);

                if (charge.Status == "succeeded")
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return false;
            }

           
        }
       
    }
}