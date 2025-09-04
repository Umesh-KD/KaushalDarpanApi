using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTEInventoryModels
{
    public class DTEItemsModel
    {
        public int ItemId { get; set; }
        public int TradeId { get; set; }
        public int OfficeID { get; set; }
        public int RoleID { get; set; }
        public int ItemCategoryId { get; set; }
        public int EquipmentsId { get; set; }
        public string? IdentificationMark { get; set; }
        public string? CampanyName { get; set; }

        public int VoucherNumber { get; set; }
        public int Quantity { get; set; }

        public int PricePerUnit { get; set; }
        public int TotalPrice { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public bool isOption { get; set; }
        public int? ItemDetailsId { get; set; }
        public int Status { get; set; }

        public bool IsConsume { get; set; }
        public int ItemType { get; set; }

    }

    public class DTEItemsSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int OfficeID { get; set; }
        public int EquipmentsId { get; set; }
        public int CollegeId { get; set; }
        public int RoleID { get; set; }
        public int StatusID { get; set; }
        public int ItemId { get; set; }




    }
    public class DTEItemsDetailsModel
    {
        public string? Item { get; set; }
        public string? Category { get; set; }
        public string? Quantity { get; set; }
        public string? ItemCode { get; set; }
        public string? EquipmentCode { get; set; }
        public int ItemId { get; set; }
        public int? ItemDetailsId { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public int? EquipmentWorking { get; set; }
        public bool isOption { get; set; }
        public string? AuctionStatus { get; set; }

    }

    public class AuctionDetailsModel
    {

        public string AuctionDoc { get; set; }
        public string Dis_AuctionDoc { get; set; }
        public string AuctionDate { get; set; }
        public int AuctionQuantity { get; set; }
        public int? ItemDetailsId { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
        public bool isOption { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int OfficeID { get; set; }
        public string RowsID { get; set; }
    }


    public class EquipmentCodeDuplicateSearch
    {
        public string ItemCategoryName { get; set; }
        public string EquipmentsCode { get; set; }
        public int IsDuplicate { get; set; }
    }

    public class CheckItemAuctionSearch
    {
        public string ItemCategoryName { get; set; }
        public string EquipmentsCode { get; set; }
        public int ItemId { get; set; }
    }


    public class inventoryIssueHistorySearchModel
    {
        public int InstituteID { get; set; }
        public int CollegeId { get; set; }
        public string? TypeName { get; set; }
        public int TradeId { get; set; }
        public int StaffID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }

    }
    public class itemReturnModel
    {
        public int ItemCount { get; set; }
        public string? staffID { get; set; }
        public int ItemCondition { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Remarks { get; set; }        
        public int ItemDetailsId { get; set; }
        public string ItemList { get; set; }   // JSON array of items
        public int TransactionID { get; set; }
        public string Type { get; set; }

    }
}



    