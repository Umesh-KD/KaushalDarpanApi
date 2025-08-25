using Kaushal_Darpan.Models.StaffMaster;

namespace Kaushal_Darpan.Models.ITITimeTable
{
    public class ITITimeTableModel
    {
        public int TimeTableID { get; set; }
        public int SemesterID { get; set; }
        public string? ExamDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int InvigilatorID { get; set; }
        public int ShiftID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public string? SubjectName { get; set; }
        public string? TradeName { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
        public int Eng_NonEng { get; set; }

        public List<TradeSubjectDataModel>? TradeSubjectDataModel { get; set; }
    }

    public class TradeSubjectDataModel
    {
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public string? TradeName { get; set; }
        public int TradeID { get; set; }
    }


    public class ITI_TimeTableInvigilatorModel
    {
        public int ID { get; set; }
        public int TimeTableID { get; set; }
        public int SemesterID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
    }


    public class NewITI_TimeTableValidateModel
    {
        public int TimeTableID { get; set; }
        public string? SubjectCode { get; set; }
        public int SemesterID { get; set; }
        //public List<StreamList>? StreamList { get; set; }
        public List<ITISubjectList>? ITISubjectList { get; set; }
    }

    public class StreamList
    {
        public int StreamID { get; set; }
        public string? Name { get; set; }

    }
    public class ITISubjectList
    {
        public int ID { get; set; }
        public string? Name { get; set; }

    }

    //public class ITITimeTableSearchModel
    //{
    //    public int? DepartmentID { get; set; }
    //    public int? SemesterID { get; set; }
    //    public int? InstituteID { get; set; }
    //    public int? EndTermID { get; set; }
    //    public int? ShiftID { get; set; }
    //    public int? FinancialYearID { get; set; }
    //    public int? Eng_NonEng {  get; set; }
    //    public string? Action {  get; set; }
    //    public int? Status { get; set; }
    //}


}
