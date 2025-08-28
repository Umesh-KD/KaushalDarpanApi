using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.IDfFundDetailsModel
{
    public class DepositList
    {
        public string HeadName { get; set; } = string.Empty;
        public decimal ReceivedAmount { get; set; } = 0; // Use decimal for money
    }

    public class IDfFundDetailsModel
    {

        public string PrincipalName { get; set; } = string.Empty;
        public decimal OpeningBalance { get; set; } = 0;
        public int FinYearID { get; set; } = 0;
        public int FinancialYearID { get; set; } = 0;
        public int FinYearQuaterID { get; set; } = 0;
        public decimal ReceivedAmount { get; set; } = 0;
        public decimal Expense { get; set; } = 0;
        public decimal ClosingBalance { get; set; } = 0;
        public string Remark { get; set; } = string.Empty;
        public int InsituteID { get; set; } = 0;
        public int FundID { get; set; } = 0;
        public List<DepositList>? OtherDepositList { get; set; } 
   
    }

    public class IDfFundSearchDetailsModel :RequestBaseModel
    {

        public int FinYearID { get; set; } = 0;
        public int FundID { get; set; } = 0;
        public int FinYearQuaterID { get; set; } = 0;
        public string Action { get; set; } = "";
  


    }
    
}
