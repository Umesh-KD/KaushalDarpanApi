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
        public string Enrollment { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int? PageNumber { get; set; }
        public int ? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }
        public string? ScholarshipMode { get; set; }
        public string? SchemeName { get; set; }
        public int? CourseType { get; set; }
        public int GenderID { get; set; }
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
        public string? ScholarshipMode { get; set; }
        
    }

    public class ScholarshipRequest
    {
        public string RequestType { get; set; }
        public string CollegeType { get; set; }
        public string RequestId { get; set; }
    }

    public class ScholarshipApiResponse
    {
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public List<ScholarshipData> data { get; set; }
        public string?  JsonData { get; set; }
        public string? Department { get; set; }


    }

    public class ScholarshipData
    {
        public string academic_Year { get; set; }
        public string applicationID { get; set; }
        public string applicationDate { get; set; }
        public string applicationStatus { get; set; }
        public string traineeName { get; set; }
        public string category { get; set; }
        public string sanctionedSchemeOrSchemeType { get; set; }
        public decimal scholarshipAmount { get; set; }
        public string dateOfDisbursement { get; set; }
        public string aadhaarOrJanAadhaar { get; set; }
        public string collegeCode { get; set; }
        public string collegeName { get; set; }
        public string collegeType { get; set; }
    }

    public class ScholarshipApiSearchDataModel
    {
        public string? CollegeType { get; set; }

        public string? CollegeCode { get; set; }

        public int? DepartmentID { get; set; }

        public int? AcademicYear { get; set; }
    }


}
