using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItiStudentActivities
{
    public interface ITIStudentCenterActivities
    {
        public int SubjectID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public string? IPAddress { get; set; }
        public string IsPresentStudentCenteredActivity { get; set; }
        public string ObtainedStudentCenteredActivity { get; set; }
        public string GroupCode { get; set; }
        public string CenterCode { get; set; }
        public string SubjectCode { get; set; }
        public string? RollNo { get; set; }
        public int MaxStudentCenteredActivity { get; set; } 
        public string Marked { get; set; }
        public int ModifyBy { get; set; }
    }

    public class ITIStudentCenterActiviteSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }
        public string? RollNo { get; set; }
        public int InstituteID { get; set; }
        public string? InstituteName { get; set; }
    }

}
