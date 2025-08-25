using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.HostelManagement;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITISeatsDistributionsMaster;
using Kaushal_Darpan.Models.TimeTable;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class TimeTableController : BaseController
    {
        public override string PageName => "TimeTableController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DBContext _dbContext;
        private string _sqlQuery;
        public TimeTableController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] TimeTableSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TimeTableRepository.GetAllData(model);
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

        [HttpPost("GetSampleTimeTable")]
        public async Task<ApiResult<DataTable>> GetSampleTimeTable([FromBody] TimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.TimeTableRepository.GetSampleTimeTable(body));
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



        [HttpPost("GetTimeTableBranchSubject")]
        public async Task<ApiResult<DataTable>> GetTimeTableBranchSubject([FromBody] TimeTableValidateModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TimeTableRepository.GetTimeTableBranchSubject(model);
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
        public async Task<ApiResult<int>> SaveData([FromBody] TimeTableModel request)
        {
            ActionName = "SaveData([FromBody] CenterMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }

                    result.Data = await _unitOfWork.TimeTableRepository.SaveData(request);
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
        public async Task<ApiResult<TimeTableModel>> GetByID(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<TimeTableModel>();
                try
                {
                    var data = await _unitOfWork.TimeTableRepository.GetById(ID);
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
                    var DeleteData_Request = new TimeTableModel
                    {
                        TimeTableID = PK_ID,
                        ModifyBy = ModifyBy,
                        DepartmentID = DepartmentID
                    };
                    result.Data = await _unitOfWork.TimeTableRepository.DeleteDataByID(DeleteData_Request);
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
        public async Task<ApiResult<List<BranchSubjectDataModel>>> GetTimeTableByID(int ID)
        {
            ActionName = "GetTimeTableByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<BranchSubjectDataModel>>();
                try
                {
                    var data = await _unitOfWork.TimeTableRepository.GetTimeTableByID(ID);
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
        public async Task<ApiResult<int>> SaveInvigilator([FromBody] TimeTableInvigilatorModel request)
        {
            ActionName = "SaveInvigilator([FromBody] TimeTableModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    result.Data = await _unitOfWork.TimeTableRepository.SaveInvigilator(request);
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
        public async Task<ApiResult<TimeTableInvigilatorModel>> GetInvigilatorByID(int ID, int InstituteID)
        {
            ActionName = "GetInvigilatorByID(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<TimeTableInvigilatorModel>();
                try
                {
                    var data = await _unitOfWork.TimeTableRepository.GetInvigilatorByID(ID, InstituteID);
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



        [HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<TimeTableModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<TimeTableModel>>();

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
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<TimeTableModel>>(dt);
                        var data = await _unitOfWork.TimeTableRepository.ImportExcelFile(dataTime);

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
        public async Task<ApiResult<bool>> SaveImportExcelData([FromBody] List<TimeTableModel> request)
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
                    var isSave = await _unitOfWork.TimeTableRepository.SaveImportExcelData(request);
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

        //[HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        //public async Task<ApiResult<List<TimeTableModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        //{
        //    ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
        //    var result = new ApiResult<List<TimeTableModel>>();
        //    try
        //    {
        //        // Step 1: Validate file presence
        //        if (model.file == null || model.file.Length == 0)
        //        {
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
        //            return result;
        //        }

        //        // Step 2: Read the Excel file
        //        using (var stream = model.file.OpenReadStream())
        //        {
        //            // Prepare StringWriter for logging or debugging purposes (Optional)
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            StringWriter swSQL = new StringWriter(sb);

        //            // Register CodePagesEncodingProvider for reading older Excel formats
        //            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //            // Read Excel file into DataSet
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                    {
        //                        UseHeaderRow = true // Treat first row as headers
        //                    }
        //                });

        //                // Get the first sheet as DataTable
        //                DataTable dt = ds.Tables[0];

        //                // Step 3: Convert DataTable to your specific model list
        //                var data = new List<TimeTableModel>();
        //                data = CommonFuncationHelper.ConvertDataTable<List<TimeTableModel>>(dt);

        //                // Step 4: Map the converted data to the result model
        //                var uploadFileDataModels = new List<UploadFileWithPathDataModel>();

        //                // Step 5: Set result with the uploaded file data
        //                result.Data = data;
        //                //result.State = EnumStatus.Success;
        //                //result.Message = Constants.MSG_SAVE_SUCCESS;


        //                TimeTableModel dataTable = new TimeTableModel();
        //                using (var command = _dbContext.CreateCommand())
        //                {
        //                    command.CommandType = CommandType.StoredProcedure;
        //                    command.CommandText = "USP_TimeTable_GetById";
        //                    command.Parameters.AddWithValue("@TimeTableID", ID);
        //                    command.Parameters.AddWithValue("@action", "_getTimeTableByID");

        //                    _sqlQuery = command.GetSqlExecutableQuery();
        //                    dataTable = await command.FillAsync_DataTable();
        //                }
        //                var detaildata = new List<TimeTableModel>();
        //                if (dataTable != null)
        //                {
        //                    detaildata = CommonFuncationHelper.ConvertDataTable<List<TimeTableModel>>(dataTable);
        //                }
        //                return data;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.State = EnumStatus.Error;
        //        result.Message = Constants.MSG_ERROR_OCCURRED;
        //        result.ErrorMessage = ex.Message;

        //        // Log error
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }
        //    finally
        //    {
        //        // Dispose resources
        //        _unitOfWork?.Dispose();
        //    }

        //    return result;
        //}



        [HttpPost("SaveTimeTableWorkflow")]
        public async Task<ApiResult<bool>> SaveTimeTableWorkflow([FromBody] List<VerifyTimeTableList> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<VerifyRollNumberList> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.TimeTableRepository.SaveTimeTableWorkflow(request);
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

                        bool allStatus14 = request.All(x => x.Status == 14);
                        if (allStatus14)
                        {
                            var isNewsPublish = await _unitOfWork.TimeTableRepository.TimeTable_News_IU(request);
                            _unitOfWork.SaveChanges();
                        }
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
        [HttpPost("VerificationTimeTableList")]
        public async Task<ApiResult<DataTable>> VerificationTimeTableList([FromBody] TimeTableSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.TimeTableRepository.VerificationTimeTableList(model);
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

    }
}


