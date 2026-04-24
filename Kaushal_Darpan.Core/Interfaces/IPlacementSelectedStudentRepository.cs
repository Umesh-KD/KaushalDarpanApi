using System.Data;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementSelectedStudentRepository
    {
        Task<List<PlacementSelectedStudentResponseModel>> GetAllData(PlacementSelectedStudentSearchModel searchModel);

        Task<int> SaveAllData(List<PlacementSelectedStudentResponseModel> productDetails);
        Task<DataTable> GetStudentPlacedCount();


    }
}
