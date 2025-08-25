using Kaushal_Darpan.Models.PlacementShortListStudentMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementShortListStudentRepository
    {
        Task<List<PlacementShortListStudentResponseModel>> GetAllData(PlacementShortlistedStuSearch searchModel);

        Task<int> SaveAllData(List<PlacementShortListStudentResponseModel> productDetails);

    }
}
