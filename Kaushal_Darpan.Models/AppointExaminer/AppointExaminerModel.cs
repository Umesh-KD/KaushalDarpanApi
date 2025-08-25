using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.AppointExaminer
{
    public  class AppointExaminerModel
    {
        public int AppointExaminerID { get; set; }
        public int CourseID { get; set; }
        public int SubjectID { get; set; }
        public int SemesterID { get; set; }
        public int ExaminerID { get; set; }
        public string RollNumberFrom { get; set; }
        public string RollNumberTo { get; set; }
        public string? SSOID { get; set; }
        public string GroupCode { get; set; }
        public int ModifyBy { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int DepartmentID { get; set; }
    }
}
