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

    public class ExamLetterReport
    {
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndtermID { get; set; }
        public string? SubjectCode { get; set; }

    }

    public class ExamLetterReportModel
    {
        public int SrNo { get; set; }
        public int SemesterID { get; set; }

        public string? CenterCode { get; set; }
        public string? GroupCode { get; set; }

        public string? SubjectCode { get; set; }
        public string? SubjectName { get; set; }

        public int Total { get; set; }
        public int IsPresentTotal { get; set; }
        public int IsUFM { get; set; }
        public int IsDetain { get; set; }
        public int IsAbsent { get; set; }

        public string? ExamName { get; set; }

        public int TotalAnswerSheet { get; set; }
    }


    public class InternalAssessmentStudentReport : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int Type { get; set; }
        public int TypeID { get; set; }
        public int SchemeID { get; set; }
        public int TermPart { get; set; }
        public string? StreamIDs { get; set; }

    }



}
