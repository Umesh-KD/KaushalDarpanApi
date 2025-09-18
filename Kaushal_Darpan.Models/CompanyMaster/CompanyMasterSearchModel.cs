namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class CompanyMasterSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
    }

    public class EligibleStudentListMasterSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }

    }
}
