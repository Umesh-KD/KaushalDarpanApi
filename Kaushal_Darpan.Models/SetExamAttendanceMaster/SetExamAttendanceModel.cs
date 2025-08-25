using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.SetExamAttendanceMaster
{
    public class SetExamAttendanceModel
    {
        //public int ExamAttendanceID { get; set; }
        public int StudentID { get; set; }
        public int InstituteID { get; set; }
        public int FinancialYearID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int PaperID { get; set; }
        public int StreamID { get; set; }
        public  string? StudentRollNo { get; set; }
        public bool IsPresent { get; set; }
        public bool IsUFM { get; set; }
        public bool IsDetain { get; set; }
        public bool isFinalSubmit { get; set; }
        public string? Attendence_FileName { get; set; }
        public string? Attendence_Dis_FileName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int StudentExamPaperID { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int StudentExamID { get; set; }
        public string? Dis_UFMDocument { get; set; }
        public string? UFMDocument { get; set; }
    }
}


