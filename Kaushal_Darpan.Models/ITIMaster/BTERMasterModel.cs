using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIMaster
{
  

    public class BTERMasterModel
    {
        public int TradeId { get; set; }

        public string TradeName { get; set; }
        public int TradeTypeId { get; set; }
        public int TradeLevelId { get; set; }
        public string MinPercentageInMath { get; set; }
        public string MinPercentageInScience { get; set; }
        public string DurationYear { get; set; }
        public string NoOfSemesters { get; set; }
        public string NoOfSanctionedSeats { get; set; }
        public string MinAgeLimit { get; set; }
        public string TradeCode { get; set; }
        public string QualificationDetails { get; set; }
        public bool IsMathsScienceCompulsory { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public bool OnlyForWomen { get; set; } = false;

    }
    public class BTERSearchModel
    {
        public int TradeId { get; set; }
        public string TradeName { get; set; } = string.Empty;
        public int TradeTypeId { get; set; }
        public int TradeLevelId { get; set; }
        public string DurationYear { get; set; } = string.Empty;
        public string TradeCode { get; set; } = string.Empty;
        public int CourseTypeID { get; set; }

    }

    public class SearchBTERModelRequest
    {
        public string Class { get; set; }
        public int CategoryID { get; set; }
    }


    public class BTERFeesPerYearSearchModel
    {
        public int TradeId { get; set; }
        public int TradeSchemeId { get; set; }
        public int CollegeId { get; set; }
        public int FinancialYearID { get; set; }
        public int ReportingStatus { get; set; }
        public int FeeStatus { get; set; }
        public int? CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public string TradeName { get; set; }
        public string TradeCode { get; set; }

    }


    public class BTERCollegeLoginInfoSearchModel
    {
        public int TradeId { get; set; }
        public int TradeSchemeId { get; set; }
        public int CollegeId { get; set; }
        public int FinancialYearID { get; set; }
        public int ReportingStatus { get; set; }
        public int FeeStatus { get; set; }
        public int? CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public string TradeName { get; set; }
        public string TradeCode { get; set; }
        public string collegeIdsString { get; set; }
        public string Password { get; set; }
        public string SSOID { get; set; }
        public string CollegeCode { get; set; }
        public int DepartmentID { get; set; }
        public int DistrictID { get; set; }
        public int ITItypeID { get; set; }

    }
}
