
using Kaushal_Darpan.Models.ITI_Inspection;

namespace Kaushal_Darpan.Models.Test
{
    public class TeacherHigherEducationApplicationModel : RequestBaseModel

    {
        public int THTEAppID { get; set; }
        public int StaffID { get; set; }
        public string SSOID { get; set; }
        public string TeacherName { get; set; }
        public string DOB { get; set; }
        public string JoiningDate { get; set; }
        public int AppliedCourse { get; set; }
        public string? AppliedInstitute { get; set; }
        public int PHDStatus { get; set; }
        public string? AppliedInstituteDistance { get; set; }
        public int AppliedInstituteCourseCategory { get; set; }
        public int AppliedInstituteSubCategory { get; set; }
        public string? Remark { get; set; }
        public int CreatedBy{ get; set; }
        public int SessionID { get; set; }

        public string? InstituteID { get; set; }
        public int? InstituteType { get; set; }
        public int? IsQualificationRecorded { get; set; }
        public string? InstituteTypeName { get; set; }

        public List<CollegeDetailList>? CollegeDetailList { get; set; }
    }


    public class CollegeDetailList
    {
        public int ID { get; set; }
        public int StatusID { get; set; }
        public int THTEAppID { get; set; }
        public int CommitteStatus { get; set; }
        public int UserID { get; set; }
        public string? CollegeName { get; set; }
        public string? Distance{ get; set; }
        public string? Remarks{ get; set; }
        public int? InstituteType { get; set; }
        public string? InstituteTypeName { get; set; }
        public int? InstituteStatus { get; set; }
        public int? DTECommitteeStatus { get; set; }
    }


