using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CollegeMaster
{
    public class CollegeMasterSearchModel:RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public string InstituteCode { get; set; }
        public string InstituteName { get; set; }
        public string Email { get; set; }
        public int ManagementType { get; set; }
        public int DistrictId { get; set; }
        public string SSOID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RoleID { get; set; } = 0;
        public int CourseType { get; set; } = 0;

        
    }

    public class CollageDashboardSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int CommonID { get; set; }
    }

    public class CollegeListSearchModel: RequestBaseModel
    {
      public int InstitutionManagementTypeID { get; set; }
      public int IsProfileComplete { get; set; }
    }
}
