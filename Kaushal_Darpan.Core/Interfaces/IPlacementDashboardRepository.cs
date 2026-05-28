using Kaushal_Darpan.Models.PlacementDashboard;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementDashboardRepository
    {
        Task<DataTable> GetAllData(PlacementDashboardModel model);

        //-------------------ITI PLACEMENT-----------------------------

        Task<DataTable> GetITIAllData(ITIPlacementDashboardModel model);
        Task<DataTable> GetIIPDashboardData(PlacementDashboardModel model);
    }
}
