using BankAccountSimulation.Domain.DTO;
using BankAccountSimulation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountSimulation.DataAccess.Database
{
    public class BankAccountSimulatorDbContext : DbContext
    {
        public BankAccountSimulatorDbContext(DbContextOptions<BankAccountSimulatorDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AverageBalanceTable>(x => x.HasNoKey());
            modelBuilder.Entity<TopBalanceCustomersTable>(x => x.HasNoKey());
        }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<FinancialProduct> FinancialProduct { get; set; }
        public DbSet<FinancialMovements> FinancialMovements { get; set; }    
        public DbSet<AverageBalanceTable> AverageBalanceTable { get; set; }
        public DbSet<TopBalanceCustomersTable> TopBalanceCustomersTable { get; set; }
    }
}
