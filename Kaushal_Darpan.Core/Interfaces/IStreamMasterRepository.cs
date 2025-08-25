using Kaushal_Darpan.Models.StreamMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStreamMasterRepository
    {
        Task<DataTable> GetAllData(int StreamTypeID);
        Task<StreamMasterModel> GetById(int PK_ID);
        Task<int> SaveData(StreamMasterModel productDetails);
        Task<bool> DeleteDataByID(StreamMasterModel productDetails);
    }
}
