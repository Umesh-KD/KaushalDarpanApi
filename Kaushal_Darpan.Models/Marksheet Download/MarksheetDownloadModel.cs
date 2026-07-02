namespace Kaushal_Darpan.Models.MarksheetDownloadModel
{
    public class MarksheetDownloadSearchModel
    {
        public int? ModifyBy { get; set; } = 0;
        public int? SemesterID { get; set; }
        public int? MarksheetID { get; set; }
        public int? InstituteID { get; set; }
        public string? IsRevised { get; set; }
        public int? IsBridge { get; set; }
        public int? ResultTypeID { get; set; }
        public int? RollNo { get; set; }
        public int? DepartmentID { get; set; }
        public int? StudentID { get; set; }
        public int? Eng_NonEngID { get; set; }
        public int? EndTermID { get; set; }
        public int? ExamTypeID { get; set; }
        public int? RWHResultID { get; set; }
        public int? AcademicYearID { get; set; }
        public string? IPAddress { get; set; }
        public string? MarksheetPath { get; set; }
        public string? Marksheet { get; set; }
        public string? SessionName { get; set; }
        public string? MarksheetFile { get; set; }
        public string? MarksheetFilePath { get; set; }
        public string? DOB { get; set; }
        public string SRNO { get; set; } = string.Empty;

        public bool? IsReval {  get; set; }
        public bool? IsRWHResult { get; set; }
        public bool? IsLateral { get; set; }
        public int? ReqId { get; set; }
        public int? StudentTypeID { get; set; }

        public int RequestEndTerm { get; set; }

        public int? FianancialYearID { get; set; }
        public int? DocumentID { get; set; }

    }

    public class BackPaperReportDataModel : RequestBaseModel
    {
        public int? InstituteID { set; get; }
        public int? SemesterID { set; get; }
    }

    public class GenerateMarksheetModel: RequestBaseModel
    {
        public string? MarksheetPath { get; set; }
        public string? MarksheetFile { get; set; }
        public int? StudentID { get; set; }
        public int? SemesterID { get; set; }
        public int? RollNo { get; set; }
        public int? ResulTypeID { get; set; }
    }
    public class StudentDownloadInfo
    {
        public int? RollNo { get; set; }
        public int? MarksheetID { get; set; }
        public string? MarksheetFile { get; set; }
        public string? MarksheetFilePath { get; set; }
    }
    public class StudentResultSearchModel
    {
        public int? EndTermID { get; set; }
        public int? SemesterID { get; set; }
        public int? ResultType { get; set; }
        public string? RollNo { get; set; }
        public string? DOB { get; set; }
    }

    public class MarksheetSaveDataModel
    {
        public int MarkSheetID { get; set; }
        public int EndTermID { get; set; }
        public int CourseType { get; set; }
        public string SrNo { get; set; } = string.Empty;
        public int StudentID { get; set; }
        public int StudentExamID { get; set; }
        public int Year { get; set; }
        public int SemesterID { get; set; }

        public string StudentName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string EnrollmentNo { get; set; } = string.Empty;
        public string RollNo { get; set; } = string.Empty;

        public string DOB { get; set; } = string.Empty;

        public int InstituteID { get; set; }
        public string InstituteName { get; set; } = string.Empty;

        public string StreamName { get; set; } = string.Empty;
        public int StreamId { get; set; }
        public string StreamCode { get; set; } = string.Empty;

        public int IsUFM { get; set; }
        public int IsRWH { get; set; }
        public int MarksheetYear { get; set; }
        public int Type { get; set; }

        public string EndTerm { get; set; } = string.Empty;
        public string EndTermSpl { get; set; } = string.Empty;

        public int IsSplExam { get; set; }

        public string Session { get; set; } = string.Empty;
        public string SessionSpl { get; set; } = string.Empty;

        public string ResultDate { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }

        public int IsReval { get; set; }
        public int Result { get; set; }
        public int IsIssued { get; set; }
        public int RevisedId { get; set; }
        public int IsVersion { get; set; }
        public int IsRevisedIssueDate { get; set; }
        public int UfCategory { get; set; }
        public int IsBridge { get; set; }
        public int IsRWHResult { get; set; }
        public int RWHResultId { get; set; }
        public int VersionCount { get; set; }
        public int UpdateLogId { get; set; }
        public int IsLiteral { get; set; }

        public string Remark { get; set; } = string.Empty;

        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }

        public DateTime RTS { get; set; }

        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

        public int MarkSheetID_Old { get; set; }
        public int IsRevised { get; set; }
        public int ResultTypeId { get; set; }

        public string MarksheetFile { get; set; } = string.Empty;
        public string MarksheetFilePath { get; set; } = string.Empty;
        public List<MarksheetSubjectDataModel> SubjectDetails {  get; set; } = new List<MarksheetSubjectDataModel>();
        public List<MarksheetResultDataModel> ResultDetails { get; set; } = new List<MarksheetResultDataModel>();
    }

    public class MarksheetSubjectDataModel
    {
        public string StudentName { get; set; } = string.Empty;
        public int StudentID { get; set; }
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectCredits { get; set; } = string.Empty;
        public string EarnedCredits { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public bool IsStudentCenteredActivity { get; set; }
        public int IsExCurrent { get; set; }
    }
    public class MarksheetResultDataModel
    {

        // --- Flags ---
        public bool IsReval { get; set; }
        public bool IsLiteral { get; set; }
        public int ResultTypeId { get; set; }

        // --- Semester 1 ---
        public decimal SubjectCreditsSem1 { get; set; }
        public decimal EarnedCreditsSem1 { get; set; }
        public decimal CGPASem1 { get; set; }
        public decimal SGPASem1 { get; set; }

        // --- Semester 2 ---
        public decimal SubjectCreditsSem2 { get; set; }
        public decimal EarnedCreditsSem2 { get; set; }
        public decimal CGPASem2 { get; set; }
        public decimal SGPASem2 { get; set; }

        // --- Semester 3 ---
        public decimal SubjectCreditsSem3 { get; set; }
        public decimal EarnedCreditsSem3 { get; set; }
        public decimal CGPASem3 { get; set; }
        public decimal SGPASem3 { get; set; }

        // --- Semester 4 ---
        public decimal SubjectCreditsSem4 { get; set; }
        public decimal EarnedCreditsSem4 { get; set; }
        public decimal CGPASem4 { get; set; }
        public decimal SGPASem4 { get; set; }

        // --- Semester 5 ---
        public decimal SubjectCreditsSem5 { get; set; }
        public decimal EarnedCreditsSem5 { get; set; }
        public decimal CGPASem5 { get; set; }
        public decimal SGPASem5 { get; set; }

        // --- Semester 6 ---
        public decimal SubjectCreditsSem6 { get; set; }
        public decimal EarnedCreditsSem6 { get; set; }
        public decimal CGPASem6 { get; set; }
        public decimal SGPASem6 { get; set; }

        // --- Final Summaries & Results ---
        public decimal Percentage { get; set; }
        public string Result { get; set; } = string.Empty;
        public string? ResultDeclareDate { get; set; } = string.Empty; // Nullable in case date is empty
        public string DiplomaFinalResult { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string TotalSubjectCredits { get; set; } = string.Empty;
        public string TotalEarnedCredits { get; set; } = string.Empty;
    }
}
