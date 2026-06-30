using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.ITIStudentMeritInfo;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentRepository
    {
        Task<DataTable> GetStudentDashboard(StudentSearchModel request);
        Task<List<StudentDetailsModel>> GetAllData(StudentSearchModel filterModel);
        Task<List<StudentDetailsModel>> GetStudentDataBy_StudID(StudentSearchModel filterModel);
        Task<List<StudentDetailsModel>> ITIGetAllData(StudentSearchModel filterModel);
        Task<DataTable> GetStudentDeatilsByAction(StudentSearchModel filterModel);
        Task<DataTable> GetITIStudentDeatilsByAction(StudentSearchModel filterModel);
        Task<int> UpdateStudentSsoMapping(StudentSearchModel request);
        Task<int> StudentPlacementMapping(StudentSearchModel request);
        Task<DataTable> GetStudentDeatilsBySSOId(string ssoid, int DepartmentID);       
        Task<StudentMeritInfoModel> GetStudentMeritinfo(StudentSearchModel filterModel);
        Task<DataSet> GetITIStudentMeritinfo(StudentSearchModel body);
        Task<DataTable> GetProfileDashboard(StudentSearchModel filterModel);
        Task<DataTable> GetDataStudentBySSOId(string ssoid, int DepartmentID);
        Task<int> AddStudentData(VerifierDataModel productDetails);
        Task<DataTable> GetAttendanceTimeTable(AttendanceTimeTableModal model);
        Task<DataTable> ITIGetStudentAttendanceTimeTable(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendance_ITIReport(AttendanceTimeTableModal model);
        Task<DataTable> GetTeacherAttendence(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendance_PercentReport(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendance(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendanceWitMarkingStatus(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendanceReport(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendancePercentReport(AttendanceTimeTableModal model);
        Task<DataTable> GetStudentAttendanceSubjectwise(AttendanceTimeTableModal model);
        Task<DataTable> GetHolidaysmaster(DateTime? start, DateTime? end);
        Task<int> AddStudentAttendance(BasePostAttendanceTimeTableModal model);
        Task<int> PostAttendanceTimeTable(PostAttendanceTimeTable model);
        Task<int> RePostAttendanceTimeTable(PostAttendanceTimeTableITI model);
        Task<int> SaveRecheckData(List<RecheckDocumentModel> productDetails);

        Task<DataTable> GetStudentApplication(StudentSearchModel body);
        Task<DataTable> GetReverApplication(StudentSearchModel body);

        Task<int> ITIAddStudentAttendance(List<PostAttendanceTimeTableModal> model);
        Task<DataTable> ITIGetAttendanceTimeTable(AttendanceTimeTableModal model);
        Task<DataTable> ITIReAttendanceTimeTable(AttendanceTimeTableModal model);
        Task<int> PostAttendanceTimeTableList(List<PostAttendanceTimeTable> model);


        Task<int> SetCalendarEventModel(List<CalendarEventModel> model);
        Task<int> SetCalendarEventModelITI(List<CalendarEventModel> model);
        Task<int> UpdateCalendarEventModelITI(List<CalendarEventModel> model);
        Task<int> UpdateCalendarEventModelBter(List<CalendarEventModelITI> model);


        Task<DataTable> getCalendarEventModel(CalendarEventModel model);
        Task<DataTable> getCalendarEventModelITI(CalendarEventModel model);
        Task<int> DeleteAssignTeacherForSubject(PostAttendanceTimeTable model);
        Task<DataTable> getAssignCalendarEventModelITI(CalendarEventModelITI model);
        Task<DataTable> getAssignCalendarEventModelBter(CalendarEventModelITI model);
        Task<DataTable> getdublicateCheckSection(SectionDataModel model);
        Task<DataTable> GetRosterDisplay_PDFTimeTable(RosterDisplayTimeTableDataModel model);
        Task<DataSet> GetRosterDisplay_PDFTimeTableDownload(RosterDisplayTimeTableDataModel model);


        Task<DataTable> GetReAssignTeacher(ReAssignTeacherDataModel model);

        Task<int> ReAssignTeacherForSaveLC(ReAssignTeacherSaveModel model);

        Task<DataTable> GetStudentAttendanceTLC(AttendanceTimeTableTLCModal model);

        Task<int> SaveStudentAttendanceTLC(List<PostAttendanceTimeTableTLCModal> model);

        Task<DataTable> GetReAttendanceTimeTable(AttendanceTimeTableModal model);

        Task<DataTable> GetAssignedLCStream(PostAttendanceTimeTable model);

        Task<int> ResetStudentSsoMapping(StudentSearchModel request);
        Task<List<StudentRecentActivity>> GetStudentRecentActivity(int studentId);
        Task<List<StudentMarksheetModel>> GetStudentMarksheetList(StudentMarksheetListDataModel model);
    }
}
