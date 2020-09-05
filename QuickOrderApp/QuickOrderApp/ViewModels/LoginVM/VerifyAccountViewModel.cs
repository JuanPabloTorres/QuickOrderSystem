using Library.DTO;
using QuickOrderApp.Services;
using QuickOrderApp.Views.Login;
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
			try
			{
				res = await this.authService.VerifyAuthCode(code: code);
				Device.BeginInvokeOnMainThread(async () =>
				{
					if (res is ResponseDto r)
					{
						if (r.HasErrors)
						{
							await App.Current.MainPage.DisplayAlert("Error", r.TextMessage, "ok");
						}
						else
						{
							await App.Current.MainPage.DisplayAlert("Success", r.TextMessage, "ok");
							
							Device.BeginInvokeOnMainThread(async () =>
							{
								await Shell.Current.GoToAsync(LoginPage.Route);
							});
						}
					}
				});
			}
			catch (System.Exception ex)
			{
			}
		}
	}
}