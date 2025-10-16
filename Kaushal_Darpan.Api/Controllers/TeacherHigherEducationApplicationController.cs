using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.ITI_Inspection;
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
    public class TeacherHigherEducationApplicationController : BaseController
    {
        public override string PageName => "TeacherHigherEducationApplicationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherHigherEducationApplicationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        //[RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        //[HttpPost("GetEnrolledStudent_Promoted")]
        //public async Task<ApiResult<DataTable>> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        //{
        //    ActionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.GetEnrolledStudent_Promoted(model);
        //        result.State = EnumStatus.Success;
        //        if (result.Data.Rows.Count == 0)
        //        {
        //            result.Message = Constants.MSG_DATA_NOT_FOUND;
        //        }
        //        else
        //        {
        //            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await _unitOfWork.DisposeAsync();
        //        result.State = EnumStatus.Error;
        //        result.Message = Constants.MSG_ERROR_OCCURRED;
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

       
        [HttpPost("SaveTeacherHighEduApp")]
        public async Task<ApiResult<int>> SaveTeacherHighEduApp([FromBody] TeacherHigherEducationApplicationModel request)
        {
            ActionName = "SaveTeacherHighEduApp([FromBody] List<EnrolledPromotedStudentSaveModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationRepository.SaveTeacherHighEduApp(request);
                    await _unitOfWork.SaveChangesAsync(); 
                    if (isSave == -1)
                    {
                        result.Data = -1;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave == -2)
                    {
                        result.Data = -1;
                        result.State = EnumStatus.Warning;
                        result.Message = "Cannot add new request because one is already pending";
                        
                    }
                    else if (isSave == -3)
                    {
                        result.Data = -1;
                        result.State = EnumStatus.Warning;
                        result.Message = "Cannot re-apply for same course if already approved";

                    }else if (isSave > 0)
                    {

                        if(isSave==1)
                        {
                            result.Data = isSave;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }

                        if (isSave==2)
                        {
                            result.Data = isSave;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        
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


        [HttpPost("GetCategoryOfApplyCourseInstitute")]
        public async Task<ApiResult<DataTable>> GetCategoryOfApplyCourseInstitute([FromBody] THTE_DDL body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TeacherHigherEducationApplicationRepository.GetCategoryOfApplyCourseInstitute(body));
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

        [HttpPost("THTE_GetStaffPersonalDetailByUserID")]
        public async Task<ApiResult<DataTable>> THTE_GetStaffPersonalDetailByUserID([FromBody] BTER_EM_GetPersonalDetailByUserID body)
        {

            ActionName = "BTER_EM_GetStaffList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.THTE_GetStaffPersonalDetailByUserID(body);

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

        
        [HttpPost("GetTHTE_ApplicationData")]
        public async Task<ApiResult<DataTable>> GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)
        {
            ActionName = "GetTHTE_ApplicationData(THTE_ApplicationSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.GetTHTE_ApplicationData(model);
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


        [HttpPost("GetTHTE_ApplicationByID")]
        public async Task<ApiResult<TeacherHigherEducationApplicationModel>> GetTHTE_ApplicationByID([FromBody] THTE_ApplicationSearchModel body)
        {
            ActionName = "GetTHTE_ApplicationByID([FromBody] THTE_ApplicationSearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<TeacherHigherEducationApplicationModel>();
                try
                {
                    var data = await _unitOfWork.TeacherHigherEducationApplicationRepository.GetTHTE_ApplicationByID(body);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<TeacherHigherEducationApplicationModel>(data);
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

        [HttpPost("DeleteTHTE_ApplicationByID")]
        public async Task<ApiResult<bool>> DeleteTHTE_ApplicationByID([FromBody] THTE_ApplicationSearchModel request)
        {
            ActionName = "DeleteTHTE_ApplicationByID([FromBody] THTE_ApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.DeleteTHTE_ApplicationByID(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_DELETE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_DELETE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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



        [HttpPost("GetAllAppliedCoursesDDL")]
        public async Task<ApiResult<DataTable>> GetAllAppliedCoursesDDL([FromBody] THTE_DDL body)
        {
            ActionName = "GetAllAppliedCoursesDDL([FromBody] THTE_DDL body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TeacherHigherEducationApplicationRepository.GetAllAppliedCoursesDDL(body));
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


        [HttpPost("GetAllInstitutionalsDDL")]
        public async Task<ApiResult<DataTable>> GetAllInstitutionalsDDL([FromBody] THTE_DDL body)
        {
            ActionName = "GetAllInstitutionalsDDL([FromBody] THTE_DDL body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TeacherHigherEducationApplicationRepository.GetAllInstitutionalsDDL(body));
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


        [HttpPost("THTE_GrtApplicationStatusHistory")]
        public async Task<ApiResult<DataTable>> THTE_GrtApplicationStatusHistory([FromBody] THTE_ApplicationSearchModel body)
        {
            ActionName = "THTE_GrtApplicationStatusHistory([FromBody] THTE_ApplicationSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TeacherHigherEducationApplicationRepository.THTE_GrtApplicationStatusHistory(body));
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


        [HttpPost("CommitteeSaveData")]
        public async Task<ApiResult<int>> CommitteeSaveData([FromBody] CommitteeDataModel request)
        {
            ActionName = " CommitteeSaveData([FromBody] CommitteeDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.CommitteeSaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

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

        [HttpPost("GetCommitteeAllData")]
        public async Task<ApiResult<DataTable>> GetCommitteeAllData([FromBody] CommitteeSearchModel body)
        {
            ActionName = "GetCommitteeAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TeacherHigherEducationApplicationRepository.GetCommitteeAllData(body));
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


        [HttpGet("GetCommitteeById_Team/{ID}")]
        public async Task<ApiResult<CommitteeDataModel>> GetCommitteeById_Team(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CommitteeDataModel>();
                try
                {
                    var data = await _unitOfWork.TeacherHigherEducationApplicationRepository.GetCommitteeById_Team(ID);
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
