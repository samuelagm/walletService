using System;
using Xunit;
using WalletApi.Controllers;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;
using WalletApi.Services;
using WalletsApi.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletsApi.Queries;

namespace WalletApi.Tests
{
    public class WalletControllerTest : TestBase
    {
        WalletController walletController;
        private TransactionReadService _txReadService;
        private TransactionWriteService _txWriteService;
        private WalletWriteService _walletWriteService;
        public WalletControllerTest()
        : base(
            new DbContextOptionsBuilder<WalletContext>()
            .UseInMemoryDatabase("WalletsDBTest")
            .Options, new DbContextOptionsBuilder<TransactionContext>()
            .UseInMemoryDatabase("WalletsDBTest")
            .Options)
        {
            _txReadService = new TransactionReadService(new TransactionContext(TransactionContextOptions));
            _txWriteService = new TransactionWriteService(new TransactionContext(TransactionContextOptions), new WalletContext(WalletContextOptions));
            _walletWriteService = new WalletWriteService(new WalletContext(WalletContextOptions));
            walletController = new WalletController(_txReadService, _txWriteService, _walletWriteService);
        }

        [Fact]
        public async Task CanCreateWalletAsync()
        {
            CreateWalletCommand command = new CreateWalletCommand
            {
                Name = "default",
                Timestamp = 1605037286186,
                CustomerId = "asd1212112qx"
            };
            Assert.NotNull((await walletController.CreateWallet(command)).Value);
        }
        [Fact]
        public async Task CanCreateTransactionAsync()
        {
            CreateTransactionOrderCommand command = new CreateTransactionOrderCommand
            {
                Value = 3000,
                Timestamp = 1605037286186,
                WalletId = "1735613419",
            };
            Assert.NotNull((await walletController.CreateTransaction(command)).Value);
        }


        [Fact]
        public async Task NoWithdrawalFromEmptyAccount()
        {
            CreateTransactionOrderCommand command = new CreateTransactionOrderCommand
            {
                Value = -50000,
                Timestamp = 1605037286186,
                WalletId = "1735613419",
            };
            Assert.Null((await walletController.CreateTransaction(command)));
        }

        [Fact]
        public async Task CanGetTransactionsAsync()
        {
            GetTransactionsQuery query = new GetTransactionsQuery
            {
                WalletId = "1735613459"
            };

            Assert.NotEmpty((await walletController.GetTransactions(query)).Value);

        }
    }
}
