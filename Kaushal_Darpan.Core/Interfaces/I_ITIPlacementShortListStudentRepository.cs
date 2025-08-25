using Kaushal_Darpan.Models.ITIPlacementShortListStudentMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIPlacementShortListStudentRepository
    {
        Task<List<ITIPlacementShortListStudentResponseModel>> GetAllData(ITIPlacementShortlistedStuSearch searchModel);

        Task<int> SaveAllData(List<ITIPlacementShortListStudentResponseModel> productDetails);

    }
}
