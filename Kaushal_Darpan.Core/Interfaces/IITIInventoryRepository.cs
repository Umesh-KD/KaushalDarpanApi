using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.ITIInventoryDashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIInventoryRepository
    {
        Task<DataTable> GetInventoryDashboard(ITIInventoryDashboard filterModel);
        Task<DataTable> GetAllEquipmentsMaster(CommonSearchModal modal);
        Task<DTEEquipmentsModel> GetByIDEquipmentsMaster(int PK_ID);
        Task<bool> SaveEquipmentsMasterData(DTEEquipmentsModel request);
        Task<bool> DeleteEquipmentsMasterByID(DTEEquipmentsModel request);
        Task<DataTable> GetAllIssuedItems(DTEIssuedItemsSearchModel SearchReq);
        Task<DataTable> GetAllRetunItem(DTEReturnItemSearchModel SearchReq);
        Task<DTEIssuedItems> GetIssuedItemsByID(int PK_ID);
        Task<int> SaveIssuedItems(DTEIssuedItems request);
        Task<bool> DeleteIssuedItemsByID(DTEIssuedItems request);
        Task<DataTable> GetAllStoks(DTEStoksSearchModel SearchReq);
        Task<DataTable> GetAllStoksBalance(DTEStoksSearchModel SearchReq);
        Task<bool> SaveDataReturnItem(ReturnDteIssuedItems request);
        Task<DataTable> GetAllCategoryMaster();
        Task<DTEItemCategoryModel> GetCategoryMasterByID(int PK_ID);
        Task<int> SaveCategoryMaster(DTEItemCategoryModel request);
        Task<bool> DeleteCategoryMasterByID(DTEItemCategoryModel request);
        Task<DataTable> GetAllItemsMaster(DTEItemsSearchModel SearchReq);
        Task<DTEItemsModel> GetItemsMasterByID(int PK_ID);
        Task<int> UpdateStatusItemsData(DTEItemsModel request);
        Task<int> SaveItemsMaster(DTEItemsModel request);
        Task<bool> DeleteItemsMasterByID(DTEItemsModel productDetails);
        Task<DTEItemsDetailsModel> GetItemDetails(int PK_ID);
        Task<List<DTEItemsDetailsModel>> GetAllItemDetails(int PK_ID);
        Task<int> UpdateItemData(List<DTEItemsDetailsModel> itemsDetails);
        Task<DataTable> GetAllAuctionList(DTEItemsSearchModel SearchReq);
        Task<int> SaveAuctionData(AuctionDetailsModel request);
        Task<DataTable> GetAllItemUnitMaster();
        Task<DTEItemUnitModel> GetItemUnitMasterByID(int PK_ID);
        Task<bool> SaveItemUnitMaster(DTEItemUnitModel request);
        Task<bool> DeleteItemUnitMasterByID(DTEItemUnitModel productDetails);
        Task<DataTable> GetAllEquipmentsMapping(DTESearchTradeEquipmentsMapping SearchReq);
        Task<DTETradeEquipmentsMapping> GetEquipmentsMappingByID(int PK_ID);
        Task<bool> SaveEquipmentsMapping(DTETradeEquipmentsMapping request);
        Task<bool> SaveEquipmentsMappingRequestData(DTERequestTradeEquipmentsMapping request);
        Task<bool> UpdateStatusEquipmentsMapping(DTEUpdateStatusMapping request);
        Task<bool> DeleteEquipmentsMappingByID(DTETradeEquipmentsMapping request);
        Task<bool> HODEquipmentVerifications(DTETradeEquipmentsMapping request);
        Task<DataTable> GetAllRequestEquipmentsMapping(DTESearchTradeEquipmentsMapping SearchReq);
        Task<bool> SaveRequestEquipmentsMapping(DTETEquipmentsRequestMapping request);
        Task<DataTable> GetEquipment_Branch_Wise_CategoryWise(int Category);
        Task<DataTable> GetAllDeadStockReport(DTEItemsSearchModel SearchReq);
        Task<DataTable> GetAllAuctionReport(DTEItemsSearchModel SearchReq);
        Task<DataTable> GetAllinventoryIssueHistory(DTEItemsSearchModel SearchReq);
        Task<DataTable> GetAllDDL(DTEItemsSearchModel SearchReq);
        Task<DataTable> GetConsumeItemList(DTEItemsSearchModel SearchReq);
    }
}
