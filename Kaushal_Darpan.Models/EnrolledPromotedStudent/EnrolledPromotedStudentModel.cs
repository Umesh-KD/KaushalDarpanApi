using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Student
{
    public class EnrolledPromotedStudentModel : RequestBaseModel
    {
        public int ApplicationID { get; set; } = 0;
        public string ApplicationNo { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public int InstituteID { get; set; } = 0;
        public int StreamID { get; set; } = 0;
        public int SemesterID { get; set; } = 0;
        public bool Selected { get; set; } = false;
        public string EnrollmentNo { get; set; } = string.Empty;
    }

    public class EnrolledPromotedStudentSaveModel : ResponseBaseModel
    {
        public int StudentId { get; set; } = 0;
        public int StudentExamID { get; set; } = 0;
        public string? Remark { get; set; } = string.Empty;
    }

}
