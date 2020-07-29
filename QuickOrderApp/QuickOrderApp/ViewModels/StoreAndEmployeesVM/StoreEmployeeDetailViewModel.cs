using Library.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class StoreEmployeeDetailViewModel : BaseViewModel
    {

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


            //EmployeeWorkHour[] orderTempWorkHour = new Library.Models.EmployeeWorkHour[Enum.GetValues(typeof(DayOfWeek)).Length];

            //foreach (var item in EmployeeDetail.EmployeeWorkHours)
            //{
            //    DayOfWeek day;
            //    Enum.TryParse(item.Day, out day);

            //    int valueint;
            //    if ((int)day == 0)
            //    {
            //        valueint = Enum.GetValues(typeof(DayOfWeek)).Length - 1;
            //    }
            //    else
            //    {
            //         valueint = (int)day - 1;

            //    }
            //    orderTempWorkHour[valueint] = item;

            //}


            //EmployeeDetail.EmployeeWorkHours = orderTempWorkHour.ToList();
            EmployeeDetail.EmployeeUser = empUserInfo;
        }
    }
}
