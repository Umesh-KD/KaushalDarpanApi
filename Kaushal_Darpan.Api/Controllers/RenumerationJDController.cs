using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.RenumerationJD;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    [RoleActionFilter(EnumRole.JDConfidential_Eng,EnumRole.JDConfidential_NonEng)]
    public class RenumerationJDController : BaseController
    {
        public override string PageName => "RenumerationJDController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RenumerationJDController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<RenumerationJDModel>>> GetAllData([FromBody] RenumerationJDRequestModel filterModel)
        {
            ActionName = "GetAllData([FromBody] RenumerationJDRequestModel filterModel)";
            var result = new ApiResult<List<RenumerationJDModel>>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.RenumerationJDRepository.GetAllData(filterModel);

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
        
        [HttpPost("SaveDataApprovedAndSendToAccounts")]
        public async Task<ApiResult<bool>> SaveDataApprovedAndSendToAccounts(List<RenumerationJDSaveModel> request)
        {
            ActionName = "SaveDataApprovedAndSendToAccounts(RenumerationJDSaveModel[] request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    if (request?.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RenumerationJDRepository.SaveDataApprovedAndSendToAccounts(request);
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
                        result.Message = Constants.MSG_UPDATE_ERROR;
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


