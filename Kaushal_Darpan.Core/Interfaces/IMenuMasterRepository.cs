using Kaushal_Darpan.Models.MenuMaster;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.MenuMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMenuMasterRepository
    {
        Task<DataTable> GetAllData(MenuMasterSerchModel request);
        Task<MenuMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(MenuMasterModel productDetails);
        Task<bool> SaveData_EditMenuDetails(MenuMasterModel productDetails);
        Task<bool> UpdateData(MenuMasterModel productDetails);
        Task<bool> DeleteDataByID(MenuMasterModel productDetails);
        Task<DataTable> MenuUserandRoleWise(MenuByUserAndRoleWiseModel model);
        Task<bool> UpdateActiveStatusByID(MenuMasterModel productDetails);
    }
}
