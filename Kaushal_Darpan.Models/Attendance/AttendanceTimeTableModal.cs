using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Attendance
{
    public class AttendanceTimeTableModal
    {
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int CourseTypeID { get; set; }
        public int StreamID { get; set; }
        public int SectionID { get; set; }
        public int DepartmentID { get; set; }
        public int SubjectID { get; set; }
        public int InstituteID { get; set; }
        public int SemesterID { get; set; }
        public int UnitID { get; set; }
        public int ShiftID { get; set; }
        public string SSOID { get; set; }
        public int RoleID { get; set; }
        public DateTime? AttendanceStartDate { get; set; }
        public DateTime? AttendanceEndDate { get; set; }
    }

    public class PostAttendanceTimeTableModal
    {
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public string? EnrollmentNo { get; set; }
        public int CourseTypeID { get; set; }
        public int StreamID { get; set; }
        public int DepartmentID { get; set; }
        public int SectionID { get; set; }
        public int InstituteID { get; set; }
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        public int SemesterID { get; set; }
        public string? StudentName { get; set; }
        public int AssignTeacherForSubjectID { get; set; }

        // Change from a single Attendances object to a list
        public List<Attendances> Attendance { get; set; }
    }

    public class Attendances
    {
        public string? Date { get; set; }
        public string? Status { get; set; }
    }

    public class PostAttendanceTimeTable
    {
        public int ID { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }
        public int CourseTypeID { get; set; }
        public int StreamID { get; set; }
        public int SectionID { get; set; }
        public int DepartmentID { get; set; }
        public int SubjectID { get; set; }
        public int SemesterID { get; set; }
        public int AssignToRoleID { get; set; }
        public int AssignByRoleID { get; set; }
        public int AssignbyStaffID { get; set; }
        public int DeleteStatus { get; set; } 
        public int ActiveStatus { get; set; } 
        public int ShiftId { get; set; } 
        public string? AssignBySSOID { get; set; }
        public string? AssignToSSOID { get; set; }
        public int InstituteID { get; set; }
        public int RoleID { get; set; }
        public int StaffID { get; set; }
        public string SectionIDs { get; set; }

    }

    public class CalendarEventModel
    {
        public int EventId { get; set; }
        public int Day { get; set; }
        public DateTime EventDate { get; set; }
        public string EventType { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int AcademicYearID { get; set; }
        public int CourseTypeID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public int? CurrentMonth { get; set; }
        public int? CurrentYear { get; set; }
    }

}
