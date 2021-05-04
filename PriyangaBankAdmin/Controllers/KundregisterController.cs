using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Services;
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
            var take = 15;
            var viewModel = new KundregisterIndexViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(0, take).Select(c=>new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            return View(viewModel);
        }

        public IActionResult GetCustomersFrom(int skip)
        {
            var viewModel = new KundregisterGetCustomersFromViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(skip, 15).Select(c => new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            return View(viewModel);
        }
        private int GetOwningCount(int customerId)
        {
            return _dbContext.GetAccountOwnerCount(customerId);
        }

        public IActionResult Kundbild(int customerId)
        {
            var client = _dbContext.GetById(customerId);
            var dt = new DateTime();
            var viewmodel = new KundregisterKundbildViewModel
            {
                Name = $"{client.Givenname} {client.Surname}",
                Birthdate = client.Birthday.ToString(),
                Telephone = client.Telephonenumber,
                Email = client.Emailaddress,
                NationalId = client.NationalId,
                StreetAddress = client.Streetaddress,
                Zip = client.Zipcode,
                City = client.City,
                Country = client.Country,
                Accounts = GetCustomerAccounts(customerId),
                NumberOfAccounts = GetCustomerAccounts(customerId).Count(),
                TotalBalance = GetCustomerAccounts(customerId).Sum(a=>a.Balance)
            };
            return View(viewmodel);
        }

        private IEnumerable<AccountItem> GetCustomerAccounts(int customerId)
        {
            return _dbContext.GetAllAccounts(customerId).Select(a => new AccountItem
            {
                TransferFrequency = a.Frequency,
                Balance = a.Balance
            });
        }
    }
}
