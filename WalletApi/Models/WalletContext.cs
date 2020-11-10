using Microsoft.EntityFrameworkCore;

namespace WalletApi.Models
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
    }
}