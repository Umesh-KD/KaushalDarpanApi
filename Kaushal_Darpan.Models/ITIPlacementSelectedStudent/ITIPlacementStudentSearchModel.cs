
namespace Kaushal_Darpan.Models.ITIPlacementSelectedStudentMaster
{
    public class ITIPlacementSelectedStudentSearchModel
    {
        public int CampusPostID { get; set; }
        public int HiringRoleID { get; set; }
        //public int CampusWiseHireRoleID { get; set; }
        public int InstituteID { get; set; }
        public int BranchID { get; set; }
        public int _10thPre { get; set; }
        public int _12thPre { get; set; }
        public int DiplomaPre { get; set; }
        public string? FinancialYearID { get; set; }
        public int NoOfBack { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng {  get; set; }
    }

    public class ITIStudentSelectedModel
    {
        public int UserID { get; set; }
        public int StudentID { get; set; }
        public int CampusID { get; set; }
        public bool IsShortListed { get; set; }
        public bool IsIsPlaced { get; set; }
        public int ModifyBy { get; set; }
        public string IPAddress { get; set; }
    }



}
