using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentSchemes
{
    public class ChapsPayment : IPaymentScheme
    {
        public bool IsMatch(PaymentScheme paymentScheme)
        {
            return paymentScheme == PaymentScheme.Chaps;
        }

        public MakePaymentResult Process(Account account, decimal amount)
        {
            if (account == null || !account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) || account.Status != AccountStatus.Live)
                return new MakePaymentResult { Success = false };

            return new MakePaymentResult { Success = true };
        }
    }
}
