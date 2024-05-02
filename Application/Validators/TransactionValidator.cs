using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class TransactionValidator :AbstractValidator<Transaction>
    {
        public TransactionValidator() {
            RuleFor(x => x.Sum).NotNull().GreaterThan(0);
            RuleFor(x => x.Bank).NotNull().NotEmpty();
            RuleFor(x => x.DueDate).GreaterThan(DateTime.Now);
        }
    }
}
