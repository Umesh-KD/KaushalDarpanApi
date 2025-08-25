using Kaushal_Darpan.Models.MasterConfiguration;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMasterConfigurationBterRepository
    {
        Task<DataTable> GetAllFeeData(FeeConfigurationBterModel request);
        Task<FeeConfigurationBterModel> GetFeeByID(int PK_ID);
        Task<int> SaveFeeData(FeeConfigurationBterModel request);
        Task<int> DeleteFeeByID(int PK_ID);
        Task<int> SaveSerialData(SerialMasterBterModel request);
        Task<DataTable> GetAllSerialData(SerialMasterBterModel request);
        Task<SerialMasterBterModel> GetSerialByID(int PK_ID);
        Task<int> DeleteSerialByID(int PK_ID);
    }
}
