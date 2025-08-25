using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models.CommonSubjectMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICommonSubjectMasterRepository
    {
        Task<bool> DeleteById(M_CommonSubject entity);
        Task<List<CommonSubjectMasterResponseModel>> GetAllData(CommonSubjectMasterSearchModel model);
        Task<M_CommonSubject> GetById(int commonSubjectId);
        Task<int> SaveData(M_CommonSubject entity);
        Task<bool> SaveDataChild(List<M_CommonSubject_Details> entity);
    }
}
