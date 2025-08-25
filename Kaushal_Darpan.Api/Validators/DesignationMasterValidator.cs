using FluentValidation;
using Kaushal_Darpan.Models.DesignationMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class DesignationMasterValidator : AbstractValidator<DesignationMasterModel>
    {
        public DesignationMasterValidator()
        {
            RuleFor(x => x.DesignationNameEnglish)
                .NotEmpty().WithMessage("Please enter Designation Name English!");
            RuleFor(x => x.DesignationNameHindi)
                .NotEmpty().WithMessage("Please enter Designation Name Hindi!");
            RuleFor(x => x.DesignationNameShort)
                .NotEmpty().WithMessage("Please enter Designation Name Short!");
        }
    }
}
