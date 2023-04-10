using bank_transaction_system.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace bank_transaction_system.Controllers
{
    public class BankAccount : Controller
    {
        private readonly IBankAccountService _service;
        private readonly ICustomersService _customerService;

        public BankAccount(IBankAccountService service, ICustomersService customerService)
        {
            _service = service;
            _customerService = customerService;
        }
        
        [HttpPost, Route("customers/{Name}/BankAccounts/AddAccount")]
        public async Task<JsonResult> AddAccount(string Name)
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                var customer = await _customerService.GetCustomerByNameAsync(Name);
                await _service.AddAccountAsync(customer);
                var accounts = await _service.GetCustomerBankAccountsAsync(customer.Id);

                return Json(accounts);
            }
            return Json(new {error = "You must login."});
        }

        [HttpPost, Route("customers/{Name}/BankAccounts/{accountId}/Delete")]
        public async Task<JsonResult> DeleteAccount(int accountId)
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                var account = await _service.GetBankAccountAsync(accountId);
                var customer = await _customerService.GetCustomerByNameAsync(HttpContext.Session.GetString("username"));
                
                if(customer.Id != account.CustomerId)
                {
                    return Json(new {error = "You are unauthoraized to delete this account."});
                }

                await _service.DeleteAccountAsync(accountId);
                var accounts = await _service.GetCustomerBankAccountsAsync(customer.Id);

                return Json(accounts);
            }
            return Json(new {error = "login first."});
        }
    }
}