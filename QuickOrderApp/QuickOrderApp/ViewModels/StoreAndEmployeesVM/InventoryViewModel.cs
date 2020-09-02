using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class InventoryViewModel : BaseViewModel
    {
        public ObservableCollection<ProductPresenter> StoreInventory { get; set; }

        private string searchItem;

        public string SearchItem
        {
            get { return searchItem; }
            set { searchItem = value;
                OnPropertyChanged();
            }
        }


        public ICommand SearchItemCommand { get; set; }

        public ICommand ShowAllCommand { get; set; }

        public InventoryViewModel()
        {
            StoreInventory = new ObservableCollection<ProductPresenter>();


            SearchItemCommand = new Command(async () => 
            {


                var itemResult = await productDataStore.SearchItemOfStore(StoreControlPanelViewModel.YourSelectedStore.StoreId.ToString(), SearchItem);

                if (itemResult != null)
                {

                var productPresenter = new ProductPresenter(itemResult);


                StoreInventory.Clear();

                StoreInventory.Add(productPresenter);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Item could not be found. Try again with different name.", "OK");
                }
               
            
            });

            ShowAllCommand = new Command(() => 
            {

                LoadInventory();
            
            
            });
            LoadInventory();

            MessagingCenter.Subscribe<ProductPresenter>(this, "DeleteProductInventory", (obj) => 
            {

                StoreInventory.Remove(obj);
            
            });
        }

        async Task LoadInventory()
        {
            StoreInventory.Clear();

            var data = productDataStore.GetProductFromStore(StoreControlPanelViewModel.YourSelectedStore.StoreId);

            foreach (var item in data)
            {

                var productPresenter = new ProductPresenter(item);
                StoreInventory.Add(productPresenter);
            }


        }
    }
}
