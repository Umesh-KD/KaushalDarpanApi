using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.Student;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRoleMasterRepository
    {
        Task<DataTable> GetAllData(RoleSearchModel body);
        Task<RoleMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(RoleMasterModel productDetails);
        Task<bool> UpdateData(RoleMasterModel productDetails);
        Task<bool> DeleteDataByID(RoleMasterModel productDetails);
    }
}
