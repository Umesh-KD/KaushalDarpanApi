namespace Kaushal_Darpan.Models.CollegeMaster
{
    public class CollegeMasterModel
    {
        public int InstituteID { get; set; }
        public int Capacity { get; set; }
        public string InstituteCode { get; set; }
        public string InstitutionDGTCode { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string InstituteNameHindi { get; set; }
        public int CollegeTypeID { get; set; }
        public int TypeID { get; set; }
        public int? CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? SSOID { get; set; }
        public string? Email { get; set; }
        public string? FaxNumber { get; set; }
        public string? Website { get; set; }
        public string? LandNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? LandlineStd { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public string? Address { get; set; }
        public string? PinCode { get; set; }
        public int TehsilID { get; set; }
        public int InstitutionManagementTypeID { get; set; }
        public int InstitutionCategoryID { get; set; }
        public int? TimeTableID { get; set; }
        public int DepartmentID { get; set; }
        public int FinancialYearId { get; set; }
        public int EndTermID { get; set; }
        public bool IsPayment { get; set; }
        public bool IsProfileComplete { get; set; }

    }

    public class CollegeMasterRequestModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public string SSOID { get; set; }
    }
}
