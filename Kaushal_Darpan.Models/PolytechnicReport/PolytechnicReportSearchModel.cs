using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PolytechnicReport
{
    public class PolytechnicReportSearchModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public string? Status { get; set; }
        public int UserID { get; set; }
        public string InstituteCode { get; set; }
        public string InstituteName { get; set; }
        public string Email { get; set; }
        public int ManagementType { get; set; }
        public int DistrictId { get; set; }
        public string SSOID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int RoleID { get; set; }
        public int ZoneID { get; set; }
        public int StreamID { get; set; }
        public int TehsilID { get; set; }
        public int ExamTypeID { get; set; }
        public int StreamTypeID { get; set; }

    }

}
