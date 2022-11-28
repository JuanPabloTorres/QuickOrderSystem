using Library.DTO;
using Library.Models;
using Library.Services;
using Library.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using QuickOrderApp.ConfigPayment;
using QuickOrderApp.Services.HubService;
using System;
using Xamarin.Forms;

namespace QuickOrderApp
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public static Store CurrentStore;

        public static string LocalBackendUrl = "http://192.168.0.2:5000";

        public static AppUser LogUser;

        private void SetupServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IUserDataStore, UserDataStore>();

            services.AddTransient<IStoreDataStore, StoreDataStore>();

            services.AddTransient<IOrderProductDataStore, OrderProductDataStore>();

            services.AddTransient<IRequestDataStore, RequestDataStore>();

            services.AddTransient<IEmployeeDataStore, EmployeeDataStore>();

            services.AddTransient<IStoreLicenseDataStore, StoreLicenceDataStore>();

            services.AddTransient<IWorkHourDataStore, WorkHourDataStore>();

            services.AddTransient<IEmployeeWorkHourDataStore, EmployeeWorkHourDataStore>();

            services.AddTransient<ICardDataStore, CardDataStore>();

            services.AddTransient<IStripeServiceDS, StripeServiceDS>();

            services.AddTransient<ISubcriptionDataStore, SubcriptionDataStore>();

            services.AddTransient<IStoreDataStore, StoreDataStore>();

            services.AddTransient<IOrderDataStore, OrderDataStore>();

            services.AddTransient<IStoreLicenseDataStore, StoreLicenceDataStore>();

            services.AddTransient<IUserConnectedDataStore, UserConnectedDataStore>();

            ServiceProvider = services.BuildServiceProvider();
        }

        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzEyODgyQDMxMzgyZTMyMmUzMG5kVWdEWkdxRFg3c2VzOWorUUI1LzUzNGtGVmsyd3JhYjJ4ZUZXQnloMFE9");

            Dependencies();

            MainPage = new AppShell();

            bool islogged = false;

            if (!islogged)
            {
                Shell.Current.GoToAsync("LoginRoute");
            }
            else
            {
            }
        }

        public static CardPaymentToken CardPaymentToken { get; set; } = new CardPaymentToken();
        public static ComunicationService ComunicationService { get; set; } = new ComunicationService();
        public static TokenDTO TokenDto { get; set; }
        public static UsersConnected UsersConnected { get; set; }

        protected override void OnResume()
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

        protected override void OnSleep()
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

        protected override void OnStart()
        {
        }

        private void Dependencies()
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
    }
}