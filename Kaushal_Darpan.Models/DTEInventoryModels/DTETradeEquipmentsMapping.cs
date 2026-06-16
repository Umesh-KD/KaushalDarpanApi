using Kaushal_Darpan.Models.ItemsMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTEInventoryModels
{
    public class DTETradeEquipmentsMapping
    {
        public int TE_MappingId { get; set; }
        public int TradeId { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int TradeIdTypeId { get; set; }
        public int Status { get; set; }
        public int ItemID { get; set; }
        public string? Remark { get; set; }
        public int InstituteID { get; set; }
    }

    public class DTERequestTradeEquipmentsMapping
    {
        public int TE_MappingId { get; set; }
        public int TradeId { get; set; }
        public int OfficeID { get; set; }
        public int RoleID { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public int? ApprovedQuantity { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int TradeIdTypeId { get; set; }
        public int Status { get; set; }
        public int InstituteID { get; set; }
        public string? IdentificationMark { get; set; }
        public string? CompanyName { get; set; }
        public int PricePerUnit { get; set; }
        public int TotalPrice { get; set; }
        public string? VoucherNumber { get; set; }
        public string? Specification { get; set; }
        public int MappingId { get; set; }
    }

    public class DTEUpdateStatusMapping
    {
        public int TE_MappingId { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int Status { get; set; }
        public int OfficeID { get; set; }
    }

    public class DTETEquipmentsRequestMapping
    {
        public int TE_MappingId { get; set; }
        public int TradeId { get; set; }
        public int OfficeID { get; set; }
        public int RoleID { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int TradeIdTypeId { get; set; }
        public int Status { get; set; }
        public int InstituteID { get; set; }
        public string? IndentDocument { get; set; }
        public string? Remarks { get; set; }
    }
    public class DTESearchTradeEquipmentsMapping
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int OfficeID { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int TradeId { get; set; }
        public int InstituteID { get; set; }
    }

    public class ItemsIssueReturnModels : RequestBaseModel
    {
        public int? ItemId { get; set; }
        public int? TradeId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? StaffId { get; set; }
        public int? VoucherNumber { get; set; }
        public int? Quantity { get; set; }
        public decimal? PricePerUnit { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? CampanyName { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public int? InstituteID { get; set; }
        public int? UserId { get; set; }
        public int? TransactionID { get; set; }
    
        public int? ConditionAtReturn { get; set; }
        public string? StaffName { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Remarks { get; set; }
        public string? FileName { get; set; }
        public int staffId { get; set; }
        public int EquipmentsId { get; set; }
        public string? issuedTo { get; set; }
        public string? EquipmentName { get; set; }
        public int IssuedId { get; set; }
        public int itemCategoryId { get; set; }

        public List<ItemsDetails>? ItemList { get; set; }
        public int? ItemDetailsId { get; set; }

        public int? StreamID { get; set; }
        public int? LabID { get; set; }
        public int? RoleID { get; set; }
        public string? IndentNo { get; set; }
    }

    public class ItemsDetails
    {
        public string? Item { get; set; }
        public string? ItemCategoryName { get; set; }
        public int? Quantity { get; set; }
        public string? ItemCode { get; set; }
        public int? ItemId { get; set; }
        public int? EquipmentCode { get; set; }
        public int? ItemDetailsId { get; set; }
        public int? TransactionID { get; set; }
        public int? equipmentStatus { get; set; }
        public bool? isOption { get; set; }
        public int? AuctionStatus { get; set; }
        public int? UsedQuantity { get; set; }
        public int? IssuedId { get; set; }

    }


    public class DTETEquipmentsRequestMappingRevert
    {
        public int Id { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }     
        public int Status { get; set; }
        public string? Remark { get; set; }
    }


    public class InventoryIssueHistoryListModels
    {
       
        public int staffId { get; set; }
        
    }

    public class ItemsDataModels
    {
        public string? Action { get; set; }
        public int CategoryId { get; set; }
        public int EquipmentId { get; set; }
        public int InstituteID { get; set; }
        public int RoleID { get; set; }
    }


    public class CategoryListModel
    {
        public int S_No { get; set; }
        public string CategoryName { get; set; }
    }
    public class EquipmentListModel
    {
        public int S_No { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
    }

    public class DynamicReportResponseModel
    {
        public List<CategoryListModel> CategoryList { get; set; } = new();
        public List<EquipmentListModel> EquipmentList { get; set; } = new();
    }

}
