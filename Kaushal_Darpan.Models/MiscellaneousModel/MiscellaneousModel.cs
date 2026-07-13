using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.LeaveMaster
{
    public class MiscellaneousModel
    {
        public int SemesterID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int RoleID { get; set; } = 0;
        public int UserID { get; set; } = 0;
        public string Action { get; set; } = string.Empty;
        public int SchemeID { get; set; } = 0;
        public int PresentStatus { get; set; } = 0;
        public int Type { get; set; } = 0;
        public string SubjectCode { get; set; } = string.Empty;
        public string SSOID { get; set; } = string.Empty;
        public string GroupCode { get; set; } = string.Empty;
        public int CourseType { get; set; } = 0;
    }

    public class GetMarksStatisticsModel
    {
        public int SemesterID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int DepartmentID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int RoleID { get; set; } = 0;
        public int UserID { get; set; } = 0;
        public string Action { get; set; } = string.Empty;
        public int SchemeID { get; set; } = 0;
        public int PresentStatus { get; set; } = 0;
        public int Type { get; set; } = 0;
        public string SubjectCode { get; set; } = string.Empty;
        public string SSOID { get; set; } = string.Empty;
        public string GroupCode { get; set; } = string.Empty;
        public int CourseType { get; set; } = 0;
    }

    public class ToppersModel
    {
        public int EndTermId { get; set; }
        public int CourseType { get; set; }
        public int BranchID { get; set; }
    }

    
     public class GetProvesionalMeritModel
    {
        public int EndTermId { get; set; }
        public int CourseType { get; set; }
        public int BranchID { get; set; }
    }
}