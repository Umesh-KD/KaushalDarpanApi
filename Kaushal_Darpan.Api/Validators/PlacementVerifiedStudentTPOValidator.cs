using FluentValidation;
using Kaushal_Darpan.Models.PlacementVerifiedStudentTPOMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class PlacementVerifiedStudentTPOValidator : AbstractValidator<PlacementVerifiedStudentTPOSearchModel>
    {
        public PlacementVerifiedStudentTPOValidator()
        {
            RuleFor(x => x.AgeFrom)
                .LessThanOrEqualTo(x => x.AgeTo).WithMessage("Please enter valid Age range!");
        }
    }
}
