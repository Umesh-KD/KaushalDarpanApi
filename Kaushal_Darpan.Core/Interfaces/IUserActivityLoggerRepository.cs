using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.UserActivityLogger;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUserActivityLoggerRepository
    {
        Task<int> SaveUserLogActivity(UserActivityLoggerModel model);
    }
}
