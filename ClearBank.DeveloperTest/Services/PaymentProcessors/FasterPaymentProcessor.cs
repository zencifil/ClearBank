using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentProcessors
{
    public class FasterPaymentProcessor : IPaymentProcessor
    {
        public bool IsMatch(PaymentScheme paymentScheme)
        {
            return paymentScheme == PaymentScheme.FasterPayments;
        }

        public MakePaymentResult Process(Account account, decimal amount)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) || account.Balance < amount)
                return new MakePaymentResult { Success = false };

            return new MakePaymentResult { Success = true };
        }
    }
}
