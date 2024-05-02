using Application.Command;
using FluentValidation;

namespace Application.Validators
{
    public class ArticleValidator : AbstractValidator<AddArticleCommand>
    {
        public ArticleValidator() {
            RuleFor(x => x.Name).NotNull().NotEmpty()
                  .WithMessage("The Name is required");

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0)
                .WithMessage("Please add a valid quantity");
        }
    }
}
