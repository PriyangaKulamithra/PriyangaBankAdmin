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
        private readonly IBankRepository _repository;

        public HomeController(ILogger<HomeController> logger, IBankRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                TotalCustomers = _repository.TotalCustomersCount(),
                TotalAccounts = _repository.TotalAccountsCount(),
                TotalBalance = _repository.TotalBalance()
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
