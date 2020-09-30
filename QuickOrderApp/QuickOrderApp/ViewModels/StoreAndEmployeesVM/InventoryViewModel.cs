using Library.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ICommand MoreCommand { get; set; }


        public Dictionary<string,IEnumerable<Product>> keyValues { get; set; }
        public InventoryViewModel()
        {
            StoreInventory = new ObservableCollection<ProductPresenter>();
            keyValues = new Dictionary<string, IEnumerable<Product>>();

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

            ShowAllCommand = new Command(async() => 
            {

               await LoadInventory();
            
            
            });
            LoadInventory();

            MessagingCenter.Subscribe<Product>(this, "InventoryItemUpdated", (obj) => 
            {

                var itemtoRemove = StoreInventory.Where(pid => pid.ProductId == obj.ProductId).FirstOrDefault();


                StoreInventory.Remove(itemtoRemove);
                var productPresenterUpdated = new ProductPresenter(obj);

                StoreInventory.Add(productPresenterUpdated);
            
            
            
            
            });

            MessagingCenter.Subscribe<ProductPresenter>(this, "DeleteProductInventory", (obj) => 
            {

                StoreInventory.Remove(obj);
            
            });

            MoreCommand = new Command(async() =>
            {

                var result = await productDataStore.GetDifferentProductFromStore(keyValues["productAdded"],StoreControlPanelViewModel.YourSelectedStore.StoreId);


                if (result != null)
                {
                    List<Product> tempData = new List<Product>();

                    foreach (var item in keyValues["productAdded"])
                    {

                        if (!tempData.Any(s => s.ProductId == item.ProductId))
                        {

                            tempData.Add(item);

                        }
                    }

                    foreach (var item in result)
                    {
                        if (!tempData.Any(s => s.ProductId == item.ProductId))
                        {

                            tempData.Add(item);

                        }

                    }


                    keyValues.Clear();
                    keyValues.Add("productAdded", tempData);

                    foreach (var item in keyValues["productAdded"])
                    {

                        if (!StoreInventory.Any(s => s.ProductId == item.ProductId))
                        {
                            var productPresenter = new ProductPresenter(item);


                            StoreInventory.Add(productPresenter);

                        }
                    }
                }
              

            });
        }

        async Task LoadInventory()
        {

            if (StoreInventory.Count() > 0)
            {
                StoreInventory.Clear();

            }

            var data = productDataStore.GetProductFromStore(StoreControlPanelViewModel.YourSelectedStore.StoreId);

            if (!keyValues.ContainsKey("productAdded"))
            {
                keyValues.Add("productAdded", data);

            }

            foreach (var item in data)
            {
                var productPresenter = new ProductPresenter(item);
                StoreInventory.Add(productPresenter);
            }


        }
    }
}
