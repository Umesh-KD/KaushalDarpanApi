using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
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
    public class CounsellingImportCandidateListController : BaseController
    {
        public override string PageName => "CounsellingImportCandidateListController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CounsellingImportCandidateListController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetSampleExcelFile")]
        public async Task<ApiResult<DataTable>> GetSampleExcelFile()
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CounsellingImportCandidateListRepository.GetSampleExcelFile());
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

        [HttpPost("GetCandidateList")]
        public async Task<ApiResult<DataTable>> GetCandidateList([FromBody] CounsellingAllotmentListModel body)
        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingImportCandidateListRepository.GetCandidateList(body);

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



        [HttpPost("SampleImportExcelFile"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<CounsellingImportExcelModel>>> ImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<CounsellingImportExcelModel>>();

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
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<CounsellingImportExcelModel>>(dt);
                        var data = await _unitOfWork.CounsellingImportCandidateListRepository.ImportExcelFile(dataTime);

                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
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
        }

        [HttpPost("SaveImportExcelData")]
        public async Task<ApiResult<bool>> SaveImportExcelData([FromBody] List<CounsellingImportExcelModel> request)
        {
            ActionName = "SaveImportExcelData([FromBody] List<TimeTableModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //request.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    //});
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.CounsellingImportCandidateListRepository.SaveImportExcelData(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

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
                    await _unitOfWork.DisposeAsync();
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

        [HttpPost("EditCandidateExcelDataById")]
        public async Task<ApiResult<int>> EditCandidateExcelDataById([FromBody] CounsellingImportExcelModel request)
        {
            ActionName = "EditCandidateExcelDataById([FromBody] CounsellingImportExcelModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var isSave = await _unitOfWork.CounsellingImportCandidateListRepository.EditCandidateExcelDataById(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_ADD_ERROR;
                    }
                    else if (isSave > 0)
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
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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


