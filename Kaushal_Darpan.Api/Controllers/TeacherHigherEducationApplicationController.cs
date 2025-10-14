using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.CenterObserver;
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
                    //ipaddress
                   
                    // regular subject
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationRepository.SaveTeacherHighEduApp(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = 1;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
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
    }
}
