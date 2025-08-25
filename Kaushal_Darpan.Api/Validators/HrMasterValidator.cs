using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.HrMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class HrMasterValidator : AbstractValidator<HRMaster>
    {
        public HrMasterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter Name !");

            RuleFor(x => x.MobileNo)
             .NotEmpty().WithMessage("Please enter Mobile Number!")
             .Matches(Constants.RGX_MOBILE_NO).WithMessage("Please enter Valid Mobile Number!");

            RuleFor(x => x.EmailId)
           .NotEmpty().WithMessage("Please enter Email!")
           .EmailAddress().WithMessage("Please enter Valid Email!");


            RuleFor(x => x.PlacementCompanyID)
           .NotEmpty().WithMessage("Please select Company!")
           .NotEqual(0).WithMessage("Please select Company!");


        }
    }
}
