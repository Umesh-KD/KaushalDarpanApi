using Kaushal_Darpan.Models.CreateTpoMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICreateTpoRepository
    {
        Task<List<CreateTpoAddEditModel>> GetAllData(CreateTpoSearchModel filterModel);
        Task<List<TpoWebModel>> GetTPOHomeData(TpoWebModel filterModel);
        Task<int> SaveAllData(List<CreateTpoAddEditModel> productDetails);
        Task<int> DeleteAllData(List<CreateTpoAddEditModel> productDetails);
        Task<int> SaveData(List<CreateTpoAddEditModel> productDetails);

    }
}
