using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CenterObserver
{
    public class CenterObserverDataModel: RequestBaseModel
    {
        public int TeamID { get; set; }
        public int UserID { get; set; }
        public string? TeamName { get; set; }
        public List<ObserverDetailsDataModel>? ObserverDetails { get; set; }
        public string? IPAddress { get; set; }
        public int? DeploymentID { get; set; }
    }

    public class ObserverDetailsDataModel
    {
        public int DistrictID { get; set; }
        public string? DistrictName { get; set;}
        public int InstituteID { get; set; }
        public string? InstituteName { get;set;}
        public int StreamID { get; set; }
        public string? StreamName { get; set; }
        public int SemesterID { get; set; }
        public string? SemesterName { get; set; }
        public string? SSOID { get; set; }
        public string? ShiftName { get; set; }
        public string? StaffName { get; set; }
        public int ShiftID { get; set; }
        public int StaffID { get; set; }
        public int? UserID { get; set; }
        public bool IsIncharge { get; set; }
        public bool? IsPresent { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? photo { get; set; }

    }

    public class CenterObserverSearchModel: RequestBaseModel
    {
        public int? TeamID { get; set; }
        public int? DeploymentStatus { get; set; }
        public int? StaffID { get; set; }
        public int? UserID { get; set; }
        public int? Status { get; set; }
        public int? DeploymentID { get; set; }
        public int? TypeID { get; set; }
        public string? ExamDate { get; set; }
        public string? TeamName { get; set; }
    }

    public class CenterObserverDeployModel: RequestBaseModel
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public int TeamID { get; set; }
        public int UserID { get; set; }
    }

    public class DeploymentDataModel: RequestBaseModel
    {
        public  int DistrictID { get; set; }
        public  int InstituteID { get; set; }
        public  int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public  int TeamID { get; set; }
        public  int UserID { get; set; }
                
        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? ShiftName { get; set; }
        public string? IPAddress { get; set; }
    }

    public class StaffMasterDDLDataModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
    }

    public class CenterMasterDDLDataModel : RequestBaseModel
    {
        public int DistrictID { get; set; }
    }

    public class CODeploymentDataModel: RequestBaseModel
    {
        public List<ObserverDetailsDataModel>? ObserverDetails { get; set; }
        public  int DistrictID { get; set; }
        public  int InstituteID { get; set; }
        public  int ShiftID { get; set; }
        public string? DeploymentDate { get; set; }
        public  int CenterObserverTeamID { get; set; }
        public  int UserID { get; set; }
        public string? TeamName { get; set; }
                
        public string? DistrictName { get; set; }
        public string? InstituteName { get; set; }
        public string? ShiftName { get; set; }
        public  bool Selected { get; set; }
        public  int DeploymentStatus { get; set; }
        public  int DeploymentID { get; set; }
        public  int InspectionTeamID { get; set; }
        public string? IPAddress { get; set; }
        public string? DutyOrderPath { get; set; }
        public string? DutyOrder { get; set; }
        public string? ExamDate { get; set; }

    }

    public class GenerateDutyOrder: RequestBaseModel
    {
        public int DeploymentID { get; set; }
        public int CenterObserverTeamID { get; set; }
        public string? IPAddress { get; set; }
    }

    public class UpdateAttendance : RequestBaseModel
    {
        public int? DeploymentID { get; set; }
        public int? COAttendanceID { get; set; }
        public int? UserID { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
        public string? photo { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public bool? IsPresent { get; set; }
        public int? TypeID { get; set; }


    }

    public class PostIsRequestCenterObserver
    {
        public int DeploymentID { get; set; }
        public int TeamID { get; set; }
        public DateTime? DeploymentDate { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int IsRequest { get; set; }
        public int UserID { get; set; }
        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }
        public string? IPAddress { get; set; }
        public string? Remark { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int TypeID { get; set; }
    }
}
