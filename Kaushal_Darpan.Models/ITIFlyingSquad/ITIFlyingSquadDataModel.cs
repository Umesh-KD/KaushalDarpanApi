using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIFlyingSquad
{
    public class ITIFlyingSquadDataModel : RequestBaseModel
    {
        public int TeamID { get; set; }
        public int UserID { get; set; }
        public string? TeamName { get; set; }
        public List<ITIFlyingDetailsDataModel>? ObserverDetails { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITIFlyingDetailsDataModel
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

    public class ITIFlyingSquadSearchModel : RequestBaseModel
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

    public class ITIFlyingSquadDeployModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
    }

    public class ITIFlyingDeploymentDataModel : RequestBaseModel
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

    //public class ITIStaffMasterDDLDataModel : RequestBaseModel
    //{
    //    public int InstituteID { get; set; }
    //}

    //public class ITICenterMasterDDLDataModel : RequestBaseModel
    //{
    //    public int DistrictID { get; set; }
    //}

    public class ITICOFlyinDeploymentDataModel : RequestBaseModel
    {
        public List<ITIFlyingDetailsDataModel>? ObserverDetails { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int FlyingSquadTeamID { get; set; }
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

    public class ITIFlyingGenerateDutyOrder : RequestBaseModel
    {
        public int DeploymentID { get; set; }
        public int FlyingSquadTeamID { get; set; }
        public string? IPAddress { get; set; }
    }
}
