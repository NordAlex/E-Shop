using FluentValidation;

namespace EShop.Catalog.Application.Items.Commands.CreateItem
{
    public class CreateItemValidation : AbstractValidator<CreateItemCommand>
    {
        public CreateItemValidation()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.CategoryId)
                .GreaterThanOrEqualTo(0)
                .NotEmpty();

            RuleFor(v => v.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Amount)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
