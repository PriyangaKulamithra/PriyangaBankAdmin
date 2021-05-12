using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Kundregister
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Birthday { get; set; }
        public string NationalId { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public decimal TotalBalanceOfAllAccounts { get; set; }
        public int NumberOfAccounts { get; set; }
    }
}
