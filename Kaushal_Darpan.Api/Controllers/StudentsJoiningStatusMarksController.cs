using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BterStudentJoinStatus;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using Kaushal_Darpan.Models.studentve;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]
    public class StudentsJoiningStatusMarksController : BaseController
    {
        public override string PageName => "StudentsJoiningStatusMarksController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentsJoiningStatusMarksController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] StudentsJoiningStatusMarksModel request)
        {
            ActionName = "SaveData([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StudentsJoiningStatusMarksRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data ==1 )
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved Successfully .!";
                    }
                    else if (result.Data == 2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Updated Successfully .!";

                    }
                    else if (result.Data == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Faild";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error adding data.!";
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


        [HttpPost("SaveReporting")]
        public async Task<ApiResult<int>> SaveReporting([FromBody] AllotmentReportingModel request)
        {
            ActionName = "SaveReporting([FromBody] AllotmentReportingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StudentsJoiningStatusMarksRepository.SaveReporting(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data ==1)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved Successfully .!";
                    }
                    else if (result.Data == 2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Updated Successfully .!";

                    }
                    else if (result.Data == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Faild";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error adding data.!";
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



        [HttpPost("SaveCorrectDocument")]
        public async Task<ApiResult<int>> SaveCorrectDocument([FromBody] AllotmentReportingModel request)
        {
            ActionName = "SaveReporting([FromBody] AllotmentReportingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StudentsJoiningStatusMarksRepository.SaveCorrectDocument(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data ==1)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved Successfully .!";
                    }
                    else if (result.Data == 2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Updated Successfully .!";

                    }
                    else if (result.Data == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Faild";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error adding data.!";
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



        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetAllData(body));
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


        [HttpPost("CheckAllotment")]
        public async Task<ApiResult<DataTable>> CheckAllot([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.CheckAllot(body));
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






        [HttpPost("GetAllotmentdata")]
        public async Task<ApiResult<AllotmentReportingModel>> GetAllotmentdata(StudentsJoiningStatusMarksSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AllotmentReportingModel>();
                try
                {
                    var data = await _unitOfWork.StudentsJoiningStatusMarksRepository.GetAllotmentdata(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<AllotmentReportingModel>(data);
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


        [HttpPost("GetAllReuploadDocumentList")]
        public async Task<ApiResult<DataTable>> GetAllReuploadDocumentList([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetAllReuploadDocumentList(body));
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


        [HttpPost("GetCorrectDocumentdata")]
        public async Task<ApiResult<AllotmentReportingModel>> GetCorrectDocumentdata(StudentsJoiningStatusMarksSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AllotmentReportingModel>();
                try
                {
                    var data = await _unitOfWork.StudentsJoiningStatusMarksRepository.GetCorrectDocumentdata(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<AllotmentReportingModel>(data);
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


        [HttpPost("GetUpgradedbyUpwardList")]
        public async Task<ApiResult<DataTable>> GetUpgradedbyUpwardList([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetUpgradedbyUpwardList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetUpgradedbyUpwardList(body));
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
