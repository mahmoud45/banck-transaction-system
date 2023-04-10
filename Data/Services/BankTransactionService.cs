using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank_transaction_system.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data.Services
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly AppDbContext _context;
        public BankTransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CancelTransaction(int TransactionId)
        {
            var transaction = await _context.BankTransaction.Where(t => t.Id == TransactionId).FirstOrDefaultAsync();

            if(transaction != null)
            {
                var totalHours = DateTime.Now.Subtract(transaction.CreationDate).TotalHours;
                if(totalHours > 24)
                    return "Transaction cannot be cancelled as more than 24 hours has based.";
                
                var fromAccount = await _context.BankAccount.Where(ba => ba.Id == Int32.Parse(transaction.FromAccountId)).FirstOrDefaultAsync()??null;
                var toAccount = await _context.BankAccount.Where(ba => ba.Id == Int32.Parse(transaction.ToAccountId)).FirstOrDefaultAsync()??null;
            
                if(fromAccount == null || toAccount == null)
                    return "One or more of the accounts does not exist.";

                fromAccount.CurrentBalance += transaction.Amount;
                toAccount.CurrentBalance -= transaction.Amount;

                _context.BankTransaction.RemoveRange(transaction);

                await _context.SaveChangesAsync();

                return "Transaction has been cancelled successfully.";
            }
            return "Transaction does not exist.";
        }

        public async Task<IEnumerable<BankTransaction>> GetAccountTransactionsAsync(int accountId)
        {
            var transactions = await _context.BankTransaction.Where(bt => bt.FromAccountId.Equals(accountId.ToString())).ToListAsync();
            return transactions;
        }

        public async Task<string> MakeTransaction(int fromId, int toId, long amount)
        {
            var fromAccount = await _context.BankAccount.Where(ba => ba.Id == fromId).FirstOrDefaultAsync();
            var toAccount = await _context.BankAccount.Where(ba => ba.Id == toId).FirstOrDefaultAsync();

            if(fromAccount == null || toAccount == null)
            {
                return "Account does not exist.";
            }

            else if(amount > fromAccount.CurrentBalance)
                return "The current balance is less than amount.";

            else if (amount <= 0)
                return "Invalid amount";
            
            fromAccount.CurrentBalance -= amount;
            toAccount.CurrentBalance += amount;

            BankTransaction tranasaction = new BankTransaction
            {
                FromAccountId = fromAccount.Id.ToString(),
                ToAccountId = toAccount.Id.ToString(),
                Amount = amount,
                CreationDate = DateTime.Now
            };

            await _context.BankTransaction.AddAsync(tranasaction);

            await _context.SaveChangesAsync();

            return "Transaction done successfully.";
        }
    }
}