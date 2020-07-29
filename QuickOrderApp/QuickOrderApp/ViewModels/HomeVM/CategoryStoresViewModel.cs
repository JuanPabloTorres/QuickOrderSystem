using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.HomeVM
{
    [QueryProperty("Category", "CategoryId")]
    public class CategoryStoresViewModel : BaseViewModel
    {
        public ObservableCollection<StorePresenters> Stores { get; set; }

        private Store selectedStore;

        public Store SelectedStore
        {
            get { return selectedStore; }
            set
            {
                selectedStore = value;
                OnPropertyChanged();

            }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged();

                GetSpecificStoreCategory(Category);
            }
        }


        public CategoryStoresViewModel()
        {
            Stores = new ObservableCollection<StorePresenters>();
        }

        public async Task GetSpecificStoreCategory(string category)
        {
            var data = await StoreDataStore.GetSpecificStoreCategory(category);

            foreach (var store in data)
            {
                var storepresenter = new StorePresenters(store);

                Stores.Add(storepresenter);
            }


        }
    }
}
