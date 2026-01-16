using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIResults
{
    public class ITIResultsModel
    {
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public int SemesterID { get; set; }
        public int UserID { get; set; }
        public int InstituteId { get; set; }
        public string Action { get; set; }
        public int ExamType { get; set; }
        public int TradeScheme { get; set; }

    }

    public class ITIStudentPassFailResultsModel
    {
        public int FinancialYearID { get; set; }
        public int EndTermId { get; set; }
        public int SemesterID { get; set; }
        public int UserID { get; set; }
        public int InstituteId { get; set; }
        public string Action { get; set; }
        public int ExamType { get; set; }
        public int TradeScheme { get; set; }
        public int Results { get; set; }
        public int StudentType { get; set; }
        public int is_appeared { get; set; }
        public int TradeId { get; set; }

    }

    public class TradeSubjectModel
    {
        public int TradeId { get; set; }
        public string TradeCode { get; set; }
        public string TradeName { get; set; }
        public string DurationYear { get; set; }
        public string AcadSession { get; set; }
        public int SubjectId { get; set; }
    }
}
