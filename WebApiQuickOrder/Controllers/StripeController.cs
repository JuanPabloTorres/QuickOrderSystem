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
            //Crear customer en Quick Order Stripe Account
            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

            //Informacion del customer o user en stripe
            var optionsCustomers = new CustomerCreateOptions
            {
                Description = "New Customer From Quick Order",
                Name = userDTO.Name,
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                Address = new AddressOptions() { Line1 = userDTO.Address }


            };

            //Mando un api call de crear un customer
            var customerservice = new CustomerService();
            var customertoken = await customerservice.CreateAsync(optionsCustomers);


            //Verificamos que el id hay sido creado de manera correcta
            if (!string.IsNullOrEmpty(customertoken.Id))
            {
                // Retornamos el valor id del customer.
                return customertoken.Id;
            }
            else
            {
                return string.Empty;
            }
        }

        [HttpPost("[action]")]
        public async Task<bool> UpdateStripeCustomer(UserDTO updateinformation)
        {
            try
            {
                var options = new CustomerUpdateOptions
                {
                    Description = "Customer Update From Quick Order",
                    Name = updateinformation.Name,
                    Email = updateinformation.Email,
                    Phone = updateinformation.Phone,
                    Address = new AddressOptions() { Line1 = updateinformation.Address }

                };
                var service = new CustomerService();
                var updateResult = await service.UpdateAsync(updateinformation.StripeCustomerId, options);

                if (!string.IsNullOrEmpty(updateResult.Id))
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

        [HttpPost("[action]/{stripeuserId}")]
        public async Task<string> InsertStripeCardToCustomer(PaymentCard paymentCard,string stripeuserId)
        {

            try
            {
                //Insertar tarjeta a un customer
                StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

                //Obtenemos los valores de la tarjeta 
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

                //Creamos el token de la tarjeta
                var tokenService = new TokenService();

                var stripeToken = await tokenService.CreateAsync(tokenoptions);


                //El token pasara la informacion necesaria de la tarjeta
                var CardCreateoptions = new CardCreateOptions
                {
                    Source = stripeToken.Id,
                };

                //Comenzamos a usar el servicio de la tarjeta.
                var cardservice = new CardService();

                //Creamos la tarjeta para un el customer de stripe 
                var cardserviceToken = await cardservice.CreateAsync(stripeuserId, CardCreateoptions);


                //Verificamos los valores si son correctos.
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

        [HttpPost("[action]/{stripeuserId}")]
        public async Task<StripePaymentCardResult> InsertStripeCardToStripeUser(PaymentCard paymentCard, string stripeuserId)
        {

            try
            {
                //Insertar tarjeta a un customer
                StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

                //Obtenemos los valores de la tarjeta 
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

                //Creamos el token de la tarjeta
                var tokenService = new TokenService();

                var stripeToken = await tokenService.CreateAsync(tokenoptions);


                //El token pasara la informacion necesaria de la tarjeta
                var CardCreateoptions = new CardCreateOptions
                {
                    Source = stripeToken.Id,
                };

                //Comenzamos a usar el servicio de la tarjeta.
                var cardservice = new CardService();

                //Creamos la tarjeta para un el customer de stripe 
                var cardserviceToken = await cardservice.CreateAsync(stripeuserId, CardCreateoptions);


                //Verificamos los valores si son correctos.
                if (!string.IsNullOrEmpty(cardserviceToken.Id))
                {

                    var result = new StripePaymentCardResult(cardserviceToken.Id);
                    return result;
                }
                else
                {
                    return new StripePaymentCardResult(true, "Token could not be creted try again.");
                }
            }
            catch (Exception e)
            {

                return new StripePaymentCardResult(true, e.Message);
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
                    StatementDescriptor = "Order Id:" + orderId.Substring(0, 8),
                    Capture = true,
                    Customer = customerId,
                    Description = "Payment of Order" + orderId,
                    ApplicationFeeAmount = 002


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


        [HttpGet("[action]/{storeId}/{total}/{paymentCardId}/{orderId}")]
        public async Task<bool> MakePaymentWithCard(Guid storeId, double total,Guid paymentCardId, string orderId)
        {

            try
            {
                var paymentCard = _context.PaymentCards.Where(pc => pc.PaymentCardId == paymentCardId).FirstOrDefault();
                var key = await _context.Stores.Where(s => s.StoreId == storeId).FirstOrDefaultAsync();

                StripeConfiguration.ApiKey = key.SKKey;

                string valuetotal = total.ToString("0.00").Replace(".", "");


                

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

                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = long.Parse(valuetotal),
                    Currency = "USD",
                    ReceiptEmail = "est.juanpablotorres@gmail.com",
                    StatementDescriptor = "Order Id:" + orderId.Substring(0, 8),
                    Capture = true,
                    Source = stripeToken.Id,
                    Description = "Payment of Order " + orderId,


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

        [HttpGet("[action]/{customerId}/{cardId}")]
        public async Task<bool> DeleteCardFromCustomer(string customerId, string cardId)
        {
            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

            var service = new Stripe.CardService();
            var cardToken =await service.DeleteAsync(customerId, cardId);

            return cardToken.Deleted.Value;

        }

        //[HttpPost("[action]")]
        //public async Task<StripePaymentCardResult> GetAccount(PaymentCard paymentCard)
        //{
        //    try
        //    {
        //        StripeConfiguration.ApiKey = "{{PLATFORM_SECRET_KEY}}";
        //        var customerOptions = new CustomerCreateOptions
        //        {
        //            Email = "person@example.edu",

        //        };
        //        var requestOptions = new RequestOptions();
        //        requestOptions.StripeAccount = "{{CONNECTED_ACCOUNT_ID}}";
        //        var customerService = new CustomerService();
        //        Customer customer = customerService.Create(customerOptions, requestOptions);

        //        // Fetching an account just needs the ID as a parameter
        //        var accountService = new AccountService();
        //        Account account = accountService.Get("{{CONNECTED_STRIPE_ACCOUNT_ID}}");
        //    }
        //    catch (Exception e)
        //    {

        //        var result = new StripePaymentCardResult(true, e.Message);

        //        return result;
        //    }

        //}

        [HttpPost("[action]")]
        public async Task<StripePaymentCardResult> CreateCardToken(PaymentCard paymentCard)
        {
            try
            {
                StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["publickey"];


                var service = new ChargeService();

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



                return new StripePaymentCardResult(stripeToken.Id);
            }
            catch (Exception e)
            {

                var result = new StripePaymentCardResult(true, e.Message);

                return result;
            }
          
        }

        [HttpGet("[action]/{customerId}")]
        public async Task<string> CreateACustomerSubcription(string customerId)
        {

            try
            {
                StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

                //==================================================================
                // Create Product Subcription

                var productoptions = new ProductCreateOptions
                {
                    Name = "Quick Order Subcription",
                    Active = true,
                    Description = "Quick Order Admin System Subcription",
                    Type = "service"


                };

                var productservice = new ProductService();
                var producttoken = await productservice.CreateAsync(productoptions);


                var priceoptions = new PriceCreateOptions
                {
                    UnitAmount = 200,
                    Currency = "usd",
                    Recurring = new PriceRecurringOptions
                    {
                        Interval = "month",
                    },
                    Product = producttoken.Id,
                };
                var priceservice = new PriceService();
                var priceservicetoken = await priceservice.CreateAsync(priceoptions);

                //======================================================================= End Create Product Subcription


                //===================================================================================
                //Create Subcription to store


                var options = new SubscriptionCreateOptions
                {
                    Customer = customerId,
                    Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = priceservicetoken.Id,
                    },

                  },

                };
                var service = new SubscriptionService();
                Subscription subscription = await service.CreateAsync(options);

                if (!string.IsNullOrEmpty(subscription.Id))
                {
                    //var newSubcription = new Subcription()
                    //{
                    //    StripeCustomerId = customerId,
                    //    StripeSubCriptionID = subscription.Id
                    //};

                    //_context.Subcriptions.Add(newSubcription);

                    //try
                    //{

                    //_context.SaveChanges();
                    //}
                    //catch (Exception e)
                    //{

                    //    Console.WriteLine(e);
                    //}



                    return subscription.Id;




                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {

                
                Console.WriteLine(e);

                return string.Empty;
            }
           


        }

        [HttpGet("[action]/{storestripeAccId}/{quickOrderFee}/{storeId}")]
        public async Task<string> TransferQuickOrderFeeFromStore(string storestripeAccId,string quickOrderFee,string storeId)
        {

            try
            {
                var key = await _context.Stores.Where(s => s.StoreId.ToString() == storeId).FirstOrDefaultAsync();
                //StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];
                StripeConfiguration.ApiKey = key.SKKey;
                string totalfee = quickOrderFee.Replace(".", "");

                var options = new TransferCreateOptions
                {
                    Amount = long.Parse(totalfee),
                    Currency = "usd",
                    Destination = "acct_1HDvl5BahC5Vu88T",
                    Description = "Order Fee Quick Order",                     
                };
                var service = new TransferService();
                var transfertoken = await service.CreateAsync(options);

                if (!string.IsNullOrEmpty(transfertoken.Id))
                {
                    return transfertoken.Id;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return string.Empty;
            }
          

          
        }


        public async Task<string> CreateSubcritionProduct()
        {
            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];


            var productoptions = new ProductCreateOptions
            {
                Name = "Quick Order Subcription",
                Active = true,
                Description = "Quick Order Admin System Subcription",
                Type = "service"
            };


            var productservice = new ProductService();
            var producttoken = await productservice.CreateAsync(productoptions);


            var priceoptions = new PriceCreateOptions
            {
                UnitAmount = 200,
                Currency = "usd",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },                 
                Product = producttoken.Id,
                
            };
            var priceservice = new PriceService();
            var pricetoken = await priceservice.CreateAsync(priceoptions);

            if (!string.IsNullOrEmpty(producttoken.Id))
            {
                return producttoken.Id;
            }
            else
            {
                return string.Empty;
            }
        }


        [HttpGet("[action]/{customerId}")]
        public async Task<bool> CancelSubcription(string customerId)
        {
            var subcription = await _context.Subcriptions.Where(sub => sub.StripeCustomerId == customerId && sub.IsDisable == false).FirstOrDefaultAsync();

            if (subcription != null)
            {
            StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["SecretKey"];

            var options = new SubscriptionUpdateOptions
            {
                CancelAtPeriodEnd = true,
            };

            var service = new SubscriptionService();
            var resultToken = await service.UpdateAsync(subcription.StripeSubCriptionID, options);

            if (resultToken.CancelAtPeriodEnd)
            {
                return true;
            }

            else
            {
                return false;
            }


            }
            else
            {
                return false;
            }

        }

        //[HttpPost("[action]")]
        //public async Task<StripePaymentCardResult> CreateBankToken(PaymentCard paymentCard)
        //{
        //    try
        //    {
        //        StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["publickey"];



        //        var tokenoptions = new TokenCreateOptions
        //        {
        //            BankAccount = new BankAccountOptions
        //            {
        //                Country = "US",
        //                Currency = "usd",
        //                AccountHolderName = "Jenny Rosen",
        //                AccountHolderType = "individual",
        //                RoutingNumber = "110000000",
        //                AccountNumber = "000123456789"
        //            }
        //        };

        //        var tokenservice = new TokenService();

        //        Token stripebankaccountToken = await tokenservice.CreateAsync(tokenoptions);

        //        if (!string.IsNullOrEmpty(stripebankaccountToken.Id))
        //        {
        //            var result =  new StripePaymentCardResult()
        //            { 
        //             TokenId = stripebankaccountToken.Id

        //            };

        //            return result;

        //        }
        //        else
        //        {
        //            return new StripePaymentCardResult(true, "Token is null or empty. Try again.");
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        var result = new StripePaymentCardResult(true, e.Message);

        //        return result;
        //    }

        //}


        //[HttpGet("[action]/{stripebankaccountToken}")]
        //public async Task<string> CreateStripeAccount(UserDTO userDTO, string stripebankaccountToken)
        //{

        //    try
        //    {
        //        StripeConfiguration.ApiKey = this._configuration.GetSection("Stripe")["publickey"];



        //        var options = new AccountCreateOptions
        //        {
        //            Type = "custom",
        //            Country = "US",
        //            Email = userDTO.Email,
        //            Capabilities = new AccountCapabilitiesOptions
        //            {
        //                CardPayments = new AccountCapabilitiesCardPaymentsOptions
        //                {
        //                    Requested = true,
        //                },
        //                Transfers = new AccountCapabilitiesTransfersOptions
        //                {
        //                    Requested = true,
        //                },
        //            },
        //            BusinessType = "individual",
        //            DefaultCurrency = "usd",
        //            ExternalAccount = stripebankaccountToken,
        //            Individual = new PersonCreateOptions()
        //            {
        //                FirstName = userDTO.Name,
        //                Email = userDTO.Email,
        //                Gender = userDTO.Gender.ToString(),
        //                Address = new AddressOptions() { Line1 = userDTO.Address },
        //                Phone = userDTO.Phone


        //            }



        //        };
        //        var service = new AccountService();
        //        var _Newaccounttoken = await service.CreateAsync(options);

        //        return _Newaccounttoken.Id;
        //    }
        //    catch (Exception e)
        //    {

        //        Console.WriteLine(e.Message);

        //        return string.Empty;
        //    }



        //}



    }
}