namespace Kaushal_Darpan.Models.Report
{
    public class ReportBaseModel : RequestBaseModel
    {
        public int StudentID { get; set; }
        public int InstituteID { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? ApplicationNo { get; set; }
        public int? StudentExamID { get; set; }
        public int? SemesterID { get; set; }
        public string? Action { get; set; }
        public string? SubjectCode { get; set; }
        public int ExamType { get; set; }
        public string CommonSubjectText { get; set; } = string.Empty;
        public string? FileType { get; set; }
    }

    public class BlankReportModel : RequestBaseModel
    {
        public int BranchID { get; set; }
        public int SemesterID { get; set; }
        public int ShiftID { get; set; }
        public int CategoryID { get; set; }
        public int SubjectID { get; set; }
        public int ExamCategoryID { get; set; }
        public int InstituteID { get; set; }
        public int CenterID { get; set; }
        public string? SubjectCode { get; set; }
        public string? ExamDate { get; set; }
    }



}
