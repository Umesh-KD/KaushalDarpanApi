using Kaushal_Darpan.Models.ApplicationData;

namespace Kaushal_Darpan.Models.CompanyMaster
{
    public class IndustryInstitutePartnershipMasterModels
    {
        public int ID { get; set; }
        public int? CompanyID { get; set; }
        public int? PlacementCompanyID { get; set; }
        public string? Name { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int DepartmentID { get; set; }
        public string? CompanyPhoto { get; set; }
        public string? Dis_CompanyName { get; set; }
        public string? CompanyDocument { get; set; }
        public string? Dis_DocName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
        public int EventTypeID { get; set; }
        public string? Logo { get; set; }
        public string? Dis_Logo { get; set; }
        public List<ConcernPersonDetailsDataModel>? ConcernPersonDetails { get; set; }
    }


    public class IndustryInstitutePartnershipMaster_Action
    {
        public int ID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }

        public int EventTypeID { get; set; }
    }

    public class IndustryTrainingMaster
    {
        public int IndustryTRID { get; set; }
        public int IndustryID { get; set; }
        public int EventTypeID { get; set; }
        public DateTime EventDate { get; set; }
        public int SemesterID { get; set; }
        public string Purpose { get; set; }   
        public int TradeID { get; set; }   
        public int DepartmentID { get; set; }   
        public bool ActiveStatus { get; set; }   
        public bool DeleteStatus { get; set; }   
        public int CreatedBy { get; set; }   
        public int ModifyBy { get; set; }   
        public string IPAddress { get; set; }   


    }
    public class IndustryTrainingSearch
    {
        public int IndustryTRID { get; set; }
        public int IndustryID { get; set; }
        public int EventTypeID { get; set; }
        public String EventDate { get; set; }
        public int SemesterID { get; set; }
        public int TradeID { get; set; }
        public int DepartmentID { get; set; }
       
    }

    public class ConcernPersonDetailsDataModel
    {
        public int? HRManagerID { get; set; }
        public int? PlacementCompanyID { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Designation { get; set; }
        public string? MobileNo { get; set; }
        public int? ModifyBy { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? DepartmentID { get; set; }
    }

    public class IIP_SearchModel: RequestBaseModel
    {
      public int? CompanyID {  get; set; }
    }

    public class IIP_EventDataModel: RequestBaseModel
    {
        public string EventName { get; set; }
        public int? EventID { get; set; } = 0;
        public int? CompanyID { get; set; } = 0;
        public int? EventTypeID { get; set; } = 0;
        public int? Event { get; set; } = 0;
        public int? SemesterID { get; set; } = 0;
        public string? EventStartDate { get; set; } = string.Empty;
        public string? EventEndDate { get; set; } = string.Empty;
        public int? EventForID { get; set; } = 0;
        public List<BranchList>? Branchlist { get; set; } = new List<BranchList>();
        public List<Semesterlist>? Semesterlist { get; set; } = new List<Semesterlist>();

        public string FileUpload { get; set; } = string.Empty;
        public string Dis_FileUpload { get; set; } = string.Empty;
        public int EventLevelID { get; set; } = 0;
        public string Remark { get; set; } = string.Empty;
        public string? SSOID { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? Designation { get; set; }
        public int? TrainingDuration { get; set; }
        public string? AreaOfDomain { get; set; }
        public int? InstituteID { get; set; }
        public int? DivisionID { get; set; }
    }

    public class BranchList
    {
        public int? StreamID { get; set; } = 0;
        public string? StreamName { get; set; } = string.Empty;
    }

    public class Semesterlist
    {
        public int? SemesterID { get; set; } = 0;
        public string? SemesterName {  get; set; } = string.Empty;
    }

    public class CompanyEventSearchModel: RequestBaseModel
    {
        public int? CompanyID { get; set; } = 0;
        public int? StudentID { get; set; } = 0;
        public int? EventID { get; set; } = 0;
        public int? EventStatus { get; set; } = 0;

        public int? StaffID { get; set; } = 0;
        public int? UserID { get; set; } = 0;
        public int? InterestedStatus { get; set; } = 0;
        public string? Remarks { get; set; } = "";
        public string? Action { get; set; } = "";
    }

    public class UpdateConsentStatusDataModel
    {
        public int? ConsentID { get; set; }
        public int? ModifyBy { get; set; }
        public int? Status { get; set; }
        public string? Remark { get; set; }
    }
}
