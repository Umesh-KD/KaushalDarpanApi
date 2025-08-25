namespace Kaushal_Darpan.Models.TheoryMarks

{
    public class TheoryMarksModel
    {
        public int SubjectID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public int? StudentExamPaperRevaluationID { get; set; }
        public int StudentExamPaperID { get; set; }
        public string? IPAddress { get; set; }
        public string? IsPresentTheory { get; set; }
        public string? ObtainedTheory { get; set; }
        public string StudentName { get; set; }
        public string? InstituteNameEnglish { get; set; }
        public string? SemesterName { get; set; }
        public string? Name { get; set; }
        public string? SubjectName { get; set; }
        public string? IsPresentInternalAssisment { get; set; }
        public string? IsPresentPractical { get; set; }
        public string? ObtainedInternalAssisment { get; set; }
        public string? ObtainedPractical { get; set; }
        public string? GroupCode { get; set; }
        public string? CenterCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? RollNo { get; set; }
        public int MaxTheory { get; set; }
        public int MaxPractical { get; set; }
        public int MaxInternalAssisment { get; set; }
        public string Marked { get; set; }
        public int ModifyBy { get; set; }
        public int InternalPracticalID { get; set; }
        public bool IsChecked { get; set; }
        public bool isFinalSubmit { get; set; }
        public bool IsPracticalChecked { get; set; }
        public bool IsInternalAssesmentCheckecd { get; set; }
        public string? UFMDocument { get; set; }
        public string? Dis_UFMDocument { get; set; }
        public int? StudentID { get; set; }
        public int? StudentExamID { get; set; }


    }
    public class TheorySearchModel: RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }
        public int InternalPracticalID { get; set; }
        public string? RollNo { get; set; }
        public string? ExaminerCode { get; set; }
        public int GroupCodeID { get; set; }
        public string? SSOID { get; set; }
        public int InstituteID { get; set; }
        public int UserID { get; set; }
        public int? RoleID { get; set; }
        public bool IsConfirmed { get; set; }
        public string? SubjectType { get; set; }
    }

    public class StudentFailTheoryReportModel
    {
        public int SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string SPNNO { get; set; }
        public string StudentName { get; set; }
        public decimal ObtainedTheory { get; set; }
        public bool IsPresentTheory { get; set; }
        public bool IsTheory { get; set; }
        public bool ActiveStatus { get; set; }
        public string Grade { get; set; }
        public decimal MaxTheory { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string RollNo { get; set; }
        public string IPAddress { get; set; }
        public DateTime RTS { get; set; }

        public int DepartmentID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;

    }


    public class StudentItiSearchModel
    {
        public int CollegeID { get; set; } = 0;
        public string EnrollmentNo { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string TradeID { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class ExaminerFeedbackDataModel : RequestBaseModel
    {
        public int? ExaminerID { get; set; }
        public string? ExaminerCode { get; set; } 
        public int? GroupCodeID { get; set; }
        public string? GroupCode { get; set; }
        public string? Feedback { get; set; }
        public string? CenterCode { get; set; }
        public string? IPAddress { get; set; }
    }



}
