using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface ICustomerRepository
    {
        public IEnumerable<Account> GetAllAccounts(int customerId);
        public IEnumerable<Customer> GetAllCustomers(string q, int skip, int take);
        public IEnumerable<Customer> GetAllCustomers(string q);
        public int GetAccountOwnerCount(int customerId);
        public Customer GetById(int id);
        public decimal GetTotalBalance(int customerId);
    }
}
