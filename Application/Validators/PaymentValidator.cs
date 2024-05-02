using Domain.Entities;
using FluentValidation;

namespace Application.Validators
{
    public class PaymentValidator :AbstractValidator<Payment>
    {
        public PaymentValidator() {
            RuleFor(x => x.CustomerId).NotNull();
            RuleFor(x => x.PaymentDate).NotNull();
        }
    }
}
