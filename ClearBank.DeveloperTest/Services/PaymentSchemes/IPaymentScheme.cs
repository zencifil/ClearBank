using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentSchemes
{
    public interface IPaymentScheme
    {
        bool IsMatch(PaymentScheme paymentScheme);
        MakePaymentResult Process(Account account, decimal amount);
    }
}
