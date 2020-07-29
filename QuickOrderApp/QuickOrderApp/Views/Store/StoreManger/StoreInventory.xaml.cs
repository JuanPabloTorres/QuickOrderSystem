using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreInventory : ContentPage
    {
        InventoryViewModel InventoryViewModel;
        public StoreInventory()
        {
            InitializeComponent();
            BindingContext = InventoryViewModel = new InventoryViewModel();
        }
    }
}