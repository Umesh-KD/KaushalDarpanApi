using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICompanyMasterRepository
    {
        Task<DataTable> GetAllData(CompanyMasterSearchModel filterModel);

        Task<CompanyMasterResponsiveModel> GetById(int ID);
        Task<bool> SaveData(CompanyMasterModels productDetails);
        Task<bool> Save_CompanyValidation_NodalAction(CompanyMaster_Action model);
        Task<bool> DeleteDataByID(CompanyMasterModels productDetails);
        Task<DataTable> CompanyValidationList(CompanyMasterSearchModel filterModel);

        Task<DataTable> CompanyMasterReport(CompanyMasterSearchModel filterModel);

    }
}
