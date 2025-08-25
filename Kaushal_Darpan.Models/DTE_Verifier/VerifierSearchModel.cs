using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTE_Verifier
{
    public class VerifierSearchModel
    {
        public string Name { get; set; }
        public string SSOID { get; set; }
        public string MobileNo { get; set; }
        public int DepartmentID { get; set; }
        public int CourseType { get; set; } = 0;
        public int userid { get; set; } = 0;
        public int RoleID { get; set; } = 0;
    }
}
