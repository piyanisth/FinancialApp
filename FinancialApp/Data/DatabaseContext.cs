using FinancialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Data
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } // Customers Table
        public DbSet<Account> Accounts { get; set; } // Accounts Table


        // override purpose is inserting the mock data into db while running application at the first time.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly defining UUID as the primary key
            modelBuilder.Entity<Customer>().HasKey(c => c.UUID);
            modelBuilder.Entity<Account>().HasKey(a => a.UUID);

            var HalitSalihogluUUID = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var HalukSalihogluUUID = Guid.Parse("00000000-0000-0000-0000-000000000002");

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    UUID = HalitSalihogluUUID,
                    IDNumber = "123456789",
                    Name = "Halit",
                    Surname = "Salihoglu"
                },
                new Customer
                {
                    UUID = HalukSalihogluUUID,
                    IDNumber = "987654321",
                    Name = "Haluk",
                    Surname = "Salihoglu"
                }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    UUID = Guid.NewGuid(),
                    CustomerId = HalitSalihogluUUID,
                    Currency = "TRY",
                    AccountName = "Halit's TRY Account"
                },
                new Account
                {
                    UUID = Guid.NewGuid(),
                    CustomerId = HalukSalihogluUUID,
                    Currency = "BTC",
                    AccountName = "Haluk's Bitcoin Account"
                }
            );

        }

    }
}
