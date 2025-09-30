using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICollegeWiseScholarshipRepository
    {
        //Task<DataTable> GetAllData(CompanyMasterSearchModel filterModel);

        //Task<CompanyMasterResponsiveModel> GetById(int ID);
        //Task<bool> SaveData(CompanyMasterModels productDetails);
        //Task<bool> Save_CompanyValidation_NodalAction(CompanyMaster_Action model);
        //Task<bool> DeleteDataByID(CompanyMasterModels productDetails);
        //Task<DataTable> CompanyValidationList(CompanyMasterSearchModel filterModel);

        //Task<DataTable> CompanyMasterReport(CompanyMasterSearchModel filterModel);

        Task<DataTable> GetCollegeWiseScholarshipList(CollegeWiseScholarshipSearchModel filterModel);
        Task<DataTable> GetCollegeWiseScholarshipListReport(CollegeWiseScholarshipSearchModel filterModel);
        Task<DataTable> GetSchemeList();
        Task<DataTable> GetTypeList();

        Task<bool> SaveCollegeWiseScholarshipDetails(List<SaveCollegeWiseScholershipDetails> model);

        Task<DataTable> GetDetailList(int id);

        //Task<DataTable> GetDataByStudentId(EligibleStudentForPlacement model);

    }
}
