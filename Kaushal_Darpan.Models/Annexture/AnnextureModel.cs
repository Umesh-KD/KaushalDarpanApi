using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.LeaveMaster
{
    public class AnnextureModel
    {
        public int InstituteID { get; set; } = 0;
        public int SemesterID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int CourseTypeID { get; set; }
        public int UserID { get; set; } = 0;
        public string? InstituteName { get; set; }
        public string? EndTermName { get; set; }
        public string? InstituteCode { get; set; }
        public string? FileName { get; set; }
        public string? DisFileName { get; set; }
        public string? Code { get; set; }
    }
}
