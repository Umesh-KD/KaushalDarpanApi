using Kaushal_Darpan.Models.ITIPlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.ITIPlacementShortListStudentMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIPlacementSelectedStudentRepository
    {
        Task<List<ITIPlacementSelectedStudentResponseModel>> GetAllData(ITIPlacementSelectedStudentSearchModel searchModel);

        Task<int> SaveAllData(List<ITIPlacementSelectedStudentResponseModel> productDetails);

    }
}
