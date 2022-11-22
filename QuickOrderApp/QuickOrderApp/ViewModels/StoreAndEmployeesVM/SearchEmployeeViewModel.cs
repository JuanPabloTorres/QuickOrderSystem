using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "id")]
    public class SearchEmployeeViewModel : BaseViewModel
    {
        private string storeId;

        private string tosearch;

        public SearchEmployeeViewModel ()
        {
            Users = new ObservableCollection<SearchEmployeePresenter>();

            SearchEmployeeCommand = new Command(async () =>
            {
                if( !string.IsNullOrEmpty(ToSearch) && !string.IsNullOrWhiteSpace(ToSearch) )
                {
                    await SearchEmployee(ToSearch);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "User with that information was not found...!", "OK");
                }
            });
        }

        public ICommand SearchEmployeeCommand { get; set; }

        public string StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;

                OnPropertyChanged();
            }
        }

        public string ToSearch
        {
            get { return tosearch; }
            set
            {
                if( tosearch != value )
                {
                    tosearch = value;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SearchEmployeePresenter> Users { get; set; }

        private async Task SearchEmployee (string value)
        {
            var usersdto = await userDataStore.GetUserWithName(value);

            Users.Clear();

            foreach( var item in usersdto )
            {
                var serchEmpPresenter = new SearchEmployeePresenter(item, StoreId);

                Users.Add(serchEmpPresenter);
            }
        }
    }
}