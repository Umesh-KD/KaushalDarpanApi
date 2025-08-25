using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItemsMaster
{
    public class ItemsModel
    {
        public int ItemId { get; set; }        
        public int TradeId { get; set; }       
        public int ItemCategoryId { get; set; }
        public int EquipmentsId { get; set; }  
        public string IdentificationMark { get; set; }
        public string CampanyName { get; set; }
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
        public int? TradeIdTypeId { get; set; }
        //public string Status { get; set; }
    }

    public class ItemsSearchModel
    {
        public int TradeId { get; set; }
        public int EquipmentsId { get; set; }
        public int CollegeId { get; set; }
    }

    public class ItemsDetailsModel
    {
        public string? Item { get; set; }
        public string? Category { get; set; }
        public string? Quantity { get; set; }
        public string? ItemCode { get; set; }
        public string EquipmentCode { get; set; }
        public int ItemId { get; set; }
        public int? ItemDetailsId { get; set; }
    }

}
