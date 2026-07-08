using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.LeaveMaster
{
    public class AnnextureModel
    {
        public int InstituteID { get; set; } = 0;
        public int SemesterID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int CourseTypeID { get; set; }
        public int UserID { get; set; } = 0;
        public string? InstituteName { get; set; }
        public string? EndTermName { get; set; }
        public string? InstituteCode { get; set; }
        public string? FileName { get; set; }
        public string? DisFileName { get; set; }
        public string? Code { get; set; }
    }

    public class StudentSubjectModel
    {
        public string? EnrollmentNo { get; set; }
        public string? RollNo { get; set; }
        public string? StudentName { get; set; }

        public string? Subject1 { get; set; }
        public string? Subject2 { get; set; }
        public string? Subject3 { get; set; }
        public string? Subject4 { get; set; }
        public string? Subject5 { get; set; }
        public string? Subject6 { get; set; }
        public string? Subject7 { get; set; }
        public string? Subject8 { get; set; }
        public string? Subject9 { get; set; }
        public string? Subject10 { get; set; }
        public string? Subject11 { get; set; }
        public string? Subject12 { get; set; }
        public string? Subject13 { get; set; }
        public string? Subject14 { get; set; }
        public string? Subject15 { get; set; }
        public string? InstituteName { get; set; }
        public string? SemesterName { get; set; }
        public string? Branch { get; set; }
        public List<StudentSubjectCode> SubjectCode { get; set; }
    }


    public class StudentSubjectCode
    {
       
        public string? Subject1 { get; set; }
        public string? Subject2 { get; set; }
        public string? Subject3 { get; set; }
        public string? Subject4 { get; set; }
        public string? Subject5 { get; set; }
        public string? Subject6 { get; set; }
        public string? Subject7 { get; set; }
        public string? Subject8 { get; set; }
        public string? Subject9 { get; set; }
        public string? Subject10 { get; set; }
        public string? Subject11 { get; set; }
        public string? Subject12 { get; set; }
        public string? Subject13 { get; set; }
        public string? Subject14 { get; set; }
        public string? Subject15 { get; set; }
    }

    public class IAReportModel
    {
        public int InstituteID { get; set; } = 0;
        public int SemesterID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int CourseTypeID { get; set; }
        public int UserID { get; set; } = 0;
        public string? InstituteName { get; set; }
        public string? EndTermName { get; set; }
        public string? InstituteCode { get; set; }
        public string? FileName { get; set; }
        public string? DisFileName { get; set; }
        public string? Code { get; set; }
    }



}
