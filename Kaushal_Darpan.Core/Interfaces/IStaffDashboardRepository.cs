using Kaushal_Darpan.Models.StaffDashboard;
using Kaushal_Darpan.Models.StaffMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStaffDashboardRepository
    {
        Task<DataTable> GetAllData(StaffDashboardSearchModel model);
        Task<DataTable> GetItiStaffMaster(StaffDashboardSearchModel model);
        Task<DataTable> GetItiObserverMaster(StaffDashboardSearchModel model);
        Task<DataTable> GetItiFlyingMaster(StaffDashboardSearchModel model);
        Task<DataTable> GetPrincipleDash(StaffDashboardSearchModel model);

        Task<DataTable> GetDashReportData(StaffMasterSearchModel filterModel);
        Task<DataTable> GetAllStudentPersentData(StaffMasterSearchModel filterModel);

        Task<DataTable> GetStudentEnrCancelRequestData(StaffMasterSearchModel filterModel);
        Task<DataTable> GetAllTotalExaminerData(StaffMasterSearchModel filterModel);


        Task<DataTable> ITIGovtEMGetAllStudentPersentData(ITIGovtEMStaffMasterSearchModel filterModel);

        Task<DataTable> ITIGovtEMGetAllTotalExaminerData(ITIGovtEMStaffMasterSearchModel filterModel);

        Task<DataTable> ApporveOrRejectStudentEnrCancelRequest(StudentEnrCancelReqModel filterModel);
    }
}
