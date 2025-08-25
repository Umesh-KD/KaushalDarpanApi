using Kaushal_Darpan.Models.BTERIMCAllocationModel;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBoard_UniversityMasterRepository
    {
        Task<DataTable> GetAllData(Board_UniversityMasterSearchModel model);
        Task<Board_UniversityMasterModel> GetById(int PK_ID);
        Task<int> SaveData(Board_UniversityMasterModel model);
        Task<bool> DeleteDataByID(Board_UniversityMasterSearchModel  model);

    }
}
