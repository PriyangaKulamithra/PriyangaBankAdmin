using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Customer GetById(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c=>c.CustomerId == id);
        }

        public IEnumerable<Account> GetAllAccounts(int customerId)
        {
            return _dbContext.Accounts.Where(a => a.AccountId == customerId);
        }
    }
}
