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
    public class TheorySearchModel : RequestBaseModel
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
        public string? PaperCode { get; set; }
        public string? SubjectName { get; set; }
        public int? CenterCode { get; set; }
        public int? IsPersentAbsent { get; set; }
        public int? CheckedStatus { get; set; }
        public int centersubmitstatus { get; set; }
        public int centerpresentstatus { get; set; }
        public int? StudentStatus { get; set; }
        public string? StrKey { get; set; }
        public int? isUFM { get; set; }
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

    public class TabluationDataModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CourseType { get; set; }
        public int ResultTypeId { get; set; }
        public int EffectiveFromEndTermId { get; set; }

    }

    public class UnlockInternalMarksModel
    {
        public int InstituteID { get; set; }
        public string? InstituteCode { get; set; }
        public string? InstitutionDGTCode { get; set; }
        public string? InstituteNameEnglish { get; set; }
        public string? InstituteNameHindi { get; set; }

        public string? SSOID { get; set; }
        public string? Email { get; set; }
        public string? FaxNumber { get; set; }
        public string? Website { get; set; }

        public string? LandNumber { get; set; }
        public string? LandlineSTD { get; set; }
        public string? MobileNumber { get; set; }

        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public int TehsilID { get; set; }

        public int InstitutionManagementTypeID { get; set; }
        public int InstitutionCategoryID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public string? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public int TypeID { get; set; }
        public int? InstituteID_Old { get; set; }
        public int DepartmentID { get; set; }
        public int FinancialYearID { get; set; }

        public bool IsENG { get; set; }
        public bool IsNonENG { get; set; }
        public bool IsLateral { get; set; }

        public int? CollegeId_Old { get; set; }
        public int Capacity { get; set; }

        public bool IsDegreeNonENG { get; set; }
        public bool IsDegreeLateral { get; set; }
        public bool IsAdmission { get; set; }

        public string? Password { get; set; }
        public bool IsPayment { get; set; }

        public int CollegeTypeID { get; set; }
        public int EndTermID { get; set; }
        public bool IsProfileComplete { get; set; }
    }
    public class updateUnlockInternalMarksModel
    {
        public int InstituteID { get; set; }
        public int TypeID { get; set; }
        public int ModifyBy { get; set; }
        public int EndTermID { get; set; }

    }

    public class UFMStudentExtraInfoSaveModel : ResponseBaseModel
    {
        public int UFMStuExtraInfoID { get; set; } = 0;
        public int StudentID { get; set; } = 0;
        public int SerialNo { get; set; } = 0;
        public int SerialNo2 { get; set; } = 0;

        public string? IssueDate { get; set; }
        public string? BundleSendDate { get; set; }
        public string? Date2 { get; set; }

        public int StudentExamType { get; set; } = 0;
        public int StudentExamID { get; set; } = 0;
        public int StudentExamPaperID { get; set; } = 0;
        public string EnrollmentNo { get; set; } = string.Empty;
    }

    public class UFMStudentExtraInfoGetModel : RequestBaseModel
    {
        public int StudentID { get; set; } = 0;
        public int StudentExamID { get; set; } = 0;
        public int StudentExamPaperID { get; set; } = 0;
        public int UFMStuExtraInfoID { get; set; } = 0;
    }

    public class UFMExtraInfoSaveModel : ResponseBaseModel
    {
        public int UFMExtraInfoID { get; set; } = 0;
        public int SerialNo { get; set; } = 0;
        public int SerialNo2 { get; set; } = 0;
        public string? IssueDate { get; set; }
        public string? BundleSendDate { get; set; }
        public string? Date2 { get; set; }
    }
}
