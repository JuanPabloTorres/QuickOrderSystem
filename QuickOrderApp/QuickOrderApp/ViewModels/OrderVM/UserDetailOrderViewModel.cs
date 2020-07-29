using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Static;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.OrderVM
{
    public class UserDetailOrderViewModel : BaseViewModel
    {
        public ObservableCollection<ProductPresenter> ProductPresenters { get; set; }
        public ICommand CheckoutCommand { get; set; }

        private double total;

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
                    Total = Total + 5;
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

        public UserDetailOrderViewModel()
        {
            ProductPresenters = new ObservableCollection<ProductPresenter>();
            OrderDetail = SelectedOrder.CurrentOrder;
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

                        SetPayment(paymentCards[0].CardNumber, paymentCards[0].HolderName, paymentCards[0].Year, paymentCards[0].Month, paymentCards[0].Cvc, App.LogUser);

                        var orderUpdate = await orderDataStore.UpdateItemAsync(OrderDetail);

                        if (orderUpdate)
                        {
                            await Shell.Current.Navigation.PopAsync();
                        }

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
        }

        void SetPayment(string cardnumber, string holdername, string year, string month, string cvc, User user)
        {

            string mycustomer;
            string getchargedID;
            string refundID;

            StripeConfiguration.ApiKey = "sk_test_51GOwkJJDC8jrm2WeSR6vseZknDx3hXppYzplkbyos4hvV2QwG4bmDkLRVw2JTvZXJqykA9UMiK08J6Ls4WpcPqJL0060ea0cSp";

            //This are the sample test data use MVVM bindings to send data to the ViewModel

            Stripe.CreditCardOptions stripcard = new Stripe.CreditCardOptions();
            stripcard.Number = "4000000000003055";
            stripcard.ExpYear = (long)Convert.ToInt64(year);
            stripcard.ExpMonth = (long)Convert.ToInt64(month);
            stripcard.Cvc = cvc;
            stripcard.Name = holdername;


            //Step 1 : Assign Card to Token Object and create Token

            Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
            token.Card = stripcard;
            Stripe.TokenService serviceToken = new Stripe.TokenService();
            Stripe.Token newToken = serviceToken.Create(token);

            // Step 2 : Assign Token to the Source

            var options = new SourceCreateOptions
            {
                Type = SourceType.Card,
                Currency = "usd",
                Token = newToken.Id
            };

            var service = new SourceService();
            Source source = service.Create(options);

            //Step 3 : Now generate the customer who is doing the payment

            Stripe.CustomerCreateOptions myCustomer = new Stripe.CustomerCreateOptions()
            {
                Name = user.Name,
                Email = user.Email,
                Description = "Customer for est.juanpablotorres@gmail.com",
            };

            var customerService = new Stripe.CustomerService();
            Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

            mycustomer = stripeCustomer.Id; // Not needed

            //Step 4 : Now Create Charge Options for the customer. 

            var chargeoptions = new Stripe.ChargeCreateOptions
            {
                Amount = long.Parse(Total.ToString().Replace(".", "")),
                Currency = "USD",
                ReceiptEmail = "est.juanpablotorres@gmail.com",
                Customer = stripeCustomer.Id,
                Source = source.Id

            };

            //Step 5 : Perform the payment by  Charging the customer with the payment. 
            var service1 = new Stripe.ChargeService();
            Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment



            getchargedID = charge.Id; // Not needed
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
            Total = OrderDetail.OrderProducts.Sum(op => op.Quantity * op.Price);

            Total = (Total * 0.115) + Total;
            Total += 0.02;


            Total = Total * (2.9 / 100) + Total;


        }
    }
}
