using FluentValidation;

namespace EShop.Catalog.Application.Categories.Commands.AddCategory
{
    public class AddCategoryValidation : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidation()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
