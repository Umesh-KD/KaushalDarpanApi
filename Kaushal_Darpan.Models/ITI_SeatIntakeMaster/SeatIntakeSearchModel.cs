using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_SeatIntakeMaster
{
    public class SeatIntakeSearchModel
    {
        public int DistrictID { get; set; }
        public int CollegeTypeID { get; set; }
        public int CollegeID { get; set; }
        public int InstituteCategoryID { get; set; }
        public int TradeID { get; set; }
        public int TradeSchemeID { get; set; }
        public int RemarkID { get; set; }
        public string Shift {  get; set; }
        public string UnitNo { get; set; }
        public int SanctionedID { get; set; }
        public int StatusID { get; set; }
    }
    public class ITICollegeTradeSearchModel
    {

        public int ManagementTypeId { get; set; } = 0;
        public int CollegeID { get; set; } = 0;
        public string CollegeCode { get; set; }
        public int CollegeTradeId { get; set; } = 0;
        public int TradeID { get; set; } = 0;
        public int EndTermId { get; set; } = 0;
        public string TradeLevelId { get; set; } = "";
        public int DepartmentID { get; set; } = 0;
        public int AllotmentMasterId { get; set; } = 0;
        public string TradeCode { get; set; } = "";
        public int TradeSchemeId { get; set; } = 0;
        public int FeeStatus { get; set; } = 0;
        public int SeatStatus { get; set; } = 0;
        public bool ActiveStatus { get; set; } = true;
        public int PageSize { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int FinancialYearID { get; set; } = 0;
        public int TotalSeatAvailable { get; set; } = 0;
        public int CreateBy { get; set; } = 0;
        public string Action { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;
        public string? MinPercentageInMath { get; set; }
        public string? MinPercentageInScience { get; set; }









    }

    public class ITISeatMetrixSaveModel
    {
        public string BranchCode { get; set; }
        public string CollegeId { get; set; }
        public string CollegeTradeId { get; set; }
        public string Collegename { get; set; }
        public string EWS { get; set; }
        public string EWS_F { get; set; }
        public string EX { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public string GEN { get; set; }
        public string GEN_F { get; set; }
        public string InstituteStreamName { get; set; }
        public string KM { get; set; }
        public string MBC { get; set; }
        public string MBC_F { get; set; }
        public string MGM { get; set; }
        public string OBC { get; set; }
        public string OBC_F { get; set; }
        public string PH { get; set; }
        public string SC { get; set; }
        public string SC_F { get; set; }
        public string SMD { get; set; }
        public string ST { get; set; }
        public string ST_F { get; set; }

        public string MIN { get; set; }
        public string MIN_F { get; set; }

        public string DEV { get; set; }
        public string DEV_F { get; set; }

        public string SAH { get; set; }
        public string SAH_F { get; set; }


        public string IMC_SC { get; set; }
        public string IMC_ST { get; set; }
        public string IMC_OBC { get; set; }
        public string IMC_GEN { get; set; }


        public string Shift { get; set; }
        public string TradeId { get; set; }
        public string TradeSchemeId { get; set; }
        public string TradeLevelId { get; set; }
        public string TSP { get; set; }
        public string TSP_F { get; set; }
        public string TotalF { get; set; }
        public string TotalM { get; set; }
        public string TotalSeats { get; set; }
        public int UserId { get; set; }
        public string WID_DIV { get; set; }
        public string IPAddress { get; set; }

    }



}
