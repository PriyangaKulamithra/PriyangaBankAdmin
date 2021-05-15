using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Home.Konto;
using PriyangaBankAdmin.ViewModels.Konto;

namespace PriyangaBankAdmin.Controllers
{
    public class KontoController : Controller
    {
        private readonly IAccountsDbContext _dbContext;

        public KontoController(IAccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string accountId)
        {
            var viewmodel = new KontoIndexViewModel();
            if (int.TryParse(accountId, out int id)) viewmodel.AccountId = id;
            return View(viewmodel);
        }

        public IActionResult _GetAccountDetails(int accountId)
        {
            var viewmodel = new KontoGetAccountDetailsViewModel
            {
                AccountId = accountId,
                Balance = _dbContext.GetTransactions(accountId).First().Balance,
                Dispositions = _dbContext.GetDispositionsFromAccountId(accountId).Select(d=>new KontoGetAccountDetailsViewModel.DispositionItem
                {
                    CustomerId = d.CustomerId,
                    DispositionId = d.DispositionId,
                    DispositionType = d.Type
                })
            };
            viewmodel.Transactions = _dbContext.GetTransactions(accountId).Take(20).Select(t=>new TransactionItem
            {
                TransactionId = t.TransactionId,
                TransactionDescription = t.Operation,
                Date = t.Date,
                Amount = t.Amount,
                Balance = t.Balance
            });
            return View(viewmodel);
        }

        public IActionResult GetTransactionsFrom(int skip, int accountId)
        {
            var viewmodel = new KontoGetTransactionsFromViewModel();
            viewmodel.Transactions = _dbContext.GetTransactions(accountId).Skip(skip).Take(20).Select(t =>
                new TransactionItem
                {
                    TransactionId = t.TransactionId,
                    TransactionDescription = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    Balance = t.Balance
                });
            return View(viewmodel);
        }
    }
}
