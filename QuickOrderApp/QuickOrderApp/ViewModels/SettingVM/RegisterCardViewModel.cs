using Library.Models;
using QuickOrderApp.Utilities.Factories;
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


        
        public ICommand CompleteRegisterCardCommand { get; set; }

        public RegisterCardViewModel()
        {

            PropertiesInitializer();


            CompleteRegisterCardCommand = new Command(async () =>
            {

                HolderNameValidator = ValidatorRules.EmptyOrNullValueRule(HolderName);
                CardNumberValidator = ValidatorRules.EmptyOrNullValueRule(CardNumber);
                YearValidator = ValidatorRules.EmptyOrNullValueRule(Year);
                MonthVaidator = ValidatorRules.EmptyOrNullValueRule(Month);
                CVCValidator = ValidatorRules.EmptyOrNullValueRule(CVC);


                if (!HolderNameValidator.HasError && !CardNumberValidator.HasError && !YearValidator.HasError && !MonthVaidator.HasError && !CVCValidator.HasError)
                {

                    try
                    {

                        var cardData = new PaymentCard()
                        {
                            CardNumber = CardNumber,
                            Cvc = CVC,
                            HolderName = HolderName,
                            Month = Month,
                            Year = Year,
                            PaymentCardId = Guid.NewGuid(),
                            UserId = App.LogUser.UserId
                        };

                        var cardserviceResult = await stripeServiceDS.InsertStripeCardToStripeUser(cardData, App.LogUser.StripeUserId);

                        if (!cardserviceResult.HasError)
                        {
                            cardData.StripeCardId = cardserviceResult.TokenId;
                            var result = await CardDataStore.AddItemAsync(cardData);

                            if (result)
                            {
                                await Shell.Current.DisplayAlert("Notification", "Card succefully added.", "OK");
                            }
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Notification", cardserviceResult.ErrorMsg, "OK");
                        }


                    }
                    catch (Exception e)
                    {

                        await Shell.Current.DisplayAlert("Notification", e.Message, "OK");
                    }


                }
               



            });
        }

        void PropertiesInitializer()
        {
            HolderNameValidator = new Validator();
            CardNumberValidator = new Validator();
            CVCValidator = new Validator();
            YearValidator = new Validator();
            MonthVaidator = new Validator();
        }

    }
}
