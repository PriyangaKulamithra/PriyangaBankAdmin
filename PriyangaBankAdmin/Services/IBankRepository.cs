using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IBankRepository
    {
        public int TotalCustomersCount();
        public int TotalAccountsCount();
        public decimal TotalBalance();
        public Customer GetCustomer(string givenName);

        public Customer GetCustomer(int id);
    }
}
