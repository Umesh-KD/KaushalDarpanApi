using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITI_BGTHeadmaster;
using Kaushal_Darpan.Models.ITIBUDGET;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ITIBudgetCreateController : BaseController
    {
        public override string PageName => "ITIBudgetCreateController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ITIBudgetCreateController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BudgetHeadManagementRepository.GetAllData(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
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

        [HttpPost("GetITIBudgetDropdown")]
        public async Task<ApiResult<DataTable>> GetITIBudgetDropdown(ITIBudgetDropdownDataModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIBudgetCreateRepository.GetITIBudgetDropdown(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
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

        [HttpPost("SaveDataBudgetCreate_Admin")]
        public async Task<ApiResult<int>> SaveDataBudgetCreate_Admin([FromBody] ITIBudgetCreateDataModel request)
        {
            ActionName = "SaveDataBudgetCreate_Admin([FromBody] ITIBudgetCreateDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.ITIBudgetCreateRepository.SaveDataBudgetCreate_Admin(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;

                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
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
            });
        }

        [HttpPost("GetBudgetData")]
        public async Task<ApiResult<DataTable>> GetBudgetData(BudgetHeadSearchFilter model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIBudgetCreateRepository.GetBudgetData(model);
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
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
    }
}
