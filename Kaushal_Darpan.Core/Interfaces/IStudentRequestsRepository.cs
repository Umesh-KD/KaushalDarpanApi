using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Kaushal_Darpan.Models.StudentRequestsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentRequestsRepository
    {
        Task<DataTable> GetAllData(SearchStudentApplyForHostel SearchReq);
        Task<DataSet> GetAllRoomAvailabilties(RoomAllotmentModel request);
        
        Task<DataTable> GetAllRoomAllotment(SearchStudentAllotment SearchReq);
        Task<bool> SaveData(RoomAllotmentModel request);
        Task<bool> ApprovedReq(RoomAllotmentModel request);
        Task<bool> AllotmentCancelData(RoomAllotmentModel request);
        Task<DataTable> GetReportData(SearchStudentApplyForHostel SearchReq);
        Task<DataTable> GetHostelDashboard(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetGuestRoomDashboard(DTEApplicationDashboardModel filterModel);
        Task<DataTable> GetAllHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq);
        Task<DataTable> GetAllGenerateHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq);
        Task<DataTable> GetAllGenerateHostelWardenStudentMeritlist(SearchStudentApplyForHostel SearchReq);
        Task<int> GetAllPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> SearchReq);
        Task<int> GetAllFinalPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> SearchReq);
        Task<int> GetAllFinalCorrectionPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> SearchReq);
        Task<DataTable> GetAllfinalHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq);
        Task<int> GetAllAffidavitApproved(List<PublishHostelMeritListDataModel> SearchReq);
        Task<int> GetAllAffidavitObjection(List<PublishHostelMeritListDataModel> SearchReq);
        Task<DataTable> GetAllPrincipalstudentmeritlist(SearchStudentApplyForHostel SearchReq);
        Task<DataTable> GetAllDataStatus(SearchStudentApplyForHostel SearchReq);


    }
}
