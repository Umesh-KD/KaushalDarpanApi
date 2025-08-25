using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIPracticalExaminer
{

    public class ITIPracticalExaminerModel
    {
        public int CenterID { get; set; }
        public int InstituteID { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CourseTypeID { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ITIPracticalExaminerSearchFilter : RequestBaseModel
    {

        public int CenterID { get; set; }
        public int StudentExamID { get; set; }
        public int ExamType { get; set; }
        public int SemesterID { get; set; }
        public string? Name { get; set; }
        public string? CenterCode { get; set; }
        public string? CenterName { get; set; }
        public string? ExaminerName { get; set; }
        public string? ExaminerSSOID { get; set; }
        public string? Action { get; set; }
        public string? SSOID { get; set; }
        public int UserID { get; set; }
        public int InstituteID {get; set;}
        public int DistrictID {get; set;}
    }

    public class PracticalExaminerSearchModel
    {
        public int? DistrictID { get; set; }
        public int? InstitutionCategoryID { get; set; }
        public int? TehsilID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int CenterID { get; set; }
        public int Eng_NonEng { get; set; }
    }
    public class PracticalExaminerDetailsModel : RequestBaseModel
    {
        public int? CenterAssignedID { get; set; }
        public int? CenterID { get; set; }
        public int? InsituteID { get; set; }
        public int UserID { get; set; }

        public string? IPAddress { get; set; } = string.Empty;
        public int? CreatedBy { get; set; }
        public  int TimeTableID {get; set;}
        public string? MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? SSOID { get; set; } = string.Empty;
        public string? Name { get; set; }
    }


    public class ItiPracticalExaminerDDLDataModel : RequestBaseModel
    {
        public int InstituteID { get; set; }
        public int TimeTable  {get; set;}
    }

    public class ITIExaminerDataModel
    {
        public int SemesterID { get; set; }
        public int CenterID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        //public string? SubjectCode { get; set; }
    }


}
