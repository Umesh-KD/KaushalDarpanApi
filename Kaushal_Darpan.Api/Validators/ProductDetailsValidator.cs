using FluentValidation;
using Kaushal_Darpan.Api.Models;

namespace Kaushal_Darpan.Api.Validators
{
    public class ProductDetailsValidator : AbstractValidator<ProductDetailsCreateModel>
    {
        public ProductDetailsValidator()
        {
            RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Please enter Product Name!")
            .MinimumLength(5).WithMessage("Please enter minimum 5 charactors of Product Name!");
            RuleFor(x => x.ProductDescription).
                NotEmpty().WithMessage("Please enter Product Description!");
        }

    }

}
