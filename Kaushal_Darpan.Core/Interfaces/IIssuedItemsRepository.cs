using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.IssuedItems;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IIssuedItemsRepository
    {
        Task<DataTable> GetAllData(IssuedItemsSearchModel SearchReq);
        Task<DataTable> GetAllRetunItem(ReturnItemSearchModel SearchReq);
        Task<IssuedItems> GetById(int PK_ID);
        Task<int> SaveData(IssuedItems productDetails);
        Task<bool> DeleteDataByID(IssuedItems productDetails);
        Task<DataTable> GetAllStoks(StoksSearchModel SearchReq);
        Task<bool> SaveDataReturnItem(ReturnIssuedItems request);
    }
}
