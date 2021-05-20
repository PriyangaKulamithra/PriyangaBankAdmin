using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PriyangaBankAdmin.Data;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Home.Konto;
using PriyangaBankAdmin.ViewModels.Konto;

namespace PriyangaBankAdmin.Controllers
{
    public class KontoController : Controller
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly ITransactionService _transactionService;

        public KontoController(IAccountsRepository accountsRepository, ITransactionService transactionService)
        {
            _accountsRepository = accountsRepository;
            _transactionService = transactionService;
        }
        public IActionResult Index(string accountId)
        {
            var viewmodel = new KontoIndexViewModel();
            if (int.TryParse(accountId, out int id)) viewmodel.AccountId = id;
            return View(viewmodel);
        }

        public IActionResult _GetAccountDetails(int accountId)
        {
            var viewmodel = new KontoGetAccountDetailsViewModel();

            if (IsRegisteredAccount(accountId))
            {
                viewmodel.AccountId = accountId;
                viewmodel.Balance = _accountsRepository.GetTransactions(accountId).First().Balance;

                viewmodel.Dispositions = _accountsRepository.GetDispositionsFromAccountId(accountId).Select(d =>
                    new KontoGetAccountDetailsViewModel.DispositionItem
                    {
                        CustomerId = d.CustomerId,
                        Name = $"{d.Customer.Givenname} {d.Customer.Surname}",
                        DispositionId = d.DispositionId,
                        DispositionType = d.Type
                    });

                viewmodel.Transactions = _accountsRepository.GetTransactions(accountId).Take(20).Select(t => new TransactionItem
                {
                    TransactionId = t.TransactionId,
                    TransactionDescription = t.Operation,
                    Date = t.Date,
                    Amount = t.Amount,
                    Balance = t.Balance
                });
            }

            return View(viewmodel);
        }

        private bool IsRegisteredAccount(int accountId)
        {
            return _accountsRepository.IsAccount(accountId);
        }

        public IActionResult GetTransactionsFrom(int skip, int accountId)
        {
            var viewmodel = new KontoGetTransactionsFromViewModel();
            viewmodel.Transactions = _accountsRepository.GetTransactions(accountId).Skip(skip).Take(20).Select(t =>
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

        public IActionResult NewTransaction(int accountId)
        {
            var viewmodel = new KontoNewTransactionViewModel {AccountId = accountId};
            viewmodel.AvailableBalance = _transactionService.GetAvailableBalance(accountId);
            viewmodel.Operations = GetOperationsSelectListItems();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult NewTransaction(KontoNewTransactionViewModel model)
        {
            if(model.Amount<1) ModelState.AddModelError("Amount", "För lågt belopp");
            if(model.Amount > model.AvailableBalance) ModelState.AddModelError("Amount","För högt belopp");
            if(model.AccountId == model.TransferToAccountNumber) ModelState.AddModelError("TransferToAccountNumber", "Välj ett annat kontonummer");
            if (model.SelectedOperationId == 3)
            {
                if(!_accountsRepository.IsAccount(model.TransferToAccountNumber)) ModelState.AddModelError("TransferToAccountNumber", "Ogiltigt kontonummer");
            }

            if (ModelState.IsValid)
            {
                switch (model.SelectedOperationId)
                {
                    case 1:
                        _transactionService.Withdrawal(model.AccountId, model.Amount);
                        break;
                    case 2:
                        _transactionService.Deposit(model.AccountId, model.Amount);
                        break;
                    case 3:
                        _transactionService.Transfer(model.AccountId, model.Amount, model.TransferToAccountNumber);
                        break;
                }

                return RedirectToAction("Index", "Konto", new {accountId = model.AccountId});
            }

            model.Operations = GetOperationsSelectListItems();
            return View(model);
        }

        private List<SelectListItem> GetOperationsSelectListItems()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem("---", ""));
            var index = 1;
            foreach (var item in _transactionService.GetOperations())
            {
                list.Add(new SelectListItem(item, index.ToString()));
                index++;
            }
            return list;
        }
    }
}
