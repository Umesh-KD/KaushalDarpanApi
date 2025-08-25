using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IExaminersRepository
    {
        Task<DataTable> GetTeacherForExaminer(TeacherForExaminerSearchModel filterModel);
        Task<DataTable> GetExaminerData(TeacherForExaminerSearchModel filterModel);
        Task<int> SaveExaminerData(ExaminerMaster productDetails);
        Task<bool> DeleteDataByID(ExaminerMaster productDetails);
        Task<DataTable> GetExaminerByCode(ExaminerCodeLoginModel model);
        Task<ExaminerMaster> GetById(int PK_ID, int StaffSubjectID,int DepartmentID,int EndTermID,int CourseTypeID);
        Task<DataTable> ExaminerInchargeDashboard(ExaminerDashboardSearchModel model);
        Task<DataTable> RegistrarDashboard(ExaminerDashboardSearchModel model);
        Task<DataTable> ITSupportDashboard(ExaminerDashboardSearchModel model);
        Task<DataTable> SectionInchargeDashboard(ExaminerDashboardSearchModel model);
        Task<DataTable> ACPDashboard(ExaminerDashboardSearchModel model);
        Task<DataTable> StoreKeeperDashboard(ExaminerDashboardSearchModel model);
        Task<int> GetExaminerGroupTotalAsync(string examinerCode);
        Task<DataTable> GetRevalTeacherForExaminer(TeacherForExaminerSearchModel filterModel);

        //------------------------------------REVAL---------------------------------------------
        Task<ExaminerMaster> Getexaminer_byID_Reval(int PK_ID, int StaffSubjectID, int DepartmentID, int EndTermID, int CourseTypeID);
        Task<int> SaveExaminerData_Reval(ExaminerMaster productDetails);
        Task<DataTable> GetExaminerData_Reval(TeacherForExaminerSearchModel filterModel);
        Task<bool> DeleteByID_Reval(ExaminerMaster productDetails);
        Task<DataTable> GetExaminerByCode_Reval(ExaminerCodeLoginModel model);
    }
}
