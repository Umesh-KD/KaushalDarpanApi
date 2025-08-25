using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.ITICenterMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICenterCreationRepository
    {
        Task<DataTable> GetAllData(CenterCreationSearchModel filterModel);
        Task<DataTable> GetCenterForCcCode(CenterCreationSearchModel filterModel);
        Task<int> SaveData(List<CenterCreationAddEditModel> productDetails, int StartValue);
        Task<int> GenerateCCCode(List<GenerateCCCodeDataModel> productDetails, int StartValue);
        Task<int> RemoveCenter(List<CenterCreationAddEditModel> productDetails);
        Task<CenterCreationAddEditModel> GetById(CenterCreationSearchModel request);
        Task<int> UpdateCCCode(CenterCreationAddEditModel productDetails);
        Task<int> AssignCenterSuperintendent(CenterSuperintendentDetailsModel model);


    }



}
