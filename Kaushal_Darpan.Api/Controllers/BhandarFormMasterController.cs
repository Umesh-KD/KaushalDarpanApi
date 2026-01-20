using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BhandarFormDataModel;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ValidationActionFilter]
    public class BhandarFormMasterController : BaseController
    {
        public override string PageName => "BhandarFormMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BhandarFormMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("GetExamStudentData")]
        public async Task<ApiResult<AddBhandarFormDataModel>> GetExamStudentData(
            [FromBody] AddBhandarFormDataModel body)
        {
            ActionName = "GetExamStudentData()";
            var result = new ApiResult<AddBhandarFormDataModel>();

            try
            {
                result.Data = await _unitOfWork
                    .BhandarFormMasterRepository
                    .GetExamStudentData(body);

                if (result.Data == null)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No record found.!";
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data loaded successfully.!";
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveDataReport([FromBody] AddBhandarFormDataModel request)
        {
            ActionName = "SaveData([FromBody] ItiReportDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.BhandarFormMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.BhandarID == 0)

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
                        if (request.BhandarID == 0)
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

    }
}
