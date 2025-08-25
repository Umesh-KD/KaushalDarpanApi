using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.IssuedItems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEIssuedItemsRepository
    {
        Task<DataTable> GetAllData(DTEIssuedItemsSearchModel SearchReq);
        Task<DataTable> GetAllRetunItem(DTEReturnItemSearchModel SearchReq);
        Task<DTEIssuedItems> GetById(int PK_ID);
        Task<int> SaveData(DTEIssuedItems productDetails);
        Task<bool> DeleteDataByID(DTEIssuedItems productDetails);
        Task<DataTable> GetAllStoks(DTEStoksSearchModel SearchReq);
        Task<DataTable> GetAllStoksBalance(DTEStoksSearchModel SearchReq);

        Task<bool> SaveDataReturnDTEItem(ReturnDteIssuedItems request);
    }
}
