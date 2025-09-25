using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class UpwardMovementController : BaseController
    {
        public override string PageName => "UpwardMovementController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpwardMovementController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetDataItiStudentApplication")]
        public async Task<ApiResult<DataTable>> GetDataItiStudentApplication([FromBody] ItiStuAppSearchModelUpward body)
        {
            ActionName = " GetDataItiStudentApplication([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.UpwardMovementRepository.GetDataItiStudentApplication(body);
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
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("UpwardMomentUpdate")]
        public async Task<ApiResult<bool>> UpwardMomentUpdate(UpwardMoment model)
        {
            ActionName = "UpwardMomentUpdate(UpwardMoment model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    result.Data = await _unitOfWork.UpwardMovementRepository.UpwardMomentUpdate(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
        
        [HttpPost("GetDataItiUpwardMoment")]
        public async Task<ApiResult<DataTable>> GetDataItiUpwardMoment([FromBody] ItiStuAppSearchModelUpward body)
        {
            ActionName = " GetDataItiStudentApplication([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.UpwardMovementRepository.GetDataItiUpwardMoment(body);
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
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // Log the error
                await _unitOfWork.DisposeAsync();
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

        [HttpPost("ITIUpwardMomentUpdate")]
        public async Task<ApiResult<bool>> ITIUpwardMomentUpdate(UpwardMoment model)
        {
            ActionName = "UpwardMomentUpdate(UpwardMoment model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    result.Data = await _unitOfWork.UpwardMovementRepository.ITIUpwardMomentUpdate(model);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
