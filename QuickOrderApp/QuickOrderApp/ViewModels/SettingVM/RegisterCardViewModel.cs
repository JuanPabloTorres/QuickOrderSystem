using Library.Models;
using QuickOrderApp.Utilities.Static;
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


        //private string exp;

        //public string Exp
        //{
        //	get { return exp; }
        //	set { exp = value;
        //		OnPropertyChanged();
        //	}
        //}

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
            CompleteRegisterCardCommand = new Command(async () =>
            {

                List<string> valuestoCheck = new List<string>();


                valuestoCheck.Add(HolderName);
                valuestoCheck.Add(CardNumber);
                valuestoCheck.Add(CVC);
                valuestoCheck.Add(Month);
                valuestoCheck.Add(Year);
                //valuestoCheck.Add(Exp.ToString());

                if (GlobalValidator.CheckNullOrEmptyPropertiesOfListValues(valuestoCheck))
                {

                    var newCard = new PaymentCard()
                    {
                        UserId = App.LogUser.UserId,
                        CardNumber = CardNumber,
                        Cvc = CVC,
                        Year = Year,
                        Month = Month,
                        HolderName = HolderName,
                        PaymentCardId = Guid.NewGuid()
                    };



                    var result = await CardDataStore.AddItemAsync(newCard);

                    if (result)
                    {
                        await Shell.Current.DisplayAlert("Notification", "Card succefully added.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Some or one value is empty.", "OK");
                }



            });
        }





    }
}
