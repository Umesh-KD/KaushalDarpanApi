//using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.IssueTrackerMasters;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.ScholarshipMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IIssueTrackerRepository
    {
        Task<DataTable> GetAllData(IssueTrackerListSearchModel model);
        Task<int> SaveData(IssueSaveData request);
        Task<int> AssignIssure(List<IssueTrackerListSearchModel> productDetails);

        Task<IssueTrackerMaster> GetById(int PK_ID);
        Task<bool> DeleteDataByID(IssueTrackerMaster productDetails);
        //Task<DataTable> GetUserList();
        //Task<DataTable> GetRoleList(RoleRequest RoleID);

        
       Task<DataTable> GetUserList(int RoleID);
    }
}
