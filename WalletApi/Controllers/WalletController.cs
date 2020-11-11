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
    ///<summary>
    ///A microservice for Wallets
    ///</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {

        private TransactionReadService _txReadService;
        private TransactionWriteService _txWriteService;
        private WalletWriteService _walletWriteService;
        public WalletController(ITransactionReadQueryHandler txReadService, ITransactionWriteCommandHandler txWriteService, IWalletWriteCommandHandler walletWriteService)
        {
            _txReadService = (TransactionReadService)txReadService;
            _txWriteService = (TransactionWriteService)txWriteService;
            _walletWriteService = (WalletWriteService)walletWriteService;
        }

        ///<summary>
        ///Creates a wallet and returns the wallet, the Id represents account/wallet number
        ///</summary>
        ///<returns><code>Wallet</code></returns>
        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet(CreateWalletCommand command)
        {
            return await _walletWriteService.CreateWallet(command);
        }

        ///<summary>
        ///This method is responsible for deposits and withdrawals on a wallet
        ///</summary>
        ///<remarks>
        ///Sending a command with a negetive value would reduce the balance (withdrawal) 
        ///while the wallet balance is greater than zero, 
        ///while a positive value would increase the balance (deposit)
        /// Sample withdrawal request:
        ///
        ///     POST /Todo
        ///     {
        ///        "value": -1000,
        ///        "timestamp": "1605037286186",
        ///        "WalletId": 1735613419
        ///     }
        ///The reason for this simple implementation is to make the 
        ///transaction object compatible with event sourcing events 
        ///that can be replayed to create the current state of a wallet
        /// by summing all event values
        ///</remarks>
        [HttpPost("CreateTransaction")]
        public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionOrderCommand command)
        {
            return await _txWriteService.CreateTransaction(command);
        }

        ///<summary>
        ///Gets first 50 transacctions on a wallet
        ///</summary>
        [HttpPost("GetTransactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(GetTransactionsQuery command)
        {
            return await _txReadService.GetTransactions(command);
        }
    }
}
