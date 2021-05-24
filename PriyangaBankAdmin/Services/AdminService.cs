using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public string GetRoleForUser(string id)
        {
            var userRole =_dbContext.UserRoles.FirstOrDefault(u => u.UserId == id);
            if (userRole == null) return null;
            return _dbContext.Roles.First(r => r.Id == userRole.RoleId).Name;
        }
    }
}
