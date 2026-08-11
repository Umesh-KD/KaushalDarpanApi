namespace Kaushal_Darpan.Models.MarksheetDownloadModel
{
    public class MarksheetDownloadSearchModel : RequestBaseModel
    {
        public int? ModifyBy { get; set; } = 0;
        public int? SemesterID { get; set; }
        public int? MarksheetID { get; set; }
        public int? InstituteID { get; set; }
        public string? IsRevised { get; set; }
        public int? IsBridge { get; set; }
        public int? ResultTypeID { get; set; }
        public string? RollNo { get; set; }
        public int? StudentID { get; set; }
        public int? Eng_NonEngID { get; set; }
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

        public bool? IsReval { get; set; }
        public bool? IsRWHResult { get; set; }
        public bool? IsLateral { get; set; }
        public int? ReqId { get; set; }
        public int? StudentTypeID { get; set; }

        public int RequestEndTerm { get; set; }

        public int? FianancialYearID { get; set; }
        public int? DocumentID { get; set; }
        public int? EffectiveEndTermID { get; set; }
        public int? SchemeID { get; set; }

    }

    public class BackPaperReportDataModel : RequestBaseModel
    {
        public int? InstituteID { set; get; }
        public int? SemesterID { set; get; }
    }

    public class GenerateMarksheetModel : RequestBaseModel
    {
        public string? MarksheetPath { get; set; }
        public string? MarksheetFile { get; set; }
        public int? StudentID { get; set; }
        public int? SemesterID { get; set; }
        public string? RollNo { get; set; }
        public int? ResulTypeID { get; set; }
    }
    public class StudentDownloadInfo
    {
        public string? RollNo { get; set; }
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
        public int? EffectiveEndTermID { get; set; }
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
        public string IssueDate { get; set; } = string.Empty;

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

        public string RTS { get; set; } = string.Empty;

        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; } = string.Empty;

        public int MarkSheetID_Old { get; set; }
        public int IsRevised { get; set; }
        public int ResultTypeId { get; set; }

        public string MarksheetFile { get; set; } = string.Empty;
        public string MarksheetFilePath { get; set; } = string.Empty;
        public List<MarksheetSubjectDataModel> SubjectDetails { get; set; } = new List<MarksheetSubjectDataModel>();
        public MarksheetResultDataModel ResultDetails { get; set; } = new MarksheetResultDataModel();
        public int EffectiveEndTermID { get; set; }
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
        public string SubjectCreditsSem1 { get; set; }
        public string EarnedCreditsSem1 { get; set; }
        public string CGPASem1 { get; set; }
        public string SGPASem1 { get; set; }

        // --- Semester 2 ---
        public string SubjectCreditsSem2 { get; set; }
        public string EarnedCreditsSem2 { get; set; }
        public string CGPASem2 { get; set; }
        public string SGPASem2 { get; set; }

        // --- Semester 3 ---
        public string SubjectCreditsSem3 { get; set; }
        public string EarnedCreditsSem3 { get; set; }
        public string CGPASem3 { get; set; }
        public string SGPASem3 { get; set; }

        // --- Semester 4 ---
        public string SubjectCreditsSem4 { get; set; }
        public string EarnedCreditsSem4 { get; set; }
        public string CGPASem4 { get; set; }
        public string SGPASem4 { get; set; }

        // --- Semester 5 ---
        public string SubjectCreditsSem5 { get; set; }
        public string EarnedCreditsSem5 { get; set; }
        public string CGPASem5 { get; set; }
        public string SGPASem5 { get; set; }

        // --- Semester 6 ---
        public string SubjectCreditsSem6 { get; set; }
        public string EarnedCreditsSem6 { get; set; }
        public string CGPASem6 { get; set; }
        public string SGPASem6 { get; set; }

        // --- Final Summaries & Results ---
        public string Percentage { get; set; }
        public string Result { get; set; } = string.Empty;
        public string? ResultDeclareDate { get; set; } = string.Empty; // Nullable in case date is empty
        public string DiplomaFinalResult { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string TotalSubjectCredits { get; set; } = string.Empty;
        public string TotalEarnedCredits { get; set; } = string.Empty;
    }


    public class DiplomaCertificateDownloadSearchModel : RequestBaseModel
    {
        public int? ModifyBy { get; set; } = 0;
        public int? SemesterID { get; set; } = 0;
        public int? FinalDiplomaID { get; set; } = 0;
        public int? InstituteID { get; set; } = 0;
        public string? IsRevised { get; set; } = string.Empty;
        public int? IsBridge { get; set; } = 0;
        public int? ResultTypeID { get; set; } = 0;
        public string? RollNo { get; set; } = string.Empty;
        public int? StudentID { get; set; } = 0;
        public int? Eng_NonEngID { get; set; } = 0;
        public int? ExamTypeID { get; set; } = 0;
        public int? RWHResultID { get; set; } = 0;
        public int? AcademicYearID { get; set; } = 0;
        public string? IPAddress { get; set; } = string.Empty;
        public string? SessionName { get; set; } = string.Empty;
        public string? Dis_FileName { get; set; } = string.Empty; // name
        public string? FileName { get; set; } = string.Empty; // with file path
        public string? DOB { get; set; } = string.Empty;
        public string? SRNO { get; set; } = string.Empty;

        public bool? IsReval { get; set; } = false;
        public bool? IsRWHResult { get; set; } = false;
        public bool? IsLateral { get; set; } = false;
        public int? ReqId { get; set; } = 0;
        public int? StudentTypeID { get; set; } = 0;

        public int? RequestEndTerm { get; set; } = 0;

        public int? FianancialYearID { get; set; } = 0;
        public int? DocumentID { get; set; } = 0;
        public int? EffectiveEndTermID { get; set; } = 0;
        public string? EnrollmentNo { get; set; } = string.Empty;
        public string? StudentName { get; set; } = string.Empty;
        public string? ResultDate { get; set; } = string.Empty;
        public string? PublishDate { get; set; } = string.Empty;
        public bool? IsLocked { get; set; } = false;
        public string? DiplomaPrintingDate { get; set; }=string.Empty;
        public string? IsRevisedIssueDate { get; set; } = string.Empty;
        public int? ExamResultID { get; set; } = 0;
        public int? RevisedId { get; set; } = 0;
        public int? IsBlock { get; set; } = 0;
        public int? IsDiploma { get; set; } = 0;
        public bool? IsDuplicate { get; set; } = false;
        public int? DuplicateDiplomaId { get; set; } = 0;
        public int? RequestId { get; set; } = 0;
        public bool? IsIssued { get; set; } = false;
        public string? RegistrarSignFile { get; set; } = string.Empty;
        public string? FatherName { get; set; } = string.Empty;
        public string? StreamName { get; set; } = string.Empty;
        public string? FinalDiplomaTermName { get; set; } = string.Empty;
        public string? Division { get; set; } = string.Empty;
        public string? CourseDuration { get; set; } = string.Empty;
    }

    public class GenerateFinalDiplomaCertificateModel : RequestBaseModel
    {
        public string? FileName { get; set; } // with file path
        public string? Dis_FileName { get; set; } // file name
        public int? StudentID { get; set; }
        public int? SemesterID { get; set; }
        public string? RollNo { get; set; }
        public int? ResulTypeID { get; set; }
        public string? EnrollmentNo { get; set; }
    }

    public class FinalDiplomaCertificateSaveDataModel : ResponseBaseModel
    {
        public int FinalDiploma { get; set; } // id
        public string SrNo { get; set; } = string.Empty;
        public string Enrollment { get; set; } = string.Empty;
        public int InstituteId { get; set; }
        public int SrDiploma { get; set; }
        public string ResultDate { get; set; } = string.Empty;
        public string PublishDate { get; set; } = string.Empty;
        public byte IsLocked { get; set; }
        public string DiplomaPrintingDate { get; set; } = string.Empty;
        public byte IsRwhResult { get; set; }
        public int RwhResultId { get; set; }
        public byte IsReval { get; set; }
        public byte IsRevisedIssueDate { get; set; }
        public int ResultId { get; set; }
        public int RevisedId { get; set; }
        public byte IsBlock { get; set; }
        public int StudentId { get; set; }
        public byte IsDiploma { get; set; }
        public byte IsDuplicate { get; set; }
        public int DuplicateDiplomaId { get; set; }
        public int RequestId { get; set; }
        public byte IsIssued { get; set; }
        public int ResultTypeID { get; set; }
        public int EffectiveEndTermID { get; set; }
        public bool IsRevised { get; set; }
        public string FileName { get; set; } = string.Empty; // with file path
        public string Dis_FileName { get; set; } = string.Empty; // file name
        public int SemesterID { get; set; }
        public string? SRNO { get; set; }
    }

    public class ProvisionalDiplomaCertificateDownloadSearchModel : RequestBaseModel
    {
        public int? ModifyBy { get; set; } = 0;
        public int? SemesterID { get; set; } = 0;
        public int? FinalDiplomaID { get; set; } = 0;
        public int? InstituteID { get; set; } = 0;
        public string? IsRevised { get; set; } = string.Empty;
        public int? IsBridge { get; set; } = 0;
        public int? ResultTypeID { get; set; } = 0;
        public string? RollNo { get; set; } = string.Empty;
        public int? StudentID { get; set; } = 0;
        public int? Eng_NonEngID { get; set; } = 0;
        public int? ExamTypeID { get; set; } = 0;
        public int? RWHResultID { get; set; } = 0;
        public int? AcademicYearID { get; set; } = 0;
        public string? IPAddress { get; set; } = string.Empty;
        public string? SessionName { get; set; } = string.Empty;
        public string? Dis_FileName { get; set; } = string.Empty; // name
        public string? FileName { get; set; } = string.Empty; // with file path
        public string? DOB { get; set; } = string.Empty;
        public string? SRNO { get; set; } = string.Empty;

        public bool? IsReval { get; set; } = false;
        public bool? IsRWHResult { get; set; } = false;
        public bool? IsLateral { get; set; } = false;
        public int? ReqId { get; set; } = 0;
        public int? StudentTypeID { get; set; } = 0;

        public int? RequestEndTerm { get; set; } = 0;

        public int? FianancialYearID { get; set; } = 0;
        public int? DocumentID { get; set; } = 0;
        public int? EffectiveEndTermID { get; set; } = 0;
        public string? EnrollmentNo { get; set; } = string.Empty;
        public string? StudentName { get; set; } = string.Empty;
        public string? ResultDate { get; set; } = string.Empty;
        public string? PublishDate { get; set; } = string.Empty;
        public bool? IsLocked { get; set; } = false;
        public string? DiplomaPrintingDate { get; set; } = string.Empty;
        public string? IsRevisedIssueDate { get; set; } = string.Empty;
        public int? ExamResultID { get; set; } = 0;
        public int? RevisedId { get; set; } = 0;
        public int? IsBlock { get; set; } = 0;
        public int? IsDiploma { get; set; } = 0;
        public bool? IsDuplicate { get; set; } = false;
        public int? DuplicateDiplomaId { get; set; } = 0;
        public int? RequestId { get; set; } = 0;
        public bool? IsIssued { get; set; } = false;
        public string? RegistrarSignFile { get; set; } = string.Empty;
        public string? FatherName { get; set; } = string.Empty;
        public string? StreamName { get; set; } = string.Empty;
        public string? FinalDiplomaTermName { get; set; } = string.Empty;
        public string? Division { get; set; } = string.Empty;
        public string? CourseDuration { get; set; } = string.Empty;
        public int? EffectiveFromEndTermId { get; set; } = 0;

    }

}
