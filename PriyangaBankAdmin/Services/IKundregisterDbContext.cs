using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface IKundregisterDbContext
    {
        public Customer GetById(int id);
        public IEnumerable<Account> GetAllAccounts(int customerId);

    }
}
