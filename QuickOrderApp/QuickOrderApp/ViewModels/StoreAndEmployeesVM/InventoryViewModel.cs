using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class InventoryViewModel : BaseViewModel
    {
        public ObservableCollection<ProductPresenter> StoreInventory { get; set; }
        public InventoryViewModel()
        {
            StoreInventory = new ObservableCollection<ProductPresenter>();
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
