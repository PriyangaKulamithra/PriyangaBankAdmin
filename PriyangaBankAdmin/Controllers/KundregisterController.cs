using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;
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
            var viewModel = new KundregisterIndexViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(0, 50).Select(c=>new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            viewModel.TotalPages = viewModel.AllCustomers.Count();
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
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            return View(viewModel);
        }
        private int GetOwningCount(int customerId)
        {
            return _dbContext.GetAccountOwnerCount(customerId);
        }

        public IActionResult Customer(int customerId)
        {
            //var customer = _dbContext.GetBySearchWord(q);
            var customer = _dbContext.GetById(customerId);

            var viewmodel = new KundregisterCustomerViewModel
            {
                Name = $"{customer.Givenname} {customer.Surname}",
                Birthdate = customer.Birthday.Value.ToString("yyyy-MM-dd"),
                Telephone = customer.Telephonenumber,
                Email = customer.Emailaddress,
                NationalId = customer.NationalId,
                StreetAddress = customer.Streetaddress,
                Zip = customer.Zipcode,
                City = customer.City,
                Country = customer.Country
                //Accounts = GetCustomerAccounts(customer.CustomerId),
                //NumberOfAccounts = GetCustomerAccounts(customer.CustomerId).Count(),
                //TotalBalance = GetCustomerAccounts(customer.CustomerId).Sum(a => a.Balance)
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
