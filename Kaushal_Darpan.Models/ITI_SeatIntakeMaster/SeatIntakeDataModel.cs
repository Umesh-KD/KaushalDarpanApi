using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_SeatIntakeMaster
{
    public class SeatIntakeDataModel
    {
        public int SeatIntakeID { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public string Shift {  get; set; }
        public int LastSession { get; set; }
        public int RemarkID { get; set; }
        public int TradeSchemeID { get; set; }
        public string UnitNo { get; set; }
        public int SanctionedID { get; set; }
        public int DepartmentID { get; set; }

        public int AcademicYearID { get; set; }

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
    }
}
