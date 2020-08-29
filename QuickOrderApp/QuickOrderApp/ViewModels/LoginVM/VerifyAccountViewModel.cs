using Library.DTO;
using QuickOrderApp.Services;
using QuickOrderApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.LoginVM
{
    public class VerifyAccountViewModel
    {
        private readonly IAuthService authService;

        public string Code { get; set; } = string.Empty;
		public ICommand VerifyCodeCommand { get; set; }

        public VerifyAccountViewModel(IAuthService authService)
        {
            this.authService = authService;
			VerifyCodeCommand = new Command<string>(async (data) => await VerifyCodeHandler(data));
        }

        private async Task VerifyCodeHandler(string code)
        {
            ResponseDto? res;
           res = await this.authService.VerifyAuthCode(code: code);

            Device.BeginInvokeOnMainThread(async () =>
            {
                //FILL REGISTRATION INPUTS
                if (res is ResponseDto r)
                {
                    await App.Current.MainPage.DisplayAlert("Success", r.TextMessage, "ok");
                }
            });
         
            //await this.authService.VerifyAuthCode(code: verificationData.Code, email: verificationData.Email);
        }
    }
}
