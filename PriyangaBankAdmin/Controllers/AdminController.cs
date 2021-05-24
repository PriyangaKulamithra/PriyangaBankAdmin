using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Admin;

namespace PriyangaBankAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            var viewmodel = new AdminIndexViewModel
            {
                Admins = _adminService.GetAllAdmins().Select(a=>new AdminIndexViewModel.UserItem
                {
                    Id = a.Id,
                    Name = a.UserName,
                    Email = a.Email
                }),
                Cashiers = _adminService.GetAllCashiers().Select(a=>new AdminIndexViewModel.UserItem
                {
                    Id = a.Id,
                    Name = a.UserName,
                    Email = a.Email
                }),
                NumberOfUsers = _adminService.NumberOfEmployees()
            };
            return View(viewmodel);
        }
    }
}
