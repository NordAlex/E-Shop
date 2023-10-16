using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace EShop.Carting.Application.CartItems.Commands.AddCartItem
{
    public class AddCartItemValidation : AbstractValidator<AddCartItemCommand>
    {
        public AddCartItemValidation()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
