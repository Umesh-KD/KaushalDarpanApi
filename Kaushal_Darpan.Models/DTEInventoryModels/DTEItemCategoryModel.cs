using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTEInventoryModels
{
    public class DTEItemCategoryModel
    {
        public int ItemCategoryID { get; set; }
        public string ItemCategoryName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
    }
}
