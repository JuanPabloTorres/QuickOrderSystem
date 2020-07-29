using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.EmployeeStoreControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeOrdersControl : ContentPage
    {
        public EmployeeOrdersControl()
        {
            InitializeComponent();
            BindingContext = new StoreOrderViewModel();
        }
    }
}