using ClearBank.DeveloperTest.PaymentProcessors;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class ChapsPaymentShould
    {
        private ChapsPaymentProcessor _chapsPayment;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _chapsPayment = new ChapsPaymentProcessor();
            _account = new Account();
        }

        [Test]
        public void ReturnTrue_WhenIsMatchCalledWithChapsScheme()
        {
            var result = _chapsPayment.IsMatch(PaymentScheme.Chaps);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountIsNull()
        {
            _account = null;

            var result = _chapsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountHasBacsFlag()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _chapsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountIsNotLive()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Disabled;

            var result = _chapsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnSuccessfulResult_WhenAccountIsValid()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _account.Status = AccountStatus.Live;

            var result = _chapsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }
    }
}
