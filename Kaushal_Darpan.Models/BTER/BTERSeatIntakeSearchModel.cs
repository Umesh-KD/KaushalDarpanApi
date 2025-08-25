using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_SeatIntakeMaster
{
    public class BTERSeatIntakeSearchModel
    {
        public int DistrictID { get; set; }
        public int CollegeTypeID { get; set; }
        public int CollegeID { get; set; }
        public int InstituteCategoryID { get; set; }
        public int TradeID { get; set; }
        public int TradeSchemeID { get; set; }
        public int AcademicYearID { get; set; }
        public int RemarkID { get; set; }
        public string Shift {  get; set; }
        public string UnitNo { get; set; }
        public int SanctionedID { get; set; }
        public int StatusID { get; set; }
        public int EndTermId { get; set; } = 0;
        public string MinPercentageInMath { get; set; }
        public string MinPercentageInScience { get; set; }
        public string CollegeCode { get; set; }
        public string TradeCode { get; set; }
    }
    public class BTERCollegeTradeSearchModel
    {
        public int ManagementTypeId { get; set; } = 0;
        public int CollegeID { get; set; } = 0;
        public string CollegeCode { get; set; } = "";
        public string StreamCode { get; set; } = "";
        public int CollegeStreamId { get; set; } = 0;
        public int StreamID { get; set; } = 0;
        public int TradeLevelId { get; set; } = 0;
        public int TradeCode { get; set; } = 0;
        public int StreamTypeId { get; set; } = 0;
        public int FeeStatus { get; set; } = 0;
        public int ActiveStatus { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int FinancialYearID { get; set; } = 0;
        public int EndTermId { get; set; } = 0;
        public string Action { get; set; } = string.Empty;
        public int ShiftID { get; set; } = 0;
        public int TotalSeats { get; set; } = 0;
        public int TotalSeatAvailable { get; set; }
        public int TotalAdmissionSeats { get; set; } = 0;
        public int CreatedBy { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int AllotmentMasterId { get; set; } = 0;
        public int Status { get; set; } = 0;
        public int StreamFor { get; set; } = 0;
        public string IPAddress { get; set; } = "";               

    }

    public class BranchStreamTypeWiseSearchModel
    {
        public int StreamTypeId { get; set; }
        public string? Action { get; set; }
        public int EndTermId { get; set; } = 0;
    }
}
//