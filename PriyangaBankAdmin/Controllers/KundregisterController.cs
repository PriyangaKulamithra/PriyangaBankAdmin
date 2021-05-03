using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Services;

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
            return View();
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
