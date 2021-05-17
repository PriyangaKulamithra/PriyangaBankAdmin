using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Kundregister
{
    public class KundregisterGetCustomersFromViewModel
    {
        public IEnumerable<CustomerItem> AllCustomers { get; set; }
        public string Q { get; set; }
        public int TotalItems { get; set; }
    }
}
