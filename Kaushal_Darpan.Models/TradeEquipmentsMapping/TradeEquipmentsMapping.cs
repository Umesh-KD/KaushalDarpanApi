using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.TradeEquipmentsMapping
{
    public class TradeEquipmentsMapping
    {
        public int TE_MappingId {  get; set; }
        public int TradeId {  get; set; }
        public int CategoryId {  get; set; }
        public int EquipmentId {  get; set; }
        public int Quantity {  get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int? TradeIdTypeId { get; set; }
    }

    public class SearchTradeEquipmentsMapping
    {
        public int CategoryId { get;  set; }
        public int EquipmentId { get;  set; }
        public int TradeId { get;  set; }
    }


}
