using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.RenumerationAccounts;
using Kaushal_Darpan.Models.RenumerationJD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    [RoleActionFilter(EnumRole.AccountsSec_Eng,EnumRole.AccountsSec_NonEng)]

    public class RenumerationAccountsController : BaseController
    {
        public override string PageName => "RenumerationAccountsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RenumerationAccountsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<RenumerationAccountsModel>>> GetAllData([FromBody] RenumerationAccountsRequestModel filterModel)
        {
            ActionName = "GetAllData([FromBody] RenumerationAccountsRequestModel filterModel)";
            var result = new ApiResult<List<RenumerationAccountsModel>>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.RenumerationAccounts.GetAllData(filterModel);

                if (data != null)
                {
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // Log the error
                _unitOfWork.Dispose();
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

        [HttpPost("SaveDataApprovedFromAccounts")]
        public async Task<ApiResult<bool>> SaveDataApprovedFromAccounts(RenumerationAccountsSaveModel request)
        {
            ActionName = "SaveDataApprovedFromAccounts(RenumerationAccountsSaveModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //check dblicate 
                    var hasDublicate = await _unitOfWork.RenumerationAccounts.HasDblicateTvNoAndVoucharNo(request);
                    if (hasDublicate != 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = "TVNo or VoucherNo already exists.";
                        return result;
                    }

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RenumerationAccounts.SaveDataApprovedFromAccounts(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_SAVE_Duplicate;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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

        [HttpPost("UpdateDataApprovedFromAccounts")]
        public async Task<ApiResult<bool>> UpdateDataApprovedFromAccounts(RenumerationAccountsSaveModel request)
        {
            ActionName = "UpdateDataApprovedFromAccounts(RenumerationAccountsSaveModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.RenumerationAccounts.UpdateDataApprovedFromAccounts(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_SAVE_Duplicate;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
                    result.ErrorMessage = ex.Message;

                    // Log the error
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
    }
}
