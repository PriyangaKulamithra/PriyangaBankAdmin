using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IAccountsRepository
    {
        public IEnumerable<Account> GetAccounts(int customerId);
        public Customer GetCustomer(int customerId);
        public IEnumerable<Disposition> GetDispositionsFromAccountId(int accountId);
        public IEnumerable<Transaction> GetTransactions(int accountId);
        public bool IsAccount(int AccountId);
    }
}
