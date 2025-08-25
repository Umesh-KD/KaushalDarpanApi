using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.GenerateEnroll
{
    public class GenerateEnrollMaster
    {
        public int StudentID { get; set; }
        public int ApplicationID { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public string? temp_Enrollment { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? InstituteName { get; set; }
        public string? SemesterName { get; set; }
        public string? StreamName { get; set; }
        public int EndTermID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int CourseTypeID { get; set; }

        
        public string? EnrollmentNo { get; set; }
        public string? StudentCategory { get; set; }
        public int InstituteCode { get; set; }
        public string? DOB { get; set; }
        public int PDFType { get; set; }

    }

    public class GenerateEnrollSearchModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int  Status { get; set; }
        public int ModuleID { get; set; }
        public int? VerifierStatus { get; set; }
        public int StudentTypeID { get; set; }
        public string? Action { get; set; }
        public string? Remark { get; set; }
        public int UserID { get; set; } 
        public int RoleID { get; set; }
        public int ShowAll { get; set; }
        public int PDFType { get; set; }
        public int StatusID { get; set; }
        public bool IsExaminationVerified { get; set; }
        public bool IsRegistrarVerified { get; set; }
        public int PublishOrder { get; set; }
    }
}
