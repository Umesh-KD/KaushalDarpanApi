using Kaushal_Darpan.Models.LevelMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ILevelMasterRepository
    {
        Task<bool> CreateLevel(LevelMasterModel level);
        Task<DataTable> GetAllData();

        Task<LevelMasterModel> GetLevelById(int levelId);
        Task<bool> UpdateLevelById(LevelMasterModel level);
        Task<bool> DeleteLevelById(LevelMasterModel level);
        //Task<LevelMaster> GetLevelByName(string levelName);
    }
}
