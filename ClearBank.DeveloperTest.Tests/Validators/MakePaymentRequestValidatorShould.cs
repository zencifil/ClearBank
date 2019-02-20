using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestFixture]
    public class MakePaymentRequestValidatorShould
    {
        private MakePaymentRequestValidator _validator;
        private const string DEBTOR_ACCOUNT_NUMBER = "TEST DEBTOR ACCOUNT NUMBER";
        private const string CREDITOR_ACCOUNT_NUMBER = "TEST CREDITOR ACCOUNT NUMBER";

        [SetUp]
        public void SetUp()
        {
            _validator = new MakePaymentRequestValidator();
        }

        [Test]
        public void ReturnNoError_WhenValidateIsCalled_WithValidRequest()
        {
            var request = new MakePaymentRequest
            {
                Amount = 20M,
                DebtorAccountNumber = DEBTOR_ACCOUNT_NUMBER,
                CreditorAccountNumber = CREDITOR_ACCOUNT_NUMBER,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs
            };

            var result = _validator.Validate(request);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Errors);
        }

        [Test]
        public void ReturnError_WhenValidateIsCalled_WithInvalidRequest()
        {
            var request = new MakePaymentRequest
            {
                Amount = -20M,
                DebtorAccountNumber = DEBTOR_ACCOUNT_NUMBER,
                CreditorAccountNumber = CREDITOR_ACCOUNT_NUMBER,
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs
            };

            var result = _validator.Validate(request);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.Errors);
        }
    }
}
