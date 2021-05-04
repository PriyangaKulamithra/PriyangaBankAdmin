using System.Collections.Generic;

namespace PriyangaBankAdmin.Controllers
{
    public class KundregisterCustomerViewModel
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Birthdate { get; set; }
        public string NationalId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int NumberOfAccounts { get; set; }
        public decimal TotalBalance { get; set; }

        public IEnumerable<AccountItem> Accounts { get; set; }
    }
}