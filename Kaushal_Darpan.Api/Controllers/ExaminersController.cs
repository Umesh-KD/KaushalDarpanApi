using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.UserMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidationActionFilter]
    public class ExaminersController : BaseController
    {
        public override string PageName => "ExaminersController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExaminersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetTeacherForExaminer")]
        public async Task<ApiResult<DataTable>> GetTeacherForExaminer([FromBody] TeacherForExaminerSearchModel body)
        {
            ActionName = "GetTeacherForExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ExaminersRepository.GetTeacherForExaminer(body));
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

        [HttpPost("SaveExaminerData")]
        public async Task<ApiResult<int>> SaveExaminerData([FromBody] ExaminerMaster request)
        {
            ActionName = " SaveExaminerData([FromBody] ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ExaminersRepository.SaveExaminerData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ExaminerID == 0)
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
                        result.ErrorMessage = "UserID Does Not Exist";
                    }

                    else if (result.Data == -3)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "This Examiner Code is already assigned to other user";
                    }

                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ExaminerID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        }
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

        [HttpPost("GetExaminerData")]
        public async Task<ApiResult<DataTable>> GetExaminerData([FromBody] TeacherForExaminerSearchModel body)
        {
            ActionName = "GetExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ExaminersRepository.GetExaminerData(body));
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

        [HttpDelete("DeleteByID/{ExaminerID:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteByID(int ExaminerID, int ModifyBy)
        {
            ActionName = "DeleteByID(int ExaminerID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new ExaminerMaster
                    {
                        ExaminerID = ExaminerID,
                        ModifyBy = ModifyBy,

                        //ActiveStatus = false,
                        //DeleteStatus = true,
                    };
                    result.Data = await _unitOfWork.ExaminersRepository.DeleteDataByID(mappedData);
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

        [HttpPost("GetExaminerByCode")]
        public async Task<ApiResult<DataTable>> GetExaminerByCode([FromBody] ExaminerCodeLoginModel model)
        {
            ActionName = "GetExaminerByCode([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.GetExaminerByCode(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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




        [HttpGet("GetByID/{PK_ID}/{StaffSubjectID}/{DepartmentID}/{EndTermID}/{CourseTypeID}")]
        public async Task<ApiResult<ExaminerMaster>> GetByID(int PK_ID,int StaffSubjectID,int DepartmentID,int EndTermID,int CourseTypeID)


        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ExaminerMaster>();
                try
                {
                    var data = await _unitOfWork.ExaminersRepository.GetById(PK_ID, StaffSubjectID, DepartmentID,EndTermID, CourseTypeID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ExaminerMaster>(data);
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




        [HttpPost("ExaminerInchargeDashboard")]
        public async Task<ApiResult<DataTable>> ExaminerInchargeDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "GetExaminerByCode([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.ExaminerInchargeDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [HttpPost("RegistrarDashboard")]
        public async Task<ApiResult<DataTable>> RegistrarDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "RegistrarDashboard([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.RegistrarDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [HttpPost("ITSupportDashboard")]
        public async Task<ApiResult<DataTable>> ITSupportDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "ITSupportDashboard([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.ITSupportDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [HttpPost("SectionInchargeDashboard")]
        public async Task<ApiResult<DataTable>> SectionInchargeDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "SectionInchargeDashboard([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.SectionInchargeDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [HttpGet("GetExaminerGroupTotal/{examinerCode}")]
        public async Task<IActionResult> GetExaminerGroupTotal(string examinerCode)
        {
            try
            {
                var total = await _unitOfWork.ExaminersRepository.GetExaminerGroupTotalAsync(examinerCode);
                return Ok(new
                {
                    ExaminerCode = examinerCode,
                    Total = total
                });
            }
            catch (Exception ex)
            {
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = nameof(GetExaminerGroupTotal),
                    Ex = ex
                };
                await CreateErrorLog(nex, _unitOfWork);
                return StatusCode(500, new
                {
                    Message = "An error occurred while processing your request."
                });
            }
        }


        [HttpPost("ACPDashboard")]
        public async Task<ApiResult<DataTable>> ACPDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "ACPDashboard([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.ACPDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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



        [HttpPost("StoreKeeperDashboard")]
        public async Task<ApiResult<DataTable>> StoreKeeperDashboard(ExaminerDashboardSearchModel model)
        {
            ActionName = "StoreKeeperDashboard([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.StoreKeeperDashboard(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [HttpPost("GetRevalTeacherForExaminer")]
        public async Task<ApiResult<DataTable>> GetRevalTeacherForExaminer([FromBody] TeacherForExaminerSearchModel body)
        {
            ActionName = "GetTeacherForExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ExaminersRepository.GetRevalTeacherForExaminer(body));
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

        //-----------------------------------------------------------------------REVAL---------------------------------------------------------------------------------------------
        #region Reval Examiner
        [HttpGet("Getexaminer_byID_Reval/{PK_ID}/{StaffSubjectID}/{DepartmentID}/{EndTermID}/{CourseTypeID}")]
        public async Task<ApiResult<ExaminerMaster>> Getexaminer_byID_Reval(int PK_ID, int StaffSubjectID, int DepartmentID, int EndTermID, int CourseTypeID)


        {
            ActionName = "Getexaminer_byID_Reval(int PK_ID, int StaffSubjectID, int DepartmentID, int EndTermID, int CourseTypeID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ExaminerMaster>();
                try
                {
                    var data = await _unitOfWork.ExaminersRepository.Getexaminer_byID_Reval(PK_ID, StaffSubjectID, DepartmentID, EndTermID, CourseTypeID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ExaminerMaster>(data);
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

        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("SaveExaminerData_Reval")]
        public async Task<ApiResult<int>> SaveExaminerData_Reval([FromBody] ExaminerMaster request)
        {
            ActionName = " SaveExaminerData_Reval([FromBody] ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ExaminersRepository.SaveExaminerData_Reval(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ExaminerID == 0)
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
                        result.ErrorMessage = "UserID Does Not Exist";
                    }

                    else if (result.Data == -3)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "This Examiner Code is already assigned to other user";
                    }

                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ExaminerID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        }
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

        [HttpPost("GetExaminerData_Reval")]
        public async Task<ApiResult<DataTable>> GetExaminerData_Reval([FromBody] TeacherForExaminerSearchModel body)
        {
            ActionName = "GetExaminerData_Reval([FromBody] TeacherForExaminerSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ExaminersRepository.GetExaminerData_Reval(body));
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

        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpDelete("DeleteByID_Reval/{ExaminerID:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteByID_Reval(int ExaminerID, int ModifyBy)
        {
            ActionName = "DeleteByID_Reval(int ExaminerID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new ExaminerMaster
                    {
                        ExaminerID = ExaminerID,
                        ModifyBy = ModifyBy,

                        //ActiveStatus = false,
                        //DeleteStatus = true,
                    };
                    result.Data = await _unitOfWork.ExaminersRepository.DeleteByID_Reval(mappedData);
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

        [HttpPost("GetExaminerByCode_Reval")]
        public async Task<ApiResult<DataTable>> GetExaminerByCode_Reval([FromBody] ExaminerCodeLoginModel model)
        {
            ActionName = "GetExaminerByCode_Reval([FromBody] ExaminerCodeLoginModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ExaminersRepository.GetExaminerByCode_Reval(model);
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        #endregion
    }
}
