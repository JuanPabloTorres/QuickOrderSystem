using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Library.Services;
using Library.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugin.Media;
using Plugin.Media.Abstractions;
using QuickOrderApp.ViewModels.OrderVM;
using Xamarin.Essentials;

namespace QuickOrderApp
{
    public static class Startup
    {

        #region Properties

        public static IServiceProvider ServiceProvider { get; set; }

        #endregion Properties

        #region Methods

        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource(@"QuickOrderApp.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "QuickOrderApp.appsettings.json");

            //LoggingConfiguration
            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(fullConfig);
                })
                .ConfigureServices((context, services) =>
                {
                    nativeConfigureServices(context, services);
                    ConfigureServices(context, services);
                })
                .Build();

            ServiceProvider = host.Services;

            //Keys.LicenseKeys.AddSingletonLicences();

            return ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            string backEndUrl = string.Empty;
            if (ctx.HostingEnvironment.IsDevelopment())
            {
                backEndUrl = Keys.APIKeys.LocalBackendUrl;
            }
            else
            {
                backEndUrl = Keys.APIKeys.ServerBackendUrl;
            }

            services.AddHttpClient("MyHttpClient", client =>
            {
                client.BaseAddress = new Uri($"{backEndUrl}/");
            });

            services.AddSingleton<AppShell>();
            services.AddSingleton<App>();


            #region FactoriesServices



            #endregion FactoriesServices

            #region DataStoreServices

            services.AddSingleton<IProductDataStore, ProductDataStore>();
            services.AddSingleton<IUserDataStore, UserDataStore>();
            services.AddSingleton<IStoreDataStore, StoreDataStore>();
            services.AddSingleton<IOrderProductDataStore, OrderProductDataStore>();
            services.AddSingleton<IOrderDataStore, OrderDataStore>();
            services.AddSingleton<IRequestDataStore, RequestDataStore>();
            services.AddSingleton<IEmployeeDataStore, EmployeeDataStore>();
            services.AddSingleton<IStoreLicenseDataStore, StoreLicenceDataStore>();
            services.AddSingleton<IWorkHourDataStore, WorkHourDataStore>();
            services.AddSingleton<IEmployeeWorkHourDataStore, EmployeeWorkHourDataStore>();
            services.AddSingleton<ICardDataStore, CardDataStore>();
            services.AddSingleton<IUserConnectedDataStore, UserConnectedDataStore>();
            services.AddSingleton<IRequestDataStore, RequestDataStore>();
            services.AddSingleton<IStripeServiceDS, StripeServiceDS>();
            services.AddSingleton<ISubcriptionDataStore, SubcriptionDataStore>();

            #endregion DataStoreServices

            #region ViewModelsServices

            services.AddSingleton<UserDetailOrderViewModel>();
            services.AddSingleton<OrderViewModel>();


            #endregion ViewModelsServices

            #region ServiceServices



            #endregion ServiceServices
        }

        #endregion Methods
    }
    public static class Utils
    {

        public static void ExtractSaveResource(string filename, string location)
        {
            var a = System.Reflection.Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream != null)
                {
                    lock (resFilestream)
                    {
                        var full = Path.Combine(location, filename);
                        using (var stream = File.Create(full))
                        {
                            resFilestream.CopyTo(stream);
                        }
                    }
                }
            }
        }

        public static void GetByteArrayFromPath(out byte[] source, string path)
        {
            ExtractSaveResource(path, FileSystem.CacheDirectory);
            Stream str = new StreamReader(Path.Combine(FileSystem.CacheDirectory, path)).BaseStream;
            MemoryStream ms = new MemoryStream();
            str.CopyTo(ms);
            source = ms.ToArray();
        }

        public static async Task<byte[]> PickPhoto()
        {
            MediaFile mediaFile;
            MemoryStream memoryStream;
            byte[] productImageSource = null;
            string directory = "MyStores";

            //bool init = await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 50
                });

                if (mediaFile != null)
                {
                    memoryStream = new MemoryStream();
                    mediaFile.GetStream().CopyTo(memoryStream);
                    productImageSource = memoryStream.ToArray();
                    mediaFile.Dispose();
                }
            }
            else
            {
                //--------------------------------------
                //
                // TODO: Inject PopupView and display
                //       message of permission.
                //
                //--------------------------------------
            }

            return productImageSource;
        }
    }
    public static class Keys
    {
        public static class LicenseKeys
        {
            public static void RegisterLicense()
            {
                Syncfusion.Licensing.SyncfusionLicenseProvider
                    .RegisterLicense(@"MjYxNTc0QDMxMzgyZTMxMmUzMGF6N0VLZXM1cENIL1l4ZktEVlVNSXh6NGNRbVpTdTQ1blVsb3pZVGVqQ1E9");
            }
        }

        public static class APIKeys
        {

            //To debug on Android emulators run the web backend against .NET Core not IIS
            //If using other emulators besides stock Google images you may need to adjust the IP address
            public static string ServerBackendUrl =
               DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.10.120:44100/api" : "http://192.168.10.120:44100/api";

            public static string LocalBackendUrl =
                DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.144:5000/api" : "http://192.168.1.144:5000/api";

            //public static string LocalBackendUrl =
            //    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.11.6.188:5000/api" : "http://10.11.6.188:5000/api";
        }
    }
}
