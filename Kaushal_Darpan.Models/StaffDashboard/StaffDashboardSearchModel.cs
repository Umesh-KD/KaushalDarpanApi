using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StaffDashboard
{
    public class StaffDashboardSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorAppointmentID { get; set; }
        public int StaffID { get; set; }
        public string? SSOID { get; set; }
    }
    public class TotalStudentReportedListModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CourseType { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
    }


    public class HODDashboardSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorAppointmentID { get; set; }
        public int StaffID { get; set; }
        public string? SSOID { get; set; }
    }
    public class TotalHODReportedListModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CourseType { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
    }


}
