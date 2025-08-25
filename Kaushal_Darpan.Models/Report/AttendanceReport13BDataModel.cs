using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Report
{
    public class AttendanceReport13BDataModel
    {
        public string? ExamDate {  get; set; }
        public int ShiftID { get; set; }
        public int ExamCategoryID { get; set; }
        public int InstituteID { get; set; }
        public int? DepartmentID { get; set; } = 0;
        public int? Eng_NonEng { get; set; } = 0;
        public int? EndTermID { get; set; } = 0;
        public int? StudentExamType { get; set; } = 0;
        public int SemesterID { get; set; } = 0;
        public int SubjectID { get; set; } = 0;
        public string SubjectCode { get; set; } = string.Empty;
        public int UserID { get; set; } = 0;
        public int? RoleID { get; set; } = 0;
    }

    public class AttendanceReport23DataModel
    {
        public string? ExamDate { get; set; }
        public int ShiftID { get; set; }
        public int ExamCategoryID { get; set; }
        public int InstituteID { get; set; }
        public int? DepartmentID { get; set; } = 0;
        public int? Eng_NonEng { get; set; } = 0;
        public int? EndTermID { get; set; } = 0;
        public int? StudentExamType { get; set; } = 0;
        public int? SemesterID { get; set; } = 0;
        public int? SubjectID { get; set; } = 0;
        public int? StreamID { get; set; } = 0;
        public string? SubjectCode { get; set; } = string.Empty;
        public int? UserID { get; set; } = 0;
    }
}
