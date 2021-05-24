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

        public IEnumerable<UserItem> Admins{ get; set; }
        public IEnumerable<UserItem> Cashiers { get; set; }

        public class UserItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
