using System.Collections.Generic;
using System.Threading.Tasks;
using bank_transaction_system.Models;

namespace bank_transaction_system.Data.Services
{
    public interface IBankTransactionService
    {
        Task<string> MakeTransaction(int fromId, int toId, long amount);
        Task<IEnumerable<BankTransaction>> GetAccountTransactionsAsync(int accountId);
        Task<string> CancelTransaction(int TransactionId);
    }
}