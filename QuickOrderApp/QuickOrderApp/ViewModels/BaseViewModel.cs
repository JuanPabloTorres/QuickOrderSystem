//using QuickOrderApp.Models;
using Library.Services.Interface;
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
        public IProductDataStore productDataStore => DependencyService.Get<IProductDataStore>();
        public IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();
        public IOrderProductDataStore orderProductDataStore => DependencyService.Get<IOrderProductDataStore>();
        public IStoreDataStore StoreDataStore => DependencyService.Get<IStoreDataStore>();

        public IStoreLicenseDataStore storeLicenseDataStore => DependencyService.Get<IStoreLicenseDataStore>();
        public IRequestDataStore requestDataStore => DependencyService.Get<IRequestDataStore>();
        public IOrderDataStore orderDataStore => DependencyService.Get<IOrderDataStore>();

        public IEmployeeDataStore EmployeeDataStore => DependencyService.Get<IEmployeeDataStore>();
        public IWorkHourDataStore WorkHourDataStore => DependencyService.Get<IWorkHourDataStore>();

        public IEmployeeWorkHourDataStore EmployeeWorkHour => DependencyService.Get<IEmployeeWorkHourDataStore>();

        public ICardDataStore CardDataStore => DependencyService.Get<ICardDataStore>();
        public IUserConnectedDataStore userConnectedDataStore => DependencyService.Get<IUserConnectedDataStore>();

        public IRequestDataStore RequestDataStore => DependencyService.Get<IRequestDataStore>();

        public IStripeServiceDS stripeServiceDS => DependencyService.Get<IStripeServiceDS>();

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
