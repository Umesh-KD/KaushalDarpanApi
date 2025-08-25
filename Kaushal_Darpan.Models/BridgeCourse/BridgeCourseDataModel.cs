using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.BridgeCourse
{
    public class BridgeCourseStudentMasterModel
    {
        public bool Selected { get; set; }  
        public int StudentID { get; set; }  
        public string ApplicationNo { get; set; }  
        public string StudentName { get; set; }  
        public string FatherName { get; set; } 
        public string MotherName { get; set; } 
        public string EnrollmentNo { get; set; }  
        public string MobileNo { get; set; }  
        public string InstituteName { get; set; } 
        public string BranchName { get; set; } 
        public string SemesterName { get; set; }  
        public string DistrictName { get; set; }  

        public int EndTermID { get; set; }  
        public int FinancialYearID { get; set; }  
        public int StreamID { get; set; }  
        public int SemesterID { get; set; }  
        public int InstituteID { get; set; }  
        public string Dis_DOB { get; set; }  
        public int StudentTypeID { get; set; } 
        public string StudentType { get; set; }
        public bool IsBridge { get; set; }
    }

    public class BridgeCourseStudentMarkedModel
    {
        public bool Marked { get; set; }  
        public int StudentId { get; set; }   
        public int SemesterId { get; set; }  
        public int StudentTypeId { get; set; }  
        public int RoleId { get; set; }
        public int FinancialYearID { get; set; }
        public int InstituteID { get; set; }
        public int DepartmentID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }

        public string EnrollmentNo { get; set; }
        public string? MotherName { get; set; }
        public string? Dis_DOB { get; set; }
        public int ModifyBy { get; set; }       
        public string? IPAddress { get; set; }
        public int StreamID { get; set; }
        public int EndTermID { get; set; }      
 
        public bool IsBridge { get; set; }

    }

    public class BridgeCourseStudentSearchModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public string StudentTypeID { get; set; }
    }

}
