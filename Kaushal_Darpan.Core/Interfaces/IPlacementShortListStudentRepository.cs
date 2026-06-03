using System.Data;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.Student;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementShortListStudentRepository
    {
        Task<List<PlacementShortListStudentResponseModel>> GetAllData(PlacementShortlistedStuSearch searchModel);

        Task<DataTable> GetPlacedStudentsCountList();
        Task<int> SaveAllData(List<PlacementShortListStudentResponseModel> productDetails);
        Task<int> SaveShortlistNotifyHistory(List<ForSMSNotifyStudentPlacementShorlistModel> productDetails);
        Task<int> SaveReject(List<PlacementShortListStudentResponseModel> productDetails);

    }
}
