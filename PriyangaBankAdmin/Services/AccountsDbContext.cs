using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public IEnumerable<Account> GetAccounts(int customerId)
        {
            return _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Include(d => d.Account)
                .Select(a=>a.Account);
        }

        public Customer GetCustomer(int customerId)
        {
            return _dbContext.Customers.First(c => c.CustomerId == customerId);
        }

        public IEnumerable<Transaction> GetTransactions(int accountId)
        {
            return _dbContext.Transactions.Where(t => t.AccountId == accountId).OrderByDescending(d => d.Date);
        }
    }
}
