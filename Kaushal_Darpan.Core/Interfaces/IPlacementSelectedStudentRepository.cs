using System.Data;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.Student;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementSelectedStudentRepository
    {
        Task<List<PlacementSelectedStudentResponseModel>> GetAllData(PlacementSelectedStudentSearchModel searchModel);

        Task<int> SaveAllData(List<PlacementSelectedStudentResponseModel> productDetails);
        Task<int> SaveNotifyHistory(List<ForSMSNotifyStudentPlacementShorlistModel> productDetails);
        Task<DataTable> GetStudentPlacedCount();


    }
}
