using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CompanyMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class CompanyMasterValidator : AbstractValidator<CompanyMasterModels>
    {
        public CompanyMasterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter Name !");

        //    RuleFor(x => x.Website)
        //.NotEmpty().WithMessage("Please enter Website!")
        //.Matches(Constants.RGX_WEBSITE).WithMessage("Please enter Valid Website!");

            RuleFor(x => x.Address)
           .NotEmpty().WithMessage("Please enter Address!");

            RuleFor(x => x.StateID)
          .NotEmpty().WithMessage("Please select State!")
          .NotEqual(0).WithMessage("Please select State!");

            RuleFor(x => x.DistrictID)
           .NotEmpty().WithMessage("Please select District!")
           .NotEqual(0).WithMessage("Please select District!");


        }
    }
}
