using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITICenterAllocaqtion
{

    public class ITICenterAllocationModel
    {
        public int CenterID { get; set; }
        public int InstituteID { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CourseTypeID { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITICenterAllocationSearchFilter:RequestBaseModel
    {
 
        public int CenterID { get; set; }
        public string? Name { get; set; }
        public string? CenterCode { get; set; }
        public string? CenterName { get; set; }

        public int UserID { get; set; }

        public int InstituteID { get; set; }
        public int DistrictID { get; set; }
    }


}
