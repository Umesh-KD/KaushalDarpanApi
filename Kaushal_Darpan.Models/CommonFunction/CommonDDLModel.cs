using System;
using System.Net;

namespace Kaushal_Darpan.Models.CommonFunction
{
    public class CommonDDLModel
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Unit { get; set; }
        public string? Quantity { get; set; }
        public int TypeID { get; set; }

    }

    public class ActiveTabModel
    {
        public string? ActiveTab { get; set; }
        public string? TabName { get; set; }
        public bool[]? ActiveTabAList { get; set; }

    }

    public class StreamDDL_InstituteWiseModel
    {
        public int InstituteID { get; set; }
        public int ApplicationID { get; set; }
        public string? action { get; set; }
        public int StreamType { get; set; }

    }

    public class DateSettingConfigModel
    {
        public int DepartmentID { get; set; }
        public int CourseTypeId { get; set; }
        public int AcademicYearID { get; set; }
        public int EndtermID { get; set; }
        public string Key { get; set; }
        public string SSOID { get; set; } = string.Empty;

    }

    public class SessionConfigModel
    {


        public int FinancialYearID { get; set; }
        public string FinancialYearName { get; set; }
        public int ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public Boolean IsCurrentFY { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public string Action { get; set; }
        public string EndTermName { get; set; }
        public string? Month { get; set; }
        public string? ExamType { get; set; }
    }


    public class PublicInfoModel
    {
        public int PublicInfoId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseTypeId { get; set; }
        public int AcademicYearId { get; set; }
        public int PublicInfoType { get; set; }
        public string DescriptionEn { get; set; } = "";
        public string DescriptionHi { get; set; } = "";
        public string LinkUrl { get; set; } = "";
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; } = "";
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; } = "";
        public string SortColumn { get; set; } = "";
        public string Actoin { get; set; } = "";
        public string FileName { get; set; } = "";
        public string Dis_FileName { get; set; } = "";


    }

    public class CenterSuperitendentDDL : RequestBaseModel
    {
        public int? InstituteID { get; set; }
    }

    public class CommonVerifierApiDataModel
    {
        public int VerifierID { get; set; }
        public string Name { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public string MobileNumber { get; set; }
        public string appName { get; set; }
        public string password { get; set; }



        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int CourseType { get; set; }
        public bool ShowAllApplication { get; set; }
    }
    public class NodalCenterModel
    {

        public int NodalId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseTypeId { get; set; }
        public string CenterName { get; set; }
        public string CenterCode { get; set; }
        public string OfficerSSOID { get; set; }
        public string OfficerName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }


    }

    public class CommonSignatureModel
    {

        public int SignatureId { get; set; }
        public int TypeId { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public string Action { get; set; }
        public string SignatureFile { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }

    }

    public class BterCommonSignatureModel
    {

        public int SignatureId { get; set; }
        public int TypeId { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public string Action { get; set; }
        public string SignatureFile { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }

    }
    public class UnPublishDataModel
    {

        public int UnPubishBy { get; set; }
        public string UnPublishReason { get; set; }
        public string UnPubishAttachment { get; set; }
        public string UnPublishIPAddress { get; set; }
        public int AllotmentMasterId { get; set; }
        public int AcademicYearID { get; set; }
        public int CourseTypeId { get; set; }
        public string Action { get; set; }
    }



    public class DDL_RWHEffectedEndTermModel    {

        public int DepartmentID { get; set; }
        public int CourseType { get; set; }
        public int ResultTypeID { get; set; }
        public int EndTermID { get; set; }
       
    }

}
