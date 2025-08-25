using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CollegeAdmissionSeatAllotment
{
    public class CollegeAdmissionSeatAllotmentModel
    {
    }

    public class ApplicationSearchDataModel
    {
        public int ApplicationId { get; set; }
        public int DepartmentID { get; set; }
        public string SSOID { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string? StudentName { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? Action { get; set; }

    }

    public class SeatMatrixSearchModel
    {
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicationNo { get; set; }
        public int CollegeTradeID { get; set; }
        public string AllotedCategory { get; set; }
        public string action { get; set; }
        public int TradeLevel { get; set; }
        public int AcadmicYearID { get; set; }


    }

    public class SeatAllocationDataModel
    {
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public string? MobileNo { get; set; }
        public int DirectAdmissionType { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int UserID { get; set; }
        public int CollegeTradeID { get; set; }
        public int ShiftUnit { get; set; }
        public int SeatMetrixId { get; set; }
        public string AllotedCategory { get; set; }
        public string SeatMetrixColumn { get; set; }
        public string action { get; set; }
    }
}
