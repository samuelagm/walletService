using Microsoft.EntityFrameworkCore;
using WalletApi.Models;

namespace WalletApi.Tests
{

    public class TestBase
    {
        protected TestBase(DbContextOptions<WalletContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<WalletContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new WalletContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();



                // context.SaveChanges();
            }
        }
    }
}
