using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IExamMasterRepository
    {
        Task<DataTable> GetAllData(ExamMasterDataModel filterModel);
        Task<ExamMasterDataModel> Get_ExamMasterData_ByID(int ExamMasterID);
        Task<bool> SaveData(ExamMasterDataModel exammaster);
        Task<bool> DeleteDataByID(ExamMasterDataModel productDetails);

    }
}
