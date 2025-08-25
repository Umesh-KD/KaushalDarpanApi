using Kaushal_Darpan.Models.RoleMenuRights;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRoleMenuRightsRepository
    {
        Task<DataTable> GetAllData();
        Task<List<UserRightsModel>> GetById(int RoleID,int DepartmentID, int Eng_NonEng);
        Task<bool> SaveUserRoleRight(List<UserRightsModel> request);
        //bool SaveUserRoleRight(List<RoleMenuRightsModel> request);
        //Task<bool> UpdateData(RoleMenuRightsModel productDetails);
        //Task<bool> DeleteDataByID(RoleMenuRightsModel productDetails);
    }
}
