using Kaushal_Darpan.Models.DocumentDetails;

namespace Kaushal_Darpan.Models.ApplicationData
{
    public class QualificationDataModel
    {

        public int ApplicationID { get; set; }
        public int QualificationID { get; set; }
        public int StateID { get; set; }
        public int BoardID { get; set; }
        public int BoardStateID { get; set; }
        public int BoardExamID { get; set; }
        public string? PassingID { get; set; }
        public string? RollNumber { get; set; }
        public int MarkType { get; set; }
        public decimal AggMaxMark { get; set; }
        public decimal Percentage { get; set; }
        public decimal AggObtMark { get; set; }
        public List<SupplementaryDataModel>? SupplementaryDataModel { get; set; }
        public List<LateralEntryQualificationModel>? LateralEntryQualificationModel { get; set; }
        public List<HighestQualificationModel>? HighestQualificationModel { get; set; }
        public bool IsSupplement { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int IsFinalSubmit { get; set; }
        public int CourseType { get; set; }
        public int LateralCourseID { get; set; }
        public int CategoryA { get; set; }
        public bool? isCorrectMerit { get; set; }

    }


    public class EnglishQualificationDataModel
    {
        public int ApplicationQualificationId { get; set; }
        public int? MarksTypeIDEnglish { get; set; }
        public decimal? MaxMarksEnglish { get; set; }
        public decimal? MarksObtainedEnglish { get; set; }
        public string? UniversityBoardEnglish { get; set; }
        public string? SchoolCollegeEnglish { get; set; }
        public string? YearofPassingEnglish { get; set; }
        public string? RollNumberEnglish { get; set; }
        public decimal? PercentageEnglish { get; set; }
        public int BoardStateIDEnglish { get; set; }
        public int StateOfStudyEnglish { get; set; }
    }


    public class Lateralsubject
    {
        public int LateralID { get; set; }
        public int ID { get; set; }
        public int CourseID { get; set; }
        public string? Name { get; set; }
    }

    public class LateralEntryQualificationModel
    {
        public int CourseID { get; set; }
        public int ApplicationQualificationId { get; set; }
        public string? Qualification { get; set; }
        public int StateID { get; set; }
        public int BoardID { get; set; }
        public string? BoardName { get; set; }
        public string PassingID { get; set; }
        public string RollNumber { get; set; }
        public int MarkType { get; set; }
        public string? ClassSubject { get; set; }
        public decimal AggMaxMark { get; set; }
        public decimal Percentage { get; set; }
        public decimal AggObtMark { get; set; }
        public int BoardStateID { get; set; }
        public int BoardExamID { get; set; }
        public int? CoreBranchID { get; set; }
        public int? BranchID { get; set; }
        public List<Lateralsubject>? SubjectID {  get; set; }

    }


    public class HighestQualificationModel
    {
        public int? MarksTypeIDHigh { get; set; }
        public int StateIDHigh { get; set; }
        public int ApplicationQualificationId { get; set; }
        public decimal? MaxMarksHigh { get; set; }
        public decimal? MarksObtainedHigh { get; set; }
        public string? UniversityBoard { get; set; }
        public string? SchoolCollegeHigh { get; set; }
        public string? HighestQualificationHigh { get; set; }
        public string? ClassSubject { get; set; }
        public string? YearofPassingHigh { get; set; }
        public string? RollNumberHigh { get; set; }
        public decimal? PercentageHigh { get; set; }
        public int BoardID { get; set; }
        public int BoardStateID { get; set; }
        public int BoardExamID { get; set; }
    }

    public class SupplementaryDataModel
    {
        public int SupplementryID { get; set; }
        public string PassingID { get; set; }
        public string RollN0 { get; set; }
        public string Subject { get; set; }
        public string EducationCategory { get; set; }

        public decimal? MaxMarksSupply { get; set; }
        public decimal? ObtMarksSupply { get; set; }
    }

