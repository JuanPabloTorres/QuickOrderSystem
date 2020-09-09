using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace QuickOrderApp.Utilities.Loadings
{
    public class LoadingManager: INotifyPropertyChanged
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

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

      
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        private bool isloading;

        public bool IsLoading
        {
            get { return isloading; }
            set { isloading = value;
                OnPropertyChanged();
            }
        }

        private bool contentVisible;

        public bool ContentVisible
        {
            get { return contentVisible; }
            set { contentVisible = value;
                OnPropertyChanged();
            }
        }



        public void OnLoading(bool loadingToOn = true)
        {
             IsLoading = loadingToOn;
            ContentVisible = false;
        }

        public void OffLoading(bool loadingToOff = false)
        {
            IsLoading = loadingToOff;
            ContentVisible = true;
        }

    }
}
