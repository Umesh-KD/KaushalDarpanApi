using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.StaffMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class AddStaffMasterValidator : AbstractValidator<StaffMasterModel>
    {
        public AddStaffMasterValidator()
        {
            RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Please enter Name !");

            RuleFor(x => x.SSOID)
          .NotEmpty().WithMessage("Please enter SSOID !");

            RuleFor(x => x.Address)
          .NotEmpty().WithMessage("Please enter Address !");

           RuleFor(x => x.Pincode)
          .NotEmpty().WithMessage("Please enter Pincode !");

           RuleFor(x => x.StaffStatus)
          .NotEmpty().WithMessage("Please Select staff status  !");

           RuleFor(x => x.AnnualSalary)
          .NotEmpty().WithMessage("Please enter AnnualSalary  !");

           RuleFor(x => x.Experience)
          .NotEmpty().WithMessage("Please Select staff Experience  !");




           RuleFor(x => x.MobileNumber)
          .NotEmpty().WithMessage("Please enter Mobile Number!")
          .Matches(Constants.RGX_MOBILE_NO).WithMessage("Please enter Valid Mobile Number!");

          RuleFor(x => x.AdharCardNumber)
         .NotEmpty().WithMessage("Please enter AdharCardNumber!")
         .Matches(Constants.RGX_AADHAR_NO).WithMessage("Please enter Valid AdharCardNumber!");

          RuleFor(x => x.PanCardNumber)
         .NotEmpty().WithMessage("Please enter PanCardNumber!")
         .Matches(Constants.RGX_PAN_CARD).WithMessage("Please enter Valid PanCardNumber!");




            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Please enter Email!")
           .EmailAddress().WithMessage("Please enter Valid Email!");


            RuleFor(x => x.StaffTypeID)
           .NotEmpty().WithMessage("Please select Company!")
           .NotEqual(0).WithMessage("Please select Company!");


            RuleFor(x => x.RoleID)
           .NotEmpty().WithMessage("Please select Role!")
           .NotEqual(0).WithMessage("Please select Role!");



            RuleFor(x => x.DesignationID)
           .NotEmpty().WithMessage("Please select Designation!")
           .NotEqual(0).WithMessage("Please select Designation");



            RuleFor(x => x.StateID)
           .NotEmpty().WithMessage("Please select state!")
           .NotEqual(0).WithMessage("Please select state!");


            RuleFor(x => x.SubjectID)
           .NotEmpty().WithMessage("Please select Subject!")
           .NotEqual(0).WithMessage("Please select Subject!");


            RuleFor(x => x.HigherQualificationID)
           .NotEmpty().WithMessage("Please select Higher qualification!");
           //.NotEqual(0).WithMessage("Please select Higher qualification!");

            RuleFor(x => x.SpecializationSubjectID)
           .NotEmpty().WithMessage("Please select SpecializationSubjectID!")
           .NotEqual(0).WithMessage("Please select SpecializationSubjectID!");

            RuleFor(x => x.DistrictID)
           .NotEmpty().WithMessage("Please select District!")
           .NotEqual(0).WithMessage("Please select District!");




        }
    }
}
