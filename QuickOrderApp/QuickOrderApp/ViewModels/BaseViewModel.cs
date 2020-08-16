//using QuickOrderApp.Models;
using Library.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IProductDataStore productDataStore => Startup.ServiceProvider.GetService<IProductDataStore>();
        public IUserDataStore userDataStore => Startup.ServiceProvider.GetService<IUserDataStore>();
        public IOrderProductDataStore orderProductDataStore => Startup.ServiceProvider.GetService<IOrderProductDataStore>();
        public IStoreDataStore StoreDataStore => Startup.ServiceProvider.GetService<IStoreDataStore>();

        public IStoreLicenseDataStore storeLicenseDataStore => Startup.ServiceProvider.GetService<IStoreLicenseDataStore>();
        public IRequestDataStore RequestDataStore => Startup.ServiceProvider.GetService<IRequestDataStore>();
        public IOrderDataStore orderDataStore => Startup.ServiceProvider.GetService<IOrderDataStore>();

        public IEmployeeDataStore EmployeeDataStore => Startup.ServiceProvider.GetService<IEmployeeDataStore>();
        public IWorkHourDataStore WorkHourDataStore => Startup.ServiceProvider.GetService<IWorkHourDataStore>();

        public IEmployeeWorkHourDataStore EmployeeWorkHour => Startup.ServiceProvider.GetService<IEmployeeWorkHourDataStore>();

        public ICardDataStore CardDataStore => Startup.ServiceProvider.GetService<ICardDataStore>();
        public IUserConnectedDataStore userConnectedDataStore => Startup.ServiceProvider.GetService<IUserConnectedDataStore>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
