using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Static;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class RegisterCardViewModel : BaseViewModel
    {
        private string cardnumber;

        private Validator cardnumberValidator;

        private string cvc;

        private Validator cvcValidator;

        private string holdername;

        private Validator holdernameValidator;

        private string month;

        private Validator monthValidator;

        private string year;

        private Validator yearValidator;

        public RegisterCardViewModel ()
        {
            PropertiesInitializer();

            CompleteRegisterCardCommand = new Command(async () =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if( tokenExpManger.IsExpired() )
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {
                    HolderNameValidator = ValidatorRules.EmptyOrNullValueRule(HolderName);

                    CardNumberValidator = ValidatorRules.EmptyOrNullValueRule(CardNumber);

                    YearValidator = ValidatorRules.EmptyOrNullValueRule(Year);

                    MonthVaidator = ValidatorRules.EmptyOrNullValueRule(Month);

                    CVCValidator = ValidatorRules.EmptyOrNullValueRule(CVC);

                    if( !HolderNameValidator.HasError && !CardNumberValidator.HasError && !YearValidator.HasError && !MonthVaidator.HasError && !CVCValidator.HasError )
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

                            if( !cardserviceResult.HasError )
                            {
                                cardData.StripeCardId = cardserviceResult.TokenId;

                                var result = await CardDataStore.AddItemAsync(cardData);

                                if( result )
                                {
                                    await Shell.Current.DisplayAlert("Notification", "Card succefully added.", "OK");
                                }
                            }
                            else
                            {
                                await Shell.Current.DisplayAlert("Notification", cardserviceResult.ErrorMsg, "OK");
                            }
                        }
                        catch( Exception e )
                        {
                            await Shell.Current.DisplayAlert("Notification", e.Message, "OK");
                        }
                    }
                }
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

        public Validator CardNumberValidator
        {
            get { return cardnumberValidator; }
            set
            {
                cardnumberValidator = value;

                OnPropertyChanged();
            }
        }

        public ICommand CompleteRegisterCardCommand { get; set; }

        public string CVC
        {
            get { return cvc; }
            set
            {
                cvc = value;

                OnPropertyChanged();
            }
        }

        public Validator CVCValidator
        {
            get { return cvcValidator; }
            set
            {
                cvcValidator = value;

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

        public Validator HolderNameValidator
        {
            get { return holdernameValidator; }
            set
            {
                holdernameValidator = value;

                OnPropertyChanged();
            }
        }

        public string Month
        {
            get { return month; }
            set
            {
                month = value;

                OnPropertyChanged();
            }
        }

        public Validator MonthVaidator
        {
            get { return monthValidator; }
            set
            {
                monthValidator = value;

                OnPropertyChanged();
            }
        }

        public string Year
        {
            get { return year; }
            set
            {
                year = value;

                OnPropertyChanged();
            }
        }

        public Validator YearValidator
        {
            get { return yearValidator; }
            set
            {
                yearValidator = value;

                OnPropertyChanged();
            }
        }

        private void PropertiesInitializer ()
        {
            HolderNameValidator = new Validator();

            CardNumberValidator = new Validator();

            CVCValidator = new Validator();

            YearValidator = new Validator();

            MonthVaidator = new Validator();
        }
    }
}