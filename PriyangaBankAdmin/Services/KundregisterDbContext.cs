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
            return _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId && d.Type == "OWNER")
                .Include(a=>a.Account).Select(s=>s.Account);
        }

        public IEnumerable<Customer> GetAllCustomers(string q, int skip, int take)
        {
            return _dbContext.Customers
                .Where(c=> (q== null) || c.Givenname.Contains(q) || c.Surname.Contains(q) || c.City.Contains(q))
                .Skip(skip)
                .Take(take);
        }

        public IEnumerable<Customer> GetAllCustomers(string q)
        {
            return _dbContext.Customers.Where(c=>(q==null) || c.Givenname.Contains(q) || c.Surname.Contains(q) || c.City.Contains(q));
        }

        public int GetAccountOwnerCount(int customerId)
        {
            return _dbContext.Dispositions.Count(d => d.CustomerId == customerId && d.Type == "Owner");
        }

        public Customer GetById(int id)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public decimal GetTotalBalance(int customerId)
        {

            return GetAllAccounts(customerId).Sum(s => s.Balance);
        }

        //public Customer GetBySearchWord(string q)
        //{
        //    if (Int32.TryParse(q, out int id)) return GetById(id);
        //    return _dbContext.Customers.FirstOrDefault(c => c.Givenname.Contains(q) || c.Surname.Contains(q));
        //}

    }
}
