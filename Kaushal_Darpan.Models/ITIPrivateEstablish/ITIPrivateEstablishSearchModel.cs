using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIPrivateEstablish
{
    public class ITIPrivateEstablishSearchModel
    {
       

        public int StaffID { get; set; }
        public int StaffTypeID { get; set; }
        public int RoleID { get; set; }

        public int CourseID { get; set; }
        public int SubjectID { get; set; }
        
        public int InstituteID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int StaffLevelID { get; set; }
        public int Status { get; set; }
        public int? CourseTypeId { get; set; }

    }
}
