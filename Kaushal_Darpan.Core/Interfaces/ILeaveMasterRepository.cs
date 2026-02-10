using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.LeaveMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ILeaveMasterRepository
    {
        Task<DataTable> GetAllData(LeaveMasterSearchModel filterModel);
        Task<LeaveMaster> GetById(int HRManagerID);
        Task<bool> SaveData(LeaveMaster productDetails);
        Task<bool> DeleteDataByID(LeaveMaster productDetails);
        Task<bool> Save_HrValidation_NodalAction(LeaveMaster model);
        Task<DataTable> GetStaffLeaveRequest(LeaveMasterSearchModel filterModel);
        Task<DataTable> ByIDStaffLeaveList(LeaveMasterSearchModel filterModel);
        Task<DataTable> GetRemainingLeave(LeaveMasterSearchModel filterModel);

        Task<DataTable> GetLeaveCreditStaffData(LeaveMasterSearchModel filterModel);

        Task<DataTable> GetStaffWithLeaveBalance(LeaveMasterSearchModel filterModel);

        Task<bool> Save_CreditStaffLeave(List<CreditLeaveModel> request);
    }
}
