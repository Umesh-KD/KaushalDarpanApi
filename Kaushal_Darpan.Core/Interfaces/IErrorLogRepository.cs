using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IErrorLogRepository
    {
        Task<int> AddErrorLog(Tbl_Trn_ErrorLog data);
        Task<List<Tbl_Trn_ErrorLog>> GetAllErrorLog(GenericPaginationSpecification specification);
        Task<Tbl_Trn_ErrorLog> GetErrorLogById(int errorLogId);
    }
}
