
using Kaushal_Darpan.Models.TSPAreaMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITSPAreaMasterRepository
    {

        Task<int> SaveData(TSPAreaMasterModel productDetails);

        Task<DataTable> GetAllData(TSPAreaMasterSearchModel filterModel);

        Task<TSPAreaMasterModel> GetById(int ITITspAreasId);

        Task<bool> DeleteDataByID(TSPAreaMasterModel productDetails);
        Task<DataTable> TSPArea_GetTehsil_DistrictWise(TSPTehsilModel filterModel);
    }
}
