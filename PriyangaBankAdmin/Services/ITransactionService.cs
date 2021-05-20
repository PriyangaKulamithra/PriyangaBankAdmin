using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public interface ITransactionService
    {
        public void Withdrawal(int accountId, decimal amount);
        public void Deposit(int accountId, decimal amount);
        public void Transfer(int accountId, decimal amount, int transferToAccountId);
        public decimal GetAvailableBalance(int accountId);
        public Account GetAccount(int accountId);
        public List<string> GetOperations();
    }
}
