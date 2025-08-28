using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Student
{
    public class StudentDetailsModel
    {
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? CategoryMeritNo { get; set; }
        public string? Email { get; set; }
        public string StreamName { get; set; }
        public string Semester { get; set; }
        public string FeeAmount { get; set; }
        public string? EnrollFeeAmount { get; set; }
        public string? LastDate { get; set; }
        public string FeeStatus { get; set; }
        public string? RollNo { get; set; }
        public string? EndTermName { get; set; }
        public string? EndTermType { get; set; }
        public int SemesterID { get; set; }
        public int ExamStudentStatus { get; set; }
        public int StudentSemesterID { get; set; }
        public int ActionBy { get; set; }
        public string SSOID { get; set; }
        public string Remark { get; set; }
        public int CurrentSemesterID { get; set; }
        public int ServiceID { get; set; }
        public int InstituteID { get; set; }
        public int ID { get; set; }
        public decimal ExamFee { get; set; }
        public string InstituteName { get; set; }
        public string StrStudenetStatus { get; set; }
        public string RoleName { get; set; }
        public string DOB { get; set; }
        public string AdmitCard { get; set; }
        public bool IsSelected { get; set; }
        public string[] strSelectedIds { get; set; }
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }

        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int CourseType { get; set; }
        public int ModuleID { get; set; }
        public int? StudentExamID { get; set; }
        public string? Subjects { get; set; }
        public int TransactionID { get; set; }
        public string PRN { get; set; } = string.Empty;
        public bool IsShowAdmitCard { get; set; }
        public bool DownloadExaminationForm { get; set; }

        public string? MaxMarks { get; set; }

        public int Status { get; set; }
        public string? StatusName { get; set; }
        public string Dis_ENRCancelDoc { get; set; }
        public string ENRCancelDoc { get; set; }
        public string? MinMarks { get; set; }

        public string? ObtainedMarks { get; set; }
        public bool isPublish { get; set; }
        public string? ResultStatus { get; set; }
        public string? ServiceType { get; set; }
    }

    public class StudentSearchModel : RequestBaseModel
    {
        public int StudentID { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public string SsoID { get; set; }

        public string Action { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public string PrnNo { get; set; }
        public string ApplicationNo { get; set; }
        public string EnrollmentNo { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string CreateDate { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DocumentMasterID { get; set; }
        public int ChallanNo { get; set; }
        public int FinancialYearID { get; set; }
        public int InstituteID { get; set; }
        public int TrasactionStatus { get; set; }
        public int? StudentExamID { get; set; }

    }

    public class StudentExamMarksUpdateModel
    {
        public int StudentExamPaperMarksID { get; set; }
        public decimal ObtainedMarks { get; set; }
        public bool IsPresent { get; set; }
        public bool IsFinalSubmit { get; set; }
        public int UserID { get; set; }
        public string? IPAddress { get; set; }
        public string? FileName { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }


    public class StudentEnrolmentCancelModel
    {
        public int StudentID { get; set; }
        public int NextRoleId { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int ActionId { get; set; }
        public int IsRequestedForEnrCancel { get; set; }
        public int InstituteID { get; set; }
        public int CourseType { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int RoleID { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? EndTermType { get; set; }
        public string? EndTermName { get; set; }

        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string DOB { get; set; }
        public string StreamName { get; set; }
        public string InstituteName { get; set; }
        public string Action { get; set; }
        public string Remark { get; set; }
        public string Dis_ENRCancelDoc { get; set; }
        public string ENRCancelDoc { get; set; }
    }

    public class StudentApplicationModel : RequestBaseModel
    {
        public int? ApplicationID { get; set; }
        public string? ApplicationNo { get; set; }
        public string? MobileNo { get; set; }
        public string? StudentName { get; set; }
        public int InstituteID { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public bool Selected { get; set; }

    }
    public class StudentApplicationSaveModel : ResponseBaseModel
    {
        public int ApplicationID { get; set; }
        public string Remark { get; set; }
    }

    public class ForSMSEnrollmentStudentMarkedModel : RequestBaseModel
    {

        public int StudentId { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        public string? ApplicationNo { get; set; }
        public string? MobileNo { get; set; }
        public string? MessageType { get; set; }

        //public int? EndTermID { get; set; }
    }
}
