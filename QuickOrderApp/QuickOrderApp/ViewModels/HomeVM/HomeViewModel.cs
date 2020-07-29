using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using QuickOrderApp.Utilities.Presenters.PresenterModel;
using QuickOrderApp.Views.Home;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.HomeVM
{

    public class HomeViewModel : BaseViewModel
    {

        public ObservableCollection<StorePresenters> Stores { get; set; }

        public ObservableCollection<StoreCategory> StoreCategories { get; set; }
        public ICommand SearchStoreCommand { get; set; }

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

        private StoreCategory selectedcategory;

        public StoreCategory SelectedCategory
        {
            get { return selectedcategory; }
            set
            {
                selectedcategory = value;
                OnPropertyChanged();

                Shell.Current.GoToAsync($"{CategoryStoresHome.Route}?CategoryId={SelectedCategory.Category}", animate: true);
            }
        }


        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }



        public HomeViewModel()
        {
            StoreCategories = new ObservableCollection<StoreCategory>();
            GetQuickOrderStores();


            SetStoreCategories();

            SelectedStore = new Store();

            SearchStoreCommand = new Command(async () =>
            {
                var result = await StoreDataStore.SearchStore(SearchText);

                Stores.Clear();

                foreach (var store in result)
                {
                    var storepresenter = new StorePresenters(store);

                    Stores.Add(storepresenter);
                }

            });
        }

        async Task GetQuickOrderStores()
        {
            var storeData = await StoreDataStore.GetItemsAsync();

            Stores = new ObservableCollection<StorePresenters>();

            foreach (var store in storeData)
            {
                var storepresenter = new StorePresenters(store);

                Stores.Add(storepresenter);
            }

        }

        public void SetStoreCategories()
        {
            //var values = Enum.GetValues(typeof(StoreType));

            StoreCategory barbershopcategory = new StoreCategory(StoreType.BarberShop.ToString(), "barbershop.png");

            StoreCategories.Add(barbershopcategory);

            StoreCategory grocerycategory = new StoreCategory(StoreType.Grocery.ToString(), "supermarket.png");

            StoreCategories.Add(grocerycategory);

            StoreCategory farmingcategory = new StoreCategory(StoreType.Farming.ToString(), "garden.png");


            StoreCategories.Add(farmingcategory);

            StoreCategory restaurantcategory = new StoreCategory(StoreType.Restaurant.ToString(), "food.png");

            StoreCategories.Add(restaurantcategory);

            StoreCategory autopartscategory = new StoreCategory(StoreType.AutorParts.ToString(), "support.png");

            StoreCategories.Add(autopartscategory);


            StoreCategory servicecategory = new StoreCategory(StoreType.Service.ToString(), "partnership.png");

            StoreCategories.Add(servicecategory);

            StoreCategory storecategory = new StoreCategory(StoreType.Store.ToString(), "tag.png");

            StoreCategories.Add(storecategory);

            StoreCategory clothescategory = new StoreCategory(StoreType.ClothingStore.ToString(), "clothes.png");

            StoreCategories.Add(clothescategory);






        }

    }
}
