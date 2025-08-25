using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGrivienceRepository
    {
        Task<DataTable> GetAllData(GrivienceSearchModel filterModel);
        Task<DataTable> GetResponseData(GrivienceSearchModel filterModel);

        Task<GrivienceModelsDataModel> GetById(int ID);
        Task<int> SaveData(GrivienceModelsDataModel productDetails);
        Task<int> SaveReopenData(GrivienceReopenModelsDataModel request);
       
        Task<bool> DeleteDataByID(GrivienceSearchModel productDetails);

        Task<int> GrivienceResponseSaveData(GrivienceResponseDataModel model);

        Task<GrivienceResponseDataModel> GetGrivienceResponseById(int ID);

       

    }
}
