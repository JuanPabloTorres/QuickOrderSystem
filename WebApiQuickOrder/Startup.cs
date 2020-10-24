
using Library.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System;
using System.Text;
using WebApiQuickOrder.Context;
using WebApiQuickOrder.Hubs;
using WebApiQuickOrder.Models;
using WebApiQuickOrder.Service;

namespace WebApiQuickOrder
{
    public class Startup
    {

       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
          
            StripeConfiguration.ApiKey=Configuration.GetSection("Stripe")["SecretKey"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QOContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:DevelopmentDBLocal"]));
            services.AddSignalR();

            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));
            services.AddSingleton<ITokenService, OktaTokenService>();
            services.AddMvc();

            // SERVICES
            services.AddSingleton<IOrderFactory, OrderFactory>();
            //------------------------------------------------------


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

            });

           

            services.AddAuthorization(config =>
            {
              
                //config.AddPolicy("AdmindAndEmployees", policy => policy.RequireRole("Admin","Employee"));
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
                config.AddPolicy(Policies.Employee, Policies.EmployeePolicy());
                config.AddPolicy(Policies.UserAndEmployees, Policies.UserAndEmployeesPolicy());
                config.AddPolicy(Policies.StoreControl, Policies.StoreControlPolicy());

            });
          
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ComunicationHub>("/comunicationhub");
            });
        }
    }
}
