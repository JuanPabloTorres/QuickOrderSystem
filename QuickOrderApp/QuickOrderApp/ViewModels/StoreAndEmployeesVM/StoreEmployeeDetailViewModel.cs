using Library.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class StoreEmployeeDetailViewModel : BaseViewModel
    {
       public ObservableCollection<EmployeeWorkHour> EmployeeWorkHours { get; set; }

        private Employee empDetail;

        public Employee EmployeeDetail
        {
            get { return empDetail; }
            set
            {
                empDetail = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoEditCommand { get; set; }

        public StoreEmployeeDetailViewModel()
        {
            EmployeeWorkHours = new ObservableCollection<EmployeeWorkHour>();
            MessagingCenter.Subscribe<Employee>(this, "EmpDetail", (sender) =>
            {
                GetEmployeeInformation(sender);

            });

            GoEditCommand = new Command(async () =>
            {

                await Shell.Current.GoToAsync("StoreEmployeeEditRoute");

            });


        }

        async Task GetEmployeeInformation(Employee sender)
        {
            EmployeeDetail = sender;

            var empUserInfo = await userDataStore.GetItemAsync(EmployeeDetail.UserId.ToString());
            EmployeeDetail.EmployeeUser = empUserInfo;

            var workHours = EmployeeDetail.EmployeeWorkHours.Where(wh => wh.WillWork == true).ToList();

            foreach (var item in workHours)
            {
                EmployeeWorkHours.Add(item);

            }
        }
    }
}
