﻿using Library.Models;
using QuickOrderApp.Utilities.Loadings;
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
        private Store employeeSelectedStore;

        private Employee storeEmployeeSelected;

        private string username;

        public EmployeeControlHomeViewModel ()
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

                if( App.UsersConnected != null )
                {
                    App.UsersConnected.IsDisable = true;

                    var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

                    if( result )
                    {
                        App.UsersConnected = null;
                    }
                }
                await Shell.Current.GoToAsync("../LoginRoute");
            });
        }

        public LoadingManager LoadingManager { get; set; }
        public ICommand SignOutCommand { get; set; }

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
        public Employee StoreEmployeeSelected
        {
            get { return storeEmployeeSelected; }
            set
            {
                storeEmployeeSelected = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<StorePresenters> StorePresenters { get; set; }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;

                OnPropertyChanged();
            }
        }

        public ObservableCollection<Store> UserStoreEmployee { get; set; }

        private async Task GetUserEmployeeInformation ()
        {
            //Obtine los empleos del usuario.
            var userEmployees = await EmployeeDataStore.GetUserEmployees(App.LogUser.UserId.ToString());

            UserStoreEmployee = new ObservableCollection<Store>();

            foreach( var item in userEmployees )
            {
                //Obtine el empleado
                item.EmployeeStore = await StoreDataStore.GetAvailableStoreInformation(item.StoreId);

                if( item.EmployeeStore != null )
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