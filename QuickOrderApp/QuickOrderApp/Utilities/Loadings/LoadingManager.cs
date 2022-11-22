using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuickOrderApp.Utilities.Loadings
{
    public class LoadingManager : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if( changed == null )
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T> (ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if( EqualityComparer<T>.Default.Equals(backingStore, value) )
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion INotifyPropertyChanged

        private bool contentVisible;

        private bool isloading;

        public bool ContentVisible
        {
            get { return contentVisible; }
            set
            {
                contentVisible = value;

                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return isloading; }
            set
            {
                isloading = value;
                OnPropertyChanged();
            }
        }

        public void OffLoading (bool loadingToOff = false)
        {
            IsLoading = loadingToOff;

            ContentVisible = true;
        }

        public void OnLoading (bool loadingToOn = true)
        {
            IsLoading = loadingToOn;

            ContentVisible = false;
        }
    }
}