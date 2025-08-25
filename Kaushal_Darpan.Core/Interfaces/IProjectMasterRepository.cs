using Kaushal_Darpan.Models.PaperSetter;
using Kaushal_Darpan.Models.ProjectMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IProjectMasterRepository
    {
        Task<DataTable> GetAllData(ProjectMasterSearchModel filterModel);
        Task<int> SaveData(ProjectMasterModel productDetails);
        Task<ProjectMasterModel> GetById(int PK_ID);
        Task<bool> DeleteDataByID(ProjectMasterModel productDetails);
    }
}
