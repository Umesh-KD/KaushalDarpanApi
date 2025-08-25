using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIGenerateEnrollment
{
    public class ITIGenerateEnrollMaster
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


    }

    public class ITIGenerateEnrollSearchModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
    }
}
