

namespace Kaushal_Darpan.Models.GenerateEnroll
{
    public class GenerateRollMaster : RequestBaseModel
    {
        public int StudentID { get; set; }
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
        public int InstituteCode { get; set; }
        public string? DOB { get; set; }
        public int ModifyBy { get; set; }
        public int PDFType { get; set; }
        public int? VerifyerStatus { get; set; }
        public int? StudentExamID { get; set; }

    }

    public class GenerateRollSearchModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int StudentTypeID { get; set; }
        public string? Action { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public int ShowAll { get; set; }
        public int ModuleID { get; set; }
        public int PDFType { get; set; }
        public int StatusID { get; set; }
        public bool IsExaminationVerified { get; set; }
        public bool IsRegistrarVerified { get; set; }
        public int PublishOrder { get; set; }
        public int VerifierStatus { get; set; }
    }

    public class DownloadnRollNoModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public string? CenterName { get; set; }
        public string? InstituteNameEnglish { get; set; }
        public string? BranchCode { get; set; }
        public string? StudentType { get; set; }
        public string? EndTermName { get; set; }
        public string? FinancialYearName { get; set; }
        public string? SubjectCode { get; set; }
        public int Totalstudent { get; set; }
        public int StudentTypeID { get; set; }
        public int RollListID { get; set; }

        public string FileName { get; set; } = "";
        public int CreatedBy { get; set; }
        public int PDFType { get; set; }
        public int Status { get; set; }
        public int TotalStudent { get; set; }
        public string InstituteIds { get; set; } = string.Empty;

    }

    public class DownloadAppearedPassed : RequestBaseModel
    {
        public int ResultType { get; set; }
        public int SemesterID { get; set; }
    }

    public class StatisticsBridgeCourseModel : RequestBaseModel
    {
        public string? Action { get; set; }
        public int ResultType { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
    }

    public class VerifyRollNumberList
    {
        public int RollListID { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public string? CenterName { get; set; }
        public string? InstituteNameEnglish { get; set; }
        public string? BranchCode { get; set; }
        public string? StudentType { get; set; }
        public string? EndTermName { get; set; }
        public string? FinancialYearName { get; set; }
        public int Totalstudent { get; set; }
        public int StudentTypeID { get; set; }
        public int PDFType { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public bool IsExaminationVerified { get; set; }
        public bool IsRegistrarVerified { get; set; }
    }

}
