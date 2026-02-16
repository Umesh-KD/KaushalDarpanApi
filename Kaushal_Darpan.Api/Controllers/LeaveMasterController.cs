using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.LeaveMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static Kaushal_Darpan.Core.Helper.CommonFuncationHelper;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomeAuthorize]
    [ValidationActionFilter]
    public class LeaveMasterController : BaseController
    {
        public override string PageName => "LeaveMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.LeaveMasterRepository.GetAllData(body);

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


        [HttpGet("GetByID/{ID:int}")]
        public async Task<ApiResult<LeaveMaster>> GetByID(int ID)
        {
            ActionName = "GetByID(int HRManagerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<LeaveMaster>();
                try
                {
                    var data = await _unitOfWork.LeaveMasterRepository.GetById(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<LeaveMaster>(data);
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

        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] LeaveMaster request)
        {
            ActionName = "SaveData([FromBody] LeaveMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }

                    // Set the stored procedure name and type
                    if (request.TotalDays > request.RemainingLeave)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }

                    //result.Data = await _unitOfWork.LeaveMasterRepository.SaveData(request);
                    var isSave = await _unitOfWork.LeaveMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    //if (result.Data)
                    //{
                    //    result.State = EnumStatus.Success;
                    //    if (request.StaffLeaveID == 0)
                    //    {
                    //        result.Message = Constants.MSG_SAVE_SUCCESS;
                    //    }
                    //    else
                    //    {
                    //        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    //    }
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Error;
                    //    if (request.StaffLeaveID == 0)
                    //    {
                    //        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    //    }
                    //    else
                    //    {
                    //        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    //    }
                    //}
                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATE_RANGE_ALREDY_EXIST;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else if (isSave ==2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("DeleteByID/{ID:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteByID(int ID, int ModifyBy)
        {
            ActionName = "DeleteByID(int HRManagerID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new LeaveMaster
                    {
                        StaffLeaveID = ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.LeaveMasterRepository.DeleteDataByID(mappedData);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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

        [HttpPost("SaveStaffLeaveRequest")]
        public async Task<ApiResult<bool>> SaveStaffLeaveRequest([FromBody] LeaveMaster request)
        {
            ActionName = "SaveStaffLeaveRequest([FromBody] LeaveMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }

                    // remaining leave validation
                    var body = new LeaveMasterSearchModel
                    {
                        Action = "GetRemainingLeave",
                        LeaveID = request.LeaveID,
                        StaffTypeID = request.StaffTypeID.Value,
                        StaffID = request.StaffID,
                        SessionTypeID = request.SessionTypeID,
                        FinancialYearID = request.FinancialYearID
                    };

                    // only approve
                    if (request.Action == "Approved")
                    {
                        // get
                        var dtLeaveBalance = await _unitOfWork.LeaveMasterRepository.GetRemainingLeave(body);
                        if (dtLeaveBalance == null || dtLeaveBalance.Rows.Count == 0)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "You have not Leave data!";
                            return result;
                        }
                        var leaveBalance = Convert.ToInt32(dtLeaveBalance?.Rows[0]["LeaveBlance"]);
                        if (request.TotalDays > leaveBalance)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = Constants.MSG_DO_NOT_ENOUGH_LEAVE_BALANCE;
                            return result;
                        }
                    }

                    result.Data = await _unitOfWork.LeaveMasterRepository.SaveStaffLeaveRequest(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                        result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("GetStaffLeaveRequest")]
        public async Task<ApiResult<DataTable>> GetStaffLeaveRequest([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "GetStaffLeaveRequest(LeaveMasterSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.LeaveMasterRepository.GetStaffLeaveRequest(body));
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



        [HttpPost("ByIDStaffLeaveList")]
        public async Task<ApiResult<DataTable>> ByIDStaffLeaveList([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "ByIDStaffLeaveList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.LeaveMasterRepository.ByIDStaffLeaveList(body));
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


        [HttpPost("GetRemainingLeave")]
        public async Task<ApiResult<DataTable>> GetRemainingLeave([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "ByIDStaffLeaveLGetRemainingLeaveist()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.LeaveMasterRepository.GetRemainingLeave(body));
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


        [HttpPost("GetLeaveCreditStaffData")]
        public async Task<ApiResult<DataTable>> GetLeaveCreditStaffData([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "GetLeaveCreditStaffData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.LeaveMasterRepository.GetLeaveCreditStaffData(body);

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


        [HttpPost("GetStaffWithLeaveBalance")]
        public async Task<ApiResult<object>> GetStaffWithLeaveBalance([FromBody] LeaveMasterSearchModel body)
        {
            ActionName = "GetStaffWithLeaveBalance(([FromBody] LeaveMasterSearchModel body)";
            var result = new ApiResult<object>();
            try
            {

                // Pass the entire model to the repository
                var dt = await _unitOfWork.LeaveMasterRepository.GetStaffWithLeaveBalance(body);
                var response = new
                {
                    Columns = dt.Columns.Cast<DataColumn>()
                         .Select(c => c.ColumnName)
                         .ToList(),

                    Rows = dt.AsEnumerable()
                         .Select(r => dt.Columns.Cast<DataColumn>()
                         .ToDictionary(c => c.ColumnName, c => r[c]))
                };

                result.Data = response;
                if (dt.Rows.Count > 0)
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



        [HttpPost("Save_CreditStaffLeave")]
        public async Task<ApiResult<bool>> Save_CreditStaffLeave([FromBody] List<CreditLeaveModel> request)
        {
            ActionName = "CreditStaffLeave([FromBody] Save_CreditStaffLeave request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.LeaveMasterRepository.Save_CreditStaffLeave(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("Save_CreditStaffLeave_NonGazetted")]
        public async Task<ApiResult<bool>> Save_CreditStaffLeave_NonGazetted([FromBody] List<CreditLeaveModel> request)
        {
            ActionName = "CreditStaffLeave_NonGazetted([FromBody] List<CreditLeaveModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.LeaveMasterRepository.Save_CreditStaffLeave_NonGazetted(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

    }

}


