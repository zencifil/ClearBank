using ClearBank.DeveloperTest.Services.PaymentProcessors;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class FasterPaymentShould
    {
        private FasterPaymentProcessor _fasterPayment;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _fasterPayment = new FasterPaymentProcessor();
            _account = new Account();
        }

        [Test]
        public void ReturnTrue_WhenIsMatchCalledWithFasterPaymentScheme()
        {
            var result = _fasterPayment.IsMatch(PaymentScheme.FasterPayments);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountIsNull()
        {
            _account = null;

            var result = _fasterPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountHasBacsFlag()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _fasterPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountHasInsufficientFunds()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
            _account.Balance = 0M;

            var result = _fasterPayment.Process(_account, 10M);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnSuccessfulResult_WhenAccountIsValid()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;

            var result = _fasterPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }
    }
}
