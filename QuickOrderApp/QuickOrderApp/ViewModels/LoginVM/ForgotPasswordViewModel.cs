using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.LoginVM
{
    public class ForgotPasswordViewModel:BaseViewModel
    {

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value;
				OnPropertyChanged();
			}
		}

		private string code;

		public string Code
		{
			get { return code; }
			set { code = value;
				OnPropertyChanged();
			}
		}

		public ICommand SendCodeCommand { get; set; }
		public ICommand ConfirmCodeCommand { get; set; }

		public ForgotPasswordViewModel()
		{


			SendCodeCommand = new Command(async() => 
			{

				if (!string.IsNullOrEmpty(Email))
				{
					var codeSendResult =  userDataStore.ForgotCodeSend(Email);

					if (codeSendResult)
					{
						await Shell.Current.DisplayAlert("Notification", "Code was sended to user email check the email.", "OK");
					}
				}
				else
				{
					await Shell.Current.DisplayAlert("Notification", "Empty value.", "OK");
				}
			
			});
			ConfirmCodeCommand = new Command(async() =>
			{

				if (!string.IsNullOrEmpty(Code))
				{
					var codeSendResult = userDataStore.ConfirmCode(Code);

					if (codeSendResult)
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



	}
}
