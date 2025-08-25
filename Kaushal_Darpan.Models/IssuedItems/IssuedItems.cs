using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.IssuedItems
{
    public class IssuedItems
    {
        public int IssuedId { get; set; }
        public int EquipmentsId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public int TradeId { get; set; }
        public int? IssueNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
    }

    public class IssuedItemsSearchModel
    {
        public int EquipmentsId { get; set; }
        public int TradeId { get; set; }
        public int? Issuenumber { get; set; }
        public string EquipmentCode { get; set; }
        public DateTime? Issuedate { get; set; }
    }

    public class ReturnItemSearchModel
    {
        public int EquipmentsId { get; set; }
        public int CategoryId { get; set; }
        public int? Issuenumber { get; set; }
        public DateTime? Issuedate { get; set; }
        public string FilterIssuedate { get; set; }
    }

    public class StoksSearchModel
    {
        public int EquipmentsId { get; set; }
        public int TradeId { get; set; }
       
    }


    public class ReturnIssuedItems
    {
        public int IssuedId { get; set; }
        public int ItemId { get; set; }
        public int? ReturnIssueNumber { get; set; }
        public DateTime? ReturnIssueDate { get; set; }
       
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int? ReturnStatus { get; set; }

        public string ReturnRemark { get; set; }
    }

}
