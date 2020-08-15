using Library.Models;
using QuickOrderApp.Utilities.Static;
using Stripe;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class RegisterCardViewModel : BaseViewModel
    {

        private string holdername;

        public string HolderName
        {
            get { return holdername; }
            set
            {
                holdername = value;
                OnPropertyChanged();
            }
        }

        private string cardnumber;

        public string CardNumber
        {
            get { return cardnumber; }
            set
            {
                cardnumber = value;
                OnPropertyChanged();
            }
        }

        private string cvc;

        public string CVC
        {
            get { return cvc; }
            set
            {
                cvc = value;
                OnPropertyChanged();
            }
        }

        private string month;

        public string Month
        {
            get { return month; }
            set
            {
                month = value;
                OnPropertyChanged();
            }
        }


        private string year;

        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        private Validator holdernameValidator;

        public Validator HolderNameValidator
        {
            get { return holdernameValidator; }
            set { holdernameValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator cardnumberValidator;

        public Validator CardNumberValidator
        {
            get { return cardnumberValidator; }
            set { cardnumberValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator yearValidator;

        public Validator YearValidator
        {
            get { return yearValidator; }
            set { yearValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator monthValidator;

        public Validator MonthVaidator
        {
            get { return monthValidator; }
            set {
                monthValidator = value;
                OnPropertyChanged();
            }
        }

        private Validator cvcValidator;

        public Validator CVCValidator
        {
            get { return cvcValidator; }
            set { cvcValidator = value;
                OnPropertyChanged();
            }
        }



        private DateTime exp;

        public DateTime Exp
        {
            get { return exp; }
            set
            {
                exp = value;
                OnPropertyChanged();
            }
        }




        
        public ICommand CompleteRegisterCardCommand { get; set; }

        public RegisterCardViewModel()
        {

            HolderNameValidator = new Validator();
            CardNumberValidator = new Validator();
            CVCValidator = new Validator();
            YearValidator = new Validator();
            MonthVaidator = new Validator();


            CompleteRegisterCardCommand = new Command(async () =>
            {

                HolderNameValidator = ValidatorRules.EmptyOrNullValueRule(HolderName);
                CardNumberValidator = ValidatorRules.EmptyOrNullValueRule(CardNumber);
                YearValidator = ValidatorRules.EmptyOrNullValueRule(Year);
                MonthVaidator = ValidatorRules.EmptyOrNullValueRule(Month);
                CVCValidator = ValidatorRules.EmptyOrNullValueRule(CVC);


                if (!HolderNameValidator.HasError && !CardNumberValidator.HasError && !YearValidator.HasError && !MonthVaidator.HasError && !CVCValidator.HasError)
                {

                    var newCard = new PaymentCard()
                    {
                        PaymentCardId = Guid.NewGuid(),
                        UserId = App.LogUser.UserId,
                       
                    };


                    try
                    {

                        var cardData = new PaymentCard()
                        {
                            CardNumber = CardNumber,
                            Cvc = CVC,
                            HolderName = HolderName,
                            Month = Month,
                            Year = Year,
                        };


                        var cardservicetokenId = await stripeServiceDS.InsertStripeCardToCustomer(cardData, App.LogUser.StripeUserId);

                        if (!string.IsNullOrEmpty(cardservicetokenId))
                        {
                            newCard.StripeCardId = cardservicetokenId;
                            var result = await CardDataStore.AddItemAsync(newCard);

                            if (result)
                            {
                                await Shell.Current.DisplayAlert("Notification", "Card succefully added.", "OK");
                            }
                        }


                    }
                    catch (Exception e)
                    {

                        await Shell.Current.DisplayAlert("Notification", e.Message, "OK");
                    }


                }
               



            });
        }





    }
}
