using Kaushal_Darpan.Models.CenterMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICenterMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<CenterMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(CenterMasterModel productDetails);
        Task<bool> DeleteDataByID(CenterMasterModel productDetails);

    }
}
