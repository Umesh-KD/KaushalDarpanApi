namespace Kaushal_Darpan.Models.UserMaster
{


    public class AdminUserDetailModel
    {
        public int AID { get; set; }
        public int UserID { get; set; }
        public int UserAdditionID { get; set; }
        public int ProfileID { get; set; }
        public string Name { get; set; }
        public int LevelID { get; set; }
        public int DesignationID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int RoleID { get; set; }
        public string AadhaarID { get; set; }
        public string SSOID { get; set; }
        public string Email { get; set; }
        public string EmailOfficial { get; set; }
        public string State { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int InstituteID { get; set; }
        public bool? IsCitizenQueryUser { get; set; }
        public int? QueryType { get; set; }
        public int? CourseType { get; set; }
        public int? UserRole { get; set; }
    }
    public class AdminUserSearchModel : RequestBaseModel
    {
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int UserAdditionID { get; set; }
        public int ProfileID { get; set; }
        //public int RoleID { get; set; }
        public string? Name { get; set; }
        public string? SSOID { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int? UserRole { get; set; }

    }


    public class AssignHodBranch
    {
        public int ModifyBy { get; set; }
        public int UserID { get; set; }
        public int UserAdditionalID { get; set; }
        public int InstituteID { get; set; }
        public int CourseTypeID { get; set; }
        public List<Branchlist> Branchlist { get; set; }
        public int RoleID { get; set; }
        public int? UserRole { get; set; }


    }

    public class Branchlist
    {
        public int StreamID { get; set; }
        public string? StreamName { get; set; }
    }

    public class StreamMasterForHodModel : RequestBaseModel
    {
        public int StreamType { get; set; }
        public int UserAdditionID { get; set; }
        public int InstituteID { get; set; }
    }



}
