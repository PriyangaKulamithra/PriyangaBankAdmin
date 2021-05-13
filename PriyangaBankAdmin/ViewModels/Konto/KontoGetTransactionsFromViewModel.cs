using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriyangaBankAdmin.ViewModels.Home.Konto;

namespace PriyangaBankAdmin.ViewModels.Konto
{
    public class KontoGetTransactionsFromViewModel
    {
        public IEnumerable<TransactionItem> Transactions { get; set; }

    }
}
