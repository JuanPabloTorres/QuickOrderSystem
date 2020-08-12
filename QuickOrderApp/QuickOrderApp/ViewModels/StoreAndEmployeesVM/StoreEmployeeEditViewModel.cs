using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class StoreEmployeeEditViewModel : BaseViewModel
    {
        public ObservableCollection<WorkHourPresenter> WorkHourPresenters { get; set; }

        private string selectedPosition;

        public string SelectedPosisition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Positions { get; set; }

        private Employee empDetail;

        public Employee EmpDetail
        {
            get { return empDetail; }
            set
            {
                empDetail = value;
                OnPropertyChanged();
            }
        }

        public ICommand CompleteEditCommand { get; set; }


        public StoreEmployeeEditViewModel()
        {

            MessagingCenter.Subscribe<Employee>(this, "EmpEdit", (sender) =>
            {
                EmpDetail = sender;

            });

            #region Initialize Positions Collection
            Positions = new ObservableCollection<string>();

            foreach (var item in Enum.GetValues(typeof(EmployeeType)).Cast<EmployeeType>().ToList())
            {
                Positions.Add(item.ToString());
            }

            #endregion
            #region WorkHourPresenter Initialize
            WorkHourPresenters = new ObservableCollection<WorkHourPresenter>();

            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Monday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Tuesday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Wednesday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Thursday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Friday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Saturday.ToString()));
            WorkHourPresenters.Add(new WorkHourPresenter(DayOfWeek.Sunday.ToString()));
            #endregion

            CompleteEditCommand = new Command(async () =>
            {

                List<EmployeeWorkHour> employeeWorkHour = new List<EmployeeWorkHour>();

                foreach (var item in WorkHourPresenters)
                {
                    var workhour = new EmployeeWorkHour()
                    {
                        CloseTime = Convert.ToDateTime(item.Close.ToString()),
                        Day = item.Day,
                        OpenTime = Convert.ToDateTime(item.Open.ToString()),
                        WorkHourId = Guid.NewGuid(),
                        EmpId = empDetail.EmployeeId,
                        WillWork = item.WillWork

                    };
                    employeeWorkHour.Add(workhour);

                }

                EmpDetail.EmployeeWorkHours = employeeWorkHour;

                EmployeeType type;
                Enum.TryParse(SelectedPosisition, out type);
                EmpDetail.Type = type;

                var empUpdateResult = await EmployeeDataStore.UpdateItemAsync(empDetail);

                if (empUpdateResult)
                {
                    await Shell.Current.DisplayAlert("Notification", "Employee Update", "OK");

                    //await Shell.Current.GoToAsync($"..?id={StoreControlPanelViewModel.YourSelectedStore.StoreId.ToString()}");
                }

            });
        }
    }
}
