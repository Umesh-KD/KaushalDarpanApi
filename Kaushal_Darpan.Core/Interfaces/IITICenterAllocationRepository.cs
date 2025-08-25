using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITICenterAllocationRepository
    {
        Task<DataTable> GetAllData(ITICenterAllocationSearchFilter filterModel);
        Task<DataTable> GetCenterSuperDashboard(ExaminerDashboardModel filterModel);
        Task<DataTable> GetExamCoordinatorData(ITICenterAllocationSearchFilter filterModel);
        Task<int> AssignExamCoordinatorData(PracticalExaminerDetailsModel model);
        Task<int> SaveData(List<ITICenterAllocationModel> productDetails);

        Task<DataTable> GetInstituteByCenterID(ITICenterAllocationSearchFilter productDetails);
        Task<DataTable> CenterSuperintendent(ITICenterAllocationSearchFilter filterModel);

        Task<int> AssignCenterSuperintendent(CenterSuperintendentDetailsModel model);
        Task<DataSet> DownloadCenterSuperintendent(ITICenterAllocationSearchFilter filterModel);
        Task<DataSet> DownloadExamCoordinate(ITICenterAllocationSearchFilter filterModel);
        Task<int> ITISaveWorkflow(DownloadnRollNoModel request);

        Task<DataTable> GetExamCoordinatorData_ByInstitute(ITICenterAllocationSearchFilter filterModel);

         Task<DataTable> GetExamCoordinatorData_ByUserId(ITICenterAllocationSearchFilter filterModel);
    }
}
