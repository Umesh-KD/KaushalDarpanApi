using Kaushal_Darpan.Models.CompanyMaster;
using Org.BouncyCastle.Ocsp;
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

        // ---------------------------------------------------------- BTER IIP by Ramesh ----------------------------------------------------------------------------
        Task<int> SaveData_IIP_Company(IndustryInstitutePartnershipMasterModels productDetails);
        Task<IndustryInstitutePartnershipMasterModels> GetById_IIP_CompanyDetails(IIP_SearchModel req);
        Task<bool> DeleteCompanyById_IIP(IndustryInstitutePartnershipMasterModels request);
        Task<bool> Delete_Hr(ConcernPersonDetailsDataModel request);
        Task<int> SaveData_IIP_Events(IIP_EventDataModel productDetails);
        Task<DataTable> GetCompanyEvents(CompanyEventSearchModel body);
        Task<bool> DeleteEvent_ById(IIP_EventDataModel request);
        Task<IIP_EventDataModel> GetEvent_ById(CompanyEventSearchModel request);
        Task<int> ApproveCompanyEvents(List<IndustryInstitutePartnershipMasterModels> model);
    }
}
