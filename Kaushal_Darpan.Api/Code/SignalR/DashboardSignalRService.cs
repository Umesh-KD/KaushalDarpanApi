using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CommonFunction;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Kaushal_Darpan.Api.Code.SignalR
{
    public class DashboardSignalRService : IDashboardSignalRService
    {
        string PageName = "DashboardSignalRService";
        string ActionName = string.Empty;

        private readonly IHubContext<SignalRHub> _hub;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardSignalRService(IHubContext<SignalRHub> hub, IUnitOfWork unitOfWork)
        {
            _hub = hub;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResult<SignalRDashboardModel>> GetDashboardCountRefresh()
        {
            ActionName = "GetDashboardCountRefresh()";
            var result = new ApiResult<SignalRDashboardModel>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.GetDashboardCountSignalR();
                // 
                if (result.Data == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                // 
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                //await CreateErrorLog(nex, _unitOfWork);
            }
            // 
            await _hub.Clients.All.SendAsync("DashboardCountRefresh", result); // signal-r
            return result; // return
        }
    }
}
