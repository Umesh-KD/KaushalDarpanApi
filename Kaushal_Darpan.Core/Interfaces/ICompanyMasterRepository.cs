using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICompanyMasterRepository
    {
        Task<DataTable> GetAllData(CompanyMasterSearchModel filterModel);

        //Task<CompanyMasterResponsiveModel> GetById(int ID);

        Task<CompanyMasterModels> GetByID(CompanyMasterSearchModel req);
        Task<bool> SaveData(CompanyMasterModels productDetails);
        Task<bool> Save_CompanyValidation_NodalAction(CompanyMaster_Action model);
        Task<bool> DeleteDataByID(CompanyMasterModels productDetails);
        Task<DataTable> CompanyValidationList(CompanyMasterSearchModel filterModel);
        Task<DataSet> GetCampusHr_Trail(int CompanyID);

        Task<DataTable> CompanyMasterReport(CompanyMasterSearchModel filterModel);

        Task<DataTable> GetEligibleStudentListData(EligibleStudentListMasterSearchModel filterModel);

        Task<DataTable> GetPlacementAllStudentList(PlacementStudentListSearchModel filterModel);

        Task<DataTable> GetDataByStudentId(EligibleStudentForPlacement model);

    }
}
