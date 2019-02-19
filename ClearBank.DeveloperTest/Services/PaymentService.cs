using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.PaymentSchemes;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore _accountDataStore;
        private readonly List<IPaymentScheme> _paymentSchemes;

        public PaymentService(IAccountDataStore accountDataStore)
        {
            _accountDataStore = accountDataStore;

            _paymentSchemes = new List<IPaymentScheme>
            {
                new BacsPayment(),
                new ChapsPayment(),
                new FasterPayment()
            };
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            var result = GetPaymentScheme(request).Process(account, request.Amount);

            if (result.Success)
            {
                account.Balance -= request.Amount;
                _accountDataStore.UpdateAccount(account);
            }

            return result;
        }

        private IPaymentScheme GetPaymentScheme(MakePaymentRequest request)
        {
            return _paymentSchemes.Single(scheme => scheme.IsMatch(request.PaymentScheme));
        }
    }
}
