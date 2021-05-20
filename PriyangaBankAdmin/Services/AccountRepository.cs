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
    public class AccountRepository : IAccountsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
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

        public IEnumerable<Disposition> GetDispositionsFromAccountId(int accountId)
        {
            return _dbContext.Dispositions.Include(d => d.Customer).Where(di => di.AccountId == accountId);
        }


        public IEnumerable<Transaction> GetTransactions(int accountId)
        {
            return _dbContext.Transactions.Where(t => t.AccountId == accountId).OrderByDescending(d => d.Date);
        }

        public bool IsAccount(int AccountId)
        {
            return _dbContext.Accounts.FirstOrDefault(a => a.AccountId == AccountId) == null ? false : true;
        }
    }
}
