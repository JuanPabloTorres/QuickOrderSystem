using Library.Services;
using Library.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net.NetworkInformation;
using Xamarin.Essentials;

namespace QuickOrderApp
{
	public static class Startup
	{
		#region Properties

		public static IServiceProvider ServiceProvider { get; set; }
		private static string LocalBackendUrl;

		#endregion Properties

		#region Methods

		public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
		{
			var systemDir = FileSystem.CacheDirectory;
			Utils.ExtractSaveResource(@"QuickOrderApp.appsettings.json", systemDir);
			var fullConfig = Path.Combine(systemDir, "QuickOrderApp.appsettings.json");

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

			return ServiceProvider.GetService<App>();
		}

		private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
		{
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ctx.Configuration.GetSection("Licences:Syncfusion").Value);

			if (ctx.HostingEnvironment.IsDevelopment())
			{
				LocalBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
			}
			else
			{
			}

			services.AddHttpClient("MyHttpClient", client =>
			{
				client.BaseAddress = new Uri($"{LocalBackendUrl}/");
			});

			services.AddSingleton<AppShell>();
			services.AddSingleton<App>();

			#region ServiceServices

			services.AddSingleton<IProductDataStore, ProductDataStore>();
			services.AddSingleton<IUserDataStore, UserDataStore>();
			services.AddSingleton<IStoreDataStore, StoreDataStore>();
			services.AddSingleton<IOrderDataStore, OrderDataStore>();
			services.AddSingleton<IRequestDataStore, RequestDataStore>();
			services.AddSingleton<IEmployeeDataStore, EmployeeDataStore>();
			services.AddSingleton<IStoreLicenseDataStore, StoreLicenceDataStore>();
			services.AddSingleton<IWorkHourDataStore, WorkHourDataStore>();
			services.AddSingleton<IEmployeeWorkHourDataStore, EmployeeWorkHourDataStore>();
			services.AddSingleton<ICardDataStore, CardDataStore>();
			services.AddSingleton<IUserConnectedDataStore, UserConnectedDataStore>();

			#endregion ServiceServices
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

			#endregion Methods
		}
	}
}