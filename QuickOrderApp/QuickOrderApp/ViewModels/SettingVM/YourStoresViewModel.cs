using Library.Models;
using System;
using System.Collections.Generic;
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


        public YourStoresViewModel()
        {
            UserInformation = App.LogUser;

            GoStoreControlPanelCommand = new Command<Guid>(async (value) => 
            {
                await GoStoreControlPanel(value);
            });
        }


        async Task GoStoreControlPanel(Guid StoreId)
        {
            await Shell.Current.GoToAsync($"StoreControlPanelRoute?Id={StoreId.ToString()}", animate: true);
        }
    }
}
