using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriyangaBankAdmin.Data;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Konto;
using PriyangaBankAdmin.ViewModels.Kundregister;

namespace PriyangaBankAdmin.Tests.Services
{

    [TestClass]
    public class TransactionServiceTests : BaseTest
    {
        private TransactionService sut;
        private Mock<IAccountsRepository> accountRepositoryMock;
        private Account account;

        public TransactionServiceTests()
        {
            sut = new TransactionService(dbContextInMemory);
            accountRepositoryMock = new Mock<IAccountsRepository>();

            account = fixture.Create<Account>();
            account.Balance = 100;
            dbContextInMemory.Accounts.Add(account);
            dbContextInMemory.SaveChanges();
        }

        [TestMethod]
        public void If_Insufficient_Money_In_Account_Then_Return_False()
        {
            Assert.IsFalse(sut.CheckIfSufficientBalance(account.AccountId, 101));
        }

        [TestMethod]
        public void If_sufficient_Money_In_Account_Then_Return_True()
        {
            Assert.IsTrue(sut.CheckIfSufficientBalance(account.AccountId, 99));
        }
    }
}
