using Kaushal_Darpan.Models.FlyingSquad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIFlyingSquadRepository
    {
        Task<DataTable> GetFlyingSquad(GetFlyingSquadModal model);
        Task<int> PostFlyingSquad(PostFlyingSquadModal model);
        Task<int> PostTeamFlyingSquadForm(PostTeamFlyingSquadModal model);
        Task<DataTable> GetTeamFlyingSquad(GetTeamFlyingSquadModal model);
        Task<int> PostTeamDeploymentFlyingSquadForm(PostTeamFlyingSquadModal model);
        Task<int> IsRequestFlyingSquad(PostIsRequestFlyingSquadModal model);
        Task<int> IsRequestHistoryFlyingSquad(PostIsRequestFlyingSquadModal model);
        Task<int> UpdateFlyingSquad_Attendance(UpdateFlyingSquadAttendance model);
        Task<int> PostTeamDeploymentFlyingSquad(List<PostTeamFlyingSquadUpdateModal> model);
        Task<DataTable> GetTeamDeploymentFlyingSquad(GetTeamFlyingSquadModal model);
        Task<int> SetInchargeFlyingSquad(int ID, int TeamID, int Incharge);
        Task<DataTable> GetFlyingSquad_Attendance(GetFlyingSquadAttendance model);        
        Task<int> PostFlyingSquadAttendanceForm(PostFlyingSquadAttendanceModal model);
    }
}
