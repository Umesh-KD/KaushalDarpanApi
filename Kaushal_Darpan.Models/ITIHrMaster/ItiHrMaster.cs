using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIHrMaster
{
    public class ItiHrMaster
    {
        public int HRManagerID { get; set; }
        public int PlacementCompanyID { get; set; }
        public string Name { get; set; } // Name of 
        public string MobileNo { get; set; }
        public string EmailId { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }

        public int ModifyBy { get; set; }

        public string? IPAddress { get; set; }
    }
}
