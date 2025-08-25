using Kaushal_Darpan.Models.CenterObserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_Inspection
{
    public class ITI_InspectionDataModel : RequestBaseModel
    {
        public int? InspectionTeamID { get; set; }
        public string? InspectionTeamName { get; set; }
        public int? UserID { get; set; }
        public int? LevelId { get; set; }
        public string? TeamInitials { get; set; }
        public string? IPAddress { get; set; }
        public List<InspectionMemberDetailsDataModel>? InspectionMemberDetails { get; set; }
        public List<InspectionDeploymentDataModel>? InspectionDeploymentDetails { get; set; }
        public int? TeamTypeID { get; set; }
        public string? TeamTypeName { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public string? DeploymentStatus { get; set; }
        public inspectionMember? inspectionMember { get; set; }

    }

    public class inspectionMember : RequestBaseModel
    {
        public string? StaffDetails { get; set; }
        public string? DeployDate { get; set; }
        public string? CurrentYear { get; set; }
        public string? Date { get; set; }
    }

    public class InspectionMemberDetailsDataModel : RequestBaseModel
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
    }

    public class ITI_InspectionSearchModel : RequestBaseModel
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
    }

    public class ITI_InspectionDropdownModel : RequestBaseModel
    {
        public string? action { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
        public int? ManagementTypeID { get; set; }
    }


    public class InspectionDeploymentDataModel : RequestBaseModel
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

    public class SaveCheckSSODataModel : RequestBaseModel
    {
        public int? ID { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
        public int? StreamID { get; set; }
        public int? SemesterID { get; set; }
        public string? SSOID { get; set; }
        public int? ShiftID { get; set; }
        public int? StaffID { get; set; }
        public int? ManagementTypeID { get; set; } // Nullable int with default value

        public bool IsIncharge { get; set; } = false;

        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailID { get; set; }

        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
    }



    public class ITI_InspectionAnswerModel
    {
        public List<InspectionQuestion_WithAnswer> Data { get; set; }
    }
    public class InspectionQuestion_WithAnswer
    {
        public int QuestionID { get; set; }
        public int TypeID { get; set; }
        public string Questions { get; set; } = string.Empty;
        public string AnswerText { get; set; } = string.Empty;
        public int QuestionType { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public int InspectionTeamID { get; set; }
        public int DeploymentID { get; set; }

    }


}
