using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Report
{
    public class Report33DataModel: RequestBaseModel
    {
        public string? ExamDate { get; set; }
        public int ShiftID { get; set; }
        public int ExamCategoryID { get; set; }
        public int InstituteID  { get; set; }
        public int StudentExamType { get; set; }
        public int StreamID { get; set; }
        public int Status { get; set; }
        public int SemesterID { get; set; }
        public int UserID { get; set; }
        public string? SubjectCode { get; set; }
        public string? RollNumber { get; set; }
    }
}
