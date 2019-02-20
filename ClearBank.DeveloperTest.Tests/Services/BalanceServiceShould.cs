using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class BalanceServiceShould
    {
        private BalanceService _balanceService;
        private Mock<IAccountDataStore> _accountDataStore;

        [SetUp]
        public void SetUp()
        {
            _accountDataStore = new Mock<IAccountDataStore>();
            _accountDataStore.Setup(ads => ads.UpdateAccount(It.IsAny<Account>()));

            _balanceService = new BalanceService(_accountDataStore.Object);
        }

        [Test]
        public void DeductAmountFromAccountBalance_WhenDeductBalanceIsCalled()
        {
            var account = new Account { Balance = 100M };

            _balanceService.DeductBalance(account, 20M);

            Assert.AreEqual(80M, account.Balance);
        }
    }
}
