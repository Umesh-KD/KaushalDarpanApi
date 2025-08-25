using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.MasterConfiguration;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMasterConfigurationRepository
    {
        Task<DataTable> GetAllFeeData(FeeConfigurationModel request);
        Task<FeeConfigurationModel> GetFeeByID(int PK_ID);
        Task<int> SaveFeeData(FeeConfigurationModel request);
        Task<int> DeleteFeeByID(int PK_ID);
        Task<int> SaveSerialData(SerialMasterModel request);
        Task<DataTable> GetAllSerialData(SerialMasterModel request);
        Task<SerialMasterModel> GetSerialByID(int PK_ID);
        Task<int> DeleteSerialByID(int PK_ID);
        Task<DataTable> CommonSignature(CommonSignatureModel request);
        Task<DataTable> BterCommonSignature(BterCommonSignatureModel request);
    }


}
