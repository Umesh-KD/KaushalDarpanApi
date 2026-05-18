using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.RenumerationJD;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRenumerationJDRepository
    {
        Task<List<RenumerationJDModel>> GetAllData(RenumerationJDRequestModel filterModel);
        Task<List<RenumerationJDModel>> GetAllData_Reval(RenumerationJDRequestModel filterModel);
        Task<int> SaveDataApprovedAndSendToAccounts(List<RenumerationJDSaveModel> request);
        Task<int> SaveDataApprovedAndSendToAccounts_Reval(List<RenumerationJDSaveModel> request);
    }
}
