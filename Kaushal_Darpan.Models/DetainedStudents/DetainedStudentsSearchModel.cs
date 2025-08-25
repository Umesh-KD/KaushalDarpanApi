using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DetainedStudents
{
    public class DetainedStudentsSearchModel:RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public string? EnrollmentNo { get; set; }
    }
}
