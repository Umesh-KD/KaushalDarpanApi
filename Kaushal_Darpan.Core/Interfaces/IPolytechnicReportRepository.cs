using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.PolytechnicReport;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IPolytechnicReportRepository
    {
        Task<DataTable> GetCollegeNodalDashboardData(CollageDashboardSearchModel model);
        Task<DataTable> GetAllData(PolytechnicReportSearchModel model);
        Task<DataTable> StatusChangeByID(int InstituteID, int ActiveStatus, int UserID);
        Task<PolytechnicReportModel> GetById(int PK_ID);
        Task<bool> SaveData(PolytechnicReportModel productDetails);
        Task<bool> DeleteDataByID(PolytechnicReportModel productDetails);
        Task<bool> UpdateActiveStatusByID(PolytechnicReportModel productDetails);
    }
}
