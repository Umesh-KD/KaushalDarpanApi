using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIBUDGET
{
    public class BudgetRequestModel
    {
        public int RequestID { get; set; }
        public int FinYearID { get; set; }
        public int CollegeId { get; set; }
        public decimal RequestAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string? Remarks { get; set; }
        public string? RequestFileName { get; set; }
        public string? Dis_FilePath { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int UserID { get; set; }
        public int RoleId { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public string? Action { get; set; }
        public int StatusId { get; set; }

    }
}
