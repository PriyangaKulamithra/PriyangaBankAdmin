using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Konton;
using PriyangaBankAdmin.ViewModels.Kundregister;

namespace PriyangaBankAdmin.Controllers
{
    public class KundregisterController : Controller
    {
        private readonly IKundregisterDbContext _dbContext;

        public KundregisterController(IKundregisterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var viewModel = new KundregisterIndexViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(0, 50).Select(c=>new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            }).ToList();
            viewModel.TotalPages = viewModel.AllCustomers.Count();
            return View(viewModel);
        }

        public IActionResult GetCustomersFrom(int skip)
        {
            var viewModel = new KundregisterGetCustomersFromViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(skip, 50).Select(c => new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            return View(viewModel);
        }

        private IEnumerable<AccountItem> GetCustomerAccounts(int customerId)
        {
            return _dbContext.GetAllAccounts(customerId).Select(a => new AccountItem
            {
                TransferFrequency = a.Frequency,
                Balance = a.Balance
            });
        }

        private int GetOwningCount(int customerId)
        {
            return _dbContext.GetAccountOwnerCount(customerId);
        }
    }
}
