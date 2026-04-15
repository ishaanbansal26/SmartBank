using Microsoft.AspNetCore.Mvc;
using SmartBank.MVC.Services;
using SmartBank.MVC.ViewModel;

namespace SmartBank.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountServices _accountService;

        public AccountsController(AccountServices accountService)
        {
            _accountService = accountService;
        }

        // VIEW ACCOUNTS
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWT");
            //gets the JWt TOKEN from session

            var accounts = await _accountService.GetAccounts(token);
            // calls the get accounts in the account service

            var result = new List<AccountWithTransaction>();

            foreach (var acc in accounts)
            {
                var transactions = await _accountService.GetTransactions(acc.Id, token);
                //for each account it calls the get transactions of that account

                result.Add(new AccountWithTransaction
                {
                    Account = acc,
                    Transactions = transactions
                });
                //wraps each account and its transactions with the accountwithtransaction viewmodel
            }

            return View(result);
        }

        // CREATE ACCOUNT
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString("JWT");

            await _accountService.CreateAccount(token);

            return RedirectToAction("Index");
        }

        // DELETE ACCOUNT
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWT");

            await _accountService.DeleteAccount(id, token);

            return RedirectToAction("Index");
        }

        // DEPOSIT
        [HttpPost]
        public async Task<IActionResult> Deposit(int accountId, decimal amount)
        {
            var token = HttpContext.Session.GetString("JWT");

            await _accountService.Deposit(accountId, amount, token);

            return RedirectToAction("Index");
        }

        // WITHDRAW
        [HttpPost]
        public async Task<IActionResult> Withdraw(int accountId, decimal amount)
        {
            var token = HttpContext.Session.GetString("JWT");

            await _accountService.Withdraw(accountId, amount, token);

            return RedirectToAction("Index");
        }
    }
}