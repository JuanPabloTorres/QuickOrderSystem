using Library.Models;
using Library.Services;
using Library.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickOrderAdmin.Utilities;

namespace QuickOrderAdmin
{
    public class Startup
    {

        //public static ComunicationService ComunicationService { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //ComunicationService = new ComunicationService();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<ComunicationService>();
            services.AddSingleton<UsersConnected>();
            services.AddSingleton<IStoreLicenseDataStore, StoreLicenceDataStore>();
            services.AddSingleton<IStoreDataStore, StoreDataStore>();
            services.AddSingleton<IUserDataStore, UserDataStore>();
            services.AddSingleton<IProductDataStore, ProductDataStore>();
            services.AddSingleton<IOrderDataStore, OrderDataStore>();
            services.AddSingleton<IEmployeeDataStore, EmployeeDataStore>();
            services.AddSingleton<IRequestDataStore, RequestDataStore>();
            services.AddSingleton<IEmployeeWorkHourDataStore, EmployeeWorkHourDataStore>();
            services.AddSingleton<IUserConnectedDataStore, UserConnectedDataStore>();
            services.AddSingleton<IStripeServiceDS, StripeServiceDS>();
            services.AddSingleton<ICardDataStore, CardDataStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=StoreHome}/{action=Index}/{id?}");
            });
        }
    }
}
