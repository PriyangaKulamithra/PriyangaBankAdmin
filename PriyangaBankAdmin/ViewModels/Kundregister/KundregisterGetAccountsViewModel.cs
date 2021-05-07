using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.Controllers;

namespace PriyangaBankAdmin.ViewModels.Kundregister
{
    public class KundregisterGetAccountsViewModel
    {
        public IEnumerable<AccountItem> Accounts { get; set; }
        public decimal TotalBalanceOfAllAccounts { get; set; }
    }
}
