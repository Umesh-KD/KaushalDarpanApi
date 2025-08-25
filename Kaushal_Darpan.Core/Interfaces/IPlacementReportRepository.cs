using Kaushal_Darpan.Models.PlacementReport;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementReportRepository
    {
        Task<DataTable> GetAllData(PlacementReportSearch filterModel);
    }
}
