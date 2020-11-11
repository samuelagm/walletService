using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;
using WalletsApi.Queries;

namespace WalletApi.Services
{
    public class TransactionReadService : ITransactionReadQueryHandler
    {
        private TransactionContext _context;
        public TransactionReadService(TransactionContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(GetTransactionsQuery query)
        {
            return await _context.Transactions.Where(x => x.WalletId == query.WalletId).Take(50).ToListAsync();

        }

        public Task<ActionResult<IEnumerable<Transaction>>> GetPaginatedTransactions(GetPaginatedTransactionsQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<Transaction>>> GetTransactiomsByDateRange(GetTransactiomsByDateRangeQuery query)
        {
            throw new NotImplementedException();
        }

    }
}