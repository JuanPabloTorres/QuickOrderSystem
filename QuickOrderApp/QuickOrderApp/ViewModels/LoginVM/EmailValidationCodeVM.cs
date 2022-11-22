using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.LoginVM
{
    public class EmailValidationCodeVM : BaseViewModel
    {
        private string code;

        public string Code
        {
            get { return code; }
            set
            {
                code = value;

                OnPropertyChanged();
            }
        }

        public ICommand RegisterValidationCommand => new Command(async () =>
          {
              if( !string.IsNullOrEmpty(Code) )
              {
                  var userValidateResult = await userDataStore.ValidateEmail(Code, App.LogUser.UserId.ToString());

                  if( userValidateResult )
                  {
                      await PopupNavigation.Instance.PopAsync();

                      await Shell.Current.DisplayAlert("Notification", "Register Succefully", "OK");

                      App.Current.MainPage = new AppShell();
                  }
              }
          });

        public ICommand ResendCode => new Command(async () =>
                  {
                      var resendResult = await userDataStore.ResendCode(App.LogUser.UserId.ToString());
                  });

        public ICommand ValidateCommand => new Command(async () =>
          {
              if( !string.IsNullOrEmpty(Code) )
              {
                  var userValidateResult = await userDataStore.ValidateEmail(Code, App.LogUser.UserId.ToString());

                  if( userValidateResult )
                  {
                      await PopupNavigation.Instance.PopAsync();
                  }
              }
          });
    }
}