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
        private bool isBusy = false;

        private string title = string.Empty;

        public ICardDataStore CardDataStore => DependencyService.Get<ICardDataStore>();

        public IEmployeeDataStore EmployeeDataStore => DependencyService.Get<IEmployeeDataStore>();

        public IEmployeeWorkHourDataStore EmployeeWorkHour => DependencyService.Get<IEmployeeWorkHourDataStore>();

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public IOrderDataStore orderDataStore => DependencyService.Get<IOrderDataStore>();

        public IOrderProductDataStore orderProductDataStore => DependencyService.Get<IOrderProductDataStore>();

        //public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IProductDataStore productDataStore => DependencyService.Get<IProductDataStore>();

        public IRequestDataStore requestDataStore => DependencyService.Get<IRequestDataStore>();

        public IRequestDataStore RequestDataStore => DependencyService.Get<IRequestDataStore>();

        public IStoreDataStore StoreDataStore => DependencyService.Get<IStoreDataStore>();

        public IStoreLicenseDataStore storeLicenseDataStore => DependencyService.Get<IStoreLicenseDataStore>();

        public IStripeServiceDS stripeServiceDS => DependencyService.Get<IStripeServiceDS>();

        public ISubcriptionDataStore SubcriptionDataStore => DependencyService.Get<ISubcriptionDataStore>();

        public string Title
        {
            get { return title; }

            set { SetProperty(ref title, value); }
        }

        public IUserConnectedDataStore userConnectedDataStore => DependencyService.Get<IUserConnectedDataStore>();

        public IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();

        public IWorkHourDataStore WorkHourDataStore => DependencyService.Get<IWorkHourDataStore>();

        protected bool SetProperty<T> (ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if( EqualityComparer<T>.Default.Equals(backingStore, value) )
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if( changed == null )
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}