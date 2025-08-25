using Kaushal_Darpan.Models.GroupCodeAllocation;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGroupCodeAllocationRevalRepository
    {
        Task<List<GroupCodeAllocationAddEditModel_Reval>> GetAllData(GroupCodeAllocationSearchModel filterModel);
        Task<int> SaveData(List<GroupCodeAllocationAddEditModel_Reval> request, int StartValue);
        Task<List<GroupCodeAddEditModel>> GetPartitionData(GroupCodeAllocationSearchModel filterModel);
        Task<int> SavePartitionData(List<GroupCodeAddEditModel> request);

    }
}
