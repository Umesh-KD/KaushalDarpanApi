using FluentValidation;
using Kaushal_Darpan.Models.TimeTable;

namespace Kaushal_Darpan.Api.Validators
{
    public class TimeTableValidator : AbstractValidator<TimeTableModel>
    {
        public TimeTableValidator()
        {

            RuleFor(x => x.SemesterID)
            .NotEmpty().WithMessage("Please select Semester!")
            .NotEqual(0).WithMessage("Please select Semester!");

            //RuleFor(x => x.BranchSubjectDataModel.SubjectID)
            //.NotEmpty().WithMessage("Please select Subject !")
            //.NotEqual(0).WithMessage("Please select Subject!");


            // RuleFor(x => x.ExamDate)
            //.NotEmpty().WithMessage("Please select Date!");



            //RuleFor(x => x.ShiftID)
            //.NotEmpty().WithMessage("Please select Exam Shift!")
            //.NotEqual(0).WithMessage("Please select Exam Shift!");

            //RuleFor(x => x.BranchID)
            //.NotEmpty().WithMessage("Please select Branch!")
            //.NotEqual(0).WithMessage("Please select Branch!");

        }
    }
}
