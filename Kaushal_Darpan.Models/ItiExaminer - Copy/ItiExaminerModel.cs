using System.Net;
using System.Xml.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Models.ItiExaminer
{
    public class ITIExaminerModel
    {
        public   int ExaminerID { get; set; }
        public string SSOID { get; set; }
        public string Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string? FatherName { get; set; }
        public   int Gender { get; set; }
        public string? Email { get; set; }
        public   int District { get; set; }
        public string? Address { get; set; }
        public string? AadharNumber { get; set; }
        public string? BhamashahNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? EducationQualification { get; set; }
        public   int Branch_Trade { get; set; }
        public   int Designation { get; set; }
        public string? PostingPlace { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int InstituteID { get; set; }
        public int SubjectID { get; set; }

        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
    }


    public class ItiExaminerSearchModel
    {
        public int DepartmentID { get; set; }
        public string? ExaminerCode { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? SSOID { get; set; }
        public int DistrictID { get; set; }
        public int InstituteID { get; set; }
        public int BundleID { get; set; }


    }

    public class ITITeacherForExaminerSearchModel : RequestBaseModel
    {
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int TradeType { get; set; }
        public int RollFrom { get; set; }
        public int RollTo { get; set; }
        public int SubjectType { get; set; }
        public int StudentCount { get; set; }
        public int ExaminerID { get; set; }
        public bool IsTheory { get; set; }
        public bool IsPractical { get; set; }
        public string? SubjectCode { get; set; }
    }


    public class ITIExaminerMaster
    {
        public int ExaminerID { get; set; }
        //public int SemesterID { get; set; }
        //public int StreamID { get; set; }
        public int SubjectID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID { get; set; }
        public int DesignationID { get; set; }
        public int ExamID { get; set; }
        public int GroupID { get; set; }
        public string? GroupCode { get; set; }
        public string? AssignGroupCode { get; set; }
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
        public string RollFrom { get; set; }
        public string RollTo { get; set; }
    }

    public class ItiAssignStudentExaminer :RequestBaseModel 
    {
        public int StudentExamID { get; set; }  
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int SubjectID { get; set; }
        public int CenterID { get; set; }
        public int RollNo { get; set; }
        public int ExaminerID { get; set; }
        public int AppointExaminerID { get; set; }
        public int StudentExamPaperID { get; set; }
        public bool selected { get; set; }
        public string? IPAddress { get; set; }
        public int UserID { get; set; } 
        public bool IsTheory { get; set; }
        public bool IsPractical { get; set; }
    }

    public class ITI_ExaminerDashboardModel
    {
        public int EndTermID { get; set; }
        public int ExaminerID { get; set; }
        public string? SSOID { get; set; }
    }

    public class ITI_AppointExaminerDetailsModel : RequestBaseModel
    {
        public string? SSOID { get; set; }
        public string? filename { get; set; }
        public int ExaminerID { get;set; }
        public int Status { get; set; }
    }


    public class ITITheoryExaminerModel : RequestBaseModel
    {
        public int AppointExaminerID { get; set; }

        public int CenterID { get; set; }
        public int StreamID { get; set; }
        public int BundleID { get; set; }
        public int SemesterID { get; set; }

        public string? RollNoFrom { get; set; }
        public string? RollNoTo { get; set; }
        public string? ExaminerCode { get; set; }

        public int ExaminerID { get; set; }
        public string? Action { get; set; }
        public string? SSOID { get; set; }

        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int SubjectID { get; set; }


        public List<ItiAssignStudentExaminer> StudentList { get; set; }

    }




}
