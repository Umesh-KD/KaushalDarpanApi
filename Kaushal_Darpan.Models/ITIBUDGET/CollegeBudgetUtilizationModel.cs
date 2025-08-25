using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIBUDGET
{
    
    public class CollegeBudgetUtilizationModel
    {
        public int BudgetUtilizationID { get; set; }
        public int DistributedID { get; set; }
        public int HeadID { get; set; }
        public string? HeadName { get; set; }
        public decimal? UtilizationAmount { get; set; }
        public string? UploadedFileName { get; set; }
        public int CollegeID { get; set; }
        public int FinYearID { get; set; }
        public int CreatedBy { get; set; }
        public string? Remarks { get; set; }
    }
}
