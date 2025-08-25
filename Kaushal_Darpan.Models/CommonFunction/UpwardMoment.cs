using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CommonFunction
{
    public class UpwardMoment
    {
        public int ApplicationID { get; set; }
        public int AllotmentId { get; set; }
        public bool IsUpward { get; set; }
        public int UserID { get; set; }
    }

    public class ItiStuAppSearchModelUpward
    {
        public string ApplicationNo { get; set; }
        public int FinancialYearID { get; set; }
        public string SSOID { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public string action { get; set; }
    }
}
