using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ItemCategoryMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<ItemCategoryModel> GetById(int PK_ID);
        Task<bool> SaveData(ItemCategoryModel productDetails);
        Task<bool> DeleteDataByID(ItemCategoryModel productDetails);

    }
}
