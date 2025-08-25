using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.ItemsMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ItemsMasterRepository
    {
        Task<DataTable> GetAllData(ItemsSearchModel SearchReq);
        Task<ItemsModel> GetById(int PK_ID);
        Task<bool> SaveData(ItemsModel productDetails);
        Task<bool> DeleteDataByID(ItemsModel productDetails);

        Task<ItemsDetailsModel> GetItemDetails(int PK_ID);
        Task<List<ItemsDetailsModel>> GetAllItemDetails(int PK_ID);
        Task<int> UpdateItemData(List<ItemsDetailsModel> entity);

    }
}
