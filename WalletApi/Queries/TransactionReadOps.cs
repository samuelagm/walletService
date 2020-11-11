using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Models;

namespace WalletsApi.Queries
{
    public interface ITransactionReadQuery
    {
        public string WalletId { get; set; }
    }

    public interface ITransactionReadQueryHandler
    {
        public Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(GetTransactionsQuery query);
        public Task<ActionResult<IEnumerable<Transaction>>> GetPaginatedTransactions(GetPaginatedTransactionsQuery query);
        public Task<ActionResult<IEnumerable<Transaction>>> GetTransactiomsByDateRange(GetTransactiomsByDateRangeQuery query);
    }


    public class GetTransactionsQuery : ITransactionReadQuery
    {
        public string WalletId { get; set; }
    }

    public class GetPaginatedTransactionsQuery : ITransactionReadQuery
    {

        public int PageLength { get; set; }
        public int Index { get; set; }
        public string WalletId { get; set; }
    }

    public class GetTransactiomsByDateRangeQuery : ITransactionReadQuery
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string WalletId { get; set; }
    }
}