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
                NumberOfUsers = _adminService.GetAllUsers().Count(),
                Users = _adminService.GetAllUsers().Select(u=>new AdminIndexViewModel.UserItem
                {
                    Id = u.Id,
                    Name = u.UserName,
                    Role = _adminService.GetRoleForUser(u.Id)
                }),
                Roles = _adminService.GetAllRoles().Select(r=>new AdminIndexViewModel.RoleItem
                {
                    Id = r.Id,
                    Type = r.Name
                })
            };
            return View(viewmodel);
        }
    }
}
