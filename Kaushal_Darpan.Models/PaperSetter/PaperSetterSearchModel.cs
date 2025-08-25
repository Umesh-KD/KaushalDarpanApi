using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaperSetter
{
    public class ITIFeeSearchModel
    {
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int SubjectID { get; set; }
        public int GroupCodeID { get; set; }
        public int ExamID { get; set; }
        public string? Name { get; set; }
        public int DepartmentID { get; set; }
    }
}
