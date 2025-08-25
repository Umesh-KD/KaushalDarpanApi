using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CollegeMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class CollegeMasterValidator : AbstractValidator<CollegeMasterModel>
    {
        public CollegeMasterValidator()
        {
            //RuleFor(x => x.SSOID)
            //.NotEmpty().WithMessage("Please enter SSOID!");

            RuleFor(x => x.InstitutionCategoryID)
            .NotEmpty().WithMessage("Please select Institution Category!")
            .NotEqual(0).WithMessage("Please select Institution Category!");

            RuleFor(x => x.InstitutionManagementTypeID)
            .NotEmpty().WithMessage("Please select Management Type!")
            .NotEqual(0).WithMessage("Please select Management Type!");

            //RuleFor(x => x.InstituteNameEnglish)
            //.NotEmpty().WithMessage("Please enter Institute Name English!");

            //RuleFor(x => x.InstituteNameHindi)
            //.NotEmpty().WithMessage("Please enter Institute Name Hindi!");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Please enter Email!")
            .EmailAddress().WithMessage("Please enter Valid Email!");

            //RuleFor(x => x.MobileNumber)
            //.NotEmpty().WithMessage("Please enter Mobile Number!")
            //.Matches(Constants.RGX_MOBILE_NO).WithMessage("Please enter Valid Mobile Number!");

            RuleFor(x => x.InstituteCode)
            .NotEmpty().WithMessage("Please enter Institute Code!");


            //RuleFor(x => x.InstitutionDGTCode)
            //.NotEmpty().WithMessage("Please enter Institute DGT Code!");

            //RuleFor(x => x.FaxNumber)
            //.NotEmpty().WithMessage("Please enter Fax Number!")
            //.Matches(Constants.RGX_FAX_NO).WithMessage("Please enter Valid Fax Number!");

            //RuleFor(x => x.Website)
            //.NotEmpty().WithMessage("Please enter Website!")
            //.Matches(Constants.RGX_WEBSITE).WithMessage("Please enter Valid Website!");

            //RuleFor(x => x.LandNumber)
            //.NotEmpty().WithMessage("Please enter Land Line Number!")
            //.Matches(Constants.RGX_LANDLINE_STD_NO).WithMessage("Please enter Valid Land Line Number!");

            RuleFor(x => x.DivisionID)
            .NotEmpty().WithMessage("Please select Division!")
            .NotEqual(0).WithMessage("Please select Division!");

            RuleFor(x => x.DistrictID)
            .NotEmpty().WithMessage("Please select District!")
            .NotEqual(0).WithMessage("Please select District!");

            RuleFor(x => x.TehsilID)
            .NotEmpty().WithMessage("Please select Tehsil!")
            .NotEqual(0).WithMessage("Please select Tehsil!");

            RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Please enter Address!");

            //RuleFor(x => x.PinCode)
            //.NotEmpty().WithMessage("Please enter Pin code!")
            //.Matches(Constants.RGX_PINCODE).WithMessage("Please enter Valid Pin code!");
        }
    }
}
