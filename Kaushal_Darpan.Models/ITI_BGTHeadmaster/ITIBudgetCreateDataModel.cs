using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_BGTHeadmaster
{
    public class ITIBudgetCreateDataModel
    {
        public int? BudgetTypeID {  get; set; }
        public string? BudgetTypeName { get; set; }
        public int? AcademicYearID { get; set; }
        public int? BudgetForID { get; set; }
        public int? BudgetType_Cumulative_HeadWise { get; set; }
        public string? CumulativeAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? DistributedType { get; set; }
        public List<BudgetHeadList>? BudgetHeadList { get; set; }
        public int? UserID { get; set; }
    }

    public class BudgetHeadList
    {
        public int? HeadID { get; set; }
        public string? HeadName { get; set; }
        public bool? IsUnitWise { get; set; }
        public string? UnitName { get; set; }
        public decimal? Amount { get; set; }
    }
    public class ITIBudgetDropdownDataModel
    {
        public string? Action { get; set; }
    }
}
