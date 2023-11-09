using CashFlow.Domain.Commons;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data.DbContexts
{
    public class CashFlowDbContext : DbContext
    {
        public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<UserAsset> UserAssets { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.userAssets)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.wallets)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Wallet>()
                .HasMany(w => w.Transactions)
                .WithOne(t => t.Wallet)
                .HasForeignKey(t => t.WalletId);

            modelBuilder.Entity<FinancialGoal>()
                .HasOne(fg => fg.User)
                .WithMany(u => u.financialGoals)
                .HasForeignKey(fg => fg.UserId);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UserId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
