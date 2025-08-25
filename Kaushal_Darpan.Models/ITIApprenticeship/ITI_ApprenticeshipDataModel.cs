using Kaushal_Darpan.Models.CenterObserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_Apprenticeship
{
    public class ITI_ApprenticeshipDataModel : RequestBaseModel
    {
        public int? ApprenticeshipTeamID { get; set; }
        public string? ApprenticeshipTeamName { get; set; }
        public int? UserID { get; set; }
        public int? LevelId { get; set; }
        public string? TeamInitials { get; set; }
        public string? IPAddress { get; set; }
        public List<ApprenticeshipMemberDetailsDataModel>? ApprenticeshipMemberDetails { get; set; }
        public List<ApprenticeshipDeploymentDataModel>? ApprenticeshipDeploymentDetails { get; set; }
        public int? TeamTypeID { get; set; }
        public string? TeamTypeName { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public string? DeploymentStatus { get; set; }
        public ApprenticeshipMember? ApprenticeshipMember { get; set; }

    }

    public class ApprenticeshipMember : RequestBaseModel
    {
        public string? StaffDetails { get; set; }
        public string? DeployDate { get; set; }
        public string? CurrentYear { get; set; }
        public string? Date { get; set; }
    }

    public class ApprenticeshipMemberDetailsDataModel : RequestBaseModel
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

    public class ITI_ApprenticeshipSearchModel : RequestBaseModel
    {
        public int? ApprenticeshipTeamID { get; set; }
        public int? Status { get; set; }
        public int? ApprenticeshipID { get; set; }
        public int? TypeID { get; set; }
        public string? DeploymentDate { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public string? ApprenticeshipTeamName { get; set; }
        public int? DeploymentStatus { get; set; }
        public string? TeamName { get; set; }
        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? LevelId { get; set; }
    }

    public class ITI_ApprenticeshipDropdownModel : RequestBaseModel
    {
        public string? action { get; set; }
        public int? DistrictID { get; set; }
        public int? InstituteID { get; set; }
        public int? ManagementTypeID { get; set; }
    }


    public class ApprenticeshipDeploymentDataModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int DeploymentID { get; set; }
        public int InstituteID { get; set; }
        public string? DeploymentDate { get; set; }
        public string? DeploymentDateFrom { get; set; }
        public string? DeploymentDateTo { get; set; }
        public int ApprenticeshipTeamID { get; set; }
        public int UserID { get; set; }
        public int DeploymentType { get; set; }
        public int DeploymentStatus { get; set; }
        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? IPAddress { get; set; }
        public string? NodalOfficerMobile { get; set; }
        public string? SerialNo { get; set; }
        public int? AnswerStatus { get; set; }

        public string? IndustryName { get; set; }
        public string? HeadName { get; set; }
        public string? Designation { get; set; }
        public string? Address { get; set; }
    }

    public class Apprenticeship_SaveCheckSSODataModel : RequestBaseModel
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



    public class ITI_ApprenticeshipAnswerModel
    {
        public List<ApprenticeshipQuestion_WithAnswerMain>? MainData { get; set; }
        public List<IIT_ApprenticeshipAnswerModelTradeWiseData>? TradeWiseData { get; set; }
        public List<IIT_ApprenticeshipAnswerModelTrainersData>? TrainersData { get; set; }
        public List<IIT_ApprenticeshipAnswerModelFacilityData>? FacilityData { get; set; }
    }


    public class IIT_ApprenticeshipAnswerModelTradeWiseData
    {
        public List<ApprenticeshipQuestion_WithAnswerMain>? TradeWiseList { get; set; }
    }

    public class IIT_ApprenticeshipAnswerModelTrainersData
    {
        public List<ApprenticeshipQuestion_WithAnswerMain>? TrainersList { get; set; }
    }

    public class IIT_ApprenticeshipAnswerModelFacilityData
    {
        public List<ApprenticeshipQuestion_WithAnswerMain>? FacilityList { get; set; }
    }



    public class ApprenticeshipQuestion_WithAnswerMain
    {
        public int? QuestionID { get; set; }
        public int? AnswerID { get; set; }
        public int? ParentQuestionID { get; set; }
        public int? TypeID { get; set; }
        public string? Questions { get; set; } = string.Empty;
        public string? AnswerText { get; set; } = string.Empty;
        public int? QuestionType { get; set; }
        public string? Remarks { get; set; } = string.Empty;
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public string? IPAddress { get; set; } = string.Empty;
        public int? ApprenticeshipTeamID { get; set; }
        public int? DeploymentID { get; set; }
        public DateTime? RTS { get; set; }
        public DateTime? ModifyDate { get; set; }

    }

}
