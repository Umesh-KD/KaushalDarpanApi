using Kaushal_Darpan.Models.AdminDashboard;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAdminDashboardRepository
    {
        Task<DataTable> GetAdminDashData(AdminDashboardSearchModel model);
        Task<DataTable> GetAdminDashReportsData(AdminDashReportsModel model);

        Task<DataTable> GetITI_TeacherDashboard(AdminDashboardSearchModel model);

        Task<DataTable> GetEM_JDTEDashData(EM_JDTEDashboardSearchModel model);

    }
}
