using System;
using System.Threading.Tasks;
using bank_transaction_system.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using bank_transaction_system.Models;

namespace bank_transaction_system.Controllers
{
    public class BankTransactionController : Controller
    {
        private readonly IBankTransactionService _BankTransactionService;
        private readonly IBankAccountService _BankAccountService;
        private readonly ICustomersService _CustomerService;
        public BankTransactionController(IBankTransactionService BankTransactionService, IBankAccountService BankAccountService, ICustomersService CustomerService)
        {
            _BankTransactionService = BankTransactionService;
            _BankAccountService = BankAccountService;
            _CustomerService = CustomerService;
        }

        [Route("customers/{Name}/BankAccounts/{accountId}/transactions")]
        public async Task<IActionResult> Transactions(int accountId)
        {
            var account = await _BankAccountService.GetBankAccountAsync(accountId);
            if(account == null)
                TempData["error"] = "Account does not exist.";

            var transactions = await _BankTransactionService.GetAccountTransactionsAsync(accountId);
            
            if(transactions.Any() == false)
                TempData["error"] = "No transactions for this account.";

            return View(transactions);
        }

        [Route("customers/{Name}/BankTransactions/{transactionId}/cancel-transaction")]
        public async Task<JsonResult> CancelTransaction(int transactionId, string accountId)
        {
            var username = HttpContext.Session.GetString("username");

            var customer = await _CustomerService.GetCustomerByNameAsync(username);
            var account = await _BankAccountService.GetBankAccountAsync(Int32.Parse(accountId));

            if(account.CustomerId != customer.Id)
            {
                return Json(new {fail = "You are not authorized to cancel this transaction as you are not the owner of the account which the transaction commited from."});
            }

            var cancellation = await _BankTransactionService.CancelTransaction(transactionId);

            return Json(new {success = cancellation});
        }

        [Route("customers/{Name}/BankAccounts/{id}/make-transaction")]
        public IActionResult MakeTransaction(int id)
        {
            if(HttpContext.Session.GetString("username") == null)
                return RedirectToAction("Index", "Home");
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        [Route("customers/{Name}/BankAccounts/{Id}/make-transaction")]
        public async Task<JsonResult> MakeTransaction([FromBody]BankTransaction bankTransaction)
        {
            var transactionResult = await _BankTransactionService.MakeTransaction(Int32.Parse(bankTransaction.FromAccountId)
            , Int32.Parse(bankTransaction.ToAccountId), bankTransaction.Amount);


            return Json( new {success = transactionResult});
        }
    }
}