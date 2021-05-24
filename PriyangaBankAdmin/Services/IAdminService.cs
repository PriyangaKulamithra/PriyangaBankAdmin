using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IAdminService
    {
        public IEnumerable<IdentityUser> GetAllUsers();
        public IEnumerable<IdentityRole> GetAllRoles();
        public string GetRoleForUser(string id);
    }
}
