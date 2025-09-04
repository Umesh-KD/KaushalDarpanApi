using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StaffMaster
{
    public class StaffMasterSearchModel
    {
       

        public int StaffID { get; set; }
        public int StaffTypeID { get; set; }
        public int RoleID { get; set; }

        public int CourseID { get; set; }
        public int SubjectID { get; set; }
        
        public int InstituteID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public string SSOID { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public int DepartmentID { get; set; }
        public int StaffLevelID { get; set; }
        public int Status { get; set; }
        public int? CourseTypeId { get; set; }

    }

    public class BranchHODModel
    {
        public string? Action { get; set; }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string? SSOID { get; set; }
        public int StreamID { get; set; }
        public int CollegeID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MailPersonal { get; set; }
        public string? MobileNo { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string? StreamIDs { get; set; }
        public int SemesterID { get; set; }
    }

    public class Section
    {
        public string? SectionName { get; set; }
        public int StudentCount { get; set; }
    }

    public class SectionDataModel
    {
        public string? Action { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int StreamID { get; set; }
        public List<Section>? Section { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int SemesterID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    
    public class GetSectionDataModel
    {
        public string? Action { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int StreamID { get; set; }

        //public int? SemesterID { get; set; }
        public int SectionID { get; set; }
        public int StudentCount { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    
    public class GetSectionStudentDataModel
    {
        public string? EnrollmentNo { get; set; }
        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public int StudentName { get; set; }
        public int StreamName { get; set; }
        public int StreamID { get; set; }        
    }
    public class AllSectionBranchStudentDataModel
    {
        public string? EnrollmentNo { get; set; }
        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public int StreamID { get; set; }
        public int SectionID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
    public class GetSectionBranchStudentDataModel
    {
        public string? EnrollmentNo { get; set; }
        public int ApplicationID { get; set; }
        public int StudentID { get; set; }
        public int StreamID { get; set; }
        public int SectionID { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
    }

    public class StudentEnrCancelReqModel
    { 
        public int StudentID { get; set; }
        public int ActionBy { get; set; }
        public int NextRoleId { get; set; }
        public int IsApproveOrReject { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public int RoleID { get; set; }
        public int FinancialYearID { get; set; }
        public int InstituteId { get; set; }
        public string? Remarks { get; set; }
        public int CourstType { get; set; }
        public string? ActionType { get; set; }

    }

    public class GetAllRosterDisplayModel
    {
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public DateTime? AttendanceDate { get; set; }
    }
    public class SaveRosterDisplayModel
    {
        public string? Action { get; set; }
        public int ID { get; set; }
        public int SubjectID { get; set; }
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public string? AttendanceStartTime { get; set; }
        public string? AttendanceEndTime { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public List<int>? SectionID { get; set; }
    }

    public class SearchBranchDataModel
    {
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }
    }



    public class SaveRosterDisplayMultipleModel:RequestBaseModel
    {
     
        public int ID { get; set; }
        public int SubjectID { get; set; } 
        public int StreamID { get; set; }     
        public int StaffID { get; set; }
        public int SemesterID { get; set; }   
        public DateTime? AttendanceDate { get; set; }
        public string? AttendanceStartTime { get; set; }
        public string? AttendanceEndTime { get; set; }  
        public int CreatedBy { get; set; }    
        public List<int>? SectionID { get; set; }
    }



}
