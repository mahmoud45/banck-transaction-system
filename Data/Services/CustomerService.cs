using System;
using System.Linq;
using System.Threading.Tasks;
using bank_transaction_system.Data.ViewModels;
using bank_transaction_system.Models;
using Microsoft.EntityFrameworkCore;

namespace bank_transaction_system.Data.Services
{
    public class CustomerService : ICustomersService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCustomerAsync(CustomerVM customerVM)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(customerVM.Password);

            var customer = new Customer
            {
                Name = customerVM.Name,
                Address = customerVM.Address,
                Mobile = customerVM.Mobile,
                Password = password
            };

            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            Customer customer = await _context.Customer.Where(c => c.Name.Equals(name)).FirstOrDefaultAsync();
            return customer;
        }

        public async Task<Customer> GetCustomerAsync(LoginVM loginVM)
        {
            Customer customer = await _context.Customer.Where(c => c.Name.Equals(loginVM.Name)).FirstOrDefaultAsync();
            bool verified = BCrypt.Net.BCrypt.Verify(loginVM.Password, customer.Password);

            return verified ? customer : null;
        }
    }
}