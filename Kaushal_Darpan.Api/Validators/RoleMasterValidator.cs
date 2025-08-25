using FluentValidation;
using Kaushal_Darpan.Models.RoleMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class RoleMasterValidator : AbstractValidator<RoleMasterModel>
    {
        public RoleMasterValidator()
        {
            RuleFor(x => x.RoleNameEnglish)
            .NotEmpty().WithMessage("Please enter Role Name English!");
        }

    }

}
