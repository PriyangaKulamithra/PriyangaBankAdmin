using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriyangaBankAdmin.Controllers;
using PriyangaBankAdmin.Data;
using PriyangaBankAdmin.Services;
using PriyangaBankAdmin.ViewModels.Konto;

namespace PriyangaBankAdmin.Tests.Controllers
{
    [TestClass]
    public class KontoControllerTests : BaseTest
    {
        private KontoController sut;
        private Mock<IAccountsRepository> accountRepositoryMock;
        private Mock<ITransactionService> transactionServiceMock;

        public KontoControllerTests()
        {
            accountRepositoryMock = new Mock<IAccountsRepository>();
            transactionServiceMock = new Mock<ITransactionService>();
            sut = new KontoController(accountRepositoryMock.Object, transactionServiceMock.Object);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Withdrawal_Then_Withdrawal_Is_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 1;
            viewmodel.Amount = 10;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Withdrawal(viewmodel.AccountId, viewmodel.Amount), Times.Once);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Withdrawal_Then_Other_Operations_Are_Not_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 1;
            viewmodel.Amount = 10;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Deposit(viewmodel.AccountId, viewmodel.Amount), Times.Never);
            transactionServiceMock.Verify(t => t.Transfer(viewmodel.AccountId, viewmodel.Amount, viewmodel.TransferToAccountNumber), Times.Never);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Deposit_Then_Deposit_Is_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 2;
            viewmodel.Amount = 10;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Deposit(viewmodel.AccountId, viewmodel.Amount), Times.Once);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Deposit_Then_Other_Operations_Are_Not_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 2;
            viewmodel.Amount = 10;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Withdrawal(viewmodel.AccountId, viewmodel.Amount), Times.Never);
            transactionServiceMock.Verify(t => t.Transfer(viewmodel.AccountId, viewmodel.Amount, viewmodel.TransferToAccountNumber), Times.Never);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Transfer_Then_Transfer_Is_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 3;
            viewmodel.Amount = 10;
            viewmodel.TransferToAccountNumber = viewmodel.AccountId + 1;

            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            accountRepositoryMock.Setup(e => e.IsAccount(viewmodel.TransferToAccountNumber)).Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Transfer(viewmodel.AccountId, viewmodel.Amount, viewmodel.TransferToAccountNumber), Times.Once);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Transfer_Then_Other_Operations_Are_Not_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 3;
            viewmodel.Amount = 10;
            viewmodel.TransferToAccountNumber = viewmodel.AccountId + 1;

            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            accountRepositoryMock.Setup(e => e.IsAccount(viewmodel.TransferToAccountNumber)).Returns(true);
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Withdrawal(viewmodel.AccountId, viewmodel.Amount), Times.Never);
            transactionServiceMock.Verify(t => t.Deposit(viewmodel.AccountId, viewmodel.Amount), Times.Never);
        }

        [TestMethod]
        public void If_Insufficient_Balance_Withdrawal_Is_Not_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 1;

            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(false);
            transactionServiceMock.Setup(e => e.GetOperations()).Returns(fixture.Create<List<string>>());
            
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(e=>e.Withdrawal(viewmodel.AccountId, viewmodel.Amount), Times.Never);

        }

        [TestMethod]
        public void If_Insufficient_Balance_Transfer_Is_Not_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 3;
            viewmodel.TransferToAccountNumber = viewmodel.AccountId + 1;

            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(false);
            transactionServiceMock.Setup(e => e.GetOperations()).Returns(fixture.Create<List<string>>());

            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(e => e.Transfer(viewmodel.AccountId, viewmodel.Amount, viewmodel.TransferToAccountNumber), Times.Never);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Deposit_And_Amount_Is_Negative_Then_Deposit_Is_Never_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 2;
            viewmodel.Amount = -1;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            transactionServiceMock.Setup(e => e.GetOperations()).Returns(fixture.Create<List<string>>());
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Deposit(viewmodel.AccountId, viewmodel.Amount), Times.Never);
        }

        [TestMethod]
        public void If_New_Transaction_And_OperationId_Is_Equal_To_Withdrawal_And_Amount_Is_Negative_Then_Deposit_Is_Never_Called()
        {
            var viewmodel = fixture.Create<KontoNewTransactionViewModel>();
            viewmodel.SelectedOperationId = 1;
            viewmodel.Amount = -1;


            transactionServiceMock.Setup(e => e.CheckIfSufficientBalance(viewmodel.AccountId, viewmodel.Amount))
                .Returns(true);
            transactionServiceMock.Setup(e => e.GetOperations()).Returns(fixture.Create<List<string>>());
            sut.NewTransaction(viewmodel);

            transactionServiceMock.Verify(t => t.Withdrawal(viewmodel.AccountId, viewmodel.Amount), Times.Never);
        }
    }
}