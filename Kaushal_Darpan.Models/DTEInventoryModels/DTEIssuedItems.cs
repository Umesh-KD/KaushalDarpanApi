using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTEInventoryModels
{
    public class DTEIssuedItems
    {
        public int IssuedId { get; set; }
        public int EquipmentsId { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public int TradeId { get; set; }
        public int? IssueNumber { get; set; }
        public int? IssueQuantity { get; set; }
        
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
    public class DTEIssuedItemsSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
        public int EquipmentsId { get; set; }
        public int TradeId { get; set; }
        public int? Issuenumber { get; set; }
        public string EquipmentCode { get; set; }
        public DateTime? Issuedate { get; set; }
        public int? IssueQuantity { get; set; }
    }

    public class DTEReturnItemSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int EquipmentsId { get; set; }
        public int CategoryId { get; set; }
        public int? Issuenumber { get; set; }
        public DateTime? Issuedate { get; set; }
        public string FilterIssuedate { get; set; }
    }

    public class DTEStoksSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int EquipmentsId { get; set; }
        public int TradeId { get; set; }
        public int InstituteID { get; set; }

    }

    public class ReturnDteItemSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int EquipmentsId { get; set; }
        public int CategoryId { get; set; }
        public int? Issuenumber { get; set; }
        public DateTime? Issuedate { get; set; }
        public string FilterIssuedate { get; set; }
    }

    public class ReturnDteIssuedItems
    {
        public int IssuedId { get; set; }
        public int ItemId { get; set; }
        public int? ReturnIssueNumber { get; set; }
        public int? ReturnQuantity { get; set; }
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

    public class CommonSearchModal
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
        public int OfficeID { get; set; }
    }

}
