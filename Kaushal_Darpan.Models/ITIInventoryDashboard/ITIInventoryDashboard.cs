using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIInventoryDashboard
{
    public class ITIInventoryDashboard
    {
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int UserID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public string Menu { get; set; }
        public int Status { get; set; }
    }
}
