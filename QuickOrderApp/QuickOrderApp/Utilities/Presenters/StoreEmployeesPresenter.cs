using Library.Models;
using QuickOrderApp.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class StoreEmployeesPresenter : BaseViewModel
    {
        private Employee employee;

        private bool iswork;

        public StoreEmployeesPresenter (Employee emp)
        {
            Employee = emp;

            EditCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("StoreEmployeeEditRoute");

                MessagingCenter.Send<Employee>(emp, "EmpEdit");
            });
            DetailCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("StoreEmployeeDetailRoute");

                MessagingCenter.Send<Employee>(emp, "EmpDetail");
            });
        }

        public ICommand DetailCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;

                OnPropertyChanged();
            }
        }

        public bool IsWork
        {
            get { return iswork; }
            set
            {
                iswork = value;

                OnPropertyChanged();
            }
        }
    }
}