using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.DocumentSettingDataModel;
using Kaushal_Darpan.Models.HrMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDocumentSettingRepository
    {
        Task<DataTable> GetAllData(DocumentSettingDataModel filterModel);
        Task<DocumentSettingDataModel> Get_DocumentSettingData_ByID(int DocumentSettingMasterId);
        Task<bool> SaveData(DocumentSettingDataModel exammaster);
        Task<bool> DeleteDataByID(DocumentSettingDataModel productDetails);

    }
}
