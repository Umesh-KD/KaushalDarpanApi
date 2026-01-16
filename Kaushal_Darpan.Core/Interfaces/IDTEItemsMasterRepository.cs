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

        Task<int> UpdateStatusRevert(DTEItemsModel request);
        Task<DataTable> GetAll_INV_GetCommonIssueDDL(inventoryIssueHistorySearchModel SearchReq);
        Task<DataTable> GetConsumeItemList(DTEItemsSearchModel SearchReq);
        Task<bool> SaveIssueItems(ItemsIssueReturnModels request);
        Task<DataTable> GetInventoryIssueItemList(inventoryIssueHistorySearchModel SearchReq);
        Task<DataTable> GetAll_INV_returnItem(ItemsIssueReturnModels SearchReq);
        Task<DataTable> GetAllinventoryIssueHistory(inventoryIssueHistorySearchModel SearchReq);
        Task<DataTable> GetAllInventoryIssueReturnItemList(inventoryIssueHistorySearchModel SearchReq);
        Task<DataTable> GetItemListType(DTEItemsSearchModel SearchReq);
        Task<DataTable> GetAllItemList(DTEItemsSearchModel SearchReq);
        Task<int> SaveIssueItemsList(List<ItemsIssueReturnModels> request);
        Task<DataTable> GetAllinventoryIssueReport(ItemsIssueReturnModels SearchReq);
        
        Task<DataTable> GetIssueItemList(ItemsIssueReturnModels SearchReq);
        Task<DataTable> GetInventoryIssueHistoryList(InventoryIssueHistoryListModels SearchReq);
        Task<DataTable> GetDTEIssueItemListPermanent(int itemId);

        Task<DataTable> GetDTEIssueSubmitPermanent(ItemsIssueReturnModels SearchReq);
        Task<DataTable> GetDTEGetSetLabMaster(DTELabMasterModel SearchReq);
        Task<DataTable> DTE_INV_SaveLabItemReturn(ItemsIssueReturnModels SearchReq);
        Task<int> MarkForAuctionSR6(List<ItemsIssueReturnModels> request);
        Task<DataTable> Get_SR5_ReportData(inventoryIssueHistorySearchModel SearchReq);
        Task<DataTable> Get_SR6_ReportData(inventoryIssueHistorySearchModel SearchReq);
    }
}
