using System;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Services.Data
{
    public class BankDBContext : DbContext
    {
        public BankDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BankAppDB;user=sa;password=Sanchit123@sql#;Trusted_Connection=false");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<AccountHolder> AccountHolders { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<BankCharge> BankCharges { get; set; }

    }
}
