using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;

namespace Kaushal_Darpan.Models.UserMaster
{


    public class UserRequestModel
    {
        public int AID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string AadhaarID { get; set; }
        public int DesignationID { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int DepartmentID { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int InstituteID { get; set; }
        public string UserStatus { get; set; }
        public int RoleID { get; set; }
        public int ServiceRequestId { get; set; }
    }

    public class UserSearchModel
    {
        public string UserStatus { get; set; }
    }


    public class CreatePrincipalModel
    {
        public int CollegeID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string CollegeCode { get; set; } = string.Empty;


    }

    public class RequestSearchModel
    {
        public int ServiceRequestId { get; set; } = 0;
        public int RequestId { get; set; } = 0;
        public int RequestType { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = "";
        public string SSOID { get; set; } = "";
        public int StatusId { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string SearchText { get; set; } = "";
        public int PostID { get; set; } = 0;
        public int OfficeID { get; set; } = 0;
        public int LevelID { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int DesignationID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public string RequestRemarks { get; set; } = "";
        public string OrderNo { get; set; } = "";
        public string OrderDate { get; set; } = "";
        public string JoiningDate { get; set; } = "";
        public string RequestDate { get; set; } = "";
        public int CreatedBy { get; set; } = 0;
        public string IPAddress { get; set; } = "";
        public string Action { get; set; } = "";
        public string AttachDocument_fileName { get; set; } = "";
        public string AttachDocument_file { get; set; } = "";
        public int NodalStateID { get; set; } = 0;
        public int NodalDistrictID { get; set; } =0;
        public int DivisionID { get; set; } =0;
        public int StaffTypeID { get; set; } =0;
        public int ReqRoleID { get; set; } =0;

       
    }


    #region ds


    public class ITI_EM_UnlockProfileDataModel
    {
        public int? StaffUserID { get; set; }
        public int? StaffID { get; set; }
        public string? SSOID { get; set; }
        public int? ModifyBy { get; set; }
    }

    #endregion end 

    //--------------------------------------------------------Bter--------------------
    public class BterRequestSearchModel
    {
        public int ServiceRequestId { get; set; } = 0;
        public int RequestId { get; set; } = 0;
        public int RequestType { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = "";
        public string SSOID { get; set; } = "";
        public int StatusId { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public string SearchText { get; set; } = "";
        public int PostID { get; set; } = 0;
        public int OfficeID { get; set; } = 0;
        public int LevelID { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int DesignationID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public string RequestRemarks { get; set; } = "";
        public string OrderNo { get; set; } = "";
        public string OrderDate { get; set; } = "";
        public string JoiningDate { get; set; } = "";
        public string RequestDate { get; set; } = "";
        public int CreatedBy { get; set; } = 0;
        public string IPAddress { get; set; } = "";
        public string Action { get; set; } = "";
        public string AttachDocument_fileName { get; set; } = "";
        public string AttachDocument_file { get; set; } = "";
        public int NodalStateID { get; set; } = 0;
        public int NodalDistrictID { get; set; } = 0;
        public int DivisionID { get; set; } = 0;
        public int StaffTypeID { get; set; } = 0;
        public int ReqRoleID { get; set; } = 0;

        public bool IsPensionable { get; set; }
        public string NumberOfPensionable { get; set; }

        //public bool IsEOL { get; set; }
        //public DateTime? EOLFromDate { get; set; }
        //public DateTime? EOLToDate { get; set; }
        //public bool IsEnquiries { get; set; }
        //public string Comments { get; set; }
        //public bool IsAccount { get; set; }
        //public  bool AccountComments { get; set; }


    }

}
