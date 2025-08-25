namespace Kaushal_Darpan.Models.CampusPostMaster
{
    public class CampusPostMasterModel
    {
        public int PostID { get; set; }
        public string PostNo { get; set; }
        public int PostCollegeID { get; set; }
        public int RoleID { get; set; }
        public string PostSSOID { get; set; }
        public int CompanyID { get; set; }
        public int CompanyTypeID { get; set; }
        public string Website { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string HR_Name { get; set; }
        public string HR_MobileNo { get; set; }
        public string HR_Email { get; set; }
        public string? HR_SSOID { get; set; }
        public string CampusVenue { get; set; }
        public string CampusVenueLocation { get; set; }
        public string CampusFromDate { get; set; }
        public string CampusFromTime { get; set; }
        public string CampusToDate { get; set; }
        public string CampusToTime { get; set; }
        public string CampusAddress { get; set; }
        public string JobDiscription { get; set; }
        public string Dis_JobDiscription { get; set; }
        public string CompanyName { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
        public int DepartmentID { get; set; }
        public int CampusPostType { get; set; }
        public List<CampusPostMaster_EligibilityCriteria> EligibilityCriteriaModel { get; set; }
    }
    public class CampusPostMaster_EligibilityCriteria
    {

        public int AID { get; set; }
        public int PostID { get; set; }
        public int SalaryTypeID { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int PassingYear { get; set; }
        public int ToPassingYear { get; set; }
        public int SemesterID { get; set; }
        public string? SemesterName { get; set; }
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
        public string SalaryRemark { get; set; }
        public string Gender { get; set; }
        public string OtherBenefit { get; set; }
        public string CampusType { get; set; }
        public string InterviewType { get; set; }
        public int NoOfInterviewRound { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string Dis_AgeAllowedFrom { get; set; }
        public string Dis_AgeAllowedTo { get; set; }
        public string? SalaryName { get; set; }
        public int EligibleInstitutesID { get; set; }
    }

    public class CampusPostMaster_Action
    {
        public int PostID { get; set; }
        public string Action { get; set; }
        public string ActionRemarks { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int CompanyID { get; set; }
        public int PostCollegeID { get; set; }
        public string Dis_SuspendDoc { get; set; }
        public string SuspendDocumnet { get; set; }

    }

    public class SignedCopyOfResultModel
    {
        public int SignedCopyOfResultID { get; set; }
        public int CampusPostID { get; set; }
        public int HRID { get; set; }
        public int CompanyID { get; set; }
        public string Remark { get; set; }
        public string FileName { get; set; }
        public string Dis_File { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyDate { get; set; }
        public string IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int FileTypeID { get; set; }
    }
    public class SignedCopyOfResultSearchModel
    {
        public int SignedCopyOfResultID { get; set; }
        public int CampusPostID { get; set; }
        public int HRID { get; set; }
        public int CompanyID { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public int RoleID { get; set; }

    }


    public class CampusPostQRDetail
    {
        public string Address { get; set; }              
        public string CampusAddress     { get; set; }    
        public string CampusFromDate     { get; set; }   
        public string CampusToDate     { get; set; }     
        public string CampusTypeClass     { get; set; }  
        public string CampusVenue     { get; set; }      
        public string CompanyAddress     { get; set; }   
        public int CompanyID     { get; set; }           
        public string CompanyName     { get; set; }      
        public int DistrictID     { get; set; }          
        public string HR_Email     { get; set; }         
        public string HR_MobileNo     { get; set; }      
        public string HR_Name     { get; set; }          
        public string InstituteCode     { get; set; }    
        public int PostCollegeID     { get; set; }       
        public int PostID     { get; set; }              
        public string PostNo     { get; set; }           
        public string RoleDetails     { get; set; }      
        public int StateID     { get; set; }             
        public string Status     { get; set; }           
        public string TPOCollegeName     { get; set; }   
        public string TPOSSOID     { get; set; }         
        public string Website     { get; set; }          
}

}
