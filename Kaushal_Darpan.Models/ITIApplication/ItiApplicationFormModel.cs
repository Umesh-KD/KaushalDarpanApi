using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIApplication
{
    internal class ItiApplicationFormModel
    {
        
    }

    public class PersonalDetailsDataModel
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
        public string WhatsNumber { get; set; }
        public string LandlineNumber { get; set; }
        public int IndentyProff { get; set; }
        public string DetailID { get; set; }
        public int Maritial { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int Prefential { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }

        public int ParentIncome { get; set; }
        public bool IsMinority { get; set; }
        public int IsEWSCategory { get; set; }
        public int Eligible8thTradesID { get; set; }
        public int Eligibl10thTradesID { get; set; }
        public int PWDCategoryID { get; set; }
        public int DirectAdmissionType { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public bool IsTSP { get; set; }
        public bool IsSaharia { get; set; }
        public int TspDistrictID { get; set; }
        public int IsDevnarayan { get; set; }
        public int DevnarayanTehsilID { get; set; }
        public int DevnarayanDistrictID { get; set; }
        public int TSPTehsilID { get; set; }
        public string? PH8thTradeList { get; set; }
        public string? PH10thTradeList { get; set; }
        public string? Apaarid { get; set; }
        public int Age { get; set; }
        public int IsFinalSubmit { get; set; }
        public bool IsPaymentSuccess { get; set; }
    }
    public class PHTradeList
    {
        public int Id { get; set; }
    }

    public class DocumentDetailsDataModel
    {
        public int ApplicationID { get; set; }
        public string? StudentPhoto { get; set; }
        public string? Dis_StudentPhoto { get; set; }
        public string? SignaturePhoto { get; set; }
        public string? Dis_SignaturePhoto { get; set; }
        public string? Marksheet10thPhoto { get; set; }
        public string? Dis_Marksheet10thPhoto { get; set; }
        public string? AadharPhoto { get; set; }
        public string? Dis_AadharPhoto { get; set; }
        public string? AffidavitPhoto { get; set; }
        public string? Dis_AffidavitPhoto { get; set; }
        public string? WKA_CertificatePhoto { get; set; }
        public string? Dis_WKA_CertificatePhoto { get; set; }
        public string? Marksheet8thPhoto { get; set; }
        public string? Dis_Marksheet8thPhoto { get; set; }
        public int DepartmentID { get; set; }
        public string? SSOID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public List<ItiDocumentDetailList>? ItiDocumentDetailList { get; set; }
        public List<DocumentDetailsModel>? DocumentDetails { get; set; }
    }

    public class ItiDocumentDetailList
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

    public class OptionDetailsDataModel
    {
        public int OptionID { get; set; }
        public int ApplicationID { get; set; }
        public int ManagementTypeID { get; set; }
        public string? ManagementTypeName { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? TradeName { get; set; }
        public int Priority { get; set; }
        public int TradeLevel { get; set; }
        public int TradeTypeId { get; set; }
        public string? Type { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int AcademicYear { get; set; }
        public string? MinPercentageInMath {get; set;}
        public string? MinPercentageInScience { get; set;}
    }

    public class QualificationDetailsDataModel
    {
        public int ApplicationQualificationId { get; set; }
        public int ApplicationID { get; set; }
        public string? SSOID { get; set; }
        public int DepartmentID { get; set; }
        
        public int StateID {  get; set; }
        public string? BoardUniversity { get; set; }
        public string? SchoolCollege { get; set; }
        public string? Qualification { get; set; }
        public int YearofPassing { get; set; }
        public string? RollNumber { get; set; }
        public int MarksTypeID { get; set; }
        public float MaxMarks { get; set; }
        public float MarksObtained { get; set; }
        public float Percentage {  get; set; }
        public float MathsMaxMarks { get; set; }
        public float MathsMarksObtained { get; set; }
        public float ScienceMaxMarks { get; set; }
        public float ScienceMarksObtained { get; set; }
        public string? UniversityBoard {  get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }

    public class HighestQualificationDetailsDataModel
    {
        public int StateID { get; set; }
        public string BoardUniversity { get; set; }
        public string SchoolCollege { get; set; }
        public string HighestQualification {  get; set; }
        public string YearofPassing { get; set; }
        public  string RollNumber { get; set; }
        public  int MarksTypeID { get; set; }
        public int MaxMarks {  get; set; }
        public int MarksObtained { get; set; }
        public string Percentage {  get; set; }
    }

    public class Qualification8thDetailsDataModel
    {
        public int SelectedQualification8 {  get; set; }
        public int StateID8 {  get; set; }
        public string SchoolCollege8 { get; set; }
        public  string YearofPassing8 {  get; set; }
        public  string RollNumber8 { get; set; }
        public int MarksTypeID8 { get; set; }
        public int MaxMarks8 { get; set; }
        public int MarksObtained8 { get; set; }
        public string Percentage8 {  get; set; }
    }

    public class Qualification10thDetailsDataModel
    {
        public int SelectedQualification10 { get; set; }
        public int StateID10 { get; set; }
        public string BoardUniversity10 { get; set; }
        public string YearofPassing10 { get; set; }
        public string RollNumber10 { get; set; }
        public int MarksTypeID10 { get; set; }
        public int MaxMarks10 { get; set; }
        public int MarksObtained10 { get; set; }
        public int MathsMaxMarks10 { get; set; }
        public int MathsMarksObtained10 { get; set; }
        public int ScienceMaxMarks10 { get; set; }
        public int ScienceMarksObtained10 { get; set; }
        public string Percentage10 { get; set; }
    }

    public class AddressDetailsDataModel
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

        public int ModifyBy {  get; set; }
        public int DepartmentID { get; set; }
        public string SSOID { get; set; }
        public string NonRajasthanBlockName { get; set; } = string.Empty;
    }

    public class ItiApplicationSearchModel
    {
        public int DepartmentID { get; set; }
        public string? SSOID { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public int ApplicationID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public string? StudentName{ get; set; }


        public int ApplicationId { get; set; }
        public int EndTermID { get; set; }
      
        public string? EnrollmentNo { get; set; }
    }

    public class ItiApplicationUnlockDataModel: RequestBaseModel
    {
        public string? SSOID  { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public int? ApplicationID { get; set; }
        public string? StudentName { get; set; }
        public int? RoleID { get; set; }
        public int? ApplicationId { get; set; }
        public int? UserID { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITI_DirectAdmissionApplyDataModel: RequestBaseModel
    {
        public int? ApplicationID { get; set; }
    }
}
