using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Admin
{
    public class AdminIndexViewModel
    {
        public int NumberOfUsers { get; set; }
        public IEnumerable<RoleItem> Roles { get; set; }

        public class RoleItem
        {
            public string Id { get; set; }
            public string Type { get; set; }
        }

        public IEnumerable<UserItem> Users{ get; set; }

        public class UserItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
        }
    }
}
