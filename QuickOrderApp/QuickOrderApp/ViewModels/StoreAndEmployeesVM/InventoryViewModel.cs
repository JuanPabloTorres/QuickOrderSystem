using Library.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    public class InventoryViewModel : BaseViewModel
    {
        public ObservableCollection<Product> StoreInventory { get; set; }
        public InventoryViewModel()
        {
            StoreInventory = new ObservableCollection<Product>();
            LoadInventory();
        }

        async Task LoadInventory()
        {
            StoreInventory.Clear();

            var data = productDataStore.GetProductFromStore(StoreControlPanelViewModel.YourSelectedStore.StoreId);

            foreach (var item in data)
            {
                StoreInventory.Add(item);
            }


        }
    }
}
