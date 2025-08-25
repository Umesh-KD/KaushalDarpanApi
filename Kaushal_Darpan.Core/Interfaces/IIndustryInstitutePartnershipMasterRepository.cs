using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IIndustryInstitutePartnershipMasterRepository
    {
        Task<DataTable> GetAllData(IndustryInstitutePartnershipMasterSearchModel filterModel);

        Task<IndustryInstitutePartnershipMasterResponsiveModel> GetById(int ID);
        Task<int> SaveData(IndustryInstitutePartnershipMasterModels productDetails);
        Task<bool> Save_IndustryInstitutePartnershipValidation_NodalAction(IndustryInstitutePartnershipMaster_Action model);
        Task<bool> DeleteDataByID(IndustryInstitutePartnershipMasterModels productDetails);
        Task<DataTable> IndustryInstitutePartnershipValidationList(IndustryInstitutePartnershipMasterSearchModel filterModel);

        Task<int> SaveIndustryTrainingData(IndustryTrainingMaster productDetails);


        Task<DataTable> GetAllIndustryTrainingData(IndustryTrainingSearch filterModel);

    }
}
