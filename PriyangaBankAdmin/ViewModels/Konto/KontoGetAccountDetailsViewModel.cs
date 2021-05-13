using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriyangaBankAdmin.ViewModels.Home.Konto
{
    public class KontoGetAccountDetailsViewModel
    {
        public IEnumerable<TransactionItem> Transactions { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
    }

    public class TransactionItem
    {
        public int TransactionId { get; set; }
        public string TransactionDescription { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        
    }
}
