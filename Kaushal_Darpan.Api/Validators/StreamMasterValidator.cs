using FluentValidation;
using Kaushal_Darpan.Models.StreamMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class StreamMasterValidator : AbstractValidator<StreamMasterModel>
    {
        public StreamMasterValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please enter Branch Name!");
            RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Please enter Branch Code!");


            RuleFor(x => x.Qualifications)
            .NotEmpty().WithMessage("Please enter Qualifications!");


            RuleFor(x => x.StreamTypeID)
            .NotEmpty().WithMessage("Please select Stream Type!")
            .NotEqual(0).WithMessage("Please select Stream Type!");

            RuleFor(x => x.Duration)
            .NotEmpty().WithMessage("Please enter Duration!");


        }
    }
}
