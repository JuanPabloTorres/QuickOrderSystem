using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderApp.Web.Context
{
    public class QOContext:DbContext
    {
      
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }

        public DbSet<ForgotPassword> ForgotPasswords { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<StoreLicense> StoreLicenses { get; set; }


        public QOContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>().HasMany(w => w.WorkHours).WithOne(s => s.WorkHourStore).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
