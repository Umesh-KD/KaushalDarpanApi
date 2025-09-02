using System;

namespace Kaushal_Darpan.Models.ITIAllotment
{


    public class AllotmentdataModel
    {
        public int AllotmentId { get; set; }
        public int TradeLevel { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
        public int AcademicYearID { get; set; }
        public string IPAddress { get; set; }
    }

    public class SearchModel
    {
        public string? AllotmentId { get; set; }
        public int CollegeID { get; set; }
        public int StreamTypeID { get; set; }
        public int AllotmentMasterId { get; set; }
        public int PageNumber { get; set; }
        public int AcademicYearID { get; set; }
        public int PageSize { get; set; }
        public int TradeID { get; set; }
        public int InstituteID { get; set; }
        public int TradeSchemeId { get; set; }
        public int StInstituteID { get; set; }
        public int TradeLevel { get; set; }
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
        public int IsPH { get; set; }
        public string? action { get; set; }
        public string SearchText { get; set; } = "";
        public string FilterType { get; set; } = "";
        public string CollegeCode { get; set; } = "";
        public string StreamCode { get; set; } = "";
        public string TradeCode { get; set; } = "";
        public string FeePaid { get; set; } = "";
        public int ManagementTypeID { get; set; } 
        public string ?ReportingStatus { get; set; } 

    }

    public class ITIDirectAllocationDataModel
    {
        public int InstituteID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public int AcademicYearID { get; set; }
        public string? MobileNo { get; set; }
        public int DirectAdmissionType { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public int CollegeTradeID { get; set; }
        public int TradeLevel { get; set; }
        public int ShiftUnit { get; set; }
        public int SeatMetrixId { get; set; }
        public string AllotedCategory { get; set; }
        public string SeatMetrixColumn { get; set; }
    }
    public class ITIDirectAllocationSearchModel
    {
        public int InstituteID { get; set; }
        public int AcademicYearID { get; set; }
        public int TradeID { get; set; }
        public int ApplicationID { get; set; }
        public int ApplicationNo { get; set; }
        public int CollegeTradeID { get; set; }
        public string AllotedCategory { get; set; }
        public int TradeLevel { get; set; }


    }


    public class StudentthdranSeatModel
    {
        public int? AllotmentId { get; set; }
        public int? CollegeID { get; set; }
        public int ApplicationID { get; set; }
        public string DoucmentName { get; set; } = string.Empty;
        public int? UserID { get; set; } 
        public string? Remarks { get; set; } 

    }


}
