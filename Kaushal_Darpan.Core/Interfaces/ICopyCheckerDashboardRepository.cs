using Kaushal_Darpan.Models.StaffDashboard;
using Kaushal_Darpan.Models.TheoryMarks;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICopyCheckerDashboardRepository
    {
        Task<DataTable> GetCopyCheckerDashData(CopyCheckerRequestModel request);
        Task<DataTable> GetAllData(ExaminerDashboardModel model);
        Task<DataTable> GetCopyCheckerDashData_Reval(CopyCheckerRequestModel request);
    }
}
