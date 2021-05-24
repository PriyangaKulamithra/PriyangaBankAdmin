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
        public IEnumerable<IdentityUser> GetAllAdmins();
        public IEnumerable<IdentityUser> GetAllCashiers();
        public string GetRoleForUser(string id);
        public int NumberOfEmployees();
    }
}
