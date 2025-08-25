using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ApplicationMessageModel
{
    public class ApplicationMessageDataModel
    {
        public string? MobileNo { get; set; }
        public string? MessageType { get; set; }
        public string? ApplicantName { get; set; }
        public string? ApplicationNo { get; set; } = string.Empty;
        public string? ApplicationType { get; set; } = string.Empty;
        public int? DepartmentID { get; set; } = 0;
        public string? Scheme { get; set; } = string.Empty;
        public int? MeritId { get; set; }

        public List<ApplicationDetails>? ApplicationDetails { get; set; }

    }

    public class ApplicationDetailsModel
    {
        public string ApplicationID { get; set; }
        public string ApplicationNo { get; set; }
        public string Remark { get; set; }
        public string StudentName { get; set; }
        public string DepartmentName { get; set; }

    }

    public class ApplicationDetails
    {
        public string? ApplicationID { get; set; }

    }
}