    public class THTE_DDL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
        public int? StaffID { get; set; }
    }

    public class THTE_ApplicationSearchModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }
        public string Name { get; set; }
        public int StaffID { get; set; }
        public string? action { get; set; }
        public int RoleID { get; set; }
    }

    public class PrincipleApplicationListSearchModel: RequestBaseModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }
        public string Name { get; set; }
        public int StaffID { get; set; }
        public int? status { get; set; }
        public int? UserID { get; set; }
        
    }

    public class THTE_DropdownDataModel
    {
        public string? action { get; set; }
        public int? RoleID { get; set; }
    }

    public class UpdateApplicationStatusDataModel_Principle
    {
        public int? THTEAppID { get; set; }
        public int? ModifyBy { get; set; }
        public int? status { get; set; }
        public int? RoleID { get; set; }
        public string? Remark { get; set; }
        public string? CommitteeDocs { get; set; }
        public string? Dis_CommitteeDocs { get; set; }
        public int? DTECommitteID { get; set; }
    }

    public class UpdateApplicationStatusDataModel_Committee
    {
        public int? status { get; set; }
        public int? CommitteeID { get; set; }
        public string? Remark { get; set; }
        public int? RoleID { get; set; }
        public int? ModifyBy { get; set; }
        public string? CommitteeDocs { get; set; }
        public string? Dis_CommitteeDocs { get; set; }
        public List<ApplicationListDataModel_THTE>? ApplicationListData {  get; set; }
    }

    public class ApplicationListDataModel_THTE
    {
        public int? THTEAppID { get; set; }
    }

    public class ApplicationGenrateOrderByDteListSearchModel : RequestBaseModel
    {
        public int Id { get; set; }
        public int THTEAppID { get; set; }

        public string Name { get; set; }
        public int StaffID { get; set; }
        public int? status { get; set; }
        public string THTEAppIDs { get; set; }
    }


    public class CommitteeDataModel : RequestBaseModel
    {
        public int? InspectionTeamID { get; set; }
        public int? CommitteeID { get; set; }
        public string? InspectionTeamName { get; set; }
        public int? UserID { get; set; }
        public int? LevelId { get; set; }
        public string? TeamInitials { get; set; }
        public string? IPAddress { get; set; }
        public List<CommitteeMemberDetailsDataModel>? InspectionMemberDetails { get; set; }
        public List<CommitteeDeploymentDataModel>? InspectionDeploymentDetails { get; set; }
        public int? TeamTypeID { get; set; }
        public string? TeamTypeName { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public string? DeploymentStatus { get; set; }
        public CommitteeMember? inspectionMember { get; set; }

    }


    public class CommitteeMemberDetailsDataModel : RequestBaseModel
    {
        public int? ID { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
        public int? StreamID { get; set; }
        public int? SemesterID { get; set; }
        public string? SSOID { get; set; }
        public int? ShiftID { get; set; }
        public int? StaffID { get; set; }
        public int? ManagementTypeID { get; set; }
        public bool? IsIncharge { get; set; }

        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? StreamName { get; set; }
        public string? SemesterName { get; set; }
        public string? ShiftName { get; set; }
        public string? StaffName { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? photo { get; set; }

        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public int? CommitteeID { get; set; }
    }

    public class CommitteeDeploymentDataModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int DeploymentID { get; set; }
        public int InstituteID { get; set; }
        public string? DeploymentDate { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public int InspectionTeamID { get; set; }
        public int UserID { get; set; }
        public int DeploymentType { get; set; }
        public int DeploymentStatus { get; set; }
        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? IPAddress { get; set; }
        public string? NodalOfficerMobile { get; set; }
        public string? SerialNo { get; set; }
        public int? AnswerStatus { get; set; }
    }

    public class CommitteeMember : RequestBaseModel
    {
        public string? StaffDetails { get; set; }
        public string? DeployDate { get; set; }
        public string? CurrentYear { get; set; }
        public string? Date { get; set; }
    }

    public class CommitteeSearchModel : RequestBaseModel
    {
        public int? InspectionTeamID { get; set; }
        public int? Status { get; set; }
        public int? InspectionID { get; set; }
        public int? TypeID { get; set; }
        public string? DeploymentDate { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public string? InspectionTeamName { get; set; }
        public int? DeploymentStatus { get; set; }
        public string? TeamName { get; set; }
        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? LevelId { get; set; }
        public int? DistrictID { get; set; }
    }

    public class CommitteeStaffSSOIDSearchModel
    {
        public int DepartmentID { get; set; }
        public string? SSOID { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
    }

    public class InstituteCommitteListDataModel
    {
        public int DepartmentID { get; set; }
        public int CommitteeID { get; set; }
        public int InstituteID { get; set; }
        public string? action { get; set; }
    }

    public class DTECommitteeDataModel: RequestBaseModel
    {
        public int? DTECommitteeID { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
        public string? DTECommitteeName { get; set; }
        public List<DTECommitteeMemberDetailsDataModel>? DTECommitteeMemberDetails { get; set; }
    }

    public class DTECommitteeMemberDetailsDataModel
    {
        public int? CommitteeMemberID { get; set; }
        public int? StaffID { get; set; }
        public bool? IsIncharge { get; set; }
        public string? SSOID { get; set; }
        public string? StaffName { get; set; }
    }

    public class StaffDetailsPreviewDataModel
    {
        public string? SSOID { get; set; }
        public string? Office { get; set; }
        public string? ServiceBookDesignation { get; set; }
        public string? ServiceBookBranch { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? DateofBirth { get; set; }
        public string? DateOfFirstAppointment { get; set; }
        public string? DepartmentJoiningDate { get; set; }
        public string? DateOfJoining { get; set; }
        public string? DateOfAppointment { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmployeeID { get; set; }
        public string? CurrentDesignation { get; set; }
        public string? CurrentBranch { get; set; }
        public string? Experience { get; set; }
        public string? QualificationAtJoining { get; set; }
        public string? QualificationAfterJoining { get; set; }
        public string? DateOfRetirement { get; set; }

        public int StaffID { get; set; }
        public int StaffUserID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

}

