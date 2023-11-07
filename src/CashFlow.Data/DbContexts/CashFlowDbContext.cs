using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CashFlow.Data.DbContexts

{
    public class CashFlowDbContext : DbContext
    {
        public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options)
        : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<FinancialGoal> FinancialGoal { get; set; }
    }
}
