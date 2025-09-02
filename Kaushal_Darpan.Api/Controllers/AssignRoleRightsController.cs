using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.StaffMaster;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidationActionFilter]
    public class AssignRoleRightsController: BaseController
    {
        public override string PageName => "AssignRoleRightsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssignRoleRightsController(IMapper mapper, IUnitOfWork unitOfWork)

        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] List<AssignRoleRightsModel> request)
        {
            ActionName = "SaveData([FromBody] List<AssignRoleRightsModel> request";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });

                    result.Data = await _unitOfWork.AssignRoleRightsRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_SUCCESS;
                        
                    }
                }
                catch (System.Exception ex)
                {
                    _unitOfWork.Dispose();
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

        [HttpGet("GetAssignedRoleById/{UserID}")]
        public async Task<ApiResult<List<AssignRoleRightsModel>>> GetAssignedRoleById(int UserID)
        {
            ActionName = "GetAssignedRoleById(int UserId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<AssignRoleRightsModel>>();
                try
                {
                    var data = await _unitOfWork.AssignRoleRightsRepository.GetAssignedRoleById(UserID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<AssignRoleRightsModel>>(data);
                        result.Data = mappedData;
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
                    _unitOfWork.Dispose();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }
    }
}
