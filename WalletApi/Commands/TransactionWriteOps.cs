using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Models;

namespace WalletsApi.Commands
{

    public interface ITransactionWriteCommand
    {
        public System.Guid Id { get; }
        public string WalletId { get; set; }
    }

    public interface ITransactionWriteCommandHandler
    {
        public Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionOrderCommand command);
    }

    public class CreateTransactionOrderCommand : ITransactionWriteCommand
    {
        public CreateTransactionOrderCommand()
        {
            Id = System.Guid.NewGuid();
        }
        public System.Guid Id { get; }
        public long Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Timestamp { get; set; }
        public string WalletId { get; set; }
    }

}