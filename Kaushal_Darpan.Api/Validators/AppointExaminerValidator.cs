using FluentValidation;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.CompanyMaster;

namespace Kaushal_Darpan.Api.Validators
{
    public class AppointExaminerValidator : AbstractValidator<AppointExaminerModel>
    {
        public AppointExaminerValidator()
        {
     
            RuleFor(x => x.CourseID)
           .NotEmpty().WithMessage("Please select Course!")
           .NotEqual(0).WithMessage("Please select Course!");

            RuleFor(x => x.SemesterID)
           .NotEmpty().WithMessage("Please select Semester!")
           .NotEqual(0).WithMessage("Please select Semester!");

            RuleFor(x => x.SubjectID)
           .NotEmpty().WithMessage("Please select Subject!")
           .NotEqual(0).WithMessage("Please select Subject!");

            RuleFor(x => x.ExaminerID)
           .NotEmpty().WithMessage("Please select Examiner SSOID!")
           .NotEqual(0).WithMessage("Please select Examiner SSOID!");

            RuleFor(x => x.RollNumberFrom)
           .NotEmpty().WithMessage("Please Enter Roll No From!");

            RuleFor(x => x.RollNumberTo)
           .NotEmpty().WithMessage("Please Enter  Roll No To!");

            RuleFor(x => x.GroupCode)
           .NotEmpty().WithMessage("Please Enter GroupCode!");







        }
    }
}
