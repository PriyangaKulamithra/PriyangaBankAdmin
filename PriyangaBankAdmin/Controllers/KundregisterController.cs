using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JW;
using Microsoft.AspNetCore.Authorization;
using PriyangaBankAdmin.Data;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Kundregister;

namespace PriyangaBankAdmin.Controllers
{
    [Authorize(Roles = "Admin, Cashier")]
    public class KundregisterController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public KundregisterController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IActionResult Index(string q, int page = 1)
        {
            var totalItems = _customerRepository.GetAllCustomers(q).Count();
            var pageSize = 50;
            var pager = new Pager(totalItems, page, pageSize, 9);
            var skip = CalculateHowManyCustomersToSkip(page, pageSize);

            var viewModel = new KundregisterIndexViewModel
            {
                Q = q,
                TotalPageCount = pager.TotalPages,
                DisplayPages = pager.Pages,
                CurrentPage = page,
                TotalCustomerCount = totalItems
            };
            viewModel.AllCustomers = _customerRepository.GetAllCustomers(q, skip, pageSize).Select(c => new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            }).ToList();

            return View(viewModel);
        }

        public IActionResult GetCustomersFrom(string q, int totalItems, int skip)
        {
            var viewModel = new KundregisterGetCustomersFromViewModel { Q = q, TotalItems = totalItems };
            viewModel.AllCustomers = _customerRepository.GetAllCustomers(q, skip, 50).Select(c => new CustomerItem
            {
                Id = c.CustomerId,
                Name = $"{c.Givenname} {c.Surname}",
                NationalId = c.NationalId,
                City = c.City,
                NumberOfOwnAccounts = GetOwningCount(c.CustomerId)
            });
            return View(viewModel);
        }

        public IActionResult Kundbild(string id)
        {
            var viewmodel = new KundregisterKundbildViewModel();
            if (int.TryParse(id, out int convertedId)) viewmodel.Id = convertedId;
            return View(viewmodel);
        }

        public IActionResult _CustomerDetails(string q)
        {
            var viewModel = new CustomerDetailsViewModel();
            if (int.TryParse(q, out int id))
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null) return View(viewModel);

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
                viewModel.NumberOfAccounts = GetNumberOfAccounts(id);
                return View(viewModel);
            }
            return View(viewModel);
        }

        public IActionResult _GetAccounts(int customerId)
        {
            var viewModel = new KundregisterGetAccountsViewModel
            {
                Accounts = GetCustomerAccounts(customerId),

            };
            viewModel.TotalBalanceOfAllAccounts = GetTotalBalanceOfAllAccounts(customerId);
            return View(viewModel);
        }

        private int GetNumberOfAccounts(int customerId)
        {
            return _customerRepository.GetAccountOwnerCount(customerId);
        }

        private IEnumerable<AccountItem> GetCustomerAccounts(int customerId)
        {
            return _customerRepository.GetAllAccounts(customerId).Select(a => new AccountItem
            {
                AccountId = a.AccountId,
                Frequency = a.Frequency,
                Created = a.Created,
                Balance = a.Balance,
                IsOwner = a.Dispositions.FirstOrDefault(d => d.Type == "OWNER" && d.CustomerId == customerId) == null ? false : true
            });
        }

        private int GetOwningCount(int customerId)
        {
            return _customerRepository.GetAccountOwnerCount(customerId);
        }

        private int CalculateHowManyCustomersToSkip(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

        private decimal GetTotalBalanceOfAllAccounts(int customerId)
        {
            return _customerRepository.GetTotalBalance(customerId);
        }
    }
}
