using Plugin.SharedTransitions;
using QuickOrderApp.Views.Home;
using QuickOrderApp.Views.Settings;
using QuickOrderApp.Views.Store;
using QuickOrderApp.Views.Store.EmployeeStoreControlPanel;
using QuickOrderApp.Views.Store.StoreManger;

using Xamarin.Forms;

namespace QuickOrderApp
{
    public partial class AppShell : SharedTransitionShell
    {
        public AppShell()
        {
            InitializeComponent();
            RegistersRoutes();
        }

        void RegistersRoutes()
        {
            Routing.RegisterRoute("HomePageRoute", typeof(HomePage));
            Routing.RegisterRoute("StoreHomeRoute", typeof(StoreHome));
            Routing.RegisterRoute("ProductRoute", typeof(Products));
            Routing.RegisterRoute("DetailOrderRoute", typeof(DetailOrder));
            Routing.RegisterRoute("StoreOrderRoute", typeof(StoreOrders));
            Routing.RegisterRoute("UserInformationRoute", typeof(UserInformation));
            Routing.RegisterRoute("UpdateProfileRoute", typeof(UpdateProfile));
            Routing.RegisterRoute("GetLicensenStoreRoute", typeof(GetLicenseStorePage));
            Routing.RegisterRoute("RegisterStoreRoute", typeof(RegisterStorePage));
            Routing.RegisterRoute("StoreControlPanelRoute", typeof(StoreControlPanel));
            Routing.RegisterRoute("AddProductRoute", typeof(AddProductPage));
            Routing.RegisterRoute("OrderPageRoute", typeof(OrdersPage));
            Routing.RegisterRoute("StoreDetailOrderRoute", typeof(StoreOrderDetailPage));
            Routing.RegisterRoute("StoreEmployeeRoute", typeof(StoreEmployeesPage));
            Routing.RegisterRoute("StoreEmployeeEditRoute", typeof(StoreEmployeeEdit));
            Routing.RegisterRoute("StoreEmployeeDetailRoute", typeof(StoreEmployeeDetail));
            Routing.RegisterRoute("EmployeeControlPanelRoute", typeof(EmployeeControlPanel));
            Routing.RegisterRoute("InventoryRoute", typeof(StoreInventory));
            Routing.RegisterRoute("RegisterCardRoute", typeof(RegisterCardPage));
            Routing.RegisterRoute("StoreLicenseRoute", typeof(GetStoreLicensePage));
            Routing.RegisterRoute("categoryhomeRoute", typeof(CategoryHomePage));
            Routing.RegisterRoute(CategoryStoresHome.Route, typeof(CategoryStoresHome));


        }
    }
}
