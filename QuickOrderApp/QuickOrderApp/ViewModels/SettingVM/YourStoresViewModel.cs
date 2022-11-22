using Library.Models;
using QuickOrderApp.Managers;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.SettingVM
{
    public class YourStoresViewModel : BaseViewModel
    {
        private Store selectedStore;

        private User userinformation;

        public YourStoresViewModel ()
        {
            UserInformation = App.LogUser;

            StorePresenters = new ObservableCollection<StorePresenters>();

            GoStoreControlPanelCommand = new Command<Guid>(async (value) =>
            {
                await GoStoreControlPanel(value);
            });
        }

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;

                OnPropertyChanged();

                if( !( SelectedStore.StoreId == Guid.Empty ) )
                {
                    GoStoreControlPanelCommand.Execute(SelectedStore.StoreId);
                }
            }
        }

        public ObservableCollection<StorePresenters> StorePresenters { get; set; }

        public User UserInformation
        {
            get { return userinformation; }
            set
            {
                userinformation = value;

                OnPropertyChanged();
            }
        }

        protected ICommand GoStoreControlPanelCommand { get; set; }

        public async Task ExecuteLoadItems ()
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();
            }
            else
            {
                if( StorePresenters.Count > 0 )
                {
                    StorePresenters.Clear();
                }

                var yourStores = StoreDataStore.GetStoresFromUser(App.LogUser.UserId);

                foreach( var item in yourStores )
                {
                    var storePresenter = new StorePresenters(item);

                    StorePresenters.Add(storePresenter);
                }
            }
        }

        private async Task GoStoreControlPanel (Guid StoreId)
        {
            TokenExpManger tokenExpManger = new TokenExpManger(App.TokenDto.Exp);

            if( tokenExpManger.IsExpired() )
            {
                await tokenExpManger.CloseSession();
            }
            else
            {
                await Shell.Current.GoToAsync($"StoreControlPanelRoute?Id={StoreId.ToString()}", animate: true);
            }
        }
    }
}