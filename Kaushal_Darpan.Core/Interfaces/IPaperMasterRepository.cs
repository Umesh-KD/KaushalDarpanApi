using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.PaperMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPaperMasterRepository
    {
        Task<DataTable> GetAllData(PaperMasterSearchModel model);
        Task<PapersMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(PapersMasterModel productDetails);
        Task<bool> DeleteDataByID(PapersMasterModel productDetails);

        Task<DataTable> GetAllPaperUploadData(PaperUploadSearchModel body);
        Task<int> SavePaperUploadData(PaperUploadModel request);
    }
}
