using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Kundregister
{
    public class KundregisterIndexViewModel
    {
        
        public IEnumerable<CustomerItem> AllCustomers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
        public IEnumerable<int> DisplayPages;
    }

    public class CustomerItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string City { get; set; }
        public int NumberOfOwnAccounts { get; set; }
    }
}
