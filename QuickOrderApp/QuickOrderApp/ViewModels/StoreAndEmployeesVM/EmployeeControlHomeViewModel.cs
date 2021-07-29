using Library.Models;
using QuickOrderApp.Utilities.Loadings;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Views.Login;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class EmployeeControlHomeViewModel : BaseViewModel
    {


        public ObservableCollection<Store> UserStoreEmployee { get; set; }
        public ObservableCollection<StorePresenters> StorePresenters { get; set; }
        private string username;

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private Store employeeSelectedStore;



        private Employee storeEmployeeSelected;

        public Employee StoreEmployeeSelected
        {
            get { return storeEmployeeSelected; }
            set
            {
                storeEmployeeSelected = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignOutCommand { get; set; }

        public ICommand DisableBackCommand = new Command(() =>
        {


        });

        public LoadingManager LoadingManager { get; set; }



        public EmployeeControlHomeViewModel()
        {
            LoadingManager = new LoadingManager();
            Username = App.LogUser.Name;
            StorePresenters = new ObservableCollection<StorePresenters>();

            Task.Run(async () =>
            {

                LoadingManager.OnLoading();
                await GetUserEmployeeInformation();

                LoadingManager.OffLoading();
            });

            SignOutCommand = new Command(async () =>
            {

                await App.ComunicationService.Disconnect();


                if (App.UsersConnected != null)
                {
                    App.UsersConnected.IsDisable = true;
                    var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

                    if (result)
                    {
                        App.UsersConnected = null;
                    }

                }
                await Shell.Current.GoToAsync($"{LoginPage.Route}");

            });
        }

        async Task GetUserEmployeeInformation()
        {
            //Obtine los empleos del usuario.
            var userEmployees = await EmployeeDataStore.GetUserEmployees(App.LogUser.UserId.ToString());
            UserStoreEmployee = new ObservableCollection<Store>();

            foreach (var item in userEmployees)
            {

                //Obtine el empleado
                item.EmployeeStore = await StoreDataStore.GetAvailableStoreInformation(item.StoreId);
                if (item.EmployeeStore != null)
                {
                    var empWorkHours = await EmployeeWorkHour.GetEmployeeWorkHours(item.EmployeeId.ToString());
                    item.EmployeeWorkHours = empWorkHours.ToList();


                    var storePresenter = new StorePresenters(item.EmployeeStore);

                    StorePresenters.Add(storePresenter);

                }
                //UserStoreEmployee.Add(item.EmployeeStore);
            }

            App.LogUser.Employees = userEmployees.ToList();
        }
    }
}
