using FiapBank.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FiapBank.Domain;

public class FiapBankContext(DbContextOptions<FiapBankContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<CheckingAccount> CheckingAccounts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SavingsAccount> SavingsAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>().HasData(new Bank("FiapBank"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
