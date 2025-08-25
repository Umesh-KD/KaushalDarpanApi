using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class CommonSubjectController : BaseController
    {
        public override string PageName => "CommonSubjectController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommonSubjectController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<CommonSubjectMasterResponseModel>>> GetAllData([FromBody] CommonSubjectMasterSearchModel model)
        {
            ActionName = "GetAllData([FromBody] CommonSubjectMasterSearchModel model)";
            var result = new ApiResult<List<CommonSubjectMasterResponseModel>>();
            try
            {
                result.Data = await _unitOfWork.CommonSubjectMasterRepository.GetAllData(model);
                if (result.Data.Count > 0)
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
        }

        [HttpGet("GetByID/{CommonSubjectId:int}")]
        public async Task<ApiResult<CommonSubjectMasterModel>> GetByID(int CommonSubjectId)
        {
            ActionName = "GetByID(int CommonSubjectId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CommonSubjectMasterModel>();
                try
                {
                    var data = await _unitOfWork.CommonSubjectMasterRepository.GetById(CommonSubjectId);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<CommonSubjectMasterModel>(data);
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

        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] CommonSubjectMasterModel request)
        {
            ActionName = "SaveData([FromBody] CommonSubjectMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = _mapper.Map<M_CommonSubject>(request);
                    //parent
                    mappedData.RTS = DateTime.Now;
                    mappedData.CreatedBy = request.ModifyBy;
                    mappedData.ModifyDate = DateTime.Now;
                    mappedData.Remark = string.Empty;
                    mappedData.ActiveStatus = true;
                    mappedData.DeleteStatus = false;
                    var pid = await _unitOfWork.CommonSubjectMasterRepository.SaveData(mappedData);
                    //child
                    mappedData.commonSubjectDetails.ForEach(x => x.CommonSubjectID = pid);
                    result.Data = await _unitOfWork.CommonSubjectMasterRepository.SaveDataChild(mappedData.commonSubjectDetails);
                    _unitOfWork.SaveChanges();
                    //result
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.CommonSubjectID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.CommonSubjectID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        }
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

        [HttpDelete("DeleteByID/{CommonSubjectId:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteByID(int CommonSubjectId, int ModifyBy)
        {
            ActionName = "DeleteByID(int CommonSubjectId, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new M_CommonSubject
                    {
                        CommonSubjectID = CommonSubjectId,
                        ModifyBy = ModifyBy,
                        ModifyDate = DateTime.Now,
                        ActiveStatus = false,
                        DeleteStatus = true
                    };
                    result.Data = await _unitOfWork.CommonSubjectMasterRepository.DeleteById(mappedData);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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


