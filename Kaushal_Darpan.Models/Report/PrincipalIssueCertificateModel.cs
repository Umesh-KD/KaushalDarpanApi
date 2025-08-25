using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Report
{
    public class PrincipalIssueCertificateModel
    {
        
        public int UserID { get; set; }
        public string ?Name { get; set; }
        public string? Designation { get; set; }
        public string? InstituteName { get; set; }
        public string? IssueDate { get; set; }

    }
}
