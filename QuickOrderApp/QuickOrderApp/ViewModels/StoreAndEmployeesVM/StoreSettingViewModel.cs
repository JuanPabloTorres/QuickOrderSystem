using Library.Models;
using QuickOrderApp.Views.Store.StoreManger;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class StoreSettingViewModel : BaseViewModel
    {
        private string storeDescription;

        private Store storeInformation;

        private string storeName;

        public StoreSettingViewModel ()
        {
            MessagingCenter.Subscribe<Store>(this, "StoreInformation", (sender) =>
            {
                StoreName = sender.StoreName;

                StoreDescription = sender.StoreDescription;

                StoreInformation = sender;
            });

            MessagingCenter.Subscribe<Store>(this, "UpdatedStore", (sender) =>
            {
                StoreName = sender.StoreName;

                StoreDescription = sender.StoreDescription;

                StoreInformation = sender;
            });
        }

        public ICommand CancelSubcriptionCommand => new Command(async () =>
          {
              var result = await stripeServiceDS.CancelSubcription(App.LogUser.StripeUserId);

              if( result )
              {
                  storeInformation.IsDisable = true;

                  var storeDeleted = await StoreDataStore.DisableStore(storeInformation);

                  await Shell.Current.DisplayAlert("Notification", "Subcription are canceled and store is going to be disable.", "OK");

                  if( App.LogUser.Stores.Count > 0 )
                  {
                      if( App.LogUser.Stores.Remove(StoreInformation) )
                      {
                          await Shell.Current.GoToAsync("HomePageRoute");
                      }
                  }
              }
              else
              {
                  await Shell.Current.DisplayAlert("Notification", "Error...!", "OK");
              }
          });

        public string StoreDescription
        {
            get { return storeDescription; }
            set
            {
                storeDescription = value;

                OnPropertyChanged();
            }
        }

        public Store StoreInformation
        {
            get { return storeInformation; }
            set { storeInformation = value; }
        }

        public string StoreName
        {
            get { return storeName; }
            set
            {
                storeName = value;

                OnPropertyChanged();
            }
        }

        public ICommand UpdateStoreCommand => new Command(async () =>
          {
              await Shell.Current.GoToAsync($"{UpdateStorePage.Route}", animate: true);

              MessagingCenter.Send<Store>(StoreInformation, "StoreUpdateMsg");
          });

        //public ICommand DeleteStoreCommand => new Command(async () =>
        //{
        //    storeInformation.IsDisable = true;
        //    var storeDeleted = await StoreDataStore.DisableStore(storeInformation);

        //    if (storeDeleted)
        //    {
        //        await Shell.Current.GoToAsync("HomePageRoute");
        //    }

        //});
    }
}