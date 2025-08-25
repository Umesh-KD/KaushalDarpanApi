using Kaushal_Darpan.Models.ViewPlacedStudents;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IViewPlacedStudentsRepository
    {
        Task<DataTable> GetAllData(ViewPlacedStudents filterModel);
        Task<DataTable> GetAllPlacementReportData(PlacementReportSearchData filterModel);
        Task<DataTable> GetAllPlacementData();
    }
}
