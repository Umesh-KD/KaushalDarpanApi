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
    public class BterStudentJoinstatusController : BaseController
    {
        public override string PageName => "BterStudentJoinstatusController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BterStudentJoinstatusController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] BterStudentsJoinStatusMarksMedel request)
        {
            ActionName = "SaveData([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BterStudentJoinStatusRepository.SaveData(request);
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
        
        [HttpPost("SaveWithdrawData")]
        public async Task<ApiResult<int>> SaveWithdrawData([FromBody] BterStudentsJoinStatusMarksMedel request)
        {
            ActionName = "SaveData([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BterStudentJoinStatusRepository.SaveWithdrawData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved Successfully .!";
                    }
                    else if (result.Data < 0)
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
        public async Task<ApiResult<int>> SaveReporting([FromBody] BterAllotmentReportingModel request)
        {
            ActionName = "SaveReporting([FromBody] AllotmentReportingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BterStudentJoinStatusRepository.SaveReporting(request);
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

        [HttpPost("SaveInstituteReporting")]
        public async Task<ApiResult<int>> SaveInstituteReporting([FromBody] BterAllotmentReportingModel request)
        {
            ActionName = "SaveReporting([FromBody] AllotmentReportingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BterStudentJoinStatusRepository.SaveInstituteReporting(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data == 1)
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


        [HttpPost("NodalReporting")]
        public async Task<ApiResult<int>> NodalReporting([FromBody] BterAllotmentReportingModel request)
        {
            ActionName = "NodalReporting([FromBody] AllotmentReportingModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BterStudentJoinStatusRepository.NodalReporting(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data == 1)
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
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] BterStudentsJoinStatusMarksSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BterStudentJoinStatusRepository.GetAllData(body));
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
        
        [HttpPost("GetWithdrawAllotmentData")]
        public async Task<ApiResult<DataTable>> GetWithdrawAllotmentData([FromBody] BterStudentsJoinStatusMarksSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BterStudentJoinStatusRepository.GetWithdrawAllotmentData(body));
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
        public async Task<ApiResult<AllotmentReportingModel>> GetAllotmentdata(BterStudentsJoinStatusMarksSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<AllotmentReportingModel>();
                try
                {
                    var data = await _unitOfWork.BterStudentJoinStatusRepository.GetAllotmentdata(searchRequest);
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

        [HttpPost("GetStudentAllotmentDetails")]
        public async Task<ApiResult<DataTable>> GetStudentAllotmentDetails([FromBody] BterStudentsJoinStatusMarksSearchModel body)
        {
            ActionName = "GetStudentAllotmentDetails()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.BterStudentJoinStatusRepository.GetStudentAllotmentDetails(body));
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
