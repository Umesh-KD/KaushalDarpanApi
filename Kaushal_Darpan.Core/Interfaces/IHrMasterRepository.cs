using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.HrMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IHrMasterRepository
    {
        Task<DataTable> GetAllData(HrMasterSearchModel filterModel);
        Task<HRMaster> GetById(int ID);
        Task<bool> SaveData(HRMaster productDetails);
        Task<bool> DeleteDataByID(HRMaster productDetails);
        Task<bool> Save_HrValidation_NodalAction(HrMaster_Action model);
        Task<DataTable> HrValidationList(HrMasterSearchModel filterModel);
    }
}
