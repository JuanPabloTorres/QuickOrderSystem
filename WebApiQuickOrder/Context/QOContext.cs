using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiQuickOrder.Context
{
    public class QOContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }

        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<UserRequest> Requests { get; set; }

        public DbSet<StoreLicense> StoreLicenses { get; set; }
        public DbSet<EmployeeWorkHour> EmployeeWorkHours { get; set; }

        public DbSet<PaymentCard> PaymentCards { get; set; }

        public DbSet<UsersConnected> usersConnecteds { get; set; }



        public QOContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasMany(w => w.EmployeeWorkHours).WithOne(s => s.Employee).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
