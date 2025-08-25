using FluentValidation;
using Kaushal_Darpan.Models.SubjectMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class SubjectMasterValidator : AbstractValidator<SubjectMaster>
    {
        public SubjectMasterValidator()
        {
            RuleFor(x => x.SubjectName)
            .NotEmpty().WithMessage("Please enter Subject Name!");
            RuleFor(x => x.SubjectCode)
            .NotEmpty().WithMessage("Please enter Subject Code!");



            RuleFor(x => x.SubjectType)
             .NotEmpty().WithMessage("Please select Subject Type!");





        }
    }
}
