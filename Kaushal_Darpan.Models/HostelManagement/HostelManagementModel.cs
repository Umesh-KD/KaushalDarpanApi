
namespace Kaushal_Darpan.Models.HostelManagementModel
{
    public class HostelManagementDataModel
    {
        public int HostelID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public string HostelName { get; set; }
        public int HostelType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
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


    public class HostelManagementSearchModel
    {
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public string? HostelName { get; set; }
        public int? HostelType { get; set; }
        public int? PhoneNumber { get; set; }
        public int? Address { get; set; }


    }
    public class HostelStudentSearchModel
    {
        public int EndTermID { get; set; }
        public int StudentID { get; set; }
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int ReqId { get; set; }
        public int DepartmentID { get; set; }
        public int PartnerApplicationID { get; set; }
        public string? Action { get; set; }


    }
    public class EditHostelStudentSearchModel
    {
        public int EndTermID { get; set; }
        public int StudentID { get; set; }
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int DepartmentID { get; set; }
        public int PartnerApplicationID { get; set; }
        public string? Action { get; set; }
        public int RoleID { get; set; }
        public int ReqId { get; set; }


    }

    public class HostelRoomSeatDataModel
    {
        public int HRSMasterID { get; set; }
        public int HostelID { get; set; }
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

    public class HostelRoomSeatSearchModel
    {
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public int HostelID { get; set; }
        public int RoomType { get; set; }
        public int? SeatCapacity { get; set; }
        public int? RoomQuantity { get; set; }


    }
    public class StudentApplyHostelData
    {
        public int ReqId { get; set; } 
        public int StudentID { get; set; } 
        public int PartnerApplicationID { get; set; }
        public string? FatherContactNo { get; set; }
        public string? LocalGuardianName { get; set; } 
        public string? LocalGuardianContactNo { get; set; }
        public int AllotedHostelLastEndTerm { get; set; }
        public int AllotedHostelInLastSessionRoomNo { get; set; } 
        public int AllotedHostelInLastSessionFeeDetails { get; set; } 
        public int AnyWorningForShortOfAttendance { get; set; }
        public int AnyWarningForInvovementAgainstDiscipline { get; set; }
        public string? RoomPartnerName { get; set; }
        public string? RoomPartnerYear { get; set; }
        public string? RoomPartnerBranch { get; set; }
        public string? RoomPartnerSFS { get; set; }
        public string? RoomPartnerRegular { get; set; } 
        public int EndTermId { get; set; } 
        public int HostelID { get; set; } 
        public int DepartmentID { get; set; } 
        public string? AffidavitDocument { get; set; } 
        public string? dis_AffidavitDocument { get; set; }
        public string? SupportingDocument { get; set; }
        public string? dis_SupportingDocument { get; set; }
        public decimal TotalAvg { get; set; }

        public int CurrentEndTermId { get; set; }
        public int CourseTypeID { get; set; }

    }


    public class HostelFacilitiesDataModel
    {
        public int HFID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public int WaterCooler { get; set; }
        public int RoWater { get; set; }
        public int NearbyMarket { get; set; }
        public string? MarketDistance { get; set; }
        public int PlayGround { get; set; }
        public string? PlayGroundDistance { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }

    }

    public class HostelFacilitiesSearchModel
    {
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public int HostelID { get; set; }
       // public int HFID { get; set; }
        public int WaterCooler { get; set; }
        public int RoWater { get; set; }
        public int NearbyMarket { get; set; }
        public string? MarketDistance { get; set; }
        public int PlayGround { get; set; }
        public string? PlayGroundDistance { get; set; }
        public string FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }

    }

    public class CollegeHostelDetailsDataModel
    {
        public int HFID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int HostelID { get; set; }
        public string HostelName { get; set; }
        public int HostelType { get; set; }
        public int WaterCooler { get; set; }
        public int RoWater { get; set; }
        public int NearbyMarket { get; set; }
        public string MarketDistance { get; set; }
        public int PlayGround { get; set; }
        public string PlayGroundDistance { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }

        public string FacilitiesName { get; set; }
        public bool IsFacilities { get; set; }

    }

    public class CollegeHostelDetailsSearchModel
    {
        public int? UserID { get; set; }
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public int HostelID { get; set; }
        public string HostelName { get; set; }
        public int HostelType { get; set; }
        public int WaterCooler { get; set; }
        public int RoWater { get; set; }
        public int NearbyMarket { get; set; }
        public string MarketDistance { get; set; }
        public int PlayGround { get; set; }
        public string PlayGroundDistance { get; set; }

        public string FacilitiesName { get; set; }

        public bool IsFacilities { get; set; }
    }



    public class StatusChangeModel
    {
        public int PK_ID { get; set; }
        public int ModifyBy { get; set; }
    }


}

