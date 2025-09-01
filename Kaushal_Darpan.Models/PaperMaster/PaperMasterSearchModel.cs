using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaperMaster
{
    public class PaperMasterSearchModel
    {
        public int DepartmentID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CourseTypeID { get; set; }
        public int FinancialYearID { get; set; }
        public int EndTermID { get; set; }

    }

    public class SubjectBranchWiseSearchModel
    {
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
    }
}
