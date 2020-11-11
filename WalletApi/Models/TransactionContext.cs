using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace WalletApi.Models
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> Transactions { get; set; }
    }
}