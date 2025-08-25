using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.GenerateAdmitCard
{
    public class GenerateAdmitCardModel
    {
        public int StudentID { get; set; }
        public int StudentExamID  { get; set; }
        public int ApplicationID { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? Rollnumber { get; set; }
        public string? StudentName { get; set; }
        public string? StreamName { get; set; }
        public string? FatherName { get; set; }
        public string? InstituteName { get; set; }
        public string? SemesterName { get; set; }
        public string? StudentCategory { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteCode { get; set; }
        public string? DOB { get; set; }
        public string? AdmitCard { get; set; }        
        public string? IPAddress { get; set; }

        public string? AdmitCardPath { get; set; }
        public string? MarksheetPath { get; set; }
        public string? Marksheet { get; set; }
        public int? Eng_NonEng { get; set; }

    }

    public class GenerateAdmitCardSearchModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int StudentID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int EndTermID { get; set; }

        public int DepartmentID { get; set; }
        public int StudentExamID { get; set; }
        public int Eng_NonEng {  get; set; }
        public string EnrollmentNo {  get; set; }
        public string? AdmitCardPath { get; set; }
        public string? AdmitCard { get; set; }
        public bool IsYearly { get; set; }
        public string InsituteIds { get; set; } = string.Empty;
        public int TotalRecord { get; set; }


    }


}
