using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.RenumerationExaminer;
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
    [RoleActionFilter(EnumRole.Examiner_Eng,EnumRole.Examiner_NonEng)]
    public class RenumerationExaminerController : BaseController
    {
        public override string PageName => "RenumerationExaminerController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RenumerationExaminerController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<RenumerationExaminerModel>>> GetAllData([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "GetAllData([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<List<RenumerationExaminerModel>>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.RenumerationExaminerRepository.GetAllData(filterModel);

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

        [HttpPost("GetTrackStatusData")]
        public async Task<ApiResult<List<TrackStatusDataModel>>> GetTrackStatusData([FromBody] RenumerationExaminerRequestModel filterModel)
        {
            ActionName = "GetTrackStatusData([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<List<TrackStatusDataModel>>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.RenumerationExaminerRepository.GetTrackStatusData(filterModel);

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
    }
}


