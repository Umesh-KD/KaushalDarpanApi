using AutoMapper;
using Azure;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.HostelManagementModel;

using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]    
    //[ValidationActionFilter]        
    public class HostelManagementController : BaseController
    {
        public override string PageName => "HostelManagementController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HostelManagementController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveData")]
        //public async Task<ApiResult<int>> SaveData([FromBody] HostelManagementDataModel request)
        //{
        //    ActionName = "SaveData([FromBody] HostelManagement request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.HostelManagementRepository.SaveData(request);
        //            _unitOfWork.SaveChanges();
        //            if (result.Data > 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.HostelID == 0)
        //                    result.Message = "Saved successfully .!";
        //                else
        //                    result.Message = "Updated successfully .!";
        //            }
        //            //else if (result.Data == -2)
        //            //{
        //            //    result.State = EnumStatus.Warning;
        //            //    result.ErrorMessage = "Already save";

        //            //}
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.HostelID == 0)
        //                    result.ErrorMessage = "There was an error adding data.!";
        //                else
        //                    result.ErrorMessage = "There was an error updating data.!";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}





        public async Task<ApiResult<int>> SaveData([FromBody] HostelManagementDataModel request)
        {
            ActionName = "SaveData([FromBody] HostelManagement request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.SaveData(request);
               

                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = request.HostelID == 0 ? "Saved successfully.!" : "Updated successfully.!";
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "This record already exists.";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = request.HostelID == 0
                            ? "There was an error adding data.!"
                            : "There was an error updating data.!";
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;

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



        [HttpPost("GetAllHostelList")]
        public async Task<ApiResult<DataTable>> GetAllHostelList([FromBody] HostelManagementSearchModel body)
        {
            ActionName = "GetAllHostelList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetAllHostelList(body));
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



        [HttpGet("GetByHostelId/{PK_ID:int}")]
        public async Task<ApiResult<HostelManagementDataModel>> GetByHostelId(int PK_ID)
        {
            ActionName = "GetByHostelId(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<HostelManagementDataModel>();
                try
                {
                    var data = await _unitOfWork.HostelManagementRepository.GetByHostelId(PK_ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<HostelManagementDataModel>(data);
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

        /*delete is used to remove the existing record*/
        [HttpPost("DeleteDataByID")]
        public async Task<ApiResult<bool>> DeleteDataByID([FromBody] StatusChangeModel request)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.DeleteDataByID(request);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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


        [HttpPost("GetStudentDetailsForHostelApply")]
        public async Task<ApiResult<DataTable>> GetStudentDetailsForHostelApply([FromBody] HostelStudentSearchModel body)
        {
            ActionName = "GetStudentDetailsForHostelApply()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetStudentDetailsForHostelApply(body));
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

        [HttpPost("GetStudentDetailsForHostel_Principle")]
        public async Task<ApiResult<DataTable>> GetStudentDetailsForHostel_Principle([FromBody] HostelStudentSearchModel body)
        {
            ActionName = " GetStudentDetailsForHostel_Principle([FromBody] HostelStudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetStudentDetailsForHostel_Principle(body));
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


        [HttpPost("GetHostelNameList")]
        public async Task<ApiResult<DataTable>> GetHostelNameList([FromBody] HostelRoomSeatSearchModel body)
        {
            ActionName = "GetHostelNameList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetHostelNameList(body));
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



        //[HttpPost("SaveRoomSeatData")]
        //public async Task<ApiResult<int>> SaveRoomSeatData([FromBody] HostelRoomSeatDataModel request)
        //{
        //    ActionName = "SaveRoomSeatData([FromBody] HostelManagement request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.HostelManagementRepository.SaveRoomSeatData(request);
        //            _unitOfWork.SaveChanges();
        //            if (result.Data == 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.HRSMasterID == 0)
        //                    result.Message = "Saved successfully .!";
        //                else
        //                    result.Message = "Updated successfully .!";
        //            }
        //            else if (result.Data == -2)
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = "Duplicate Entry";

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.HRSMasterID == 0)
        //                    result.ErrorMessage = "There was an error adding data.!";
        //                else
        //                    result.ErrorMessage = "There was an error updating data.!";
        //            }

        //        }
        //        catch (System.Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}

        [HttpPost("SaveDataRoomSeatMaster")]
        public async Task<ApiResult<int>> SaveData([FromBody] HostelRoomSeatDataModel request)
        {
            ActionName = "SaveData([FromBody] HostelRoomSeatDataModel request)";


            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.SaveRoomSeatData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.HRSMasterID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.HRSMasterID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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




        //[HttpPost("SaveRoomSeatData")]
        //public async Task<ApiResult<int>> SaveRoomSeatData([FromBody] HostelRoomSeatDataModel request)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.HostelManagementRepository.SaveRoomSeatData(request);
        //            _unitOfWork.SaveChanges();
        //            if (result.Data > 0)
        //            {


        //                result.State = EnumStatus.Success;
        //                if (result.Data == 1)
        //                {
        //                    result.Message = Constants.MSG_SAVE_SUCCESS;
        //                }
        //                else
        //                {
        //                    result.Message = Constants.MSG_UPDATE_SUCCESS;
        //                }
        //            }
        //            else if (result.Data == -2)
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.HRSMasterID == 0)
        //                {
        //                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //                }
        //                else
        //                {
        //                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = "SaveData",
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message.ToString();

        //        }
        //        finally
        //        {
        //            _unitOfWork.Dispose();
        //        }
        //        return result;
        //    });
        //}





        [HttpPost("GetAllRoomSeatList")]
        public async Task<ApiResult<DataTable>> GetAllRoomSeatList([FromBody] HostelRoomSeatSearchModel body)
        {
            ActionName = "GetAllRoomSeatList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetAllRoomSeatList(body));
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


        [HttpGet("GetByHRSMasterID/{PK_ID:int}")]
        public async Task<ApiResult<HostelRoomSeatDataModel>> GetByHRSMasterID(int PK_ID)
        {
            ActionName = "GetByHRSMasterID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<HostelRoomSeatDataModel>();
                try
                {
                    var data = await _unitOfWork.HostelManagementRepository.GetByHRSMasterID(PK_ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<HostelRoomSeatDataModel>(data);
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


        [HttpPost("DeleteDataByHRSMasterID")]
        public async Task<ApiResult<int>> DeleteDataByHRSMasterID([FromBody] StatusChangeModel request)
        {
            ActionName = "DeleteDataByHRSMasterID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.DeleteDataByHRSMasterID(request);
                    _unitOfWork.SaveChanges();

                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Hostel is already in use. Deletion not allowed.";
                    }


                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
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


        [HttpPost("SaveFacilities")]
        public async Task<ApiResult<int>> SaveFacilities([FromBody] HostelFacilitiesDataModel request)
        {
            ActionName = "SaveFacilities([FromBody] HostelManagement request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.SaveFacilities(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.HFID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.HFID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("HostelFacilityList")]
        public async Task<ApiResult<DataTable>> HostelFacilityList([FromBody] HostelFacilitiesSearchModel body)
        {
            ActionName = "HostelFacilityList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.HostelFacilityList(body));
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

        [HttpGet("GetByHFID/{PK_ID:int}")]
        public async Task<ApiResult<DataTable>> GetByHFID(int PK_ID)
        {
            ActionName = "GetByHFID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.HostelManagementRepository.GetByHFID(PK_ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DataTable>(data);
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

        [HttpPost("DeleteDataByHFID")]
        public async Task<ApiResult<bool>> DeleteDataByHFID([FromBody] StatusChangeModel request)
        {
            ActionName = "DeleteDataByHFID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.DeleteDataByHFID(request);
                    _unitOfWork.SaveChanges();

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

        [HttpPost("StudentApplyHostel")]
        public async Task<ApiResult<int>> StudentApplyHostel([FromBody] StudentApplyHostelData request)
        {
            ActionName = "SaveRoomSeatData([FromBody] HostelManagement request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.StudentApplyHostel(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data >= 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 0)
                            result.Message = "Apply successfully .!";
                        else
                            result.Message = "Document Updated successfully .!";
                    }
                   
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StudentID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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



        [HttpPost("EditStudentApplyHostel")]
        public async Task<ApiResult<int>> EditStudentApplyHostel([FromBody] StudentApplyHostelData request)
        {
            ActionName = "EditStudentApplyHostel([FromBody] HostelManagement request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.EditStudentApplyHostel(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data >= 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 0)
                            result.Message = "Apply successfully .!";
                        else
                            result.Message = "Document Updated successfully .!";
                    }
                   
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StudentID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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
        [HttpPost("HostelWardenupdateData")]
        public async Task<ApiResult<int>> HostelWardenupdateData([FromBody] StudentApplyHostelData request)
        {
            ActionName = "HostelWardenupdateData([FromBody] HostelManagement request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.HostelManagementRepository.HostelWardenupdateData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data >= 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 0)
                            result.Message = "Apply successfully .!";
                        else
                            result.Message = "Document Updated successfully .!";
                    }
                   
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StudentID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("CollegeHostelDetailsList")]
        public async Task<ApiResult<DataSet>> CollegeHostelDetailsList([FromBody] CollegeHostelDetailsSearchModel body)
        {
            ActionName = "CollegeHostelDetailsList()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.CollegeHostelDetailsList(body));
                result.State = EnumStatus.Success;
                if (result.Data.Tables.Count == 0)
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

        [HttpPost("IsFacilitiesStatusByID")]
        public async Task<ApiResult<bool>> IsFacilitiesStatusByID([FromBody] StatusChangeModel request)
        {
            ActionName = "IsFacilitiesStatusByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    
                    result.Data = await _unitOfWork.HostelManagementRepository.IsFacilitiesStatusByID(request);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
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

        [HttpPost("GetLastFYEndTerm")]
        public async Task<ApiResult<DataSet>> GetLastFYEndTerm([FromBody] HostelStudentSearchModel request)
        {
            ActionName = "GetLastFYEndTerm()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await _unitOfWork.HostelManagementRepository.GetLastFYEndTerm(request);
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



        [HttpPost("GetAllotedHostelDetails")]
        public async Task<ApiResult<DataTable>> GetAllotedHostelDetails([FromBody] HostelStudentSearchModel body)
        {
            ActionName = "GetAllRoomSeatList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.HostelManagementRepository.GetAllotedHostelDetails(body));
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


