using System.Threading.Tasks;
using bank_transaction_system.Data.Services;
using bank_transaction_system.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


using System;


namespace bank_transaction_system.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomersService _CustomerService;
        private readonly IBankAccountService _BankAccountService;
        public CustomersController(ICustomersService Customerservice, IBankAccountService BankAccountService)
        {
            _CustomerService = Customerservice;
            _BankAccountService = BankAccountService;
        }

        public IActionResult Register()
        {
            if(HttpContext.Session.GetString("username") != null)
                return RedirectToAction("Index");
            return View(new CustomerVM());
        }

        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("username") != null)
                return RedirectToAction("Index");

            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }
            
            var customer = await _CustomerService.GetCustomerAsync(loginVM);
            
            if(customer != null)
            {
                HttpContext.Session.SetString("username", loginVM.Name);
                
                return RedirectToAction("Index", new { Name = loginVM.Name});
            }
            
            TempData["Error"] = "Invalide username or password";
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerVM customerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(customerVM);
            }
            var check = await _CustomerService.GetCustomerByNameAsync(customerVM.Name);
            if(check != null)
            {
                ModelState.AddModelError(String.Empty, "Name already exists");
                return View(customerVM);
            }
                
            await _CustomerService.AddCustomerAsync(customerVM);

            HttpContext.Session.SetString("username", customerVM.Name);
            return RedirectToAction("Index", new { Name = customerVM.Name});
        }

        [Route("customers/{Name}/BankAccounts")]
        public async Task<IActionResult> Index(string Name)
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                var customer = await _CustomerService.GetCustomerByNameAsync(Name);

                var customerBankAccounts = await _BankAccountService.GetCustomerBankAccountsAsync(customer.Id);
            
                return View("Index", customerBankAccounts);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SignOut(string Name)
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                HttpContext.Session.Remove("username");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}