using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.FlyingSquad
{
    public class GetFlyingSquadModal
    {
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int TeamID { get; set; }
        public int FlyingSquadDeploymentID { get; set; }
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
    }
    public class PostFlyingSquadModal
    {
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int TeamDeploymentID { get; set; }
        public string? TeamName { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int InstituteType { get; set; }
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }
        public int RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }
    public class PostFlyingSquadAttendanceModal
    {
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int TeamDeploymentID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public bool IsPresent { get; set; }
        public string? Remark { get; set; }
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }
        public string? IPAddress { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }

    public class PostTeamFlyingSquadModal
    {
        public string? OperationType { get; set; }
        public string? TeamName { get; set; }
        public int ID { get; set; }
        public int TeamID { get; set; }
        public ShiftA? ShiftA { get; set; }
        public ShiftA? ShiftB { get; set; }        
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int ActiveStatus { get; set; }
        public int DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public Boolean? Selected { get; set; }
    }

    public class PostTeamFlyingSquadUpdateModal
    {
        public string? OperationType { get; set; }
        public int ID { get; set; }
        public int TeamID { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
    }
public class GetTeamFlyingSquadModal
    {
        public int TeamID { get; set; }
        public string? TeamName { get; set; }
        public int DepartmentID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int ShiftID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
    }
    public class GetTeamMemberFlyingSquadModal
    {
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
    }
    public class GetFlyingSquadAttendance
    {
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int DeploymentID { get; set; }
    }

    public class UpdateFlyingSquadAttendance
    {
        public int ID { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int IsPresent { get; set; }
        public string? Remark { get; set; }
        public string? photo { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
    }
    public class ShiftA
    {
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int InstituteType { get; set; }
        public int ShiftID { get; set; }
        public DateTime? DeploymentDate { get; set; }
    }

    public class GetFlyingSquadDutyOrder
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int ID { get; set; }
        public string? OperationType { get; set; }
        public int TeamID { get; set; }
        public int IsPresent { get; set; }
        public string? Remark { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
    }

    public class PostIsRequestFlyingSquadModal
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
