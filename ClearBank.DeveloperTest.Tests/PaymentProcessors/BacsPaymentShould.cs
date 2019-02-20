using ClearBank.DeveloperTest.PaymentProcessors;
using ClearBank.DeveloperTest.Types;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests
{
    [TestFixture]
    public class BacsPaymentShould
    {
        private BacsPaymentProcessor _bacsPayment;
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _bacsPayment = new BacsPaymentProcessor();
            _account = new Account();
        }

        [Test]
        public void ReturnTrue_WhenIsMatchCalledWithBacsScheme()
        {
            var result = _bacsPayment.IsMatch(PaymentScheme.Bacs);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountIsNull()
        {
            _account = null;

            var result = _bacsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnUnsuccessfulResult_WhenAccountHasChapsFlag()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            var result = _bacsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ReturnSuccessfulResult_WhenAccountIsValid()
        {
            _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            var result = _bacsPayment.Process(_account, It.IsAny<decimal>());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }
    }
}
