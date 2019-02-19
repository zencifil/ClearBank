using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.PaymentSchemes;
using ClearBank.DeveloperTest.Types;
using FluentValidation;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IValidator _validator;
        private readonly IAccountDataStore _accountDataStore;
        private readonly IBalanceService _balanceService;
        private readonly List<IPaymentScheme> _paymentSchemes;

        public PaymentService(IValidator validator,
            IAccountDataStore accountDataStore,
            IBalanceService balanceService)
        {
            _validator = validator;
            _accountDataStore = accountDataStore;
            _balanceService = balanceService;

            _paymentSchemes = new List<IPaymentScheme>
            {
                new BacsPayment(),
                new ChapsPayment(),
                new FasterPayment()
            };
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (validationResult.Errors.Any())
                return new MakePaymentResult { Success = false };

            var account = _accountDataStore.GetAccount(request.DebtorAccountNumber);
            var result = GetPaymentScheme(request).Process(account, request.Amount);

            if (result.Success)
                _balanceService.DeductBalance(account, request.Amount);

            return result;
        }

        private IPaymentScheme GetPaymentScheme(MakePaymentRequest request)
        {
            return _paymentSchemes.Single(scheme => scheme.IsMatch(request.PaymentScheme));
        }
    }
}
