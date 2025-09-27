namespace Kaushal_Darpan.Models.CollegeWiseScholarship
{
    //public class CompanyMasterSearchModel
    //{
    //    public string Name { get; set; }
    //    public string Status { get; set; }
    //    public int ModifyBy { get; set; }
    //    public int RoleID { get; set; }
    //    public int DepartmentID { get; set; }
    //}

    //public class EligibleStudentListMasterSearchModel
    //{
    //    public string Name { get; set; }
    //    public string Status { get; set; }
    //    public int ModifyBy { get; set; }
    //    public int RoleID { get; set; }
    //    public int DepartmentID { get; set; }
    //    public int InstituteID { get; set; }

    //    public int? PageNumber { get; set; }
    //    public int? PageSize { get; set; }
    //    public string? SortOrder { get; set; }
    //    public string? SortColumn { get; set; }

    //}

    public class CollegeWiseScholarshipSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int? PageNumber { get; set; }
        public int ? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }

    }

    //public class EligibleStudentForPlacement
    //{
    //    public int ID { get; set; }

    //}

    public class SaveCollegeWiseScholershipDetails
    {
        public int ID { get; set; }
        public int SchemeID { get; set; }
        public string SchemeName { get; set; }
        public string? ScholarShipAmount { get; set; }
        public string ScholarShipApprovalID { get; set; }
        public DateTime ScholarShipDate { get; set; }
        public int ScholarShipTypeID { get; set; }
        public string ScholarShipTypeName { get; set; }
        public int StudentID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }


}
