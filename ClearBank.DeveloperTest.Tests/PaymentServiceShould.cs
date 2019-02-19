using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class PaymentServiceShould
    {
        private PaymentService _paymentService;
        private Mock<IAccountDataStore> _accountDataStore;
        private Mock<IBalanceService> _balanceService;
        private MakePaymentRequest _request;

        [SetUp]
        public void SetUp()
        {
            _accountDataStore = new Mock<IAccountDataStore>();
            _balanceService = new Mock<IBalanceService>();
            _balanceService.Setup(bs => bs.DeductBalance(It.IsAny<Account>(), It.IsAny<decimal>()));
            _request = new MakePaymentRequest();

            _paymentService = new PaymentService(_accountDataStore.Object, _balanceService.Object);
        }

        [Test]
        public void ReturnSuccessfulResult_WhenPaymentIsMade()
        {
            _request.PaymentScheme = PaymentScheme.Bacs;
            _request.Amount = 10M;
            var account = new Account { Balance = 100M, AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs };
            _accountDataStore.Setup(ads => ads.GetAccount(It.IsAny<string>())).Returns(account);

            var result = _paymentService.MakePayment(_request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenPaymentFails()
        {
            _request.PaymentScheme = PaymentScheme.Bacs;
            _request.Amount = 10M;
            var account = new Account { Balance = 100M, AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps };
            _accountDataStore.Setup(ads => ads.GetAccount(It.IsAny<string>())).Returns(account);

            var result = _paymentService.MakePayment(_request);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }
    }
}
