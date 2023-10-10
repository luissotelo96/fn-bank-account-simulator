using BankAccountSimulation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAccountSimulation.DataAccess.Database
{
    public class BankAccountSimulatorDbContext : DbContext
    {
        public BankAccountSimulatorDbContext(DbContextOptions<BankAccountSimulatorDbContext> options) : base(options) { }

        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<FinancialProduct> FinancialProduct { get; set; }
        public DbSet<FinancialMovements> FinancialMovements { get; set; }
    }
}
