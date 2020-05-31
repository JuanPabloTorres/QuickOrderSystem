using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using QuickOrderApp.Models;
using QuickOrderApp.Services;
using Library.Services.Interface;

namespace QuickOrderApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IProductDataStore productDataStore => DependencyService.Get<IProductDataStore>();
        public IUserDataStore userDataStore => DependencyService.Get<IUserDataStore>();
        public IOrderProductDataStore orderProductDataStore => DependencyService.Get<IOrderProductDataStore>();
        public IStoreDataStore StoreDataStore => DependencyService.Get<IStoreDataStore>();

        public IOrderDataStore orderDataStore => DependencyService.Get<IOrderDataStore>();
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
