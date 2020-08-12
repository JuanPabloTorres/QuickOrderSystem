using Library.DTO;
using Library.Models;
using Library.Services;
using Plugin.SharedTransitions;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.Views.Login;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QuickOrderApp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";

        public static string LocalBackendUrl =
        DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.56.1:5000/api" : "http://192.168.56.1:5000/api";

        public static bool UseMockDataStore = true;
        public static User LogUser;
        public static Store CurrentStore;
        public static TokenDTO TokenDto { get; set; }
        public static ComunicationService ComunicationService { get; set; }

        public static UsersConnected UsersConnected { get; set; }
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjY0MTU4QDMxMzgyZTMxMmUzMEx6QkJ4RjEvcHl6V2VaMFF3TENBa0tUU1c1RWpKWlh3bDNUdXduc3J6Q2c9");

            Dependencies();



            // MainPage = new NavigationPage(new PaymentPage());
            //MainPage = new NavigationPage(new LoginPage());
            MainPage = new AppShell();

            bool islogged = false;
            if (!islogged)
            {
                Shell.Current.GoToAsync("LoginRoute");
            }
            else
            {

            }

            //SharedTransitionNavigationPage
        }

        void Dependencies()
        {
            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<ProductDataStore>();
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<StoreDataStore>();
            DependencyService.Register<OrderProductDataStore>();
            DependencyService.Register<OrderDataStore>();
            DependencyService.Register<RequestDataStore>();
            DependencyService.Register<EmployeeDataStore>();
            DependencyService.Register<StoreLicenceDataStore>();
            DependencyService.Register<WorkHourDataStore>();
            DependencyService.Register<EmployeeWorkHourDataStore>();
            DependencyService.Register<CardDataStore>();
            DependencyService.Register<UserConnectedDataStore>();
            DependencyService.Register<RequestDataStore>();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
