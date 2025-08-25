using FluentValidation;
using Kaushal_Darpan.Models.UserMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class UserMasterValidation : AbstractValidator<UserMasterModel>
    {
        public UserMasterValidation()
        {
            //       RuleFor(x => x.SSOID)
            //       .NotEmpty().WithMessage("Please enter SSOID!");

            //       RuleFor(x => x.AadhaarID)
            //       .NotEmpty().WithMessage("Please enter Aadhar Number!");
            //       //.Matches(Constants.RGX_AADHAR_NO).WithMessage("Please enter Valid Aadhar Number!");

            //       RuleFor(x => x.LevelID)
            //       .NotEmpty().WithMessage("Please select Level!")
            //       .NotEqual(0).WithMessage("Please select Level!");

            //       RuleFor(x => x.DesignationID)
            //      .NotEmpty().WithMessage("Please select Designation!")
            //      .NotEqual(0).WithMessage("Please select Designation!");




            //       RuleFor(x => x.Email)
            //       .NotEmpty().WithMessage("Please enter Email!")
            //       .EmailAddress().WithMessage("Please enter Valid Email!");

            //       RuleFor(x => x.MobileNo)
            //.NotEmpty().WithMessage("Please enter Mobile Number!")
            //.Matches(Constants.RGX_MOBILE_NO).WithMessage("Please enter Valid Mobile Number!");




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

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Please select Gender!");


            RuleFor(x => x.State)
            .NotEmpty().WithMessage("Please select State!");


        }
    }
}
