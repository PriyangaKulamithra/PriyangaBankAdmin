using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IAccountsDbContext
    {
        public Account GetAccount(int id);
        public Customer GetCustomer(int id);
    }
}
