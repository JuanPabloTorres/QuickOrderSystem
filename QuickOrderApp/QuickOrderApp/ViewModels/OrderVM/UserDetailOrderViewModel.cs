using Library.Models;
using QRCoder;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Static;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace QuickOrderApp.ViewModels.OrderVM
{
    public class UserDetailOrderViewModel : BaseViewModel
    {
        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }
        public ICommand CheckoutCommand { get; set; }

        private double total;

        private int orderQuantity;

        public int OrderQuantity
        {
            get { return orderQuantity; }
            set { orderQuantity = value;
                OnPropertyChanged();
            }
        }

        private double totalTax;

        public double TotalTax
        {
            get { return totalTax; }
            set { totalTax = value;
                OnPropertyChanged();
            }
        }

        private double stripefee;

        public double StripeFee
        {
            get { return stripefee; }
            set { stripefee = value;
                OnPropertyChanged();
            }
        }




        public double Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }

        private bool ispickup;

        public bool IspickUp
        {
            get { return ispickup; }
            set
            {
                ispickup = value;
                OnPropertyChanged();

                if (IspickUp)
                {
                    IsDelivery = false;
                    OrderTypeFee = 0;

                    if (isDeliveryFeeAdded)
                    {
                        Total -= 5;
                    }

                }
            }
        }

        bool isDeliveryFeeAdded;

        private bool isdelivery;

        public bool IsDelivery
        {
            get { return isdelivery; }
            set
            {
                isdelivery = value;
                OnPropertyChanged();

                if (IsDelivery)
                {
                    IspickUp = false;

                    OrderTypeFee = 5;
                    Total += 5;
                    isDeliveryFeeAdded = true;

                }
            }
        }


        private double ordertypefee;

        public double OrderTypeFee
        {
            get { return ordertypefee; }
            set
            {
                ordertypefee = value;
                OnPropertyChanged();
            }
        }

        private Library.Models.Order orderdetail;

        public Library.Models.Order OrderDetail
        {
            get { return orderdetail; }
            set
            {
                orderdetail = value;
                OnPropertyChanged();
            }
        }


        private ImageSource qrcodeimg;

        public ImageSource QRCodeImg
        {
            get { return qrcodeimg; }
            set { qrcodeimg = value;
                OnPropertyChanged();
            }
        }


        public UserDetailOrderViewModel()
        {
            ProductPresenters = new ObservableCollection<ProductPresenter>();
            OrderDetail = SelectedOrder.CurrentOrder;
            OrderQuantity = OrderDetail.OrderProducts.Count();
            isDeliveryFeeAdded = false;
            

            SetProducts();

            IspickUp = true;
            CheckoutCommand = new Command(async () =>
            {

                var usercards = await CardDataStore.GetCardFromUser(App.LogUser.UserId);

                if (usercards.Count() > 0)
                {

                    if (OrderDetail.OrderStatus == Status.NotSubmited)
                    {
                        if (IsDelivery)
                        {

                            OrderDetail.OrderType = Library.Models.Type.Delivery;
                        }
                        if (IspickUp)
                        {
                            OrderDetail.OrderType = Library.Models.Type.PickUp;
                        }

                        if (!IsDelivery && !IspickUp)
                        {
                            OrderDetail.OrderType = Library.Models.Type.PickUp;
                        }

                        OrderDetail.OrderStatus = Status.Submited;

                        List<PaymentCard> paymentCards = new List<PaymentCard>(usercards);

                        //SetPayment(paymentCards[0].CardNumber, paymentCards[0].HolderName, paymentCards[0].Year, paymentCards[0].Month, paymentCards[0].Cvc, App.LogUser);

                        //var token = CreateToken(paymentCards[0]);

                        //if (token != null)
                        //{
                            var isTransactionSuccess =await MakePayment();

                            if (isTransactionSuccess)
                            {
                                var orderUpdate = await orderDataStore.UpdateItemAsync(OrderDetail);

                                if (orderUpdate)
                                {
                                    await Shell.Current.GoToAsync("../StoreOrderRoute");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "Bad Transaction.", "OK");
                            }

                        //}

                       

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notification", "Order was submited.", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "You dont have a card register, register a card first.", "OK");
                }

            });

            CalculateTotal();

            MessagingCenter.Subscribe<ProductPresenter>(this, "RemoveOrderProduct", async (obj) => 
            {
                ProductPresenters.Remove(obj);

                var _orderProductToRemove = OrderDetail.OrderProducts.Where(o => o.OrderProductId == obj.ProductId).FirstOrDefault();

                OrderDetail.OrderProducts.Remove(_orderProductToRemove);

                OrderQuantity = OrderDetail.OrderProducts.Count();

                if (OrderDetail.OrderProducts.Count() == 0)
                {
                    var result = await orderDataStore.DeleteItemAsync(OrderDetail.OrderId.ToString());

                    if (result)
                    {
                        await Shell.Current.GoToAsync($"../StoreOrderRoute");
                    }
                }
                CalculateTotal();


            });

            
        }

        public Token stripeToken;
        public TokenService tokenService;
        //public string PublicApiKey = "pk_live_51GOwkJJDC8jrm2WeofpT5zqRgAKZ0LkaQEh64CHvZVcqSvCCBFE7LRV7ZSxjD3pZiTemSKhbe7XBVLX1Q57v2yKc00BW4iat88";
      
         //public string TestApiKey = "pk_test_51GOwkJJDC8jrm2We0q0lyl2DRkIOjqJ6psQaHWdbrc1gbfQyDYQhdWwcv9SX6ulQr2yaQjXnsSCpnhaMJfwKf52900Orbmba9I";
         public string TestApiKey = "pk_test_51GOwkJJDC8jrm2We0q0lyl2DRkIOjqJ6psQaHWdbrc1gbfQyDYQhdWwcv9SX6ulQr2yaQjXnsSCpnhaMJfwKf52900Orbmba9I";
         async void SetPayment(string cardnumber, string holdername, string year, string month, string cvc, User user)
        {

            var key = await StoreDataStore.GetStoreDestinationPaymentKey(OrderDetail.StoreId);

            if (!string.IsNullOrEmpty(key))
            {
                StripeConfiguration.ApiKey = key;


                string mycustomer;
                string getchargedID;
                string refundID;

                Stripe.CreditCardOptions stripcard = new Stripe.CreditCardOptions();
                stripcard.Number = cardnumber;
                stripcard.ExpYear = (long)Convert.ToInt64(year);
                stripcard.ExpMonth = (long)Convert.ToInt64(month);
                stripcard.Cvc = cvc;
                stripcard.Name = holdername;

                Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
                token.Card = stripcard;
                Stripe.TokenService serviceToken = new Stripe.TokenService();
                Stripe.Token newToken = serviceToken.Create(token);

                var options = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "usd",
                    Token = newToken.Id,


                };

                string valuetotal = Total.ToString("0.00").Replace(".", "");



                var service = new SourceService();


                Source source = service.Create(options);

                //Step 3 : Now generate the customer who is doing the payment

                Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = new AddressOptions() { Line1 = user.Address, Country = "Puerto Rico" },
                    Description = "Customer for est.juanpablotorres@gmail.com",
                };

                var customerService = new Stripe.CustomerService();
                Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

                mycustomer = stripeCustomer.Id; // Not needed

                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = long.Parse(valuetotal),
                    //Amount = long.Parse(Total.ToString()),
                    Currency = "USD",
                    ReceiptEmail = "est.juanpablotorres@gmail.com",
                    Customer = stripeCustomer.Id,
                    Source = source.Id

                };


                var service1 = new Stripe.ChargeService();
                Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment
                getchargedID = charge.Id; // Not needed

                await Shell.Current.DisplayAlert("Notification", charge.ReceiptUrl, "OK");
            }

            //if (!string.IsNullOrEmpty(key))
            //{
            //    StripeConfiguration.ApiKey = key;


            //    var options = new PayoutCreateOptions
            //    {
            //        Amount = 1100,
            //        Currency = "usd",
            //    };
            //    var service = new PayoutService();
            //    service.Create(options);

            //}


        }

         async Task<bool> MakePayment()
        {
            var key = await StoreDataStore.GetStoreDestinationPaymentKey(OrderDetail.StoreId);

            StripeConfiguration.ApiKey = key;

            string valuetotal = Total.ToString("0.00").Replace(".", "");

            var chargeoptions = new Stripe.ChargeCreateOptions
            {
                Amount = long.Parse(valuetotal),
                //Amount = long.Parse(Total.ToString()),
                Currency = "USD",
                ReceiptEmail = "est.juanpablotorres@gmail.com",
                //Source = stripeToken.Id,
                StatementDescriptor = "Order Id:" + OrderDetail.OrderId.ToString().Substring(0, 6),
                Capture = true,
                Customer = App.LogUser.StripeUserId,
                Description = "Payment of Order" + OrderDetail.ToString()

            };


            var service = new ChargeService();

            Charge charge = service.Create(chargeoptions);

            if (charge.Status == "succeeded")
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        public string CreateToken(PaymentCard paymentCard)
        {

                StripeConfiguration.ApiKey= TestApiKey;

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
                        AddressLine1 = "Estancias De Santa Rosa Calle Roble",
                        AddressLine2 = "",
                        AddressCity = "Villalba",
                        AddressState = "United State",
                        AddressCountry = "Puerto Rico",
                        AddressZip = "00766",
                        Currency="usd",
                        
                    },
                     
                    
                };

                tokenService = new TokenService();
           

                stripeToken = tokenService.Create(tokenoptions);
         
                return stripeToken.Id;
           
        }

        void SetProducts()
        {
            foreach (var item in OrderDetail.OrderProducts)
            {
                var productPresenter = new ProductPresenter(item);
                ProductPresenters.Add(productPresenter);

            }
        }
        void CalculateTotal()
        {

            if (Total > 0)
            {
                Total = 0;
            }

            if (OrderDetail.OrderProducts.Count() > 0)
            {

                //Total = OrderDetail.OrderProducts.Sum(op => op.Quantity * op.Price);

                //TotalTax = (Total * 0.115);
                //Total = (Total * 0.115) + Total;
                //Total += 0.02;

                //StripeFee = (Total * (2.9 / 100)) + 0.3;
                //Total = (Total * (2.9 / 100)) + Total + 0.30;



                double _netTotal = OrderDetail.OrderProducts.Sum(op => op.Quantity * op.Price);

                Total += _netTotal;

                TotalTax = (Total * 0.115);
                Total += TotalTax;
                Total += 0.02;

                StripeFee = (_netTotal * (2.9 / 100)) + 0.3;
                Total += StripeFee;
            }
            else
            {
                Total = 0.00;
            }


        }
    }
}
