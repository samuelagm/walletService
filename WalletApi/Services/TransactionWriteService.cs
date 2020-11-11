using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;
using WalletsApi.Commands;

namespace WalletApi.Services
{
    public class TransactionWriteService : ITransactionWriteCommandHandler
    {
        TransactionContext _txContext;
        WalletContext _walletContext;
        public TransactionWriteService(TransactionContext context, WalletContext walletContext)
        {
            _txContext = context;
            _walletContext = walletContext;
        }
        public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionOrderCommand command)
        {
            try
            {
                var wallet = await _walletContext.Wallets.Where(x => x.Id == command.WalletId).FirstOrDefaultAsync();

                if (wallet == null)
                {
                    return null;
                }

                if ((wallet.Balance + command.Value) < 0)
                {
                    return null;
                }

                wallet.Balance += command.Value;
                var transactionDoc = new Transaction
                {
                    WalletId = command.WalletId,
                    Type = (command.Value > 0) ? TransactionType.Deposit : TransactionType.Withdrawal,
                    WalletBalance = wallet.Balance,
                    Amount = Math.Abs(command.Value),
                    CreatedAt = DateTime.Now,
                    Timestamp = command.Timestamp
                };
                _txContext.Transactions.Add(transactionDoc);
                await _txContext.SaveChangesAsync();

                _walletContext.Entry(wallet).State = EntityState.Modified;
                await _walletContext.SaveChangesAsync();

                return transactionDoc;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }

        }


    }
}