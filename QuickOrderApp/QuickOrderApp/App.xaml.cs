using Library.DTO;
using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Plugin.SharedTransitions;
using QuickOrderApp.ConfigPayment;
using QuickOrderApp.Services.HubService;
using QuickOrderApp.Utilities.Dependency;
using QuickOrderApp.Utilities.Dependency.Interface;
using QuickOrderApp.Views.Login;
using System.Threading;
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
        DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.144:5000/api" : "http://192.168.1.144:5000/api";

        public static bool UseMockDataStore = false;

        public static User LogUser;

        public static Store CurrentStore;
        public static TokenDTO TokenDto { get; set; }
        public static ComunicationService ComunicationService { get; set; } = new ComunicationService();

        public static UsersConnected UsersConnected { get; set; } 

        public static CardPaymentToken CardPaymentToken { get; set; } = new CardPaymentToken();

       

        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzEyODgyQDMxMzgyZTMyMmUzMG5kVWdEWkdxRFg3c2VzOWorUUI1LzUzNGtGVmsyd3JhYjJ4ZUZXQnloMFE9");
                    
            Dependencies();


            MainPage = Startup.ServiceProvider.GetRequiredService<AppShell>();

           

            //bool islogged = false;

            //if (!islogged) 
            //{
            //    Shell.Current.GoToAsync("LoginRoute");
            //}
            //else
            //{

            //}

          
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
            DependencyService.Register<StripeServiceDS>();
            DependencyService.Register<SubcriptionDataStore>();

        }

        protected override void OnStart()
        {

        }

        protected  override void OnSleep()
        {

            //await App.ComunicationService.Disconnect();

            //UserConnectedDataStore userConnectedDataStore = new UserConnectedDataStore();

            //if (App.UsersConnected != null)
            //{
            //    App.UsersConnected.IsDisable = true;
            //    var result = await userConnectedDataStore.UpdateItemAsync(App.UsersConnected);

            //    if (result)
            //    {
            //        UsersConnected = null;
            //    }

            //}

        }

        protected  override void OnResume()
        {


            //MainPage = new AppShell();

            //bool islogged = false;
            //if (!islogged)
            //{
            //    await Shell.Current.GoToAsync("LoginRoute");
            //}
            //else
            //{

            //}
        }
    }
}
