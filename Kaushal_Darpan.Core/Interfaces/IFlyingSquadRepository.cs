using Kaushal_Darpan.Models.FlyingSquad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IFlyingSquadRepository
    {
        Task<DataTable> GetFlyingSquad(GetFlyingSquadModal model);
        Task<int> PostFlyingSquad(PostFlyingSquadModal model);
        Task<int> PostTeamFlyingSquadForm(PostTeamFlyingSquadModal model);
        Task<DataTable> GetTeamFlyingSquad(GetTeamMemberFlyingSquadModal model);
        Task<int> PostTeamDeploymentFlyingSquadForm(PostTeamFlyingSquadModal model);
        Task<int> IsRequestFlyingSquad(PostIsRequestFlyingSquadModal model);
        Task<int> IsRequestHistoryFlyingSquad(PostIsRequestFlyingSquadModal model);
        Task<int> UpdateFlyingSquad_Attendance(UpdateFlyingSquadAttendance model);
        Task<int> PostTeamDeploymentFlyingSquad(List<PostTeamFlyingSquadUpdateModal> model);
        Task<DataTable> GetTeamDeploymentFlyingSquad(GetTeamFlyingSquadModal model);
        Task<int> SetInchargeFlyingSquad(int ID, int TeamID, int Incharge);
        Task<DataTable> GetFlyingSquad_Attendance(GetFlyingSquadAttendance model);
        //ITI

        Task<DataTable> GetITIFlyingSquad(GetFlyingSquadModal model);
        Task<int> PostITIFlyingSquad(PostFlyingSquadModal model);
        Task<int> PostITITeamFlyingSquadForm(PostTeamFlyingSquadModal model);
        Task<DataTable> GetITITeamFlyingSquad(GetTeamFlyingSquadModal model);
        Task<int> PostITITeamDeploymentFlyingSquadForm(PostTeamFlyingSquadModal model);

        Task<int> PostITITeamDeploymentFlyingSquad(List<PostTeamFlyingSquadUpdateModal> model);
        Task<DataTable> GetITITeamDeploymentFlyingSquad(GetTeamFlyingSquadModal model);
        Task<int> SetInchargeITIFlyingSquad(int ID, int TeamID, int Incharge);
        Task<int> PostFlyingSquadAttendanceForm(PostFlyingSquadAttendanceModal model);
    }
}
