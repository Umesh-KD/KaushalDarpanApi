using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.studentve;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ApplicationStatusController : BaseController
    {
        public override string PageName => "ApplicationStatusController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ApplicationStatusController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
     
  
        [HttpPost("StudentApplicationStatus")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] StudentSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ApplicationStatusRepository.GetAllData(body);
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


        [HttpPost("GetByID")]
        public async Task<ApiResult<List<DocumentDetailsModel>>> GetByID([FromBody] int ApplicationID)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<DocumentDetailsModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ApplicationStatusRepository.GetByID(ApplicationID);
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


        [HttpPost("SaveRevertData")]
        public async Task<ApiResult<bool>> SaveRevertData([FromBody] List<DocumentDetailsModel> request)
        {
            ActionName = "SaveAllData([FromBody] List<PlacementShortListStudentResponseModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //request.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();

                    //});
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ApplicationStatusRepository.SaveRevertData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_NO_DATA_UPDATE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
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
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
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





        [HttpPost("StudentJailAdmission")]
        public async Task<ApiResult<DataTable>> StudentJailAdmission([FromBody] StudentSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ApplicationStatusRepository.StudentJailAdmission(body);
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
