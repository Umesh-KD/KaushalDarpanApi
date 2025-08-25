using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Examiners
{
    public class ExaminerMaster
    {
        public int ExaminerID { get; set; }
        //public int SemesterID { get; set; }
        //public int StreamID { get; set; }
        public int SubjectID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID{ get; set; }
        public int DesignationID { get; set; }
        public int ExamID { get; set; }
        public int GroupID { get; set; }
        public string? GroupCode { get; set; }
        public string? AssignGroupCode{ get; set; }
        public string? Name { get; set; }
        public string? SSOID { get; set; }
        public string? ExaminerCode { get; set; }
        public bool IsAppointed { get; set; }
        public int DepartmentID { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public int CourseType { get; set; }
        public int EndTermID { get; set; }
    }
}
