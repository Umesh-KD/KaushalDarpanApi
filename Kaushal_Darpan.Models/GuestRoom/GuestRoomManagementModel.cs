
namespace Kaushal_Darpan.Models.GuestRoomManagementModel
{
    public class GuestRoomManagementDataModel
    {
        public int GuestHouseID { get; set; }
        public int DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DistrictID { get; set; }
        public int TehsilID { get; set; }

    }



    public class CreateGuestRoomDataModel
    {
        public int GuestHouseID { get; set; }
        public int DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public int PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public int DistrictID { get; set; }
        public int TehsilID { get; set; }

    }


    public class GuestRoomManagementSearchModel
    {
        public int? DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public int? PhoneNumber { get; set; }
        public int? Address { get; set; }


    }
    public class GuestRoomSeatDataModel
    {
        public int GRSMasterID { get; set; }
        public int DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public int RoomType { get; set; }
        public int RoomFee { get; set; }
        public int SeatCapacity { get; set; }
        public int RoomQuantity { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
    public class GuestRoomSeatSearchModel
    {
        public int? DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public int RoomType { get; set; }
        public int? SeatCapacity { get; set; }
        public int? RoomQuantity { get; set; }
    }
    public class GuestRoomFacilitiesDataModel
    {
        public int GFID { get; set; }
        public string? FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }
        public int DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
    public class GuestRoomFacilitiesSearchModel
    {
        public int? DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public string? FacilitiesName { get; set; }
        public int GFID { get; set; }
        public bool IsFacilities { get; set; }

    }
    public class GuestRoomDetailsDataModel
    {
        public int GFID { get; set; }
        public string? FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }
        public int DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public string? GuestHouseName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
    }
    public class GuestRoomDetailsSearchModel
    {
        public int? UserID { get; set; }
        public int? DepartmentID { get; set; }
        public int GuestHouseID { get; set; }
        public string? GuestHouseName { get; set; }
        public string? FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }
    }

    public class GuestApplyForGuestRoomDataModel
    {
        public int GuestHouseID { get; set; }
        public int GuestReqID { get; set; }
        public int UserID { get; set; }
        public int CollegeID { get; set; }
        public int DepartmentID { get; set; }
        public string? EmpID { get; set; }
        public string? RequestSSOID { get; set; }
        public string? EmpIDCardPhoto { get; set; }
        public string? Dis_EmpIDCardPhoto { get; set; }
        public string? IDProofNo { get; set; }
        public string? IDProofPhoto { get; set; }
        public string? Dis_IDProofPhoto { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public int Status { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }

        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }

        public string? Remark { get; set; }
        public string? Reason { get; set; }
        public int RoomType { get; set; }
        public int SeatCapacity { get; set; }
        public int RoomQuantity { get; set; }
        public int RoomFee { get; set; }
        public string? DepartmentName { get; set; }
        public string? InstituteName { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MailPersonal { get; set; }
        public string? MobileNo { get; set; }
        public string? PostalAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? State { get; set; }
        public int EndTermID { get; set; }
    }


    public class GuestApplyForGuestRoomSearchModel
    {
        public int GuestHouseID { get; set; }
        public int GuestReqID { get; set; }
        public int UserID { get; set; }
        public int CollegeID { get; set; }
        public int DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public string? FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }
        public int RoleID { get; set; }
        public int ModifyBy { get; set; }
        public int Status { get; set; }
    }

    public class GuestStaffProfileSearchModel
    {
        public int DepartmentID { get; set; }
        public string? SSOID { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
    }

    public class GuestHouseSearchModel
    {
        public int? DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public int? PhoneNumber { get; set; }
        public int? Address { get; set; }
    }

    public class GuestHouseDataModel
    {
        public int GuestHouseID { get; set; }
        public int DepartmentID { get; set; }
        public string? GuestHouseName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DistrictID { get; set; }
        public int TehsilID { get; set; }

    }
    public class RoomDetailsDataModel
    {
        public int DepartmentID { get; set; }
        public int GuestRoomDetailID { get; set; }
        public int GuestHouseID { get; set; }
        public int RoomTypeID { get; set; }
        public int RoomNo { get; set; }
        public int StudyTableFacilities { get; set; }
        public int AttachedBathFacilities { get; set; }
        public int FanFacilities { get; set; }
        public int CoolingFacilities { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }

    }

    public class StatusChangeGuestModel
    {
        public int PK_ID { get; set; }
        public int ModifyBy { get; set; }

    }

}

