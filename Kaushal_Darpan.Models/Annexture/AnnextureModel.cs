using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.LeaveMaster
{
    public class AnnextureModel
    {
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int Eng_NonEng { get; set; } 
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CourseTypeID { get; set; } 
        public string? InstituteName { get; set; } 
        public string? EndTermName { get; set; } 
        public int? InstituteCode { get; set; } 

    }
}
