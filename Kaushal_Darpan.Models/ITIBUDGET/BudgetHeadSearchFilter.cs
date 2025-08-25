using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIBUDGET
{
    public class BudgetHeadSearchFilter
    {
        public int FinYearID { get; set; }  
        public string? CollegeName { get; set; }
        public int CollegeID { get; set; }
        public string? ActionName { get; set; }
        public int? DistributedID { get; set; }

        public int? RequestID { get; set; }
        public int? CreatedBy { get; set; }
        public int? StatusId { get; set; }
        public int DistributedType { get; set; }


    }
}
