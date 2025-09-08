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
        Task<DataTable> GetStudentAttendance(AttendanceTimeTableModal model);
        Task<DataTable> GetHolidaysmaster(DateTime? start, DateTime? end);
        Task<int> AddStudentAttendance(List<PostAttendanceTimeTableModal> model);
        Task<int> PostAttendanceTimeTable(PostAttendanceTimeTable model);
        Task<int> SaveRecheckData(List<RecheckDocumentModel> productDetails);

        Task<DataTable> GetStudentApplication(StudentSearchModel body);
        Task<DataTable> GetReverApplication(StudentSearchModel body);

        Task<int> ITIAddStudentAttendance(List<PostAttendanceTimeTableModal> model);
        Task<DataTable> ITIGetAttendanceTimeTable(AttendanceTimeTableModal model);
        Task<int> PostAttendanceTimeTableList(List<PostAttendanceTimeTable> model);


        Task<int> SetCalendarEventModel(List<CalendarEventModel> model);


        Task<DataTable> getCalendarEventModel(CalendarEventModel model);
        Task<DataTable> getdublicateCheckSection(SectionDataModel model);
        Task<DataTable> GetRosterDisplay_PDFTimeTable(RosterDisplayTimeTableDataModel model);
        Task<DataSet> GetRosterDisplay_PDFTimeTableDownload(RosterDisplayTimeTableDataModel model);




    }
}
