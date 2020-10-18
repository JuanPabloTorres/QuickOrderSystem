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
        //public IDataStore<Item> DataStore => Startup.ServiceProvider.GetRequiredService<IDataStore<Item>>();
        public IProductDataStore productDataStore => Startup.ServiceProvider.GetRequiredService<IProductDataStore>();
        public IUserDataStore userDataStore => Startup.ServiceProvider.GetRequiredService<IUserDataStore>();
        public IOrderProductDataStore orderProductDataStore => Startup.ServiceProvider.GetRequiredService<IOrderProductDataStore>();
        public IStoreDataStore StoreDataStore => Startup.ServiceProvider.GetRequiredService<IStoreDataStore>();

        public IStoreLicenseDataStore storeLicenseDataStore => Startup.ServiceProvider.GetRequiredService<IStoreLicenseDataStore>();
        public IRequestDataStore requestDataStore => Startup.ServiceProvider.GetRequiredService<IRequestDataStore>();
        public IOrderDataStore orderDataStore => Startup.ServiceProvider.GetRequiredService<IOrderDataStore>();

        public IEmployeeDataStore EmployeeDataStore => Startup.ServiceProvider.GetRequiredService<IEmployeeDataStore>();
        public IWorkHourDataStore WorkHourDataStore => Startup.ServiceProvider.GetRequiredService<IWorkHourDataStore>();

        public IEmployeeWorkHourDataStore EmployeeWorkHour => Startup.ServiceProvider.GetRequiredService<IEmployeeWorkHourDataStore>();

        public ICardDataStore CardDataStore => Startup.ServiceProvider.GetRequiredService<ICardDataStore>();
        public IUserConnectedDataStore userConnectedDataStore => Startup.ServiceProvider.GetRequiredService<IUserConnectedDataStore>();

        public IRequestDataStore RequestDataStore => Startup.ServiceProvider.GetRequiredService<IRequestDataStore>();

        public IStripeServiceDS stripeServiceDS => Startup.ServiceProvider.GetRequiredService<IStripeServiceDS>();

        public ISubcriptionDataStore SubcriptionDataStore => Startup.ServiceProvider.GetRequiredService<ISubcriptionDataStore>();
           

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
