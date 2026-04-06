using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_SeatIntakeMaster
{
    public class BTERSeatIntakeDataModel
    {
        public int SeatIntakeID { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public string? Shift {  get; set; }
        public int LastSession { get; set; }
        public int RemarkID { get; set; }
        public int TradeSchemeID { get; set; }
        public string ?UnitNo { get; set; }
        public int SanctionedID { get; set; }
        public int DepartmentID { get; set; }
        public int TradeLevel { get; set; }

        public string? OrderNo { get; set; }
        public string? ActiveOrderNo { get; set; }
        public string? ActiveOrderDate { get; set; }
        public string? InActiveOrderNo { get; set; }
        public string? InActiveOrderDate { get; set; }

        public string? OrderDate { get; set; }
        public string? FinancialOrderDate { get; set; }
        public string? AdminOrderDate { get; set; }

        public int AcademicYearID { get; set; }
        public int PlanningID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? CollegeName { get; set; }
        public string? TradeName { get; set; }
        public string? TradeScheme { get; set; }
        public string? Remark { get; set; }
        public string? Unit_no { get; set; }
        public string? Sanctioned { get; set; }
        public string? LastSessionStr { get; set; }
        public string? LastUpdated { get; set; }
        public string? MinPercentageInMath { get; set; }
        public string? MinPercentageInScience { get; set; }
        public string? DurationYear { get; set; }
        public string? weDate { get; set; }
        public string? key { get; set; }
        public string? TradeCode { get; set; }
        public string? CollegeCode { get; set; }
        public int NoOfSanctionedSeats { get; set; }

        public int? FinancialSanctionID { get; set; }
        public int? AdminSanctionedID { get; set; }
        public string? Action { get; set; }


    }


    public class SanctionOrderModel
    {
        public int SanctionOrderID { get; set; }

        public string OrderNo { get; set; }

        public string AttachDocument { get; set; }

        public string AttachDocumentUrl { get; set; }

        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
    }


    public class SanctionOrderMasterModel
    {
        public int SanctionID   { get; set; }
        public int ParentID { get; set; }

        public string? Name { get; set; }

       
        public int DepartmentID { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
    }


    public class SeatIntakeChangeStatusModel
    {
        public int SeatIntakeID { get; set; } = 0;

        public int CollegeID { get; set; } = 0;

        public int ModifyBy { get; set; } = 0;

        public string IPAddress { get; set; } = string.Empty;

        public bool ActiveStatus { get; set; } = false;

        public int AcademicYearID { get; set; } = 0;

        public string Action { get; set; } = string.Empty;
    }

}
