using ClearBank.DeveloperTest.Types;
using FluentValidation;

namespace ClearBank.DeveloperTest.Validators
{
    public class MakePaymentRequestValidator : AbstractValidator<MakePaymentRequest>
    {
        public MakePaymentRequestValidator()
        {
            RuleFor(r => r).NotNull();
            RuleFor(r => r.DebtorAccountNumber).NotEmpty();
            RuleFor(r => r.CreditorAccountNumber).NotEmpty();
            RuleFor(r => r.Amount).GreaterThan(0M);
        }
    }
}
