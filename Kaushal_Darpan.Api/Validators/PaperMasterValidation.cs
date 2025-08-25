using FluentValidation;
using Kaushal_Darpan.Models;

namespace Kaushal_Darpan.Api.Validators
{
    public class PaperMasterValidation : AbstractValidator<PapersMasterModel>
    {
        public PaperMasterValidation()
        {

            RuleFor(x => x.StreamID)
            .NotEmpty().WithMessage("Please select Stream!")
            .NotEqual(0).WithMessage("Please select Stream!");

            RuleFor(x => x.SemesterID)
           .NotEmpty().WithMessage("Please select Semester!")
           .NotEqual(0).WithMessage("Please select Semester!");

            RuleFor(x => x.StreamID)
          .NotEmpty().WithMessage("Please select Stream!")
          .NotEqual(0).WithMessage("Please select Stream!");

            RuleFor(x => x.SubjectID)
            .NotEmpty().WithMessage("Please select Subject !")
            .NotEqual(0).WithMessage("Please select Subject!");


            // RuleFor(x => x.ExamDate)
            //.NotEmpty().WithMessage("Please select Date!");



            RuleFor(x => x.FinancialYearID)
            .NotEmpty().WithMessage("Please select Financial Year!")
            .NotEqual(0).WithMessage("Please select Exam Shift!");

            RuleFor(x => x.CommonSubjectID)
            .NotEmpty().WithMessage("Please select Common Subject!")
            .NotEqual(0).WithMessage("Please select Common Subject!");

            RuleFor(x => x.Paper_id)
            .NotEmpty().WithMessage("Please select Paper Type!")
            .NotEqual(0).WithMessage("Please select Paper Type!");


            RuleFor(x => x.SubjectCode)
            .NotEmpty().WithMessage("Please Enter Subject Code!");



            RuleFor(x => x.SubjectName)
           .NotEmpty().WithMessage("Please Enter Subject Name!");
            //            RuleFor(x => x.L)
            //.NotEmpty().WithMessage("Please Enter Subject Name!");

            //            RuleFor(x => x.T)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");

            //            RuleFor(x => x.P)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");

            //            RuleFor(x => x.Th)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");

            //            RuleFor(x => x.Pr)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");

            //            RuleFor(x => x.Ct)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");
            //            RuleFor(x => x.Tu)
            //.NotEmpty().WithMessage("Please Enter Subject Category!");
            RuleFor(x => x.Credit)
.NotEmpty().WithMessage("Please Enter Credit!");


        }
    }
}