    public class ApplicationDataModel
    {
        public int ApplicationID { get; set; }
        public string SSOID { get; set; }
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }
        public string FatherName { get; set; }
        public string FatherNameHindi { get; set; }
        public string MotherName { get; set; }
        public string MotherNameHindi { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string? WhatsNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public int IndentyProff { get; set; }
        public string DetailID { get; set; }
        public int Maritial { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public int CategoryA { get; set; }
        public string? IsPH { get; set; }
        public string? IsKM { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int Prefential { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public string? Dis_SignaturePhoto { get; set; }
        public string? Dis_StudentPhoto { get; set; }
        public string? SignaturePhoto { get; set; }
        public string? StudentPhoto { get; set; }
        public bool IsMinority { get; set; }
        public int IsFinalSubmit { get; set; }
        public int CourseType { get; set; }
        public string? CourseTypeName { get; set; }
        public bool IsTSP { get; set; }
        public bool IsSaharia { get; set; }
        public int TspDistrictID { get; set; }
        public int IsDevnarayan { get; set; }
        public int DevnarayanTehsilID { get; set; }
        public int DevnarayanDistrictID { get; set; }
        public int TSPTehsilID { get; set; }
        public int subCategory { get; set; }
        public int EWS { get; set; }
        public int CategoryD { get; set; }
        public int DirectAdmissionType { get; set; }
        public int PrefentialCategoryType { get; set; }
        public string? CertificateGeneratDate { get; set; }
        public string? CasteCertificateNo { get; set; }
        public int IsMBCCertificate { get; set; }
        public bool? isCorrectMerit { get; set; }

    }

    public class BterAddressDataModel
    {
        public int ApplicationID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int TehsilID { get; set; }
        public string CityVillage { get; set; }
        public string Pincode { get; set; }
        public string CorsAddressLine1 { get; set; }
        public string CorsAddressLine2 { get; set; }
        public string? CorsAddressLine3 { get; set; }
        public int CorsStateID { get; set; }
        public int CorsDistrictID { get; set; }
        public int CorsTehsilID { get; set; }
        public string CorsCityVillage { get; set; }
        public string CorsPincode { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int IsFinalSubmit { get; set; }
        public string NonRajasthanBlockName { get; set; }
        public string CorsNonRajasthanBlockName { get; set; }
    }

    public class BterOtherDetailsModel
    {

        public int ApplicationID { get; set; }
        public int ParentsIncome { get; set; }
        public int ApplyScheme { get; set; }
        public int EWS { get; set; }
        public int Residence { get; set; }

        public string? IncomeSource { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int IsFinalSubmit { get; set; }
        public int CategoryA { get; set; }
        public bool? IsSportsQuota { get; set; }
    }



    public class BterDocumentsDataModel
    {
        public int ApplicationID { get; set; }
        public string StudentPhoto { get; set; }
        public string SignaturePhoto { get; set; }
        public string? MotherDepCertificate { get; set; }
        public string PrefCategory { get; set; }
        public string AadharPhoto { get; set; }
        public string Marksheet { get; set; }
        public string Dis_StudentPhoto { get; set; }
        public string Dis_SignaturePhoto { get; set; }
        public string? Dis_MotherDepCertificate { get; set; }
        public string Dis_PrefCategory { get; set; }
        public string Dis_AadharPhoto { get; set; }
        public string Dis_Marksheet { get; set; }
        public string? Minority { get; set; }
        public string? Dis_Minority { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int IsFinalSubmit { get; set; }
        public List<DocumentDetailList>? DocumentDetailList { get; set; }
        public List<DocumentDetailsModel>? DocumentDetails { get; set; }
    }

    public class DocumentDetailList
    {

        public int TransactionID { get; set; }
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string DisFileName { get; set; }
        public string Folder { get; set; }
        public int DocumentID { get; set; }
        public int ModifyBy { get; set; }
    }

    public class BterOptionsDetailsDataModel
    {
        public int OptionID { get; set; }
        public int ApplicationID { get; set; }
        public int College_TypeID { get; set; }
        public string College_TypeName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int InstituteID { get; set; }
        public string InstituteName { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int Priority { get; set; }
        public int IsFinalSubmit { get; set; }
        public int CourseType { get; set; }
        public int AcademicYear { get; set; }
        public string? Type { get; set; }


    }

    public class BterSearchModel
    {
        public int ApplicationId { get; set; }
        public int DepartmentID { get; set; }
        public string SSOID { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string? StudentName { get; set; }
        public string? EnrollmentNo { get; set; }
        public int AcademicYear { get; set; }

        //public int Verfireid { get; set; }

    }

    public class BterCollegesSearchModel
    {
        public int DistrictID { get; set; }
        public string? action { set; get; }
        public string? College_TypeID { set; get; }
        public int? ApplicationID { set; get; }
        public int? InstituteID { set; get; }
        public int? StreamType { set; get; }
    }

    public class DirectAdmissionUpdatePayment : RequestBaseModel
    {
        public int ApplicationId { set; get; }
    }

}
