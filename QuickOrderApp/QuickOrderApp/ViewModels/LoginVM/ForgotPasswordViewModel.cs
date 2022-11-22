using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.LoginVM
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string code;

        private string email;

        public ForgotPasswordViewModel ()
        {
            SendCodeCommand = new Command(async () =>
            {
                if( !string.IsNullOrEmpty(Email) )
                {
                    var codeSendResult = userDataStore.ForgotCodeSend(Email);

                    if( codeSendResult )
                    {
                        await Shell.Current.DisplayAlert("Notification", "Code was sended to user email check the email.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Empty value.", "OK");
                }
            });
            ConfirmCodeCommand = new Command(async () =>
            {
                if( !string.IsNullOrEmpty(Code) )
                {
                    var codeSendResult = userDataStore.ConfirmCode(Code);

                    if( codeSendResult )
                    {
                        await Shell.Current.DisplayAlert("Notification", "Code was sended to user email check the email.", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Empty value.", "OK");
                }
            });
        }

        public string Code
        {
            get { return code; }
            set
            {
                code = value;

                OnPropertyChanged();
            }
        }

        public ICommand ConfirmCodeCommand { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;

                OnPropertyChanged();
            }
        }

        public ICommand SendCodeCommand { get; set; }
    }
}