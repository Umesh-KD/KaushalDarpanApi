using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.TimeTable;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class ITITimeTableController : BaseController
    {
        public override string PageName => "ITITimeTableController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITITimeTableController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITITimeTableSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITITimeTableRepository.GetAllData(model);
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
        public async Task<ApiResult<int>> SaveData([FromBody] ITITimeTableModel request)
        {
            ActionName = "SaveData([FromBody] CenterMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}

                    result.Data = await _unitOfWork.ITITimeTableRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.TimeTableID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.TimeTableID == 0)
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

        [HttpGet("GetByID/{ID}")]
        public async Task<ApiResult<ITITimeTableModel>> GetByID(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITITimeTableModel>();
                try
                {
                    var data = await _unitOfWork.ITITimeTableRepository.GetById(ID);
                    result.Data = data;
                    if (data != null)
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


        [HttpDelete("DeleteDataByID/{PK_ID}/{ModifyBy}/{DepartmentID}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy, int DepartmentID)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new ITITimeTableModel
                    {
                        TimeTableID = PK_ID,
                        ModifyBy = ModifyBy,
                        DepartmentID = DepartmentID
                    };
                    result.Data = await _unitOfWork.ITITimeTableRepository.DeleteDataByID(DeleteData_Request);
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

        [HttpGet("GetTimeTableByID/{ID}")]
        public async Task<ApiResult<List<TradeSubjectDataModel>>> GetTimeTableByID(int ID)
        {
            ActionName = "GetTimeTableByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<TradeSubjectDataModel>>();
                try
                {
                    var data = await _unitOfWork.ITITimeTableRepository.GetTimeTableByID(ID);
                    result.Data = data;
                    if (data != null)
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

        [HttpPost("SaveInvigilator")]
        public async Task<ApiResult<int>> SaveInvigilator([FromBody] ITI_TimeTableInvigilatorModel request)
        {
            ActionName = "SaveInvigilator([FromBody] TimeTableModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.ITITimeTableRepository.SaveInvigilator(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.TimeTableID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.TimeTableID == 0)
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

        [HttpGet("GetInvigilatorByID/{ID}/{InstituteID}")]
        public async Task<ApiResult<ITI_TimeTableInvigilatorModel>> GetInvigilatorByID(int ID, int InstituteID)
        {
            ActionName = "GetInvigilatorByID(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITI_TimeTableInvigilatorModel>();
                try
                {
                    var data = await _unitOfWork.ITITimeTableRepository.GetInvigilatorByID(ID, InstituteID);
                    result.Data = data;
                    if (data != null)
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

        [HttpPost("GetTimeTableTradeSubject")]
        public async Task<ApiResult<DataTable>> GetTimeTableTradeSubject([FromBody] NewITI_TimeTableValidateModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITITimeTableRepository.GetTimeTableTradeSubject(model);
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

        [HttpPost("GetSampleTimeTableITI")]
        public async Task<ApiResult<DataTable>> GetSampleTimeTable([FromBody] ITITimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITITimeTableRepository.GetSampleTimeTableITI(body));
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

        [HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<ITITimeTableModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<ITITimeTableModel>>();

            try
            {
                //  Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                //  Read the Excel file
                using (var stream = model.file.OpenReadStream())
                {
                    // Prepare StringWriter for logging or debugging purposes (Optional)
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StringWriter swSQL = new StringWriter(sb);

                    // Register CodePagesEncodingProvider for reading older Excel formats
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // Read Excel file into DataSet
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Treat first row as headers
                            }
                        });

                        // Get the first sheet as DataTable
                        DataTable dt = ds.Tables[0];

                        //  Convert DataTable to your specific model list
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<ITITimeTableModel>>(dt);
                        var data = await _unitOfWork.ITITimeTableRepository.ImportExcelFile(dataTime);

                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
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
        }

        [HttpPost("SaveImportExcelData")]
        public async Task<ApiResult<bool>> SaveImportExcelData([FromBody] List<ITITimeTableModel> request)
        {
            ActionName = "SaveImportExcelData([FromBody] List<TimeTableModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITITimeTableRepository.SaveImportExcelData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_ADD_ERROR;
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

        [HttpPost("SampleCBTImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<ITICBTCenterModel>>> CBTImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "CBTImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<ITICBTCenterModel>>();

            try
            {
                //  Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                //  Read the Excel file
                using (var stream = model.file.OpenReadStream())
                {
                    // Prepare StringWriter for logging or debugging purposes (Optional)
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StringWriter swSQL = new StringWriter(sb);

                    // Register CodePagesEncodingProvider for reading older Excel formats
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // Read Excel file into DataSet
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Treat first row as headers
                            }
                        });

                        // Get the first sheet as DataTable
                        DataTable dt = ds.Tables[0];

                        //  Convert DataTable to your specific model list
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<ITICBTCenterModel>>(dt);
                        var data = await _unitOfWork.ITITimeTableRepository.CBTImportExcelFile(dataTime);

                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
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
        }

        [HttpPost("SaveCBTImportExcelData")]
        public async Task<ApiResult<bool>> SaveCBTImportExcelData([FromBody] List<ITICBTCenterModel> request)
        {
            ActionName = "SaveCBTImportExcelData([FromBody] List<ITICBTCenterModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    var isSave = await _unitOfWork.ITITimeTableRepository.SaveCBTImportExcelData(request);
                    _unitOfWork.SaveChanges(); 

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_ADD_ERROR;
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

        [HttpPost("GetAllCBTData")]
        public async Task<ApiResult<DataTable>> GetAllCBTData([FromBody] ITICBTCenterModel model)
        {
            ActionName = "GetAllCBTData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITITimeTableRepository.GetAllCBTData(model);
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


        [HttpPost("GetSampleCBTCenterITI")]
        public async Task<ApiResult<DataTable>> GetSampleCBTCenterITI([FromBody] ITITimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITITimeTableRepository.GetSampleCBTCenterITI(body));
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


