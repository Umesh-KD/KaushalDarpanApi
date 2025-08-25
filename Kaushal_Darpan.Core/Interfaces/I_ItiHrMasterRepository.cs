//using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ITIHrMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ItiHrMasterRepository
    {
        Task<DataTable> GetAllData(ItiHrMasterSearchModel filterModel);
        Task<ItiHrMaster> GetById(int HRManagerID);
        Task<bool> SaveData(ItiHrMaster productDetails);
        Task<bool> DeleteDataByID(ItiHrMaster productDetails);
        Task<bool> Save_HrValidation_NodalAction(ItiHrMaster_Action model);
        Task<DataTable> HrValidationList(ItiHrMasterSearchModel filterModel);
    }
}
