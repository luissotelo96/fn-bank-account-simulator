using BankAccountSimulation.DataAccess.Database;
using BankAccountSimulation.DataAccess.Repositories;
using BankAccountSimulation.Domain.Repositories;
using BankAccountSimulation.Domain.Services;
using FnBankAccountSimulation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
[assembly: FunctionsStartup(typeof(Startup))]
namespace FnBankAccountSimulation
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<BankAccountSimulatorDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("SQLConnectionString"));
            });

            builder.Services.AddScoped<ICustomerService, CustomerService> ();
            builder.Services.AddScoped<ICustomerRepository, CustomerSqlRepository>();

            builder.Services.AddScoped<IFinancialProductService, FinancialProductService>();
            builder.Services.AddScoped<IFinancialProductRepository, FinancialProductSqlRepository>();
        }        
    }
}
