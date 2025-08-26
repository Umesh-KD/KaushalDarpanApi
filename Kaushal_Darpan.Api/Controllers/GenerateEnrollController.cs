using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.ScholarshipMaster;
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
    public class GenerateEnrollController : BaseController
    {
        public override string PageName => "GenerateEnrollController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateEnrollController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetGenerateEnrollData")]
        public async Task<ApiResult<DataTable>> GetGenerateEnrollData(GenerateEnrollSearchModel model)
        {
            ActionName = "GetGenerateEnrollData(GenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateEnrollRepository.GetGenerateEnrollData(model));
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

        [HttpPost("SaveEnrolledData")]
        public async Task<ApiResult<bool>> SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.SaveEnrolledData(request);
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

        [RoleActionFilter(EnumRole.ACP, EnumRole.ACP_NonEng)]
        [HttpPost("OnPublish")]
        public async Task<ApiResult<bool>> OnPublish([FromBody] List<GenerateEnrollMaster> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // 1. generate the enrollment no.
                    var isSave = await _unitOfWork.GenerateEnrollRepository.OnPublish(request);
                    if (isSave > 0)
                    {
                        // 2. save student in student exam for regular
                        var smModel = new List<StudentMarkedModel>();
                        request.ForEach(x =>
                        {
                            smModel.Add(new StudentMarkedModel
                            {
                                ModifyBy = x.ModifyBy,
                                StudentId = x.StudentID,
                                Marked = true,//already marked
                                EndTermID = x.EndTermID,
                            });
                        });
                        await _unitOfWork.PreExamStudentRepository.SaveEnrolledStudentExam(smModel);
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

        [HttpPost("ChangeEnRollNoStatus")]
        public async Task<ApiResult<int>> ChangeEnRollNoStatus([FromBody] GenerateEnrollSearchModel request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.ChangeEnRollNoStatus(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful
                    if (isSave == -1)
                    {
                        result.Data = isSave;
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
                        result.Message = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = ex.Message;

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



        [HttpPost("SaveApplicationWorkFlow")]
        public async Task<ApiResult<bool>> SaveApplicationWorkFlow([FromBody] GenerateEnrollSearchModel request)
        {
            ActionName = "SaveApplicationWorkFlow([FromBody] GenerateEnrollSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.GenerateEnrollRepository.SaveApplicationWorkFlow(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        //if (request.ScholarshipID == 0)
                        //{
                        //    result.Message = Constants.MSG_SAVE_SUCCESS;
                        //}
                        //else
                        //{
                        //    result.Message = Constants.MSG_UPDATE_SUCCESS;
                        //}
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        //if (request.ScholarshipID == 0)
                        //{
                        //    result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        //}
                        //else
                        //{
                        //    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        //}
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

        [HttpPost("GetPublishedEnRollData")]
        public async Task<ApiResult<DataTable>> GetPublishedEnRollData(GenerateEnrollSearchModel model)
        {
            ActionName = "GetPublishedEnRollData(GenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateEnrollRepository.GetPublishedEnRollData(model));
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

        [HttpPost("GetEligibleStudentButPendingForVerification")]
        public async Task<ApiResult<DataTable>> GetEligibleStudentButPendingForVerification(GenerateEnrollSearchModel model)
        {
            ActionName = "GetEligibleStudentButPendingForVerification(GenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateEnrollRepository.GetEligibleStudentButPendingForVerification(model));
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


        [HttpPost("SaveEligibleStudentButPendingForVerification")]
        public async Task<ApiResult<int>> SaveEligibleStudentButPendingForVerification([FromBody] List<EligibleStudentButPendingForVerification> request)
        {
            ActionName = "SaveEligibleStudentButPendingForVerification([FromBody] List<StudentMarkedModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //validation
                    if (request.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.SaveEligibleStudentButPendingForVerification(request);
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
        [HttpPost("GetEligibleStudentVerified")]
        public async Task<ApiResult<DataTable>> GetEligibleStudentVerified(GenerateEnrollSearchModel model)
        {
            ActionName = "GetEligibleStudentVerified(GenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateEnrollRepository.GetEligibleStudentVerified(model));
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

        [HttpPost("StudentEnrollment_RegistrarStatus")]
        public async Task<ApiResult<int>> StudentEnrollment_RegistrarStatus([FromBody] List<EligibleStudentButPendingForVerification> request)
        {
            ActionName = "StudentEnrollment_RegistrarStatus([FromBody] List<EligibleStudentButPendingForVerification> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //validation
                    if (request.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.StudentEnrollment_RegistrarStatus(request);
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

        [HttpPost("StudentEnrollment_ReturnByRegistrar")]
        public async Task<ApiResult<int>> StudentEnrollment_ReturnByRegistrar([FromBody] List<EligibleStudentButPendingForVerification> request)
        {
            ActionName = "StudentEnrollment_ReturnByRegistrar([FromBody] List<EligibleStudentButPendingForVerification> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //validation
                    if (request.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.StudentEnrollment_ReturnByRegistrar(request);
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

        [HttpPost("GetEnRollData_RegistrarVerify")]
        public async Task<ApiResult<DataTable>> GetEnRollData_RegistrarVerify(GenerateEnrollSearchModel model)
        {
            ActionName = "GetEnRollData_RegistrarVerify(GenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateEnrollRepository.GetEnRollData_RegistrarVerify(model));
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
    }
}
