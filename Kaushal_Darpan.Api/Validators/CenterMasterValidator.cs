using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CenterMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class CenterMasterValidator : AbstractValidator<CenterMasterModel>
    {
        public CenterMasterValidator()
        {

            RuleFor(x => x.CenterName)
           .NotEmpty().WithMessage("Please enter Center Name!");



            RuleFor(x => x.SSOID)
            .NotEmpty().WithMessage("Please enter SSOID!");


            //RuleFor(x => x.MobileNumber)
            //.NotEmpty().WithMessage("Please enter Mobile Number!")
            //.Matches(Constants.RGX_MOBILE_NO).WithMessage("Please enter Valid Mobile Number!");

            RuleFor(x => x.CenterCode)
            .NotEmpty().WithMessage("Please enter Center Code!");


        
            RuleFor(x => x.DivisionID)
            .NotEmpty().WithMessage("Please select Division!")
            .NotEqual(0).WithMessage("Please select Division!");

            RuleFor(x => x.DistrictID)
            .NotEmpty().WithMessage("Please select District!")
            .NotEqual(0).WithMessage("Please select District!");

            RuleFor(x => x.TehsilID)
            .NotEmpty().WithMessage("Please select Tehsil!")
            .NotEqual(0).WithMessage("Please select Tehsil!");

            //RuleFor(x => x.Address)
            //.NotEmpty().WithMessage("Please enter Address!");

            //RuleFor(x => x.PinCode)
            //.NotEmpty().WithMessage("Please enter Pin code!")
            //.Matches(Constants.RGX_PINCODE).WithMessage("Please enter Valid Pin code!");
        }
    }
}
