namespace Kaushal_Darpan.Models.TimeTable
{
    public class TimeTableModel
    {
        public int AID { get; set; }
        public int TimeTableID { get; set; }
        public int SemesterID { get; set; }
        public string? ExamDate { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int InvigilatorID { get; set; }
        public int ShiftID { get; set; }
        public int StreamId { get; set; }
        public int SubjectID { get; set; }
        public string? SubjectCode { get; set; }
        public string? branchCode { get; set; }
        public string? SubjectName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public string ExamType { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public int IsYearly { get; set; } 
        public List<BranchSubjectDataModel>? BranchSubjectDataModel { get; set; }
    }

    public class BranchSubjectDataModel
    {
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public string? PaperCode { get; set; }
        public string? BranchName { get; set; }
        public int BranchID { get; set; }

    }

    public class TimeTableInvigilatorModel
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


    public class TimeTableValidateModel
    {
        public int TimeTableID { get; set; }
        public string? SubjectCode { get; set; }
        public int SemesterID { get; set; }
        //public List<StreamList>? StreamList { get; set; }
        public List<SubjectList>? SubjectList { get; set; }
    }

    public class StreamList
    {
        public int StreamID { get; set; }
        public string? Name { get; set; }

    }
    public class SubjectList
    {
        public int ID { get; set; }
        public string? Name { get; set; }

    }
    public class VerifyTimeTableList
    {
        public int TimeTableID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public bool Selected { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int ModuleID { get; set; }
        public string? TimeTablePdf { get; set; }
        public string? IPAddress { get; set; }
        public int IsYearly { get; set; }

    }


    public class TimeTableHeaderModel
    {
        public string OrderNo { get; set; } = string.Empty;
        public string CurrentDate { get; set; } = string.Empty;
        public string EndTermName { get; set; } = string.Empty;
        public string FinancialYearName { get; set; } = string.Empty;
        public string ExamName { get; set; } = string.Empty;
        public int SemesterID { get; set; } 
        public int EndTermID { get; set; }
        public string CourseTypeName { get; set; } = string.Empty;
        public string YearName { get; set; } = string.Empty;
        public string Var1 { get; set; } = string.Empty;
        public string CourseTypeNameFull { get; set; } = string.Empty;
        public string ExamScheme { get; set; } = string.Empty;
        public string CommonSubjectText { get; set; } = string.Empty;
        public string SignatureFile { get; set; } = string.Empty;

    }

}
