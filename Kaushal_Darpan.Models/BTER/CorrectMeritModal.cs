using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.studentve;

namespace Kaushal_Darpan.Models.BTER
{
    //public class CorrectMeritModal
    //{
    //}

    public class CorrectMeritSearchModel: RequestBaseModel
    {
        public int ApplicationID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int BranchID { get; set; }
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int CategoryD { get; set; }
        public int EndTermId { get; set; }
        public int AcademicYearID { get; set; }
        public string ApplicationNo { get; set; }
        public string action { get; set; }
        public string Gender { get; set; }
        public string IPAddress { get; set; }

    }

    public class BterMeritSearchModel
    {
        public int ApplicationId { get; set; }
        public int DepartmentID { get; set; }
        public string SSOID { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string? StudentName { get; set; }
        public string? EnrollmentNo { get; set; }

    }

   

    public class MeritDocumentScrutinyModel
    {
        public int ApplicationID { get; set; }
        public int MeritId { get; set; }
        public int VerifierID { get; set; }
        public string SSOID { get; set; }
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }
        public string FatherName { get; set; }
        public string FatherNameHindi { get; set; }
        public string MotherName { get; set; }
        public string MotherNameHindi { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string CertificateGeneratDate { get; set; }
        public string CasteCertificateNo { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string CategoryA { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string ENR_ID { get; set; }
        public int coursetype { get; set; }
        public bool IsRajasthani { get; set; }
        public int IsEws { get; set; }
        public string? AadharNo { get; set; }

        public int? TradeLevel { get; set; }
        public int? TradeID { get; set; }
        public int? InstituteID { get; set; }
        public int? DirectAdmissionTypeID { get; set; }
        public int? BranchID { get; set; }
        public string PrefentialCategoryType { get; set; }
        public string? Apaarid { get; set; }
        public string? DepartmentName { get; set; }
        public string? WhatsNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public string IndentyProff { get; set; }
        public string DetailID { get; set; }
        public string Maritial { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }

        public string? IsPH { get; set; }
        public string? IsKM { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int Prefential { get; set; }
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
        public int RoleID { get; set; }
        public int IsMBCCertificate { get; set; }

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
        public int status { get; set; }
        public string? Remark { get; set; }
        public int LateralCourseID { get; set; }
        public string? ApplicationNo { get; set; }
        public List<VerificationDocumentDetailList>? VerificationDocumentDetailList { get; set; }
        public List<MeritDocumentPushModel>? DocumentPushModel { get; set; }
        public List<RemarkModel>? RemarkModel { get; set; }

        public string? ActionRemarks { get; set; }
        public int? CategoryAbyChecker { get; set; }
        public string? CommonRemark { get; set; }
    }

    public class MeritDocumentPushModel
    {
        public int DocumentDetailsID { get; set; }
        public int DocumentMasterID { get; set; }
        public int TransactionID { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
        public string Remark { get; set; }
        public string FileName { get; set; }
        public string DisFileName { get; set; }
        public string Category { get; set; }

    }

    public class CorrectMerit_ApplicationSearchModel
    {
        public int? ApplicationID {  get; set; }
        public int? MeritID {  get; set; }
    }

    public class CorrectMeritApproveDataModel : RequestBaseModel
    {
       public int? ApplicationID {  set; get; }
       public int? MeritId {  set; get; }
       public int? AcademicYearID {  set; get; }
       public int? status {  set; get; }
       public int? ModifyBy {  set; get; }
        public string? ApplicationNo { set; get; }
        public string? Remark { set; get; }
        public string? IPAddress { set; get; }
    }
}
