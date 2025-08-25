using Kaushal_Darpan.Models.ITIPlacementStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PlacementStudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIPlacementStudentRepository
    {
        Task<List<ITIPlacementStudentResponseModel>> GetAllData(ITIPlacementStudentSearchModel searchModel);
        Task<DataTable> GetPlacementconsent(ITIStudentConsentSearchmodel body);
  
        Task<int> SaveData(ITICampusStudentConsentModel productDetails);


    }
}
