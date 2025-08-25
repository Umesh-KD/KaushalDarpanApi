using Kaushal_Darpan.Models.UserMenuRights;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUserMenuRightsRepository
    {
        Task<DataTable> GetAllData();
        Task<List<UserMenuRightsModel>> GetById(UserAndRoleMenuModel model);
        Task<List<UserMenuRightsModel>> PrincipleMenu(UserAndRoleMenuModel model);
        Task<bool> SaveUserMenuRight(List<UserMenuRightsModel> request);
        //bool SaveUserRoleRight(List<RoleMenuRightsModel> request);
        //Task<bool> UpdateData(RoleMenuRightsModel productDetails);
        //Task<bool> DeleteDataByID(RoleMenuRightsModel productDetails);
    }
}
