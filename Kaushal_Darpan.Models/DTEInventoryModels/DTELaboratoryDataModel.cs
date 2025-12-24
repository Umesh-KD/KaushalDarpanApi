using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTEInventoryModels
{
    public class DTELaboratoryDataModel
    {
        public int StreamID { get; set; } = 0;
        public int LabID { get; set; } = 0;
        public string LabName { get; set; } = string.Empty;
        public int staffID { get; set; } = 0;
        public bool ActiveStatus { get; set; } = true;
        public bool DeleteStatus { get; set; } = false;
        public int CreatedBy { get; set; } = 0;
        public int ModifyBy { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public int OfficeID { get; set; } = 0;
        public int RoleID { get; set; } = 0;
        public string ActionName { get; set; } = string.Empty;
    }

    public class LabDetailsSearchModel
    {
        public int? UserID { get; set; }
    }
}
