using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class GetLicenceViewModel : BaseViewModel
    {

        public ICommand GetLicenseCommand { get; set; }

        public GetLicenceViewModel()
        {
            GetLicenseCommand = new Command<string>(async (arg) =>
            {

                var result = await Shell.Current.DisplayAlert("Notification", "Your are going to get a store license.For the cost $100 by month. Are you OK with that?", "Yes", "No");

                if (result)
                {
                    var cardResult = await CardDataStore.GetCardFromUser(App.LogUser.UserId);

                    if (cardResult != null)
                    {
                        //Make the Payment transation here...

                        //Check that the payment was submited.

                        //Send Lincese Code


                        var licenseReuslt = await storeLicenseDataStore.PostStoreLicense(App.LogUser.Email, App.LogUser.Name);

                        if (licenseReuslt)
                        {
                            await App.Current.MainPage.DisplayAlert("Notification", "License added succefully.", "OK");
                        }

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Notification", "You dont have a card register, register a card first.", "OK");
                    }
                }

            });
        }

    }
}
