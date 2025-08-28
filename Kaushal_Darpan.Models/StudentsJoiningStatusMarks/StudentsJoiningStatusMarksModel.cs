namespace Kaushal_Darpan.Models.StudentsJoiningStatusMarks
{
    public class StudentsJoiningStatusMarksModel
    {
        public int ReportingId { get; set; }
        public int AllotmentId { get; set; }
        public string? ReportingStatus { get; set; }
        public string? ReportingRemark { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }

    }
    public class StudentsJoiningStatusMarksSearchModel
    {


        public int AllotmentId { get; set; }

        public int AllotmentMasterId { get; set; }

        public int ApplicationID { get; set; }

        public int CollegeId { get; set; }

        public int TradeId { get; set; }
        public int TradeLevel { get; set; }
        public string? ReportingStatus { get; set; }
        public string? TradeCode { get; set; }
        public int TradeSchemeId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int FinancialYearID { get; set; }


    }


    public class ReportCollegeForAdminModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
    }



    public class ReportCollegeModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
    }


}
