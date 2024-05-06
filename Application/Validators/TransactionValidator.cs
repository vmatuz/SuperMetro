using Application.Command;
using FluentValidation;

namespace Application.Validators
{
    public class TransactionValidator :AbstractValidator<AddTransactionCommand>
    {
        public TransactionValidator() {
            RuleFor(x => x.Sum).NotNull().GreaterThan(0);
            RuleFor(x => x.Bank).NotNull().NotEmpty();
            RuleFor(x => x.DueDate).GreaterThan(DateTime.Now);
            RuleFor(x => x.Articles).NotEmpty().NotNull();
            RuleFor(x => x.Payments).NotEmpty().NotNull();
        }
    }
}
