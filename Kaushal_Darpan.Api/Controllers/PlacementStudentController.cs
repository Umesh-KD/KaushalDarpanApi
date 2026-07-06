using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PlacementStudentMaster;

using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class PlacementStudentController : BaseController
    {
        public override string PageName => "PlacementStudentController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlacementStudentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<PlacementStudentResponseModel>>> GetAllData([FromBody] PlacementStudentSearchModel searchModel)
        {
            ActionName = "GetAllData([FromBody] PlacementStudentSearchModel searchModel)";
            var result = new ApiResult<List<PlacementStudentResponseModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.PlacementStudentRepository.GetAllData(searchModel);

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

        [HttpPost("GetPlacementconsent")]
        public async Task<ApiResult<DataTable>> GetPlacementconsent([FromBody] StudentConsentSearchmodel searchModel)
        {
            ActionName = "CampusValidationList(int CollegeID,string Status)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.PlacementStudentRepository.GetPlacementconsent(searchModel));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
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


        [HttpPost("SaveData")]
        public async Task<ApiResult<string>> SaveData([FromBody] CampusStudentConsentModel request)
        {
            ActionName = "SaveData([FromBody] CampusStudentConsentModel request)";
            var result = new ApiResult<string>();
            try
            {
                request.IPAddress = CommonFuncationHelper.GetIpAddress();

                var (_result, _registrationNo) = await Task.Run(() => _unitOfWork.PlacementStudentRepository.SaveData(request));
                await _unitOfWork.SaveChangesAsync();

                // registration
                result.Data = _registrationNo;
                // result
                if (_result > 0)
                {
                    result.State = EnumStatus.Success;
                    if (request.ConsentID == 0)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                }
                else if (_result == -2)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (request.ConsentID == 0)
                    {
                        result.Message = Constants.MSG_ADD_ERROR;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_ERROR;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [HttpGet("GetStudentConsentCount/{StudentID}/{PostID}")]
        public async Task<ApiResult<DataTable>> GetStudentConsentCount(int StudentID,int PostID)
        {
            ActionName = "GetStudentConsentCount(int StudentID)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.PlacementStudentRepository.GetStudentConsentCount(StudentID,PostID));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
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


        [HttpGet("GetStudentLatestResume/{StudentID}")]
        public async Task<ApiResult<DataTable>> GetStudentLatestResume(int StudentID)
        {
            ActionName = "GetStudentLatestResume(int StudentID)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.PlacementStudentRepository.GetStudentLatestResume(StudentID));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
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



