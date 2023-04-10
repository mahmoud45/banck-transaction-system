using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bank_transaction_system.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly AppDbContext _context;
        public BankAccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountAsync(Customer customer)
        {
            var bankAccount = new BankAccount
            {
                CustomerId = customer.Id,
                CurrentBalance = 1000,
                CreationDate = DateTime.Now
            };

            await _context.BankAccount.AddAsync(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var bankAccount = await _context.BankAccount.Where(ba => ba.Id == id).FirstOrDefaultAsync();
            if(bankAccount != null)
                _context.BankAccount.RemoveRange(bankAccount);

            await _context.SaveChangesAsync();
        }

        public async Task<BankAccount> GetBankAccountAsync(int AccountId)
        {
            var account = await _context.BankAccount.Where(ba => ba.Id == AccountId).FirstOrDefaultAsync();
            return account ?? null;
        }

        public async Task<List<BankAccount>> GetCustomerBankAccountsAsync(int CustomerId)
        {
            var accounts = await _context.BankAccount.Where(ba => ba.CustomerId == CustomerId).ToListAsync();
            return accounts;
        }
    }
}