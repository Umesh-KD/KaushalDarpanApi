using Kaushal_Darpan.Models.DesignationMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDesignationMasterRepository
    {
        Task<DataTable> GetAllData(DesignationMasterSearchModel body);
        Task<DesignationMasterModel> GetById(int designationID);
        Task<bool> SaveData(DesignationMasterModel request);
        Task<bool> UpdateData(DesignationMasterModel request);
        Task<bool> DeleteDataById(DesignationMasterModel request);
        Task<int> DesignationActiveDeActive(DesignationMasterSearchModel body);
    }
}
