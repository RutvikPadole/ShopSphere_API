using FluentValidation;
using ShopSphere_API.DTOs;

namespace ShopSphere_API.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required");

            RuleFor(x => x.Price)
                .NotEmpty
                .with
        }

    }
}
