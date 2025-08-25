using Kaushal_Darpan.Models.GroupCodeAllocation;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGroupCodeAllocationRepository
    {
        Task<List<GroupCodeAllocationAddEditModel>> GetAllData(GroupCodeAllocationSearchModel filterModel);
        Task<int> SaveData(List<GroupCodeAllocationAddEditModel> request, int StartValue);
        Task<List<GroupCodeAddEditModel>> GetPartitionData(GroupCodeAllocationSearchModel filterModel);
        Task<int> SavePartitionData(List<GroupCodeAddEditModel> request);
    }
}
