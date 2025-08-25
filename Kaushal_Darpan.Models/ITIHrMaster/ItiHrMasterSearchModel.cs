using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIHrMaster
{
    public class ItiHrMasterSearchModel
    {
        public string Name { get; set; }
        public int PlacementCompanyID { get; set; }
        public int DepartmentID { get; set; }
        public string Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
    }

    public class ItiHrMaster_Action
    {
        public int HRManagerID { get; set; }
        public int ActionBy { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public string Action { get; set; }
        public string? ActionRemarks { get; set; }
    }

}
