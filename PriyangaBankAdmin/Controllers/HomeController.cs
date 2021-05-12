using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriyangaBankAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Services;

namespace PriyangaBankAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBankDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IBankDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                TotalCustomers = _dbContext.TotalCustomersCount(),
                TotalAccounts = _dbContext.TotalAccountsCount(),
                TotalBalance = _dbContext.TotalBalance()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
