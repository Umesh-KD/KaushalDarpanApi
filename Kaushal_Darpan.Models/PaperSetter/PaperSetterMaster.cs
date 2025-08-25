using Kaushal_Darpan.Models.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.PaperSetter
{
    public class PaperSetterMaster
    {
        public int PaperSetterID { get; set; }
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

    public class AppointPaperSetterDataModel : RequestBaseModel
    {
        public int? PaperSetterID { get; set; }
        public int? PaperSetterStatus { get; set; }
        public int? SemesterID { get; set; }
        public int? StreamID { get; set; }
        public int? SubjectID { get; set; }
        public int? ModifyBy { get; set; }
        public int? CommonSubjectYesNo { get; set; }
        public int? CommonSubjectID { get; set; }
        public string? IPAddress { get; set; }
        public List<StaffForPaperSetterListDataModel>? StaffDetails { get; set; }
    }

    public class StaffForPaperSetterListDataModel
    {
        public string? SSOID { get; set; }
        public string? Name { get; set; }
        public string? DesignationName { get; set; }
        public string? InstituteName { get; set; }
        public string? DateOfJoining { get; set; }
        public string? MobileNumber { get; set; }
        public string? SubjectName { get; set; }
        public string? Email { get; set; }
        public string? SubjectCode { get; set; }
        public string? StreamName { get; set; }
        public string? QualificationName { get; set; }

        public int? StaffID { get; set; }
        public int? StaffUserID { get; set; }
        public int? DesignationID { get; set; }
        public int? InstituteID { get; set; }
        public int? PaperSetterID { get; set; }
        public int? SubjectID { get; set; }
        public int? StreamID { get; set; }
        public int? StaffSubjectId { get; set; }

        public bool? IsAppointed { get; set; }
        public bool? Selected { get; set; }

        public int? GroupCodeID { get; set; }
        public int? ExamID { get; set; }
        public int? CommonSubjectYesNo { get; set; }
        public int? CommonSubjectID { get; set; }
    }

    public class VerifyPaperSetterDataModel
    {
        public int PaperSetterID { set; get; }
        public int? StaffID { set; get; }
        public int PaperSetterStatus { set; get; }
        public int UserID  { get; set; }
        public int? SemesterID { get; set; }
        public int? EndTermID { get; set; }
        public string? IPAddress  { get; set; }
        public string? GeneratedOrder  { get; set; }
        public string? GeneratedOrderPath { get; set; }
    }

}
