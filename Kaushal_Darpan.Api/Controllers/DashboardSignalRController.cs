using AutoMapper;
using Kaushal_Darpan.Api.Code.SignalR;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardSignalRController : BaseController
    {
        public override string PageName => "DashboardSignalRController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDashboardSignalRService _dashboardSignalRService;

        public DashboardSignalRController(IMapper mapper, IUnitOfWork unitOfWork, IDashboardSignalRService dashboardSignalRService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dashboardSignalRService = dashboardSignalRService;
        }

        [HttpGet("GetDashboardCount")]
        public async Task<ApiResult<SignalRDashboardModel>> GetDashboardCount()
        {
            ActionName = "GetDashboardCount()";
            var result = new ApiResult<SignalRDashboardModel>();
            try
            {
                result.Data = await _unitOfWork.CommonFunctionRepository.GetDashboardCountSignalR();
                if (result.Data == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpPost("SaveDashboardCount")]
        public async Task<ApiResult<bool>> SaveDashboardCount()
        {
            ActionName = "SaveDashboardCount()";
            var result = new ApiResult<bool>();
            try
            {
                var issave = await _unitOfWork.CommonFunctionRepository.SaveDashboardCountSignalR();
                if (!issave)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_NO_DATA_SAVE;
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
                await CreateErrorLog(nex, _unitOfWork);
            }

            await _dashboardSignalRService.GetDashboardCountRefresh(); // refresh signal-r to get
            return result;
        }
    }
}
