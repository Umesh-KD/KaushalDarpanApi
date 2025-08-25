using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTERInternalSliding
{
    public class BTERInternalSlidingDataModel
    {
    }

    public class BTERInternalSlidingSearchModel
    {
        public int AllotmentId { get; set; }
        public int CollegeID { get; set; }
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
        public int InsID { get; set; }
        public int UnitID { get; set; }
        public int CreatedBy { get; set; }
        public int StInstituteID { get; set; }
        public int UserId { get; set; }
        public string? action { get; set; }
        public string? IPAddress { get; set; }
        public int StreamTypeID { get; set; }
        public int AcademicYearID { get; set; }
        public int EndTermId { get; set; }
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationNo { get; set; }
        public int SwapApplicationID { get; set; }
        public string SwapApplicationNO { get; set; }
    }
}
