using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels
{
    
    public class HomeViewModel:BaseViewModel
    {

        public ObservableCollection<StorePresenters> Stores { get; set; }


        private Store selectedStore;

        public Store SelectedStore
        {
            get { return selectedStore; }
            set { selectedStore = value;
                OnPropertyChanged();
               
            }
        }


        public HomeViewModel()
        {
            GetStore();
            SelectedStore = new Store();
        }

        async Task GetStore()
        {
            var storeData = await StoreDataStore.GetItemsAsync();

            Stores = new ObservableCollection<StorePresenters>();

            foreach (var store in storeData)
            {
                var storepresenter = new StorePresenters(store);

                Stores.Add(storepresenter);
            }

        }

      

    }
}
