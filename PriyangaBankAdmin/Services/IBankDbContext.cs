using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IBankDbContext
    {
        public int TotalCustomersCount();
        public int TotalAccountsCount();
        public decimal TotalBalance();
        public Customer GetCustomer(string givenName);
        

    }
}
