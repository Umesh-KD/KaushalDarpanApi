using Kaushal_Darpan.Models.ITIAdminDashboard;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIAdminDashboardRepository
    {
        Task<DataTable> GetAdminDashData(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetITINodalDashboard(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetAdminDashSCVTData(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetITIsWithNumberOfFormsList(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetITIsWithNumberOfFormsPriorityList(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetAllData(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetItiDashApplicationData(ItiAdminDashApplicationSearchModel model);
        Task<DataTable> GetITIPrincipalDashboard(ITIAdminDashboardSearchModel model);
        Task<DataTable> GetProfileData( int InstituteID);

        Task<DataTable> GetItiOptionFormData(ItiAdminDashApplicationSearchModel body);

        Task<DataTable> GetRegistrarDashData(ITIDashboardSearchModel model);
        Task<DataTable> GetSecretaryJDDashData(ITIDashboardSearchModel model);

        Task<DataTable> GetAdminDashNCVTData(ITIAdminDashboardSearchModel model);
    }
}
