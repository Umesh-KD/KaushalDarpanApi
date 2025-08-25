using Kaushal_Darpan.Models.PlacementDashboard;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementDashboardRepository
    {
        Task<DataTable> GetAllData(PlacementDashboardModel model);
    }
}
