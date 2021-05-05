using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public class AccountsDbContext : IAccountsDbContext
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountsDbContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Account GetAccount(int id)
        {
            return _dbContext.Accounts.First(a => a.AccountId == id);
        }


        public Customer GetCustomer(int id)
        {
            return _dbContext.Customers.First(c => c.CustomerId == id);
        }
    }
}
