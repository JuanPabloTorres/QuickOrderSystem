using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TokenExpAttribute : ValidationAttribute
    {
        public override bool IsValid (object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            if( DateTime.Now >= App.TokenDto.Exp )
            {
                Task.Run(async () =>
                {
                    await Shell.Current.DisplayAlert("Notification", "Token has expired...!", "OK");

                    await App.ComunicationService.Disconnect();

                    App.Current.MainPage = new AppShell();

                    await Shell.Current.GoToAsync("LoginRoute");
                });

                return true;
            }
            else
            {
                return false;
            }
        }

        //public string TokenExpDate { get; set; }

        //public TokenExpAttribute(string expDate)
        //{
        //    this.TokenExpDate = expDate;
        //}

        //public TokenExpAttribute()
        //{
        //    CheckExpireToken();
        //}

        //public async static void CheckExpireToken()
        //{
        //    if (DateTime.Now >= App.TokenDto.Exp)
        //    {
        //    await Shell.Current.DisplayAlert("Notification", "Token has expired...!", "OK");

        //    await App.ComunicationService.Disconnect();
        //    App.Current.MainPage = new AppShell();

        //    await Shell.Current.GoToAsync("LoginRoute");

        //    }

        //}
    }
}