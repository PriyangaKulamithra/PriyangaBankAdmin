using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public class KundregisterDbContext : IKundregisterDbContext
    {
        private readonly ApplicationDbContext _dbContext;

        public KundregisterDbContext(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Account> GetAllAccounts(int customerId)
        {
            return _dbContext.Accounts.Where(a => a.AccountId == customerId).ToList();
        }

        public IEnumerable<Customer> GetAllCustomers(int skip, int take)
        {
            return _dbContext.Customers.Skip(skip).Take(take);
        }

        public int GetAccountOwnerCount(int customerId)
        {
            return _dbContext.Dispositions.Count(d => d.CustomerId == customerId && d.Type == "Owner");
        }

        public Customer GetById(int id)
        {
            return _dbContext.Customers.First(c => c.CustomerId == id);
        }

        //public Customer GetBySearchWord(string q)
        //{
        //    if (Int32.TryParse(q, out int id)) return GetById(id);
        //    return _dbContext.Customers.FirstOrDefault(c => c.Givenname.Contains(q) || c.Surname.Contains(q));
        //}

    }
}
