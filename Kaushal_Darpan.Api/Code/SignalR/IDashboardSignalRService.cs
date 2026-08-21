using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.CommonFunction;

namespace Kaushal_Darpan.Api.Code.SignalR
{
    public interface IDashboardSignalRService
    {
        Task<ApiResult<SignalRDashboardModel>> GetDashboardCountRefresh();
    }
}
