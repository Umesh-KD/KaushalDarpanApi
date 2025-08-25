using FluentValidation;
using Kaushal_Darpan.Models.CommonSubjectMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class CommonSubjectValidator : AbstractValidator<CommonSubjectMasterModel>
    {
        public CommonSubjectValidator()
        {
            RuleFor(x => x.CommonSubjectName)
           .NotEmpty().WithMessage("Please enter Common Subject Name!");

            RuleFor(x => x.SemesterID)
            .NotEmpty().WithMessage("Please select Semester!")
            .NotEqual(0).WithMessage("Please select Semester!");

            //child
            RuleFor(x => x.commonSubjectDetails)
                .Must(x => x.Count > 0).WithMessage("Please select Subject!");

            RuleForEach(x => x.commonSubjectDetails)
            .SetValidator(new CommonSubjectDetailsValidator());

        }
    }

    public class CommonSubjectDetailsValidator : AbstractValidator<CommonSubjectDetailsMasterModel>
    {
        public CommonSubjectDetailsValidator()
        {
            RuleFor(x => x.SubjectID)
           .NotEmpty().WithMessage("Please enter Common Subject Id!")
           .GreaterThan(0).WithMessage("Please enter Common Subject Id!");
        }
    }
}
