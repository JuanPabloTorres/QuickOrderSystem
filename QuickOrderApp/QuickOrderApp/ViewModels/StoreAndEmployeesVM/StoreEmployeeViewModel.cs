using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("StoreId", "Id")]
    public class StoreEmployeeViewModel : BaseViewModel
    {
        private string storeId;

        public StoreEmployeeViewModel ()
        {
            Employees = new ObservableCollection<StoreEmployeesPresenter>();
        }

        //public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<StoreEmployeesPresenter> Employees { get; set; }

        public string StoreId
        {
            get { return storeId; }
            set
            {
                storeId = value;

                OnPropertyChanged();

                GetEmployeeDataOfStore(storeId);
            }
        }

        public async Task GetEmployeeDataOfStore (string storeId)
        {
            Guid storeGuidId = new Guid(storeId);

            var employeesData = await EmployeeDataStore.GetEmployeesOfStore(storeGuidId);

            if( Employees.Count > 0 )
            {
                Employees.Clear();
            }
            foreach( var item in employeesData )
            {
                var user = await userDataStore.GetItemAsync(item.UserId.ToString());

                var workhours = await EmployeeWorkHour.GetEmployeeWorkHours(item.EmployeeId.ToString());

                item.EmployeeUser = user;

                item.EmployeeWorkHours = OrderingEmpWorkHour(workhours.ToList());

                var employeePresenter = new StoreEmployeesPresenter(item);

                Employees.Add(employeePresenter);
            }
        }

        private IList<EmployeeWorkHour> OrderingEmpWorkHour (IList<EmployeeWorkHour> employeeWorks)
        {
            EmployeeWorkHour[] orderTempWorkHour = new Library.Models.EmployeeWorkHour[Enum.GetValues(typeof(DayOfWeek)).Length];

            foreach( var item in employeeWorks )
            {
                DayOfWeek day;

                Enum.TryParse(item.Day, out day);

                int valueint;

                if( (int) day == 0 )
                {
                    valueint = Enum.GetValues(typeof(DayOfWeek)).Length - 1;
                }
                else
                {
                    valueint = (int) day - 1;
                }
                orderTempWorkHour[valueint] = item;
            }

            return orderTempWorkHour.ToList();
        }
    }
}