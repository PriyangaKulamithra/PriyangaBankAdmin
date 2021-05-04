using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Kundregister
{
    public class KundregisterIndexViewModel
    {
        public IEnumerable<CustomerItem> AllCustomers { get; set; }
    }

    public class CustomerItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public int NumberOfOwnAccounts { get; set; }
    }
}
