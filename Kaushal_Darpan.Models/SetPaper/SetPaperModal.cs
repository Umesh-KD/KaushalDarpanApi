using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SetPaper
{
    public class GetSetPaperModal
    {
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
    }
    public class PostSetPaperModal
    {
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int QuestionLimit { get; set; }
        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }

    public class PostAddQuestionModal
    {
        public string? OperationType { get; set; }
        public int? QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int PaperID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public string? AnswerOptions { get; set; }
    }
    public class GetQuestionModal
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int PaperID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
    }

    public class GetPaperAssignStaffModal
    {
        public int ID { get; set; }
        public int PaperID { get; set; }
        public int QuestionID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int StaffID { get; set; }
    }


    public class PostAddPaperAssignStaffModal
    {
        public string? OperationType { get; set; }
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public int ExamPaperAssignStaffID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int PaperID { get; set; }
        public int StaffID { get; set; }
        public int QuestionLimit { get; set; }
        public int ActiveStatus { get; set; }
        public bool? isSelected { get; set; }
        public int DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }

    

}
