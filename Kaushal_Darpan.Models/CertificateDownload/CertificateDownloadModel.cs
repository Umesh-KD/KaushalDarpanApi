using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CertificateDownload
{

    public class CertificateSearchModel
    {
        public int MigrationID { get; set; }
        public int ExamTypeID { get; set; }
        public string? EnrollmentNo { get; set; }
        public int RevisedID { get; set; }
        public int RWHResultID { get; set; }
        public int EndTermID { get; set; }
        public int MigrationTypeID { get; set; }
        public int ProvisionalTypeID { get; set; }
        public int InstituteID { get; set; }
        public int UserID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
       

    }
}
