using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using bank_transaction_system.Data.ViewModels;
using bank_transaction_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace bank_transaction_system.Data.Services
{
    public interface ICustomersService
    {
        Task AddCustomerAsync(CustomerVM customerVM);
        Task<Customer> GetCustomerByNameAsync(string name);
        Task<Customer> GetCustomerAsync(LoginVM loginVM);
    }
}