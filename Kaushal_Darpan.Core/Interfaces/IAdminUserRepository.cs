using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.UserMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<DataTable> GetAllData(AdminUserSearchModel body);
        Task<AdminUserDetailModel> GetById(AdminUserSearchModel body);
        Task <List<Branchlist>> GetHodBranch(AdminUserSearchModel body);
        Task<int> SaveData(AdminUserDetailModel productDetails);
        Task<int> AssignHOD(AssignHodBranch productDetails);
        Task<bool> UpdateData(AdminUserDetailModel productDetails);
        Task<bool> DeleteDataByID(AdminUserSearchModel request);
        Task<DataTable> GetStreamMasterForHod(StreamMasterForHodModel request);
    }
}
