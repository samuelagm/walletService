using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletApi.Models;

namespace WalletsApi.Commands
{

    public interface IWalletWriteCommand
    {
        public long Timestamp { get; set; }
    }

    public interface IWalletWriteCommandHandler
    {
        public Task<ActionResult<Wallet>> CreateWallet(CreateWalletCommand command);
        public Task<ActionResult<Wallet>> FreezeWallet(FreezeWalletCommand command);
        public Task<ActionResult<Wallet>> DeleteWallet(DeleteWalletCommand command);
    }

    public class CreateWalletCommand : IWalletWriteCommand
    {
        public string Name { get; set; }
        public long Timestamp { get; set; }
        public string CustomerId { get; set; }
    }

    public class FreezeWalletCommand : IWalletWriteCommand
    {
        public long Timestamp { get; set; }
        public string WalletId { get; set; }
    }

    public class DeleteWalletCommand : IWalletWriteCommand
    {
        public string WalletId { get; set; }
        public long Timestamp { get; set; }
    }
}