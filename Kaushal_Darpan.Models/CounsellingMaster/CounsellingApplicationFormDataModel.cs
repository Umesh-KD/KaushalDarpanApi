using Kaushal_Darpan.Models.DocumentDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Models.CounsellingMaster
{
    public class CounsellingApplicationFormDataModel
    {
        public int? CandidateID { get; set; }
        public string? SSOID { get; set; }
        public string? CandidateName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public int GenderId { get; set; }
        public string? DOB { get; set; }
        public int CategoryA_ID { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int BlockID { get; set; }
        public string? Pincode { get; set; }
        public string? AadharNo { get; set; }
        public string? JanAadharNo { get; set; }
        public string? JanAadharMobileNo { get; set; }
        public string? JanAadharName { get; set; }
        public string? JanAadharMemberId { get; set; }
        public string? Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public string? CourseType { get; set; }
        public int ProfileStatus { get; set; }
        public string? ApplicationNo { get; set; }
        public int ReligionID { get; set; }
        public int NationalityID { get; set; }
        public int MaritalID { get; set; }
        public int PWDCategoryID { get; set; }
        public bool IsMinority { get; set; }
        public int IsFinalSubmit { get; set; }
        public string? DepartmentName { get; set; }
        public string? SubmittedStep { get; set; }
        public string? RollNumber { get; set; }
        public string? Designation { get; set; }
        public string? Trade { get; set; }
        public string? MeritNo { get; set; }
        public int SelectionCategoryID { get; set; } = 0;
        public bool IsTSP { get; set; } = false;
        public int HomeDistrictID { get; set; } = 0;
        public bool IsPH { get; set; } = false;
        public bool IsExServicemen { get; set; } = false;
        public bool IsSportsPerson { get; set; } = false;
        public bool IsSpouseInSameService { get; set; } = false;
        public bool IsShahidDependent { get; set; } = false;
        public bool IsAnyIncurableDiseases { get; set; } = false;
    }

    public class CounsellingApplicationSearchModel
    {
        public int? CandidateId { get; set; }
        public int? DepartmentID { get; set; }
        public string? SSOID { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public string? CandidateName { get; set; }
        public string? MobileNo { get; set; }
        public string? AadharNo { get; set; }
        public string? DOB { get; set; }
        public string? Action { get; set; }

    }

    public class CounsellingOptionFormDataModel : RequestBaseModel
    {
        public int? OptionID { get; set; }
        public int? Priority { get; set; }
        public int? CandidateID { get; set; }
        public int? TradeId { get; set; }
        public string? TradeName { get; set; }
        public int? InstituteID { get; set; }
        public int? CourseType { get; set; }
        public int? ModifyBy { get; set; }
        public string? Type { get; set; }
        public List<InstituteListDataModel_Coun>? InstituteList { get; set; }
    }

    public class InstituteListDataModel_Coun
    {
        public int? InstituteOptionID { set; get; }
        public int? OptionID { set; get; }
        public int? InstituteID { set; get; }
        public string? InstituteName { get; set; }
        public int? Priority {  set; get; }
        public string? Type { get; set; }
    }

    public class Counselling_DropdownDataModel : RequestBaseModel
    {
        public string? Action {  get; set; }
        public int? TradeID { get; set; }
        public int? InstituteID { get; set; }
    }

    public class Counselling_DocumentDataModel
    {
        public int? CandidateID { get; set; }
        public int? ModifyBy { get; set; }
        public int? DepartmentID { get; set; }
        public int? IsFinalSubmit { get; set; }
        public List<Counselling_DocumentDetailList>? Counselling_DocumentDetailList { get; set; }
        public List<Counselling_DocumentDetailsModel>? Counselling_DocumentDetails { get; set; }
    }

    public class Counselling_DocumentDetailList
    {

        public int? CandidateID { get; set; }
        public string? ColumnName { get; set; }
        public string? TableName { get; set; }
        public string? FileName { get; set; }
        public string? DisFileName { get; set; }
        public string? Folder { get; set; }
        public int? DocumentMasterID { get; set; }
        public int? ModifyBy { get; set; }
    }

    public class CounsellingApplicationPreviewDataModel
    {
        public int CandidateID { get; set; }
        public string? ApplicationNo { get; set; }
        public string? CandidateName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? AadharNo { get; set; }
        public string? MaritalStatusName { get; set; }
        public string? Religion { get; set; }
        public string? DOB { get; set; }
        public string? Email { get; set; }
        public string? CategoryA { get; set; }
        public string? MobileNo { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? Pincode { get; set; }
        public string? Age { get; set; }
        public string? StudentPhoto { get; set; }
        public string? SignaturePhoto { get; set; }
        public int? ProfileStatus { get; set; }
        public int? IsfinalSubmit { get; set; }
        public int? ServiceID { get; set; }
        public int? DepartmentID { get; set; }
        public int? CourseTypeID { get; set; }
        public int? UniqueServiceID { get; set; }
        public List<OptionviewData_Counselling>? OptionViewData { get; set; }
        public List<PendingDataModel_Counselling>? PendingDataModel { get; set; }
        public List<Counselling_DocumentDetailsModel>? DocumentDetailList { get; set; }
        public int? CategoryAId { get; set; }
        public int? GenderId { get; set; }
        public string? Nationality { get; set; }
        public string? Category_E { get; set; }
    }

    public class OptionviewData_Counselling
    {
        public string? InstituteName { get; set; }
        public int? TradeId { get; set; }
        public int? Priority { get; set; }
        public string? TradeName { get; set; }
    }

    public class PendingDataModel_Counselling
    {
        public string Pending { get; set; }
        public int Index { get; set; }
    }

}
