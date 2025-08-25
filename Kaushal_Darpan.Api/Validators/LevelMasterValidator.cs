using FluentValidation;
using Kaushal_Darpan.Models.LevelMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class LevelMasterValidator : AbstractValidator<LevelMasterModel>
    {
        public LevelMasterValidator()
        {
            RuleFor(x => x.LevelNameEnglish)
            .NotEmpty().WithMessage("Please enter Level Name English!");


            RuleFor(x => x.LevelNameHindi)
           .NotEmpty().WithMessage("Please enter Level Name Hindi!");

            RuleFor(x => x.LevelNameShort)
           .NotEmpty().WithMessage("Please enter Level Name Short!");
        }




    }

}
