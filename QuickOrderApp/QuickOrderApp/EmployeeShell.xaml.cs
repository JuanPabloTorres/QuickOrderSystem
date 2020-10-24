using QuickOrderApp.Views.Login;
using QuickOrderApp.Views.Store;
using QuickOrderApp.Views.Store.EmployeeStoreControlPanel;
using QuickOrderApp.Views.Store.StoreManger;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeShell : Xamarin.Forms.Shell
    {
        public EmployeeShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("EmployeeControlPanelRoute", typeof(EmployeeControlPanel));
            Routing.RegisterRoute("StoreControlPanelRoute", typeof(StoreControlPanel));
            Routing.RegisterRoute("StoreControlEmployee", typeof(StoreControlEmployee));           
            Routing.RegisterRoute("DetailOrderRoute", typeof(DetailOrder));          
            Routing.RegisterRoute("EmployeeOrderControl", typeof(EmployeeOrdersControl));
            Routing.RegisterRoute("InventoryRoute", typeof(StoreInventory));
            Routing.RegisterRoute(LoginPage.Route, typeof(LoginPage));
            Routing.RegisterRoute(OrderScannerPage.Route, typeof(OrderScannerPage));
            Routing.RegisterRoute(StoreOrderDetailPage.Route, typeof(StoreOrderDetailPage));
            Routing.RegisterRoute(ScannedOrder.Route, typeof(ScannedOrder));
            Routing.RegisterRoute(LoginPage.Route, typeof(LoginPage));
            Routing.RegisterRoute(RegisterPage.Route, typeof(RegisterPage));
            Routing.RegisterRoute(ForgotPasswordPage.Route, typeof(ForgotPasswordPage));


        }
    }
}