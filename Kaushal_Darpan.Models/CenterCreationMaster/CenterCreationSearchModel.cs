namespace Kaushal_Darpan.Models.CenterCreationMaster
{
    public class CenterCreationSearchModel
    {
        public int? DistrictID { get; set; }
        public int? InstitutionCategoryID { get; set; }
        public int? TehsilID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CenterID { get; set; }
        public int Eng_NonEng { get; set; }
    }
    public class CenterSuperintendentDetailsModel :RequestBaseModel
    {
        public int? CenterAssignedID { get; set; }
        public int? CenterID { get; set; }
        public int? InsituteID { get; set; }
        public int UserID { get; set; }
        public string? IPAddress { get; set; } = string.Empty;
        public int? CreatedBy { get; set; }
    }

    public class ITIAssignPracticaLExaminer : RequestBaseModel
    {
        public int? CenterAssignedID { get; set; }
        public int? CenterID { get; set; }
        public int? InsituteID { get; set; }
        public int UserID { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SSOID { get; set; } = string.Empty;   
        public string Name { get; set; } 
        public int? CreatedBy { get; set; }
    }



}