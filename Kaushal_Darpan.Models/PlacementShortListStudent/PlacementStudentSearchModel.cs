
namespace Kaushal_Darpan.Models.PlacementShortListStudentMaster
{
    public class PlacementShortListStudentSearchModel
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
        public int DepartmentID { get; set; }
        public int Eng_NonEng {  get; set; }
    }

    public class StudentShortlistModel
    {
        public int UserID { get; set; }
        public int StudentID { get; set; }
        public int CampusID { get; set; }
        public bool IsShortListed { get; set; }
        public bool IsIsPlaced { get; set; }
        public int ModifyBy { get; set; }
        public string IPAddress { get; set; }
    }

    public class PlacementShortlistedStuSearch
    {
        public int UserId { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int BranchID { get; set; }
        public int RoleId { get; set; }
        public int CampusPostID { get; set; }
        public int? InstituteId { get; set; }
        public int? HiringRoleID { get; set; }

        public string? NotifyStatus {  get; set; }
        public int? FinancialYearID {  get; set; }
        
    }


    public class PlacementDynamicReportModel
    {
        public int? DepartmentID { get; set; } = 0;
        public int? Eng_NonEng { get; set; } = 0;// course type
        public int? EndTermID { get; set; } = 0;
        public int? TermPart { get; set; } = 0;
        public int? FinancialYearID { get; set; } = 0;
        public int? IsYearly { get; set; } = 0;
        public int? RoleID { get; set; } = 0;
        public int StudentEntryType { get; set; }
        public int InstituteId { get; set; } = 0;
        public int? ServiceID { get; set; } = 0;
        public int? UserID { get; set; } = 0;
        public string? ExamDate { get; set; }
        public int? CollageID { get; set; } = 0;
        public int? SessionTypeID { get; set; } = 0;
        public int SelectedInsituteID { get; set; } = 0;
        public int? SchemeID { get; set; } = 0;
        public int? CampusPostID { get; set; } = 0;

        public string? ActionFlag { get; set; }
        public string? NotifyStatus { get; set; }

    }


}
