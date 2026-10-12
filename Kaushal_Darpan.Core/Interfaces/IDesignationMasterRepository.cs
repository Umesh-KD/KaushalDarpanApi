using Kaushal_Darpan.Models.DesignationMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDesignationMasterRepository
    {
        Task<DataTable> GetAllData(DesignationMasterSearchModel body);
        Task<DataTable> GetAllRequests(QueryRequestModel body);
        Task<DesignationMasterModel> GetById(int designationID);
        Task<QueryRequestModel> GetQueryRequestByID(int RequestID);
        Task<bool> SaveData(DesignationMasterModel request);
        Task<bool> SaveQueryRequest(QueryRequestModel request);
        Task<bool> UpdateData(DesignationMasterModel request);
        Task<bool> DeleteDataById(DesignationMasterModel request);
        Task<bool> DeleteQueryRequest(QueryRequestModel request);
        Task<int> DesignationActiveDeActive(DesignationMasterSearchModel body);
    }
}
