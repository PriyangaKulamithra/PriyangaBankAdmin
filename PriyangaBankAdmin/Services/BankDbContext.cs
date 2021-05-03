using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public class BankDbContext : IBankDbContext
    {
        private readonly ApplicationDbContext _dbContext;

        public BankDbContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int TotalCustomersCount()
        {
            return _dbContext.Customers.Count();
        }

        public int TotalAccountsCount()
        {
            return _dbContext.Accounts.Count();
        }

        public decimal TotalBalance()
        {
            return _dbContext.Accounts.Sum(a => a.Balance);
        }

        public Customer GetCustomer(string givenName)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
        }
    }
}
