using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriyangaBankAdmin.Services;

namespace PriyangaBankAdmin.Tests.Services
{
    [TestClass]
    class TransactionServiceTests
    {


        private TransactionService sut;

        public TransactionServiceTests()
        {
            sut = new TransactionService();
        }
        [TestMethod]
        public void Fail_to_withdrawal_money_when_insufficient_balance()
        {

        }

        [TestMethod]
        public void Fail_to_transfer_money_to_other_account_when_insufficient_balance()
        {

        }

        [TestMethod]
        public void Fail_to_deposit_amount_less_than_1()
        {

        }

        [TestMethod]
        public void Fail_to_withdraw_amount_less_than_1()
        {

        }

        [TestMethod]
        public void Correct_Transaction_Blablabla()
        {

        }
    }
}
