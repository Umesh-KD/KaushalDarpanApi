using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BTER
{
    public class GETJanaadharListDataModel
    {
        public int InstituteID { get; set; }
        public int StudentID { get; set; }
        public string? InstituteCode { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
    }

    public class PostJanaadharDataModel
    {
        public int StudentID { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public string? JanAadharMemberId { get; set; }
    }

    public class GETStudentJanaadharDataModel
    {
        public int EnrollmentNo { get; set; }
        public string? InstituteCode { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Institute { get; set; }
        public string? Mobile { get; set; }
        public string? JanaadharNO { get; set; }
        public string? JanaadharStatus { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
    }
}