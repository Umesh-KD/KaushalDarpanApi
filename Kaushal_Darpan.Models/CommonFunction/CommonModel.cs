using System;
using System.Net;

namespace Kaushal_Darpan.Models.CommonModel
{
    public class DownloadStudentEnrollmentDetailsModel
    {
        public string? Name { get; set; }
        public string? ApplicationNo { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? MobileNo { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }
        public int RoleID { get; set; }

    }
    public class DownloadStudentChangeEnrollmentDetailsModel
    {
        public string? BranchCode { get; set; }
        public string? OldEnrollmentNo { get; set; }
        public string? EnrollmentNo { get; set; }
        public int InstituteID { get; set; }
        public int InstituteCode { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }
        public int RoleID { get; set; }

    }
    
    public class OptionalFormatReportModel
    {
        public string? BranchCode { get; set; }
        public string? CenterCode { get; set; }
        public string? RollNo { get; set; }
        public string? PaperCode { get; set; }
        public int InstituteCode { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }
        public int RoleID { get; set; }

    }
    
    public class DateWiseAttendanceReport
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CenterID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? FromExamDate { get; set; }
        public string? ToExamDate { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }
        public int RoleID { get; set; }

    }

    public class PassoutStudentReport
    {
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }

    }

    public class InstituteStudentReport
    {
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }

    }


}
