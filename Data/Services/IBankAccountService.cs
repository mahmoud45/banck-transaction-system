using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using bank_transaction_system.Data.ViewModels;
using bank_transaction_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace bank_transaction_system.Data.Services
{
    public interface IBankAccountService
    {
        Task AddAccountAsync(Customer customer);
        Task DeleteAccountAsync(int id);
        Task<List<BankAccount>> GetCustomerBankAccountsAsync(int CustomerId);
        Task<BankAccount> GetBankAccountAsync(int AccountId);
    }
}