using ClearBank.DeveloperTest.Types;
using FluentValidation;

namespace ClearBank.DeveloperTest.Validators
{
    public class MakePaymentRequestValidator : AbstractValidator<MakePaymentRequest>
    {
        public MakePaymentRequestValidator()
        {
            RuleFor(r => r.DebtorAccountNumber).NotEmpty().WithMessage("Debtor account number cannot be empty");
            RuleFor(r => r.CreditorAccountNumber).NotEmpty().WithMessage("Creditor account number cannot be empty");
            RuleFor(r => r.Amount).GreaterThan(0M).WithMessage("Amount cannot be less than or equal to zero.");
        }
    }
}
