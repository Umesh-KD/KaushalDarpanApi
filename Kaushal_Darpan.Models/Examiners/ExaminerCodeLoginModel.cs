using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Examiners
{
    public class ExaminerCodeLoginModel : RequestBaseModel
    {
        public int ExaminerID { get; set; }
        public int? GroupCodeID { get; set; }
        public string SSOID { get; set; }        
        public string ExaminerCode { get; set; }        
    }

    public class ExaminerDashboardSearchModel : RequestBaseModel
    {
        public int RoleID { get; set; }
        public string SSOID { get; set; }
        public string ExaminerCode { get; set; }
        public int InstituteId { get; set; }
    }


}
