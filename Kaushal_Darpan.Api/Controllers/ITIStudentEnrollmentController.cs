using AutoMapper;
using Azure;
using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.EmitraPayment;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentJanAadharDetail;
using Kaushal_Darpan.Models.StudentMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class ITIStudentEnrollmentController : BaseController
    {
        public override string PageName => "StudentEnrollmentController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIStudentEnrollmentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpPost("GetPreExamStudent")]
        //public async Task<ApiResult<DataTable>> GetPreExamStudent(PreExamStudentModel model)
        //{
        //    ActionName = "GetPreExamStudent()";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        result.Data = await _unitOfWork.StudentEnrollmentRepository.GetPreExamStudent(model);
        //        result.State = EnumStatus.Success;
        //        if (result.Data.Rows.Count == 0)
        //        {
        //            result.State = EnumStatus.Success;
        //            result.Message = "No record found.!";
        //            return result;
        //        }
        //        result.State = EnumStatus.Success;
        //        result.Message = "Data load successfully .!";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await _unitOfWork.DisposeAsync();
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //        // write error log
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }
        //    return result;
        //}

        [HttpPost("GetStudentAdmitted")]
        public async Task<ApiResult<DataTable>> GetStudentAdmitted(PreExamStudentModel model)
        {
            ActionName = "GetStudentAdmitted()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIStudentEnrollmentRepository.GetStudentAdmitted(model);
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

        //[HttpPost("EditStudentData_PreExam")]
        //public async Task<ApiResult<bool>> EditStudentData_PreExam([FromBody] StudentMasterModel request)
        //{
        //    ActionName = "EditStudentData_PreExam([FromBody] StudentMasterModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.StudentEnrollmentRepository.EditStudentData_PreExam(request);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = "Updated successfully .!";
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "There was an error updating data.!";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("PreExam_UpdateEnrollmentNo")]
        //public async Task<ApiResult<bool>> PreExam_UpdateEnrollmentNo([FromBody] PreExam_UpdateEnrollmentNoModel request)
        //{
        //    ActionName = "PreExam_UpdateEnrollmentNo([FromBody] PreExam_UpdateEnrollmentNoModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.StudentEnrollmentRepository.PreExam_UpdateEnrollmentNo(request);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = "Updated successfully .!";
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "There was an error updating data.!";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

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
                    //if (request.Any(x => x.RoleId != (int)EnumRole.DTE_Eng && x.RoleId != (int)EnumRole.DTETraing))
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                    //    return result;
                    //}
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITIStudentEnrollmentRepository.SaveAdmittedFinalStudentData(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

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
                    await _unitOfWork.DisposeAsync();
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
        //[HttpPost("SaveAdmittedStudentData")]
        //public async Task<ApiResult<bool>> SaveAdmittedStudentData([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveAdmittedStudentData([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Addimited && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Addimited) || x.Status != (int)EnumExamStudentStatus.SelectedForEnrollment))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveAdmittedStudentData(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("UndoRejectAtbter")]
        //public async Task<ApiResult<bool>> UndoRejectAtbter([FromBody] List<RejectMarkModel> request)
        //{
        //    ActionName = "UndoRejectAtbter([FromBody] List<RejectMarkModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            //if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Addimited && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Addimited) || x.Status != (int)EnumExamStudentStatus.SelectedForEnrollment))
        //            //{
        //            //    result.State = EnumStatus.Warning;
        //            //    result.Message = Constants.MSG_VALIDATION_FAILED;
        //            //    return result;
        //            //}
        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.UndoRejectAtbter(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("SaveEligibleForEnrollment")]
        //public async Task<ApiResult<bool>> SaveEligibleForEnrollment([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveEligibleForEnrollment([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Principal && x.RoleId != (int)EnumRole.Principal_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.EnrolledFeePaid || x.Status != (int)EnumExamStudentStatus.EligibleForEnrollment))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveEligibleForEnrollment(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("SaveSelectedForExamination")]
        //public async Task<ApiResult<bool>> SaveSelectedForExamination([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveSelectedForExamination([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => (x.StudentFilterStatusId != (int)EnumExamStudentStatus.Enrolled && x.StudentFilterStatusId != (int)EnumExamStudentStatus.New_Enrolled) || x.Status != (int)EnumExamStudentStatus.SelectedForExamination))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveSelectedForExamination(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("SaveEligibleForExamination")]
        //public async Task<ApiResult<bool>> SaveEligibleForExamination([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveEligibleForExamination([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Principal && x.RoleId != (int)EnumRole.Principal_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => x.StudentFilterStatusId != (int)EnumExamStudentStatus.ExaminationFeesPaid || x.Status != (int)EnumExamStudentStatus.EligibleForExamination))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // regular subject
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveEligibleForExamination(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("SaveRejectAtBTER")]
        //public async Task<ApiResult<bool>> SaveRejectAtBTER([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveRejectAtBTER([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RejectatBTER || x.Status != (int)EnumExamStudentStatus.RejectatBTER))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }



        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveRejectAtBTER(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        //[HttpPost("SaveDropout")]
        //public async Task<ApiResult<bool>> SaveDropout([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveDropout([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Principal && x.RoleId != (int)EnumRole.Principal_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => x.Status != (int)EnumExamStudentStatus.Dropout))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.Dropout || x.Status != (int)EnumExamStudentStatus.Dropout))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }



        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveDropout(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}



        //[HttpPost("SaveRevokeDropout")]
        //public async Task<ApiResult<bool>> SaveRevokeDropout([FromBody] List<StudentMarkedModel> request)
        //{
        //    ActionName = "SaveRevokeDropout([FromBody] List<StudentMarkedModel> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //validation
        //            if (request.Any(x => x.RoleId != (int)EnumRole.Admin && x.RoleId != (int)EnumRole.Admin_NonEng))
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
        //                return result;
        //            }
        //            if (request.Any(x => x.Status != (int)EnumExamStudentStatus.RevokeDropout))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }
        //            if (request.Any(x => x.StudentFilterStatusId == (int)EnumExamStudentStatus.RevokeDropout || x.Status != (int)EnumExamStudentStatus.RevokeDropout))
        //            {
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_VALIDATION_FAILED;
        //                return result;
        //            }



        //            //ipaddress
        //            request.ForEach(x =>
        //            {
        //                x.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            });
        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.StudentEnrollmentRepository.SaveRevokeDropout(request);
        //            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}












        #endregion




        [HttpPost("UploadTraineeData")]
        public async Task<ApiResult<RootDataModel>> UploadTraineeData([FromBody] List<NCVTChunkInfoDataModel> request)
        {
            TraineeUploadResponse objResponse = new TraineeUploadResponse();
            ActionName = "public async Task<ApiResult<bool>> UploadTraineeData([FromBody] List<StudentMarkedModelForJoined> request)\r\n([FromBody] List<StudentMarkedModelForJoined> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<RootDataModel>();
                //var apidetails = new NCVT_APIDetailsModel();
                try
                {

                    var apidetails = await _unitOfWork.ITIStudentEnrollmentRepository.GetNcvt_APIDetails();
                    NCVT_APIDetailsModel resultList = CommonFuncationHelper.ConvertDataTable<NCVT_APIDetailsModel>(apidetails);
                    if (resultList != null)
                    {
                        foreach (var record in request)
                        {
                            //get token 
                            var token = await ThirdPartyServiceHelper.GetAccessTokenAsync(resultList);
                            if (token != null && token.status =="success" && token.data != null)
                            {
                                var dataresult = await _unitOfWork.ITIStudentEnrollmentRepository.GetNCVTStudentData(record);
                                List<ITITraineeUploadModel> request = new List<ITITraineeUploadModel>();
                                //get data from database 
                                var response = await ThirdPartyServiceHelper.UploadTraineeData(dataresult, "", token.data, resultList?.DataPushApiUrl);

                               
                                if (response.State == EnumStatus.Success)
                                {
                                    var apirespose = JsonConvert.DeserializeObject<RootDataModel>(response.Data);
                                    if (apirespose.log_id != null)
                                    {


                                        #region "LOGS"
                                        try
                                        {
                                            UploadTrainee_LogsModel logsData = new UploadTrainee_LogsModel();
                                            logsData.Response = response.Data;
                                            logsData.log_id = apirespose.log_id;
                                            var log = await _unitOfWork.ITIStudentEnrollmentRepository.SaveUploadTraineeLogs(logsData);
                                            await _unitOfWork.SaveChangesAsync();
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        #endregion
                                        result.Data = apirespose;
                                        result.State = EnumStatus.Warning;
                                        result.Message = apirespose?.Message;
                                        try
                                        {
                                            record.Log_id= apirespose.log_id;
                                            var isSave = await _unitOfWork.ITIStudentEnrollmentRepository.updateLogIdOnData(record);
                                            await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                        //  if (isSave > 0)
                                        //  {
                                        //      result.Data = apirespose;
                                        //      result.State = EnumStatus.Success;
                                        //      result.Message = apirespose.Message;
                                        //  }
                                        //  else
                                        //  {
                                        //      result.Data = apirespose;
                                        //      result.State = EnumStatus.Warning;
                                        //      result.Message = apirespose.Message;
                                        //  }
                                    }
                                    else
                                    {

                                        result.Data = apirespose;
                                        result.State = EnumStatus.Warning;
                                        result.Message = "Something went Wrong";
                                    }
                                }
                                else
                                {
                                    result.State = EnumStatus.Error;
                                    result.Message = response?.Data;
                                }
                            }
                            else
                            {
                                result.State = EnumStatus.Error;
                                result.ErrorMessage = "something went wrong when generate token";
                                result.Message = token.message;
                              
                            }
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "service details not found.";
                    }
                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetNcvtStudentData_Chunks")]
        public async Task<ApiResult<DataTable>> GetNcvtStudentData_Chunks(ChunksSearchModel model)
        {
            ActionName = "GetStudentAdmitted()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIStudentEnrollmentRepository.GetNcvtStudentData_Chunks(model);
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



        ////-------------------API TO SAVE LOGS-------------------------------

        //[HttpPost("SaveUploadTraineeLogs")]
        //public async Task<ApiResult<bool>> SaveUploadTraineeLogs([FromBody] UploadTrainee_LogsModel request)
        //{
        //    ActionName = "SaveUploadTraineeLogs([FromBody] UploadTrainee_LogsModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {

        //            if (!ModelState.IsValid)
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Validation failed!";
        //                return result;
        //            }


        //            result.Data = await _unitOfWork.ITIStudentEnrollmentRepository.SaveUploadTraineeLogs(request);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.LogID == 0)
        //                {
        //                    result.Message = Constants.MSG_SAVE_SUCCESS;
        //                }
        //                else
        //                {
        //                    result.Message = Constants.MSG_UPDATE_SUCCESS;
        //                }
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.LogID == 0)
        //                {
        //                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //                }
        //                else
        //                {
        //                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}


    }
}
