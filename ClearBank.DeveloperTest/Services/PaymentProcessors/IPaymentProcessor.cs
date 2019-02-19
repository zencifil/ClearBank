using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services.PaymentProcessors
{
    public interface IPaymentProcessor
    {
        bool IsMatch(PaymentScheme paymentScheme);
        MakePaymentResult Process(Account account, decimal amount);
    }
}
