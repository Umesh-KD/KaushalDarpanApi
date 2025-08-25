using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.ITICenterMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITICenterMasterRepository
    {
        Task<DataTable> GetAllData(CenterCreationSearchModel filterModel);
        Task<int> SaveCenterData(List<ITICenterCreationAddEditModel> productDetails, int StartValue);
        Task<ITICenterMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(ITICenterMasterModel productDetails);
        Task<bool> DeleteDataByID(ITICenterMasterModel productDetails);

    }
}
