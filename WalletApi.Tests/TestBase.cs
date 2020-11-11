using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;

namespace WalletApi.Tests
{

    public class TestBase
    {

        protected TestBase(DbContextOptions<WalletContext> walletContextOptions,
     DbContextOptions<TransactionContext> transactionContextOptions)
        {
            WalletContextOptions = walletContextOptions;
            TransactionContextOptions = transactionContextOptions;
            Hydrate();
        }
        protected DbContextOptions<WalletContext> WalletContextOptions { get; }
        protected DbContextOptions<TransactionContext> TransactionContextOptions { get; }


        private void Hydrate()
        {
            using (var context = new WalletContext(WalletContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var wallet = new Wallet
                {
                    Id = "1435623429",
                    CustomerId = "ac11r21d554",
                    Name = "default",
                    Balance = 0,
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    Timestamp = 1605032522017
                };

                var wallet2 = new Wallet
                {
                    Id = "1735613459",
                    CustomerId = "pl989123hj1",
                    Name = "default",
                    Balance = 5000,
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    Timestamp = 1605032617254
                };

                var wallet3 = new Wallet
                {
                    Id = "1735613419",
                    CustomerId = "qx9102311a1",
                    Name = "default",
                    Balance = 10000,
                    Deleted = false,
                    CreatedAt = DateTime.Now,
                    Timestamp = 1605032760952
                };

                context.AddRange(wallet, wallet2, wallet3);
                context.SaveChanges();
            }

            using (var context = new TransactionContext(TransactionContextOptions))
            {
                var transaction = new Transaction
                {
                    WalletId = "1735613459",
                    Type = TransactionType.Deposit,
                    WalletBalance = 5000,
                    Amount = 5000,
                    CreatedAt = DateTime.Now,
                    Timestamp = 1605033203009
                };

                context.AddRange(transaction);
                context.SaveChanges();
            }
        }


    }
}
