
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletApi.Models;
using WalletsApi.Commands;

namespace WalletApi.Services
{
    ///<summary>
    ///Handles all write commands on a wallet
    ///</summary>
    public class WalletWriteService : IWalletWriteCommandHandler
    {
        private readonly WalletContext _context;
        private readonly Random random = new Random();

        public WalletWriteService(WalletContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Wallet>> CreateWallet(CreateWalletCommand command)
        {
            var wallet = new Wallet
            {
                Id = GenerateAccountNumber(),
                CustomerId = command.CustomerId,
                Name = command.Name,
                Balance = 0,
                Deleted = false,
                CreatedAt = DateTime.Now,
                Timestamp = command.Timestamp
            };

            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }
        public async Task<ActionResult<Wallet>> DeleteWallet(DeleteWalletCommand command)
        {
            var wallet = await _context.Wallets.FindAsync(command.WalletId);

            if (wallet == null)
            {
                return null;
            }
            wallet.Deleted = true;
            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(command.WalletId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return wallet;
        }

        public async Task<ActionResult<Wallet>> FreezeWallet(FreezeWalletCommand command)
        {
            var wallet = await _context.Wallets.FindAsync(command.WalletId);

            if (wallet == null)
            {
                return null;
            }
            wallet.Frozen = true;
            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(command.WalletId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return wallet;
        }

        ///<summary>
        ///A naive account number/walletId generator
        ///</summary>
        private string GenerateAccountNumber()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                output.Append(random.Next(0, 10));
            }
            return output.ToString();
        }
        private bool WalletExists(string walletId)
        {
            return _context.Wallets.Any(e => e.Id == walletId);
        }
    }

}