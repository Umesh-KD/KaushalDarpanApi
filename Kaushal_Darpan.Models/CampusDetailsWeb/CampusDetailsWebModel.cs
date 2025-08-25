namespace Kaushal_Darpan.Models.CampusDetailsWeb
{
    public class CampusDetailsWebModel
    {
        public int PostID { get; set; }
        public string PostNo { get; set; }
        public int PostCollegeID { get; set; }
        public int RoleID { get; set; }
        public string PostSSOID { get; set; }
        public int CompanyID { get; set; }
        public string Website { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string HR_Name { get; set; }
        public string HR_MobileNo { get; set; }
        public string HR_Email { get; set; }
        public string HR_SSOID { get; set; }
        public string CampusVenue { get; set; }
        public string CampusFromDate { get; set; }
        public string CampusFromTime { get; set; }
        public string CampusToDate { get; set; }
        public string CampusToTime { get; set; }
        public string CampusAddress { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
        public List<CampusPostMaster_EligibilityCriteria> EligibilityCriteriaModel { get; set; }
    }
    public class CampusPostMaster_EligibilityCriteria
    {

        public int AID { get; set; }
        public int PostID { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int PassingYear { get; set; }
        public int SemesterID { get; set; }
        public string SemesterName { get; set; }
        public float MinPre_10 { get; set; }
        public float MinPre_12 { get; set; }
        public float MinPre_Diploma { get; set; }
        public int NoofBackPapersAllowed { get; set; }
        public string AgeAllowedFrom { get; set; }
        public string AgeAllowedTo { get; set; }
        public int HiringRoleID { get; set; }
        public string HiringRoleName { get; set; }
        public int NoofPositions { get; set; }
        public string CTC { get; set; }
        public string OtherBenefit { get; set; }
        public string CampusType { get; set; }
        public string InterviewType { get; set; }
        public int NoOfInterviewRound { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string Dis_AgeAllowedFrom { get; set; }
        public string Dis_AgeAllowedTo { get; set; }
    }

    public class CampusPostMaster_Action
    {
        public int PostID { get; set; }
        public string Action { get; set; }
        public string ActionRemarks { get; set; }
        public int ActionBy { get; set; }
    }
}
