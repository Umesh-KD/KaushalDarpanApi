using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.StudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAbcIdStudentDetailsMasterRepository
    {
        Task<DataTable> GetAllData(ABCIDStudentDetailsSearchModel model);
        Task<DataTable> GetABCIDCount(int ABCID);        
        Task<bool> SaveData(ABCIDStudentDetailsDataModel productDetails);
        Task<DataTable> DownloadConsolidateABCIDReport(ABCIDStudentDetailsSearchModel model);
        Task<DataTable> DownloadABCIDSummaryReport(ABCIDStudentDetailsSearchModel model);

    }
}
