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
                .NotEmpty()
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("CategoryId must be greater than 0");
        }

    }
}
