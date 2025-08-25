using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.DTE_AssignApplication;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.studentve;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class AssignApplicationMasterController : BaseController
    {
        public override string PageName => "AssignApplicationMasterController   ";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssignApplicationMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] AssignApplicaitonDataModel request)
        {
            ActionName = "SaveData([FromBody] AssignApplicaitonDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.AssignApplicationMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.VerifierID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = "The selected Application range consist of Appication Number that is already assigned";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.VerifierID == 0)
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

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<AssignApplicaitonDataModel>>> GetAllData(AssignApplicationSearchModel request)
        {
            ActionName = "GetAllData(AssignApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<AssignApplicaitonDataModel>>();
                try
                {
                    var data = await _unitOfWork.AssignApplicationMasterRepository.GetAllData(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<AssignApplicaitonDataModel>>(data);
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

        [HttpGet("GetApplicationsById/{ID}")]
        public async Task<ApiResult<AssignApplicaitonDataModel>> GetApplicationsById(int ID)
        {
            ActionName = "GetDataById(int id)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AssignApplicaitonDataModel>();
                try
                {
                    var data = await _unitOfWork.AssignApplicationMasterRepository.GetApplicationsById(ID);
                    result.Data = data;
                    if (data != null)
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

        [HttpDelete("DeleteDataByID/{ID:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new AssignApplicaitonDataModel
                    {
                        ID = ID,
                        ModifyBy = ModifyBy,

                        ActiveStatus = false,
                        DeleteStatus = true,
                    };
                    result.Data = await _unitOfWork.AssignApplicationMasterRepository.DeleteDataByID(mappedData);
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

        [HttpPost("GetStudentsData")]
        public async Task<ApiResult<List<AssignedApplicationStudentDataModel>>> GetStudentsData(StudentsAssignApplicationSearch request)
        {
            ActionName = "GetAllData(AssignApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<AssignedApplicationStudentDataModel>>();
                try
                {
                    var data = await _unitOfWork.AssignApplicationMasterRepository.GetStudentsData(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<AssignedApplicationStudentDataModel>>(data);
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

        [HttpPost("GetVerifierData")]
        public async Task<ApiResult<List<AssignedApplicationStudentDataModel>>> GetVerifierData(StudentsAssignApplicationSearch request)
        {
            ActionName = "GetVerifierData(AssignApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<AssignedApplicationStudentDataModel>>();
                try
                {
                    var data = await _unitOfWork.AssignApplicationMasterRepository.GetVerifierData(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<AssignedApplicationStudentDataModel>>(data);
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



        [HttpGet("GetStudentDataById/{ApplicationID}")]
        public async Task<ApiResult<AssignApplicaitonDataModel>> GetStudentDataById(int ApplicationID)
        {
            ActionName = "GetDataById(int id)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AssignApplicaitonDataModel>();
                try
                {
                    var data = await _unitOfWork.AssignApplicationMasterRepository.GetStudentDataById(ApplicationID);
                    result.Data = data;
                    if (data != null)
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


        [HttpPost("GetTotalAssignApplication")]
        public async Task<ApiResult<DataTable>> GetCopyCheckerDashData(RequestBaseModel request)
        {
            ActionName = "GetCopyCheckerDashData(CopyCheckerRequestModel request)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.AssignApplicationMasterRepository.GetTotalAssignCount(request);
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


        [HttpPost("AssignChecker")]
        public async Task<ApiResult<bool>> AssignChecker([FromBody] List<AssignCheckerModel> request)
        {
            ActionName = "AssignChecker([FromBody] List<BridgeCourseStudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {


                    //validation
                    //if (request.Any(x => x.RoleId != (int)EnumRole.Admin))
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                    //    return result;
                    //}
                    if (request.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                   
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.AssignApplicationMasterRepository.AssignChecker(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("RevertApplication")]
        public async Task<ApiResult<bool>> RevertApplication(RevertApplicationDataModel request)
        {
            ActionName = "RevertApplication(RevertApplicationDataModel request)";
            var result = new ApiResult<bool>();
            try
            {
                result.Data = await _unitOfWork.AssignApplicationMasterRepository.RevertApplication(request);
                _unitOfWork.SaveChanges();

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

    }

}

