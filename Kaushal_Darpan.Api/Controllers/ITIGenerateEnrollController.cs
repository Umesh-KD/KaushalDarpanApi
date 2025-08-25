using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIGenerateEnrollment;
using Kaushal_Darpan.Models.PreExamStudent;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class ITIGenerateEnrollController : BaseController
    {
        public override string PageName => "ITIGenerateEnrollController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIGenerateEnrollController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetGenerateEnrollData")]
        public async Task<ApiResult<DataTable>> GetGenerateEnrollData(ITIGenerateEnrollSearchModel model)
        {
            ActionName = "GetGenerateEnrollData(ITIGenerateEnrollSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIGenerateEnrollRepository.GetGenerateEnrollData(model));
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
        public async Task<ApiResult<bool>> SaveEnrolledData([FromBody] List<ITIGenerateEnrollMaster> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITIGenerateEnrollRepository.SaveEnrolledData(request);
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

        [HttpPost("OnPublish")]
        public async Task<ApiResult<bool>> OnPublish([FromBody] List<ITIGenerateEnrollMaster> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // 1. generate the enrollment no.
                    var isSave = await _unitOfWork.ITIGenerateEnrollRepository.OnPublish(request);
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

                        //await _unitOfWork.PreExamStudentRepository.SaveITIEnrolledStudentExam(smModel);
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


    }
}
