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


        [RoleActionFilter(EnumRole.Admin, EnumRole.Admin_NonEng)]
        [HttpPost("GetEnrolledStudent_Promoted")]
        public async Task<ApiResult<DataTable>> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            ActionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TeacherHigherEducationApplicationRepository.GetEnrolledStudent_Promoted(model);
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
                    var isSave = await _unitOfWork.TeacherHigherEducationApplicationRepository.SaveEnrolledStudentVerify_ReturnbyExamIncharge(request);
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
