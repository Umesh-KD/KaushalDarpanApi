using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GroupMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGroupMasterRepository
    {
        Task<DataTable> GetAllData(GroupSearchModel filterModel);
        Task<GroupMasterModel> GetById(int PK_ID, int DepartmentID);
        Task<bool> SaveData(GroupMasterModel productDetails);

        Task<bool> DeleteDataByID(GroupMasterModel productDetails);
    }
}
