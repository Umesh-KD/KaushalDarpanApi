using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITICenterObserver
{
    public class ITICenterObserverDataModel : RequestBaseModel
    {
        public int TeamID { get; set; }
        public int UserID { get; set; }
        public string? TeamName { get; set; }
        public List<ITIObserverDetailsDataModel>? ObserverDetails { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITIObserverDetailsDataModel
    {
        public int DistrictID { get; set; }
        public string? DistrictName { get; set; }
        public int InstituteID { get; set; }
        public string? InstituteName { get; set; }
        public int StreamID { get; set; }
        public string? StreamName { get; set; }
        public int SemesterID { get; set; }
        public string? SemesterName { get; set; }
        public string? SSOID { get; set; }
        public string? ShiftName { get; set; }
        public string? StaffName { get; set; }
        public int ShiftID { get; set; }
        public int StaffID { get; set; }
        public bool IsIncharge { get; set; }

    }

    public class ITICenterObserverSearchModel : RequestBaseModel
    {
        public int? TeamID { get; set; }
        public int? DeploymentStatus { get; set; }
        public int? StaffID { get; set; }
        public int? Status { get; set; }
        public int? DeploymentID { get; set; }
        public int? TypeID { get; set; }
        public int? UserID { get; set; }
        public string? ExamDate { get; set; }
        public string? TeamName { get; set; }
    }

    public class ITICenterObserverDeployModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
    }

    public class ITIDeploymentDataModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }

        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? ShiftName { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITIStaffMasterDDLDataModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
    }

    public class ITICenterMasterDDLDataModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
    }

    public class ITICODeploymentDataModel : RequestBaseModel
    {
        public List<ITIObserverDetailsDataModel>? ObserverDetails { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int CenterObserverTeamID { get; set; }
        public int UserID { get; set; }
        public string? TeamName { get; set; }

        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? ShiftName { get; set; }
        public bool Selected { get; set; }
        public int DeploymentStatus { get; set; }
        public int DeploymentID { get; set; }
        public string? IPAddress { get; set; }
        public string? DutyOrderPath { get; set; }
        public string? DutyOrder { get; set; }

    }

    public class ITIGenerateDutyOrder : RequestBaseModel
    {
        public int DeploymentID { get; set; }
        public int CenterObserverTeamID { get; set; }
        public string? IPAddress { get; set; }
    }

    public class SaveDeploymentResult : RequestBaseModel
    {
        public int RetvalID { get; set; }
        public string Message { get; set; }
    }
}
