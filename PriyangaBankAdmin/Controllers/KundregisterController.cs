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
        public IActionResult Index(int page = 1)
        {
            var pageSize = 50;
            var pageCount = CalculatePageCount(pageSize);
            var skip = CalculateHowManyCustomerstoSkip(page, pageSize);

            var viewModel = new KundregisterIndexViewModel();
            viewModel.AllCustomers = _dbContext.GetAllCustomers(skip, pageSize).Select(c=>new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            }).ToList();

            viewModel.TotalPageCount = CalculatePageCount(pageCount);
            viewModel.CurrentPage = page;
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

        public IActionResult Kundbild()
        {
            return View();
        }

        public IActionResult _CustomerDetails(string q)
        {
            var viewModel = new CustomerDetailsViewModel();
            if (int.TryParse(q, out int id))
            {
                var customer = _dbContext.GetById(id);
                if (customer == null)
                {
                    return View(viewModel);
                }

                viewModel.Id = customer.CustomerId;
                viewModel.Name = $"{customer.Givenname} {customer.Surname}";
                viewModel.Birthday = customer.Birthday.Value.ToString("yyyy-mm-dd");
                viewModel.NationalId = customer.NationalId;
                viewModel.Telephone = customer.Telephonenumber;
                viewModel.Telephonecountrycode = customer.Telephonecountrycode;
                viewModel.Email = customer.Emailaddress;
                viewModel.Address = customer.Streetaddress;
                viewModel.Zip = customer.Zipcode;
                viewModel.City = customer.City;
                viewModel.Country = customer.Country;
                viewModel.CountryCode = customer.CountryCode;
                viewModel.Gender = customer.Gender;
                viewModel.TotalBalanceOfAllAccounts = GetTotalBalanceOfAllAccounts(id);
                return View(viewModel);
            }
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

        private int CalculatePageCount(double pageCount)
        {
            return (int)Math.Ceiling(pageCount);
        }
        private double CalculatePageCount(int pageSize)
        {
            var totalCustomerCount = _dbContext.GetAllCustomers().Count();
            return (double)totalCustomerCount / pageSize;
        }

        private int CalculateHowManyCustomerstoSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }
        private decimal GetTotalBalanceOfAllAccounts(int customerId)
        {
            return _dbContext.GetTotalBalance(customerId);
        }
    }
}
