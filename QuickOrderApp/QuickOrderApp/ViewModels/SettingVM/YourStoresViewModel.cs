using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
   public  class YourStoresViewModel:BaseViewModel
    {

        private User userinformation;

        public User UserInformation
        {
            get { return userinformation; }
            set
            {
                userinformation = value;
                OnPropertyChanged();
            }
        }

        private Store selectedStore;

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;
                OnPropertyChanged();

                if (!(SelectedStore.StoreId == Guid.Empty))
                {
                    GoStoreControlPanelCommand.Execute(SelectedStore.StoreId);
                   
                }
            }
        }

        protected ICommand GoStoreControlPanelCommand { get; set; }

        public ObservableCollection<StorePresenters> StorePresenters { get; set; }


        public YourStoresViewModel()
        {
            UserInformation = App.LogUser;
            StorePresenters = new ObservableCollection<StorePresenters>();
            GoStoreControlPanelCommand = new Command<Guid>(async (value) => 
            {
                await GoStoreControlPanel(value);
            });
        }

        public async Task ExecuteLoadItems()
        {
            if (StorePresenters.Count > 0 )
            {
                StorePresenters.Clear();
            }

            var yourStores = StoreDataStore.GetStoresFromUser(App.LogUser.UserId);

            foreach (var item in yourStores)
            {
                var storePresenter = new StorePresenters(item);

                StorePresenters.Add(storePresenter);
            }
        }


        async Task GoStoreControlPanel(Guid StoreId)
        {
            await Shell.Current.GoToAsync($"StoreControlPanelRoute?Id={StoreId.ToString()}", animate: true);
        }
    }
}
