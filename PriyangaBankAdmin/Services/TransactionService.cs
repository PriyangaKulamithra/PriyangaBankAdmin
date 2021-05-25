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
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Withdrawal in Cash",
                Balance = account.Balance - amount,
                Amount = -amount
            };
            account.Balance -= amount;
            account.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public void Deposit(int accountId, decimal amount)
        {
            if(CheckIfSufficientBalance(accountId, amount))
            {
                var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
                var transaction = new Transaction
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    Type = "Credit",
                    Operation = "Credit in Cash",
                    Balance = account.Balance + amount,
                    Amount = amount
                };
                account.Balance += amount;
                account.Transactions.Add(transaction);
                _dbContext.SaveChanges();
            }
        }

        public void Transfer(int fromAccountId, decimal amount, int transferToAccountId)
        {
            var fromAccount = _dbContext.Accounts.First(a => a.AccountId == fromAccountId);
            var toAccount = _dbContext.Accounts.First(a => a.AccountId == transferToAccountId);
            var transactionFrom = new Transaction
            {
                AccountId = fromAccountId,
                Date = DateTime.Now,
                Type = "Debit",
                Operation = "Transfer to Another account",
                Balance = fromAccount.Balance - amount,
                Amount = -amount,
                Account = transferToAccountId.ToString()
            };
            var transactionTo = new Transaction
            {
                AccountId = transferToAccountId,
                Date = DateTime.Now,
                Type = "Credit",
                Operation = "Collection from Another account",
                Balance = toAccount.Balance + amount,
                Amount = amount,
                Account = fromAccountId.ToString()
            };
            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            fromAccount.Transactions.Add(transactionFrom);
            toAccount.Transactions.Add(transactionTo);
            _dbContext.SaveChanges();
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

        public bool CheckIfSufficientBalance(int accountId, decimal amount)
        {
            var account = _dbContext.Accounts.First(a => a.AccountId == accountId);
            return account.Balance >= amount;
        }
    }
}
