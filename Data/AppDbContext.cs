

using bank_transaction_system.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Customer>(e => e.Property(c => c.Password).HasMaxLength(120));
        }
        
        public DbSet<Customer> Customer { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<BankTransaction> BankTransaction { get; set; }
    }
}