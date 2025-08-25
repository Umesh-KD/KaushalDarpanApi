using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PlacementStudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPlacementStudentRepository
    {
        Task<List<PlacementStudentResponseModel>> GetAllData(PlacementStudentSearchModel searchModel);
        Task<DataTable> GetPlacementconsent(StudentConsentSearchmodel body);
  
        Task<int> SaveData(CampusStudentConsentModel productDetails);


    }
}
