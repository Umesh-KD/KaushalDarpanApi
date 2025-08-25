namespace Kaushal_Darpan.Models.RoleMaster
{
    public class RoleMasterModel
    {
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int LevelID { get; set; }
        public int DesignationID { get; set; }
        public string? RoleNameEnglish { get; set; }
        public string? RoleNameHindi { get; set; }
        public string? RoleNameShort { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
        public int CourseTypeID { get; set; }
        public int DepartmentID { get; set; }
    }

    public class RoleSearchModel
    {
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
    }
    public class RoleListRequestModel : RequestBaseModel
    {
        public string SSOID { get; set; }
        public bool IsWeb { get; set; } = false;
    }
}
