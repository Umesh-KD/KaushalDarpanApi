using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.ViewStudentDetailsModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class PreExamStudentController : BaseController
    {
        public override string PageName => "PreExamStudentController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PreExamStudentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetPreExamStudent")]
        public async Task<ApiResult<DataTable>> GetPreExamStudent(PreExamStudentModel model)
        {
            ActionName = "GetPreExamStudent()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetPreExamStudent(model);
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


        [HttpPost("GetEnrollmentCancelStudent")]
        public async Task<ApiResult<DataTable>> GetEnrollmentCancelStudent(PreExamStudentModel model)
        {
            ActionName = "GetEnrollmentCancelStudent()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetEnrollmentCancelStudent(model);
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

        [HttpPost("GetStudentAdmitted")]
        public async Task<ApiResult<DataTable>> GetStudentAdmitted(PreExamStudentModel model)
        {
            ActionName = "GetStudentAdmitted()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetStudentAdmitted(model);
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

        [HttpPost("GetAnnextureListPreExamStudent")]
        public async Task<ApiResult<DataTable>> GetAnnextureListPreExamStudent(PreExamStudentModel model)
        {
            ActionName = "GetAnnextureListPreExamStudent()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.PreExamStudentRepository.GetAnnextureListPreExamStudent(model));
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

        [HttpPost("EditStudentData_PreExam")]
        public async Task<ApiResult<bool>> EditStudentData_PreExam([FromBody] StudentMasterModel request)
        {
            ActionName = "EditStudentData_PreExam([FromBody] StudentMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.PreExamStudentRepository.EditStudentData_PreExam(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("PreExam_UpdateEnrollmentNo")]
        public async Task<ApiResult<bool>> PreExam_UpdateEnrollmentNo([FromBody] PreExam_UpdateEnrollmentNoModel request)
        {
            ActionName = "PreExam_UpdateEnrollmentNo([FromBody] PreExam_UpdateEnrollmentNoModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.PreExamStudentRepository.PreExam_UpdateEnrollmentNo(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("PreExam_Student_Subject")]
        public async Task<ApiResult<bool>> PreExam_Student_Subject([FromBody] PreExamStudentSubjectRequestModel request)
        {
            ActionName = "PreExam_UpdateEnrollmentNo([FromBody] PreExamStudentSubjectRequestModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.PreExamStudentRepository.PreExamStudentSubject(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("Save_Student_Exam_Status")]
        public async Task<ApiResult<bool>> Save_Student_Exam_Status([FromBody] List<Student_DataModel> request)
        {
            ActionName = "Save_Student_Exam_Status([FromBody] List<Student_DataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.Save_Student_Exam_Status(request);
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

        [HttpPost("Save_Student_Exam_Status_Update")]
        public async Task<ApiResult<bool>> Save_Student_Exam_Status_Update([FromBody] List<Student_DataModel> request)
        {
            ActionName = "Save_Student_Exam_Status_Update([FromBody] List<Student_DataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.Save_Student_Exam_Status_Update(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
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

        [HttpGet("GetStudentSubject_ByID/{StudentID}/{DepartmentID}")]
        public async Task<ApiResult<PreExamSubjectModel>> GetStudentSubject_ByID(int StudentID, int DepartmentID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<PreExamSubjectModel>();
                try
                {
                    var data = await _unitOfWork.PreExamStudentRepository.GetStudentSubject_ByID(StudentID, DepartmentID);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
                    }
                }
                catch (Exception ex)
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

        [HttpPost("SaveAdmittedFinalStudentData")]
        public async Task<ApiResult<bool>> SaveAdmittedFinalStudentData([FromBody] List<StudentMarkedModelForJoined> request)
        {
            ActionName = "SaveAdmittedFinalStudentData([FromBody] List<StudentMarkedModelForJoined> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.DTE_Eng && x.RoleId != (int)EnumRole.DTE_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveAdmittedFinalStudentData(request);
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

        #region Student ExamStatus Flow
        [HttpPost("SaveAdmittedStudentData")]
        public async Task<ApiResult<bool>> SaveAdmittedStudentData([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveAdmittedStudentData([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Addimited && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Addimited) || x.Status != (int)EnumExamStudentStatus.SelectedForEnrollment))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveAdmittedStudentData(request);
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

        [HttpPost("UndoRejectAtbter")]
        public async Task<ApiResult<bool>> UndoRejectAtbter([FromBody] List<RejectMarkModel> request)
        {
            ActionName = "UndoRejectAtbter([FromBody] List<RejectMarkModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    //if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Addimited && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Addimited) || x.Status != (int)EnumExamStudentStatus.SelectedForEnrollment))
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_VALIDATION_FAILED;
                    //    return result;
                    //}
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.UndoRejectAtbter(request);
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

        [HttpPost("SaveEligibleForEnrollment")]
        public async Task<ApiResult<bool>> SaveEligibleForEnrollment([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveEligibleForEnrollment([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Principal && x.RoleId != (int)EnumRole.Principal_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.EnrolledFeePaid || x.Status != (int)EnumExamStudentStatus.EligibleForEnrollment))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveEligibleForEnrollment(request);
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

        [HttpPost("SaveSelectedForExamination")]
        public async Task<ApiResult<bool>> SaveSelectedForExamination([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveSelectedForExamination([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Enrolled && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Enrolled) || x.Status != (int)EnumExamStudentStatus.SelectedForExamination))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveSelectedForExamination(request);
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

        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("SaveEligibleForExamination")]
        public async Task<ApiResult<int>> SaveEligibleForExamination([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveEligibleForExamination([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //validation
                    if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.ExaminationFeesPaid || x.Status != (int)EnumExamStudentStatus.EligibleForExamination))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // regular subject
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveEligibleForExamination(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = isSave;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave == -6)
                    {
                        result.Data = isSave;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_SAVE_SUCCESS_EXCEPT_UNVERIFIED_STUDENTS;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = isSave;
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

        [HttpPost("SaveRejectAtBTER")]
        public async Task<ApiResult<bool>> SaveRejectAtBTER([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveRejectAtBTER([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER || x.Status != (int)EnumExamStudentStatus.RejectatBTER))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }



                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveRejectAtBTER(request);
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
        [HttpPost("SaveDropout")]
        public async Task<ApiResult<bool>> SaveDropout([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveDropout([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Principal && x.RoleId != (int)EnumRole.Principal_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => x.Status != (int)EnumExamStudentStatus.Dropout))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.Dropout || x.Status != (int)EnumExamStudentStatus.Dropout))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }



                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveDropout(request);
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

        [HttpPost("SaveDetained")]
        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        public async Task<ApiResult<bool>> SaveDetained([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveDetained([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.Detained && x.Status != (int)EnumExamStudentStatus.Detained))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveDetained(request);
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
        #endregion

        /// <summary>
        /// #Pradeep 2025-01-22
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Save_Student_Optional_Subject")]
        public async Task<ApiResult<bool>> Save_Student_Optional_Subject([FromBody] OptionalSubjectModel request)
        {
            ActionName = "Save_Student_Optional_Subject([FromBody] OptionalSubjectModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    var isSave = await _unitOfWork.PreExamStudentRepository.Save_Student_Optional_Subject(request);
                    _unitOfWork.SaveChanges();

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = "Optional subject already assigned/added for this student.";
                    }
                    else if (isSave == -1)
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

        /// <summary>
        /// #Pradeep 2025-01-22
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="DepartmentID"></param>
        /// <returns></returns>
        [HttpGet("GetStudentOptionalSubject_ByStudentID/{StudentID}/{EndTermID}")]
        public async Task<ApiResult<DataTable>> GetStudentOptionalSubject_ByStudentID(int StudentID, int EndTermID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.PreExamStudentRepository.GetStudentOptionalSubject_ByStudentID(StudentID, EndTermID);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
                    }
                }
                catch (Exception ex)
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


        [HttpPost("GetStudentEnrollmentApprovalReject")]
        public async Task<ApiResult<DataTable>> GetStudentEnrollmentApprovalReject(PreExamStudentModel model)
        {
            ActionName = "GetStudentEnrollmentApprovalReject()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetStudentEnrollmentApprovalReject(model);
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

        [HttpPost("SaveRejectAtBTERApprovalReject")]
        public async Task<ApiResult<bool>> SaveRejectAtBTERApprovalReject([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveRejectAtBTERApprovalReject([FromBody] List<StudentMarkedModel> request)";
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
                    //if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER || x.Status != (int)EnumExamStudentStatus.ApprovalReject))
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.Message = Constants.MSG_VALIDATION_FAILED;
                    //    return result;
                    //}
                    //if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER || x.Status != (int)EnumExamStudentStatus.RejectatBTER))
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.Message = Constants.MSG_VALIDATION_FAILED;
                    //    return result;
                    //}



                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveRejectAtBTERApprovalReject(request);
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


        [HttpGet("GetStudentupdateEnrollData/{StudentID}/{statusId}/{DepartmentID}/{Eng_NonEng}/{EndTermID}/{StudentExamID}")]
        public async Task<ApiResult<PreExam_UpdateEnrollmentNoModel>> GetStudentupdateEnrollData(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)


        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<PreExam_UpdateEnrollmentNoModel>();
                try
                {
                    var data = await _unitOfWork.PreExamStudentRepository.GetStudentupdateEnrollData(StudentID, statusId, DepartmentID, Eng_NonEng, status, EndTermID, StudentExamID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<PreExam_UpdateEnrollmentNoModel>(data);
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


        [HttpPost("ViewStudentDetails")]
        public async Task<ApiResult<ViewStudentDetailsModel>> ViewStudentDetails(ViewStudentDetailsRequestModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ViewStudentDetailsModel>();
                try
                {
                    var data = await _unitOfWork.PreExamStudentRepository.ViewStudentDetails(model);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
                    }
                }
                catch (Exception ex)
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


        [HttpGet("PreExam_StudentMaster/{StudentID}/{statusId}/{DepartmentID}/{Eng_NonEng}/{EndTermID}/{StudentExamID}")]
        public async Task<ApiResult<StudentMasterModel>> PreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<StudentMasterModel>();
                try
                {
                    var data = await _unitOfWork.PreExamStudentRepository.PreExam_StudentMaster(StudentID, statusId, DepartmentID, Eng_NonEng, status, EndTermID, StudentExamID);
                    if (data != null)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
                    }
                }
                catch (Exception ex)
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


        [HttpPost("GetRejectBTERExcelData")]
        public async Task<ApiResult<DataTable>> GetRejectBTERExcelData(PreExamStudentModel model)
        {
            ActionName = "GetRejectBTERExcelData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetRejectBTERExcelData(model);
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

        [HttpPost("GetMainAnnexure")]
        public async Task<FileResult> GetMainAnnexure(AnnexureDataModel model)
        {
            ActionName = "GetMainAnnexure(AnnexureDataModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                var dt = await _unitOfWork.PreExamStudentRepository.GetMainAnnexure(model);
                //excel
                // Create a new workbook
                using (var workbook = new XLWorkbook())
                {
                    // Add a worksheet
                    var worksheet = workbook.AddWorksheet("SampleSheet");

                    int row = 3;//start
                    int col = 2;//start
                    int lastCol = 21;

                    // heading
                    worksheet.Cell(row, col++).Value = "Sr. No.";
                    worksheet.Cell(row, col++).Value = "College Name";
                    worksheet.Cell(row, col++).Value = "Student Type";
                    worksheet.Cell(row, col++).Value = "Enrollment No.(SPN)";
                    worksheet.Cell(row, col++).Value = "Year";
                    worksheet.Cell(row, col++).Value = "Student Name";
                    worksheet.Cell(row, col++).Value = "Father's Name";
                    worksheet.Cell(row, col++).Value = "Mother's Name";
                    worksheet.Cell(row, col++).Value = "Paper Code";
                    worksheet.Cell(row, col++).Value = "Status";
                    worksheet.Cell(row, col++).Value = "Program";
                    worksheet.Cell(row, col++).Value = "Transaction ID";
                    worksheet.Cell(row, col++).Value = "Payment Date";
                    worksheet.Cell(row, col++).Value = "Fee Paid";
                    worksheet.Cell(row, col++).Value = "Institute Code";
                    worksheet.Cell(row, col++).Value = "Program Code";
                    worksheet.Cell(row, col++).Value = "Center Code";
                    worksheet.Cell(row, col++).Value = "Roll No.";
                    worksheet.Cell(row, col++).Value = "Mobile";
                    worksheet.Cell(row, col++).Value = "Date of Birth";

                    // Set header row height and style
                    worksheet.Row(row).Height = 40;
                    worksheet.Row(row).Style.Font.FontSize = 13;
                    worksheet.Row(row).Style.Font.Bold = true;

                    // Iterate through the data rows
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        col = 2;//start
                        // Merging and adding content for InstituteName
                        if (i == 0 || dt.Rows[i]["InstituteName"]?.ToString() != dt.Rows[i - 1]["InstituteName"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"College Name: {dt.Rows[i]["InstituteName"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Merging and adding content for StreamName
                        if (i == 0 || dt.Rows[i]["StreamName"]?.ToString() != dt.Rows[i - 1]["StreamName"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"Stream Name: {dt.Rows[i]["StreamName"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Merging and adding content for StudentType
                        if (i == 0 || dt.Rows[i]["StreamName"]?.ToString() != dt.Rows[i - 1]["StreamName"]?.ToString() || dt.Rows[i]["StudentType"]?.ToString() != dt.Rows[i - 1]["StudentType"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"Student Type: {dt.Rows[i]["StudentType"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Add student data in normal rows
                        row++;  // Move to the next row for student data
                        worksheet.Cell(row, col++).Value = i + 1; // Sr. No.
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["InstituteName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StudentType"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["EnrollmentNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["SemesterName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StudentName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["FatherName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["MotherName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["Papers"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StatusName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StreamName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["TransctionNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["TransctionDate"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["ItemAmount"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["InstituteCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StreamCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["CenterCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["RollNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["MobileNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["Dis_DOB"]?.ToString();
                    }
                    // Apply borders to all cells in the used range
                    worksheet.Cells().Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    // Auto-adjust columns to fit content
                    worksheet.Columns().AdjustToContents();

                    // Convert the workbook to a byte array
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var fileBytes = stream.ToArray();
                        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "mainanex2.xlsx");
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
            return null;
        }

        [HttpPost("GetSpecialAnnexure")]
        public async Task<FileResult> GetSpecialAnnexure(AnnexureDataModel model)
        {
            ActionName = "GetSpecialAnnexure(AnnexureDataModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                var dt = await _unitOfWork.PreExamStudentRepository.GetSpecialAnnexure(model);
                //excel
                // Create a new workbook
                using (var workbook = new XLWorkbook())
                {
                    // Add a worksheet
                    var worksheet = workbook.AddWorksheet("SampleSheet");

                    int row = 3;//start
                    int col = 2;//start
                    int lastCol = 21;

                    // heading
                    worksheet.Cell(row, col++).Value = "Sr. No.";
                    worksheet.Cell(row, col++).Value = "College Name";
                    worksheet.Cell(row, col++).Value = "Student Type";
                    worksheet.Cell(row, col++).Value = "Enrollment No.(SPN)";
                    worksheet.Cell(row, col++).Value = "Year";
                    worksheet.Cell(row, col++).Value = "Student Name";
                    worksheet.Cell(row, col++).Value = "Father's Name";
                    worksheet.Cell(row, col++).Value = "Mother's Name";
                    worksheet.Cell(row, col++).Value = "Paper Code";
                    worksheet.Cell(row, col++).Value = "Status";
                    worksheet.Cell(row, col++).Value = "Program";
                    worksheet.Cell(row, col++).Value = "Transaction ID";
                    worksheet.Cell(row, col++).Value = "Payment Date";
                    worksheet.Cell(row, col++).Value = "Fee Paid";
                    worksheet.Cell(row, col++).Value = "Institute Code";
                    worksheet.Cell(row, col++).Value = "Program Code";
                    worksheet.Cell(row, col++).Value = "Center Code";
                    worksheet.Cell(row, col++).Value = "Roll No.";
                    worksheet.Cell(row, col++).Value = "Mobile";
                    worksheet.Cell(row, col++).Value = "Date of Birth";

                    // Set header row height and style
                    worksheet.Row(row).Height = 40;
                    worksheet.Row(row).Style.Font.FontSize = 13;
                    worksheet.Row(row).Style.Font.Bold = true;

                    // Iterate through the data rows
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        col = 2;//start
                        // Merging and adding content for InstituteName
                        if (i == 0 || dt.Rows[i]["InstituteName"]?.ToString() != dt.Rows[i - 1]["InstituteName"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"College Name: {dt.Rows[i]["InstituteName"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Merging and adding content for StreamName
                        if (i == 0 || dt.Rows[i]["StreamName"]?.ToString() != dt.Rows[i - 1]["StreamName"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"Stream Name: {dt.Rows[i]["StreamName"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Merging and adding content for StudentType
                        if (i == 0 || dt.Rows[i]["StreamName"]?.ToString() != dt.Rows[i - 1]["StreamName"]?.ToString() || dt.Rows[i]["StudentType"]?.ToString() != dt.Rows[i - 1]["StudentType"]?.ToString())
                        {
                            row++;  // Move to the next row
                            var mergedCell = worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, lastCol));
                            mergedCell.Merge();
                            mergedCell.Value = $"Student Type: {dt.Rows[i]["StudentType"]?.ToString()}";
                            mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            mergedCell.Style.Font.Bold = true;
                        }

                        // Add student data in normal rows
                        row++;  // Move to the next row for student data
                        worksheet.Cell(row, col++).Value = i + 1; // Sr. No.
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["InstituteName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StudentType"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["EnrollmentNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["SemesterName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StudentName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["FatherName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["MotherName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["Papers"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StatusName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StreamName"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["TransctionNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["TransctionDate"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["ItemAmount"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["InstituteCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["StreamCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["CenterCode"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["RollNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["MobileNo"]?.ToString();
                        worksheet.Cell(row, col++).Value = dt.Rows[i]["Dis_DOB"]?.ToString();
                    }
                    // Apply borders to all cells in the used range
                    worksheet.Cells().Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cells().Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    // Auto-adjust columns to fit content
                    worksheet.Columns().AdjustToContents();

                    // Convert the workbook to a byte array
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var fileBytes = stream.ToArray();
                        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "specialanex2.xlsx");
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
            return null;
        }


        [HttpPost("SaveRevokeDropout")]
        public async Task<ApiResult<bool>> SaveRevokeDropout([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveRevokeDropout([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => x.Status != (int)EnumExamStudentStatus.RevokeDropout))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RevokeDropout || x.Status != (int)EnumExamStudentStatus.RevokeDropout))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }



                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveRevokeDropout(request);
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

        [HttpPost("SaveRevokeDetained")]
        [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        public async Task<ApiResult<bool>> SaveRevokeDetained([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveDetained([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.DetainedRevoke && x.Status != (int)EnumExamStudentStatus.DetainedRevoke))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.PreExamStudentRepository.SaveRevokeDetained(request);
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

        [HttpPost("GetPreExamStudentForVerify")]
        public async Task<ApiResult<DataTable>> GetPreExamStudentForVerify(PreExamStudentModel model)
        {
            ActionName = "GetPreExamStudent()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PreExamStudentRepository.GetPreExamStudentForVerify(model);
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

        [HttpPost("VerifyByExaminationIncharge")]
        public async Task<ApiResult<bool>> VerifyByExaminationIncharge([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "SaveEligibleForExamination([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.ExaminationIncharge && x.RoleId != (int)EnumRole.ExaminationIncharge_NonEng))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Any(x => x.Status != (int)EnumExamStudentStatus.EligibleForExamination))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // regular subject
                    var isSave = await _unitOfWork.PreExamStudentRepository.VerifyByExaminationIncharge(request);
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
        
        [HttpPost("VerifyStudent_Registrar")]
        public async Task<ApiResult<bool>> VerifyStudent_Registrar([FromBody] List<StudentMarkedModel> request)
        {
            ActionName = "VerifyStudent_Registrar([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // regular subject
                    var isSave = await _unitOfWork.PreExamStudentRepository.VerifyStudent_Registrar(request);
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
    }
}
