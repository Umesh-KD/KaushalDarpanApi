
namespace Kaushal_Darpan.Models.ITIPlacementStudentMaster
{
    public class ITIPlacementStudentSearchModel
    {
        public int CampusPostID { get; set; }
        public int CampusWiseHireRoleID { get; set; }
        public int InstituteID { get; set; }
        public int BranchID { get; set; }
        public int _10thPre { get; set; }
        public int _12thPre { get; set; }
        public int DiplomaPre { get; set; }
        public string FinancialYearID { get; set; }
        public int NoOfBack { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
    }

    public class ITICampusStudentConsentModel
    {

        public int ConsentID { get; set; } 
        public int PostID { get; set; } 
        public int StudentID { get; set; } 
        public string? SSOID { get; set; } 
        public string? Remarks { get; set; } 
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITIStudentConsentSearchmodel {
        public string? action { get; set; }
        public int PostID { get; set; }
        public int StudentID { get; set; }
        public string? SSOID { get; set; }
        public string? Status { get; set; }
        public int CompanyID { get; set; }
        public int CollegeID { get; set; }
        public int DepartmentID { get; set; }


    }

}
