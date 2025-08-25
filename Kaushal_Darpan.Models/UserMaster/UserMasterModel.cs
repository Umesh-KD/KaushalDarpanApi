namespace Kaushal_Darpan.Models.UserMaster
{


    public class UserMasterModel
    {
        public int AID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int LevelID { get; set; }
        public int DesignationID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
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
        public string? Vertical { get; set; }
        public int InstituteID { get; set; }
        public int DepartmentID { get; set; }
    }
    public class UserMasterSearchModel
    {
        public int DepartmentID { get; set; }
    }

    public class PrincipleUpdateInstituteIDModel
    {
        public int DepartmentID { get; set; }
        public int ID { get; set; }
        public int CourseType { get; set; }
        public int RoleID { get; set; }
        public string json_Data { get; set; }

    }

}
