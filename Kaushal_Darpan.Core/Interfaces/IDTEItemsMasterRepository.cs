using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
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
    public interface IDTEItemsMasterRepository
    {
        Task<DataTable> GetAllData(DTEItemsSearchModel SearchReq);
        Task<DTEItemsModel> GetById(int PK_ID);
        Task<int> UpdateStatusItemsData(DTEItemsModel request);
        Task<int> SaveData(DTEItemsModel productDetails);
        Task<bool> DeleteDataByID(DTEItemsModel productDetails);

        Task<DTEItemsDetailsModel> GetDTEItemDetails(int PK_ID);
        Task<List<DTEItemsDetailsModel>> GetAllDTEItemDetails(int PK_ID);
        Task<int> UpdateDTEItemData(List<DTEItemsDetailsModel> entity);
        Task<DataTable> GetAllAuctionList(DTEItemsSearchModel SearchReq);

        Task<int> SaveAuctionData(AuctionDetailsModel productDetails);


        Task<int> EquipmentCodeDuplicate(EquipmentCodeDuplicateSearch request);

        Task<DataTable> CheckItemAuction(CheckItemAuctionSearch request);

    }
}
