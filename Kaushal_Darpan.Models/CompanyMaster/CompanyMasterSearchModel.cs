namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class CompanyMasterSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }

        public int? ID {get;set;}
    }

    public class EligibleStudentListMasterSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int? AcademicYearID { get; set; }

        public int? StreamID { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }

    }

    public class EligibleStudentForPlacement
    {
        public int ID { get; set; }

    }


    public class PlacementStudentListSearchModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }

        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }

        public string? action { get; set; }

    }



}
