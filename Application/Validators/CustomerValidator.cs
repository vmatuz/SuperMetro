using Application.Command;
using Domain.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Application.Validators
{
    public class CustomerValidator : AbstractValidator<AddCustomerCommand>
    {
        public CustomerValidator()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty()
                .WithMessage("The Name is required");                
            RuleFor(x => x.City).NotEmpty()
                .WithMessage("The City is required");

            RuleFor(x => x.Country).NotNull().NotEmpty()
                .WithMessage("The Country is required");

            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.CreditCard).CreditCard();

        }
    }
}
