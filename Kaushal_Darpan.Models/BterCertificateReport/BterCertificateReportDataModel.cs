using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BterCertificateReport
{
    public class BterCertificateReportDataModel
    {

        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public string Action { get; set; }
        public string? EnrollmentNo { get; set; }
        public int RollNo { get; set; }
        public int RevisedType { get; set; }
        public int ResultType { get; set; }
        public int SemesterType { get; set; }
        public int StudentID { get; set; }
        public int MigrationType { get; set; }
        public int CertificateType { get; set; }
        public int RWHEffectedEndTerm { get; set; }
        public int? IsBridge { get; set; }

    }
    public class BterStatisticsReportDataModel
    {
        public int AcademicYearID { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public string? Action { get; set; }
        public int RoleID { get; set; }



    }

}
