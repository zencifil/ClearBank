using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentProcessors
{
    public class BacsPaymentProcessor : IPaymentProcessor
    {
        public bool IsMatch(PaymentScheme paymentScheme)
        {
            return paymentScheme == PaymentScheme.Bacs;
        }

        public MakePaymentResult Process(Account account, decimal amount)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                return new MakePaymentResult { Success = false };

            return new MakePaymentResult { Success = true };
        }
    }
}
