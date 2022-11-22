using QuickOrderApp.Managers;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class GetLicenceViewModel : BaseViewModel
    {
        public GetLicenceViewModel ()
        {
            GetLicenseCommand = new Command<string>(async (arg) =>
            {
                TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

                if( tokenExpManger.IsExpired() )
                {
                    await tokenExpManger.CloseSession();
                }
                else
                {
                    var result = await Shell.Current.DisplayAlert("Notification", "Your are going to get a store license.For the cost $100 by month. Are you OK with that?", "Yes", "No");

                    if( result )
                    {
                        var cardResult = await CardDataStore.GetCardFromUser(App.LogUser.UserId, App.TokenDto.Token);

                        if( cardResult != null && cardResult.Count() > 0 )
                        {
                            //Make the Payment transation here...

                            //Check that the payment was submited.

                            //Send Lincese Code

                            var subcriptionToken = await stripeServiceDS.CreateACustomerSubcription(App.LogUser.StripeUserId);

                            if( !string.IsNullOrEmpty(subcriptionToken) )
                            {
                                var subcription = new Library.Models.Subcription()
                                {
                                    StripeCustomerId = App.LogUser.StripeUserId,
                                    StripeSubCriptionID = subcriptionToken,
                                    //StoreLicense = StoreControlPanelViewModel.YourSelectedStore.StoreLicenceId
                                };

                                var subcriptionResult = await SubcriptionDataStore.AddItemAsync(subcription);

                                if( subcriptionResult )
                                {
                                    var licenseReuslt = await storeLicenseDataStore.PostStoreLicense(App.LogUser.Email, App.LogUser.Name);

                                    if( licenseReuslt )
                                    {
                                        await App.Current.MainPage.DisplayAlert("Notification", "License added succefully.", "OK");
                                    }
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "The subcription could not be made. Try again.", "OK");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Notification", "You dont have a card register, register a card first.", "OK");
                        }
                    }
                }
            });
        }

        public ICommand GetLicenseCommand { get; set; }
    }
}