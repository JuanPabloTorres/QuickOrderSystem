using Library.Models;
using QuickOrderApp.Utilities.Presenters;
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

        //public Store EmployeeSelectedStore
        //{
        //    get { return employeeSelectedStore; }
        //    set
        //    {
        //        employeeSelectedStore = value;
        //        OnPropertyChanged();
        //        EmployeeShell.Current.GoToAsync($"StoreControlEmployee?EmpStoreId={EmployeeSelectedStore.StoreId.ToString()}", animate: true);
        //    }
        //}

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



        public EmployeeControlHomeViewModel()
        {
            Username = App.LogUser.Name;
            StorePresenters = new ObservableCollection<StorePresenters>();
            GetUserEmployeeInformation();

            SignOutCommand = new Command(() => 
            {


                Shell.Current.GoToAsync("../LoginRoute");

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
                item.EmployeeStore = await StoreDataStore.GetItemAsync(item.StoreId.ToString());
                var empWorkHours = await EmployeeWorkHour.GetEmployeeWorkHours(item.EmployeeId.ToString());
                item.EmployeeWorkHours = empWorkHours.ToList();

                var storePresenter = new StorePresenters(item.EmployeeStore);

                StorePresenters.Add(storePresenter);
                //UserStoreEmployee.Add(item.EmployeeStore);
            }

            App.LogUser.Employees = userEmployees.ToList();
        }
    }
}
