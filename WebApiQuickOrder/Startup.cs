using Library.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using WebApiQuickOrder.Models.Email;

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
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

           // var key = Encoding.ASCII.GetBytes("testignKey");
           // services.AddAuthentication(x =>
           // {
           //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           // })
           //.AddJwtBearer(x =>
           //{
           //    x.RequireHttpsMetadata = false;
           //    x.SaveToken = true;
           //    x.TokenValidationParameters = new TokenValidationParameters
           //    {
           //        ValidateIssuerSigningKey = true,
           //        IssuerSigningKey = new SymmetricSecurityKey(key),
           //        ValidateIssuer = false,
           //        ValidateAudience = false
           //    };
           //});

            //services.AddLogging();
            services.AddControllers();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IAuthCodeFactory, AuthCodeFactory>();
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

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ComunicationHub>("/comunicationhub");
            });

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ComunicationHub>("/comunicationhub");
            //});

			//Sedding Database

			//using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			//{
			//	if (!serviceScope.ServiceProvider.GetService<QOContext>().AllMigrationsApplied())
			//	{
			//		serviceScope.ServiceProvider.GetService<QOContext>().Database.Migrate();
			//		serviceScope.ServiceProvider.GetService<QOContext>().EnsureSeeded();
			//	}
			//	else
			//	{
			//		serviceScope.ServiceProvider.GetService<DataContext>().EnsureSeeded();
			//	}
			//}
		}

	}
}
