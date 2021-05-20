using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriyangaBankAdmin.Data;

namespace PriyangaBankAdmin.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Withdrawal(int accountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Deposit(int accountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Transfer(int accountId, decimal amount, int transferToAccountId)
        {
            throw new NotImplementedException();
        }

        public decimal GetAvailableBalance(int accountId)
        {
            return _dbContext.Accounts.First(a => a.AccountId == accountId).Balance;   
        }

        public Account GetAccount(int accountId)
        {
            return _dbContext.Accounts.First(a => a.AccountId == accountId);
        }

        public List<string> GetOperations()
        {
            return new List<string> {"Withdrawal in Cash", "Deposit in Cash", "Transfer to other account"};
        }
    }
}
