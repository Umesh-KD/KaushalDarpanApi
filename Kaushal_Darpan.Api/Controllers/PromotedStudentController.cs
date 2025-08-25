using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.PrometedStudentMaster;
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
    public class PromotedStudentController : BaseController
    {
        public override string PageName => "PromotedStudentController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PromotedStudentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetPromotedStudent")]
        public async Task<ApiResult<List<PrometedStudentMasterModel>>> GetPromotedStudent(PromotedStudentSearchModel model)
        {
            ActionName = "GetPromotedStudent(PromotedStudentSearchModel model)";
            var result = new ApiResult<List<PrometedStudentMasterModel>>();
            try
            {
                result.Data = await _unitOfWork.PromotedStudentRepository.GetPromotedStudent(model);
                result.State = EnumStatus.Success;
                if (result.Data.Count == 0)
                {
                    result.State = EnumStatus.Success;
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



        [HttpPost("GetITIPromotedStudent")]
        public async Task<ApiResult<DataTable>> GetITIPromotedStudent(PromotedStudentSearchModel model)
        {
            ActionName = "GetPromotedStudent(PromotedStudentSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.PromotedStudentRepository.GetITIPromotedStudent(model);
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
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







        [HttpPost("SavePromotedStudent")]
        public async Task<ApiResult<bool>> SavePromotedStudent([FromBody] List<PromotedStudentMarkedModel> request)
        {
            ActionName = "SavePromotedStudent([FromBody] List<PromotedStudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Count == 0)
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
                    // 1. promoted student in next term
                    var isSave = await _unitOfWork.PromotedStudentRepository.SavePromotedStudent(request);
                    if (isSave > 0)
                    {
                        // 2. save student in student exam for regular
                        var smModel = new List<StudentMarkedModel>();
                        request.ForEach(x =>
                        {
                            smModel.Add(new StudentMarkedModel
                            {
                                RoleId = x.RoleId,
                                ModifyBy = x.ModifyBy,
                                StudentId = x.StudentId,
                                Marked = x.Marked,
                                EndTermID = x.EndTermID
                            });
                        });
                        await _unitOfWork.PromotedStudentRepository.SaveEnrolledStudentExam_Next(smModel);

                        // 3. save student in student exam for back with papers                      
                        await _unitOfWork.PromotedStudentRepository.SaveEnrolledStudentExam_Back(smModel);
                    }
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
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
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


        [HttpPost("SaveItiPromotedStudent")]
        public async Task<ApiResult<bool>> SaveItiPromotedStudent([FromBody] List<PromotedStudentMarkedModel> request)
        {
            ActionName = "SavePromotedStudent([FromBody] List<PromotedStudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //validation
                    if (request.Any(x => x.RoleId != (int)EnumRole.Admin))
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                        return result;
                    }
                    if (request.Count == 0)
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
                    // 1. promoted student in next term
                    var isSave = await _unitOfWork.PromotedStudentRepository.SaveItiPromotedStudent(request);
                    if (isSave > 0)
                    {
                        // 2. save student in student exam for regular
                        var smModel = new List<StudentMarkedModel>();
                        request.ForEach(x =>
                        {
                            smModel.Add(new StudentMarkedModel
                            {
                                RoleId = x.RoleId,
                                ModifyBy = x.ModifyBy,
                                StudentId = x.StudentId,
                                Marked = x.Marked,
                                EndTermID = x.EndTermID
                            });
                        });
                        //await _unitOfWork.PromotedStudentRepository.SaveEnrolledStudentExam_Next(smModel);

                        //// 3. save student in student exam for back with papers                      
                        //await _unitOfWork.PromotedStudentRepository.SaveEnrolledStudentExam_Back(smModel);
                    }
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
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
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
