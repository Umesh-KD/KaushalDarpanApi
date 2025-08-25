using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CommonFunction
{
    public class ItiTradeAndCollegesDDL
    {
        public class ItiTradeSearchModel
        {
            public int CollegeID { get; set; }
            public string? action { get; set; }
            public int TradeLevel { get; set; }
            public int IsPH {  get; set; }
            public int? TradeTypeId { get; set; }
            public int? ManagementTypeID { get; set; }
            public int? DistrictID { get; set; }
            public int FinancialYear {  get; set; }
            public int? Age {  get; set; }
            public int? Gender{  get; set; }
            public decimal? SciencePercentage { get; set; }
            public decimal? MathPercentage { get; set; }
        }

        public class ItiCollegesSearchModel
        {
            public int DistrictID { get; set; }
            public string? action {  set; get; }
            public string? ManagementType {  set; get; }
            public int ManagementTypeID { set; get; }
        }
    }
}
