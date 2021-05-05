using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Konton;

namespace PriyangaBankAdmin.Controllers
{
    public class KontonController : Controller
    {
        private readonly IAccountsDbContext _dbContext;

        public KontonController(IAccountsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
            var cust = _dbContext.GetCustomer(id);
            var viewModel = new KontonIndexViewModel
            {
                Id = cust.CustomerId,
                Name = $"{cust.Givenname} {cust.Surname}",
                BirthDate = cust.Birthday.Value.ToString("yyyy-MM-dd")
            };
            return View(viewModel);
        }
    }
}
