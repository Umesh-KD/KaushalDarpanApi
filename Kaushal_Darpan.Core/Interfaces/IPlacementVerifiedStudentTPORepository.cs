using Kaushal_Darpan.Models.PlacementVerifiedStudentTPOMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementVerifiedStudentTPORepository
    {
        Task<List<PlacementVerifiedStudentTPOResponseModel>> GetAllData(PlacementVerifiedStudentTPOSearchModel searchModel);



    }
}
