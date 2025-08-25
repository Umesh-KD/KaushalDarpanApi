using FluentValidation;
using Kaushal_Darpan.Models.PlacementStudentMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class PlacementStudentValidator : AbstractValidator<PlacementStudentSearchModel>
    {
        public PlacementStudentValidator()
        {
            RuleFor(x => x.AgeFrom)
                .LessThanOrEqualTo(x => x.AgeTo).WithMessage("Enter valid Age range");
        }
    }
}
