using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WalletApi.Models;
using WalletApi.Services;
using WalletsApi.Commands;
using WalletsApi.Queries;

namespace WalletApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {

        private TransactionReadService _txReadService;
        private TransactionWriteService _txWriteService;
        private WalletWriteService _walletWriteService;
        public WalletController(WalletContext walletContext, TransactionContext transactionContext)
        {
            _txReadService = new TransactionReadService(transactionContext);
            _txWriteService = new TransactionWriteService(transactionContext, walletContext);
            _walletWriteService = new WalletWriteService(walletContext);

        }

        // POST: api/CreateWallet
        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet(CreateWalletCommand command)
        {
            return await _walletWriteService.CreateWallet(command);
        }

        // POST: api/CreateTransaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionOrderCommand command)
        {
            return await _txWriteService.CreateTransaction(command);
        }

        // POST: api/GetTransactions
        [HttpPost]
        public async Task<ActionResult<ICollection<Transaction>>> GetTransactions(GetTransactionsQuery command)
        {
            return await _txReadService.GetTransactions(command);
        }

        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }
    }
}
