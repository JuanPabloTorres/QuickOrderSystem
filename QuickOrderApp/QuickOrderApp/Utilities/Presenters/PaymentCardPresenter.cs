using Library.DTO;
using QuickOrderApp.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class PaymentCardPresenter : BaseViewModel
    {
        private string cardnumber;

        private string cvc;

        private string expmonth;

        private string expyear;

        private string holdername;

        private Guid paymentCardId;

        private string stripecardId;

        public PaymentCardPresenter (PaymentCardDTO paymentCardDTO)
        {
            this.HolderName = paymentCardDTO.HolderName;

            this.CardNumber = paymentCardDTO.CardNumber;

            this.stripecardId = paymentCardDTO.StripeCardId;

            this.PaymentCardId = paymentCardDTO.PaymentCardId;

            DeleteCardCommand = new Command(async () =>
            {
                var deleted = await CardDataStore.DeletePaymentCard(paymentCardId.ToString());

                if( deleted )
                {
                    var cardDeleteStripeResult = await stripeServiceDS.DeleteCardFromCustomer(App.LogUser.StripeUserId, this.StipeCardId);

                    if( cardDeleteStripeResult )
                    {
                        MessagingCenter.Send<PaymentCardPresenter>(this, "PaymencardDeleteMsg");
                    }
                }

                //await Shell.Current.GoToAsync($"{EditCardPage.Route}");
            });
        }

        public string CardNumber
        {
            get { return cardnumber; }
            set
            {
                cardnumber = value;

                OnPropertyChanged();
            }
        }

        public string CVC
        {
            get { return cvc; }
            set
            {
                cvc = value;

                OnPropertyChanged();
            }
        }

        public ICommand DeleteCardCommand { get; set; }

        public string ExpMonth
        {
            get { return expmonth; }
            set
            {
                expmonth = value;

                OnPropertyChanged();
            }
        }

        public string ExpYear
        {
            get { return expyear; }
            set
            {
                expyear = value;

                OnPropertyChanged();
            }
        }

        public string HolderName
        {
            get { return holdername; }
            set
            {
                holdername = value;

                OnPropertyChanged();
            }
        }

        public Guid PaymentCardId
        {
            get { return paymentCardId; }
            set
            {
                paymentCardId = value;

                OnPropertyChanged();
            }
        }

        public string StipeCardId
        {
            get { return stripecardId; }
            set
            {
                stripecardId = value;

                OnPropertyChanged();
            }
        }
    }
}