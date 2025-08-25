using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.StaffDashboard;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IVerifierMasterRepository
    {
        Task<int> SaveVerifierData(VerifierDataModel productDetails);
        Task<List<VerifierDataModel>> GetAllData(VerifierSearchModel request);
        Task<int> SaveDTENodalVerifierData(NodalVerifierDataModel request);
        Task<List<VerifierDataModel>> GetAllNodalVerifierData(VerifierSearchModel request);
        Task<VerifierDataModel> GetDataById(int id);
        Task<bool> DeleteDataByID(VerifierDataModel productDetails);
        Task<DataTable> GetVerifierDashboard(DTEApplicationDashboardModel request);
        Task<DataTable> GetNodalVerifierDashboard(StaffDashboardSearchModel model);
        Task<DataTable> GetDteNodalVerifierDashboard(StaffDashboardSearchModel filterModel);
        Task<DataTable> GetTotalStudentReportedList(TotalStudentReportedListModel model);
        Task<string> VerifierApiSSOIDGetSomeDetails(VerifierApiDataModel request);

    }
}
