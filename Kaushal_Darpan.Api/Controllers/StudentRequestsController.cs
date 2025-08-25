using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Kaushal_Darpan.Models.StudentRequestsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class StudentRequestsController : BaseController
    {
        public override string PageName => "StudentRequestsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StudentRequestsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllData(body));
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


        [HttpPost("GetAllRoomAllotment")]
        public async Task<ApiResult<DataTable>> GetAllRoomAllotment([FromBody] SearchStudentAllotment body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllRoomAllotment(body));
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


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] RoomAllotmentModel request)
        {
            ActionName = "SaveData([FromBody] RoomAllotmentModel request)";
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


                    result.Data = await _unitOfWork.iStudentRequestsRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.AllotSeatId == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.AllotSeatId == 0)
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


        [HttpPost("GetAllRoomAvailabilties")]
        public async Task<ApiResult<DataSet>> GetAllRoomAvailabilties([FromBody] RoomAllotmentModel request)
        {
            ActionName = "GetAllRoomAvailabilties()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await _unitOfWork.iStudentRequestsRepository.GetAllRoomAvailabilties(request);
                if (result.Data.Tables.Count > 0)
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


        [HttpPost("ApprovedReq/{ReqId}/{ModifyBy}")]
        public async Task<ApiResult<bool>> ApprovedReq(int ReqId, int ModifyBy)
        {
            ActionName = "ApprovedReq(int ReqId, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var UserApprovedReq = new RoomAllotmentModel
                    {
                        ReqId = ReqId,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.iStudentRequestsRepository.ApprovedReq(UserApprovedReq);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_APPROVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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


        [HttpPost("AllotmentCancelData")]
        public async Task<ApiResult<bool>> AllotmentCancelData([FromBody] RoomAllotmentModel request)
        {
            ActionName = "AllotmentCancelData([FromBody] RoomAllotmentModel request)";
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


                    result.Data = await _unitOfWork.iStudentRequestsRepository.AllotmentCancelData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ReqId == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ReqId == 0)
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



        [HttpPost("GetReportData")]
        public async Task<ApiResult<DataTable>> GetReportData([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetReportData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetReportData(body));
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


        [HttpPost("GetHostelDashboard")]
        public async Task<ApiResult<DataTable>> GetHostelDashboard([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetHostelDashboard()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.iStudentRequestsRepository.GetHostelDashboard(body);
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
                _unitOfWork.Dispose();
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




        [HttpPost("GetGuestRoomDashboard")]
        public async Task<ApiResult<DataTable>> GetGuestRoomDashboard([FromBody] DTEApplicationDashboardModel body)
        {
            ActionName = "GetGuestRoomDashboard()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.iStudentRequestsRepository.GetGuestRoomDashboard(body);
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
                _unitOfWork.Dispose();
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

        [HttpPost("GetAllHostelStudentMeritlist")]
        public async Task<ApiResult<DataTable>> GetAllHostelStudentMeritlist([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllHostelStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllHostelStudentMeritlist(body));
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
        [HttpPost("GetAllPrincipalstudentmeritlist")]
        public async Task<ApiResult<DataTable>> GetAllPrincipalstudentmeritlist([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllHostelStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllPrincipalstudentmeritlist(body));
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


       
        [HttpPost("GetAllGenerateHostelStudentMeritlist")]
        public async Task<ApiResult<DataTable>> GetAllGenerateHostelStudentMeritlist([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllGenerateHostelStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                //  Fetch data from repository using unit of work pattern
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllGenerateHostelStudentMeritlist(body));

                result.State = EnumStatus.Success;

                //  Handle empty data case
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                //  Success case with data
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
            {
                //  Handle and log exception
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                //  Prepare error log object
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };

                //  Log the error
                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }

        [HttpPost("GetAllGenerateHostelWardenStudentMeritlist")]
        public async Task<ApiResult<DataTable>> GetAllGenerateHostelWardenStudentMeritlist([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllGenerateHostelWardenStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                //  Fetch data from repository using unit of work pattern
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllGenerateHostelWardenStudentMeritlist(body));

                result.State = EnumStatus.Success;

                //  Handle empty data case
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                //  Success case with data
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
            {
                //  Handle and log exception
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                //  Prepare error log object
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };

                //  Log the error
                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }




        [HttpPost("GetAllPublishHostelStudentMeritlist")]
        public async Task<ApiResult<int>> GetAllPublishHostelStudentMeritlist([FromBody] List<PublishHostelMeritListDataModel> request)
        {
            ActionName = "GetAllPublishHostelStudentMeritlist([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iStudentRequestsRepository.GetAllPublishHostelStudentMeritlist(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                       // result.Data = true;
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


        [HttpPost("GetAllFinalPublishHostelStudentMeritlist")]
        public async Task<ApiResult<int>> GetAllFinalPublishHostelStudentMeritlist([FromBody] List<PublishHostelMeritListDataModel> request)
        {
            ActionName = "GetAllFinalPublishHostelStudentMeritlist([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iStudentRequestsRepository.GetAllFinalPublishHostelStudentMeritlist(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        // result.Data = true;
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

        [HttpPost("GetAllFinalCorrectionPublishHostelStudentMeritlist")]
        public async Task<ApiResult<int>> GetAllFinalCorrectionPublishHostelStudentMeritlist([FromBody] List<PublishHostelMeritListDataModel> request)
        {
            ActionName = "GetAllFinalCorrectionPublishHostelStudentMeritlist([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iStudentRequestsRepository.GetAllFinalCorrectionPublishHostelStudentMeritlist(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        // result.Data = true;
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

        [HttpPost("GetAllfinalHostelStudentMeritlist")]
        public async Task<ApiResult<DataTable>> GetAllfinalHostelStudentMeritlist([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllGenerateHostelWardenStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                //  Fetch data from repository using unit of work pattern
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllfinalHostelStudentMeritlist(body));

                result.State = EnumStatus.Success;

                //  Handle empty data case
                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found.!";
                    return result;
                }

                //  Success case with data
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
            {
                //  Handle and log exception
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                //  Prepare error log object
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };

                //  Log the error
                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GetAllAffidavitApproved")]
        public async Task<ApiResult<int>> GetAllAffidavitApproved([FromBody] List<PublishHostelMeritListDataModel> request)
        {
            ActionName = "GetAllAffidavitApproved([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iStudentRequestsRepository.GetAllAffidavitApproved(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        // result.Data = true;
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

        [HttpPost("GetAllAffidavitObjection")]
        public async Task<ApiResult<int>> GetAllAffidavitObjection([FromBody] List<PublishHostelMeritListDataModel> request)
        {
            ActionName = "GetAllAffidavitObjection([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.iStudentRequestsRepository.GetAllAffidavitObjection(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        // result.Data = true;
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


        [HttpPost("GetAllDataStatus")]
        public async Task<ApiResult<DataTable>> GetAllDataStatus([FromBody] SearchStudentApplyForHostel body)
        {
            ActionName = "GetAllHostelStudentMeritlist()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.iStudentRequestsRepository.GetAllDataStatus(body));
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
