using Stripe;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels
{
    public class StripeViewModel : BaseViewModel
    {
        public ICommand MakePaymentCommand { get; set; }

        string mycustomer;
        string getchargedID;
        string refundID;

        public StripeViewModel()
        {

            MakePaymentCommand = new Command(() =>
            {


                StripeConfiguration.ApiKey = "sk_test_51GOwkJJDC8jrm2WeSR6vseZknDx3hXppYzplkbyos4hvV2QwG4bmDkLRVw2JTvZXJqykA9UMiK08J6Ls4WpcPqJL0060ea0cSp";

                //This are the sample test data use MVVM bindings to send data to the ViewModel

                Stripe.CreditCardOptions stripcard = new Stripe.CreditCardOptions();
                stripcard.Number = "4000000000003055";
                stripcard.ExpYear = 2020;
                stripcard.ExpMonth = 08;
                stripcard.Cvc = "199";
                stripcard.Name = "Juan P Torres Torres";


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
                    Name = "Juan P Torres",
                    Email = "est.juanpablotorres@gmail.com",
                    Description = "Customer for est.juanpablotorres@gmail.com",
                };

                var customerService = new Stripe.CustomerService();
                Stripe.Customer stripeCustomer = customerService.Create(myCustomer);

                mycustomer = stripeCustomer.Id; // Not needed

                //Step 4 : Now Create Charge Options for the customer. 

                var chargeoptions = new Stripe.ChargeCreateOptions
                {
                    Amount = 150,
                    Currency = "USD",
                    ReceiptEmail = "est.juanpablotorres@gmail.com",
                    Customer = stripeCustomer.Id,
                    Source = source.Id

                };

                //Step 5 : Perform the payment by  Charging the customer with the payment. 
                var service1 = new Stripe.ChargeService();
                Stripe.Charge charge = service1.Create(chargeoptions); // This will do the Payment

                getchargedID = charge.Id; // Not needed
            });
        }

    }
}
