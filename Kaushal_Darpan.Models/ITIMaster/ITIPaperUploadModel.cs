using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIMaster
{
    public class ITIPaperUploadModel
    {
        public string? PaperUploadID { get; set; }
        public string? ExamID { get; set; }
        public string? ExamName { get; set; }
        public string? StreamID { get; set; }
        public string? SemesterID { get; set; }
        public string? Password { get; set; }
        public string? PaperID { get; set; }
        public string? EndTermID { get; set; }
        public string? FinancialYearID { get; set; }
        public string? FileName { get; set; }
        public string? Dis_FileName { get; set; }
        public DateTime? PaperDate { get; set; }
        public string? CenterCode { get; set; }
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int CourseType { get; set; }
    }

    public class ITIPaperUploadSearchModel
    {
        public int EndTermID { get; set; }

        public int Eng_NonEng { get; set; }
    }
}



