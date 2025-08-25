using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.RenumerationJD;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRenumerationJDRepository
    {
        Task<List<RenumerationJDModel>> GetAllData(RenumerationJDRequestModel filterModel);
        Task<int> SaveDataApprovedAndSendToAccounts(List<RenumerationJDSaveModel> request);
    }
}
