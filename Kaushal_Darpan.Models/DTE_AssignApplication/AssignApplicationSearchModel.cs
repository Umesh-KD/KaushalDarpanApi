using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTE_AssignApplication
{
    public class AssignApplicationSearchModel
    {
        public int VerifierID { get; set; }
        public int Application {  get; set; }
        public int DepartmentID {  get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
    }

    public class StudentsAssignApplicationSearch
    {
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public  int CategoryC { get; set; }
        public int Gender { get; set; }
        public int CategoryD  { get; set; }
        public int ApplicationStatus { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string FromApplication { get; set; }
        public string ToApplication { get; set; }
        public string Action { get; set; } = "";
        public int DepartmentID { get; set; }

        public int Eng_NonEng { get; set; }
        public int AcademicYearID { get; set; }
        public int VerifierID { get; set; } = 0;
    }
}
