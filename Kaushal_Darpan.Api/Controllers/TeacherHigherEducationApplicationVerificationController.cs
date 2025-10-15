using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class TeacherHigherEducationApplicationVerificationController : BaseController
    {
        public override string PageName => "TeacherHigherEducationApplicationVerificationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherHigherEducationApplicationVerificationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetEnrolledStudent_Promoted")]
        public async Task<ApiResult<DataTable>> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            ActionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.GetEnrolledStudent_Promoted(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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

        [RoleActionFilter(EnumRole.ExaminationIncharge, EnumRole.ExaminationIncharge_NonEng)]
        [HttpPost("SaveEnrolledStudentVerify_ReturnbyExamIncharge")]
        public async Task<ApiResult<bool>> SaveEnrolledStudentVerify_ReturnbyExamIncharge([FromBody] List<EnrolledPromotedStudentSaveModel> request)
        {
            ActionName = "SaveEnrolledStudentVerify_ReturnbyExamIncharge([FromBody] List<EnrolledPromotedStudentSaveModel> request)";
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
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.SaveEnrolledStudentVerify_ReturnbyExamIncharge(request);
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

        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("ApplicationList_ForPrinciple_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForPrinciple_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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

        [RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("UpdateApplicationStatus_Principle_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_Principle_THTE(request);
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



        [RoleActionFilter(EnumRole.DTE_Eng, EnumRole.DTE_NonEng)]
        [HttpPost("ApplicationList_ForDTE_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForDTE_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForDTE_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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


        [RoleActionFilter(EnumRole.DTE_Eng, EnumRole.DTE_NonEng)]
        [HttpPost("UpdateApplicationStatus_DTE_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_DTE_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_DTE_THTE(request);
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

        [HttpPost("ApplicationList_ForCommittee_THTE")]
        public async Task<ApiResult<DataTable>> ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)
        {
            ActionName = "ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.ApplicationList_ForCommittee_THTE(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
            }
            catch (System.Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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

        //[RoleActionFilter(EnumRole.Principal, EnumRole.Principal_NonEng)]
        [HttpPost("UpdateApplicationStatus_Committee_THTE")]
        public async Task<ApiResult<bool>> UpdateApplicationStatus_Committee_THTE([FromBody] UpdateApplicationStatusDataModel_Committee request)
        {
            ActionName = "UpdateApplicationStatus_Principle_THTE([FromBody] List<UpdateApplicationStatusDataModel_Principle> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationVerificationRepository.UpdateApplicationStatus_Committee_THTE(request);
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
    }
}
