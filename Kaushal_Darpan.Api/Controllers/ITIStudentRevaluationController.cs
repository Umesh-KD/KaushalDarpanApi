using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Dynamic;
using static iTextSharp.tool.xml.html.table.TableRowElement;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[ValidationActionFilter]
    public class ITIStudentRevaluationController : BaseController
    {
        public override string PageName => "ITIStudentRevaluationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ITIStudentRevaluationController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetStudentRevaluationDetails")]
        public async Task<ApiResult<DataTable>> GetStudentRevaluationDetails([FromBody] ITIStudentRevaluationDataModel body)
        {
            ActionName = "GetStudentRevaluationDetails()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetStudentRevaluationDetails(body));
               
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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

        [HttpPost("GetAllStudentRevaluation")]
        public async Task<ApiResult<DataTable>> GetAllStudentRevaluation([FromBody] StudentDetailsByRollNoModel body)
        {
            ActionName = "GetExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetAllStudentRevaluation(body));
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




        #region iti student reval request details 

        [HttpPost("GetAllRevalRequestDetails")]
        public async Task<ApiResult<DataTable>> GetAllRevalRequestDetails([FromBody] ITIRevalRequestStudentDetailsModel body)
        {
            ActionName = "GetStudentRevaluationDetails()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetAllRevalRequestDetails(body));

                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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


        [HttpPost("SaveRVLPaymentData")]
        public async Task<ApiResult<DataTable>> SaveRVLPaymentData([FromBody] RVLStudentDetailsModel body)
        {
            ActionName = "SaveRVLPaymentData()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIStudentRevaluationRepository.SaveRVLPaymentData(body);

                result.State = EnumStatus.Success;

                if (result.Data == null || result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found!";
                }
                else
                {
                    var firstRow = result.Data.Rows[0];

                    var state = firstRow["State"]?.ToString();
                    var message = firstRow["Message"]?.ToString();

                    if (string.Equals(state, "Error", StringComparison.OrdinalIgnoreCase))
                    {
                        result.State = EnumStatus.Error;
                        result.Message = message ?? "Failed to save data.";
                    }
                    else
                    {
                        result.State = EnumStatus.Success;
                        result.Message = message ?? "Student data saved successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }

        [HttpPost("GetRVLDetailByStudentApplicationNo")]
        public async Task<ApiResult<DataTable>> GetRVLDetailByStudentApplicationNo([FromBody] RVLStudentRevalRequestModel body)
        {
            ActionName = "GetRVLDetailByStudentApplicationNo()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIStudentRevaluationRepository.GetRVLDetailByStudentApplicationNo(body));

                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Error;
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

        //[HttpPost("UploadDocument")]
        //public async Task<ApiResult<bool>> UploadDocument([FromBody]  )
        //{
        //    ActionName = "UploadDocument([FromBody] CompanyMasterModels request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {

        //            if (!ModelState.IsValid)
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Validation failed!";
        //                return result;
        //            }


        //            result.Data = await _unitOfWork.CompanyMasterRepository.SaveData(request);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.ID == 0)
        //                {
        //                    result.Message = Constants.MSG_SAVE_SUCCESS;
        //                }
        //                else
        //                {
        //                    result.Message = Constants.MSG_UPDATE_SUCCESS;
        //                }
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.ID == 0)
        //                {
        //                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //                }
        //                else
        //                {
        //                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
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

        //[HttpPost]
        //[Route("UploadDocument")]
        //public async Task<IActionResult> UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel model)
        //{
        //    try
        //    {
        //        // Example: Save to DB or validate
        //        foreach (var item in model.StudentOptionList)
        //        {
        //            // Access item.UploadedCopy, item.SubjectCode etc.
        //            // Save or process each file reference
        //        }

        //        return Ok(new
        //        {
        //            State = true,
        //            Message = "Documents uploaded successfully.",
        //            ErrorMessage = ""
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            State = false,
        //            Message = "Failed to upload documents.",
        //            ErrorMessage = ex.Message
        //        });
        //    }
        //}


        [HttpPost("UploadDocument")]
        public async Task<ApiResult<bool>> UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel request)
        {
            ActionName = "UploadDocument([FromBody] ITIRevalRequestStudentDetailsModel request)";
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


                    result.Data = await _unitOfWork.ITIStudentRevaluationRepository.UploadDocument(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                        //if (request.ID == 0)
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
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                        //if (request.ID == 0)
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

        #endregion

        #region

        [HttpPost("UpdateEnrollResponseBulkExcel"), DisableRequestSizeLimit]
        public async Task<ApiResult<bool>> ImportExcelFile([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile([FromForm] UploadFileModel model)";
            var result = new ApiResult<bool>();
          
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
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<UpdateEnrollResponseBulkExcelModel>>(dt);
                        
                        var SelectedData = dt.AsEnumerable().Select(row => new UpdateEnrollResponseBulkExcelModel
                        {
                            StateRegNumber = row["State Reg Number"]?.ToString()
                        }).ToList();

                        int totalrows = SelectedData.Count;
                        int chunksize = model.ChunkSize.HasValue ? model.ChunkSize.Value : 100;
                        int processed = 0;
                        bool allChunksSucceeded = true;
                        while (processed < totalrows)
                        {
                            var chunk = SelectedData.Skip(processed).Take(chunksize).ToList();
                            result.Data = await _unitOfWork.ITIStudentRevaluationRepository.ImportExcelFile(chunk);
                            await _unitOfWork.SaveChangesAsync();
                            if (result.Data)
                            {
                                processed += chunk.Count;
                            }
                            else
                            {
                                allChunksSucceeded = false;
                                break;
                            }
                        }
                        if (allChunksSucceeded)
                        {
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = Constants.MSG_UPDATE_ERROR;

                        }

                  
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


        #endregion

        #region dynamic update data through Excel

        [HttpPost("DynamicUpdateExcelData/{ActionType}"), DisableRequestSizeLimit]
        public async Task<ApiResult<bool>> DynamicUpdateExcelData([FromForm] UploadFileModel model , string ActionType)
        {
            ActionName = "DynamicUpdateExcelData([FromForm] UploadFileModel model)";
            var result = new ApiResult<bool>();

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

                        // CHANGED: Added safety validation to prevent null reference errors if workbook is empty
                        if (ds == null || ds.Tables.Count == 0)
                        {
                            result.State = EnumStatus.Error;
                            result.Message = "No worksheets found in the Excel file.";
                            return result;
                        }

                        DataTable dt = ds.Tables[0];

                        // MODIFIED: Trim actual DataTable column names in-place so that
                        // headers with trailing/leading spaces (e.g. "Name  ") match
                        // correctly when accessed later via row[colName]. Previously only
                        // the columnNames list was trimmed, but dt's real columns still had
                        // spaces, which would throw "Column 'Name' does not belong to table".
                        foreach (DataColumn col in dt.Columns)
                        {
                            string trimmedName = col.ColumnName.Trim();
                            if (col.ColumnName != trimmedName)
                            {
                                // Avoid collision if a trimmed name already exists (e.g. "Name" and "Name " both present)
                                if (!dt.Columns.Contains(trimmedName))
                                {
                                    col.ColumnName = trimmedName;
                                }
                            }
                        }
                        



                        var columnNames = dt.Columns.Cast<DataColumn>()
                                            .Select(c => c.ColumnName.Trim())
                                            .ToList();
                      
                        var dynamicDataList = dt.AsEnumerable().Select(row =>
                        {
                            var newRow = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                            //foreach (var colName in columnNames)
                            //{

                            //    newRow[colName] = row[colName] == DBNull.Value ? null : row[colName];
                            //}

                            foreach (var colName in columnNames)
                            {
                                var cellValue = row[colName];

                                if (cellValue == DBNull.Value)
                                {
                                    newRow[colName] = null;
                                }
                                else if (cellValue is double d)
                                {
                                    // If it’s actually an integer (like 1.0 → 1)
                                    if (d % 1 == 0)
                                        newRow[colName] = Convert.ToInt64(d); // or int if range safe
                                    else
                                        newRow[colName] = d; // keep decimal values like 10.5
                                }
                                else
                                {
                                    newRow[colName] = cellValue;
                                }
                            }
                            return newRow;
                        }).ToList();
                       
                        int totalrows = dynamicDataList.Count;
                        int chunksize =model.ChunkSize.HasValue ? model.ChunkSize.Value : 100;
                        int processed = 0;
                        bool allChunksSucceeded = true;
                        int intRowCounter = 1;
                        while (processed < totalrows)
                        {
                            // FIXED: Optimized list chunking using GetRange instead of Skip().Take() (O(1) vs O(N))
                            int currentChunkSize = Math.Min(chunksize, totalrows - processed);
                            var chunk = dynamicDataList.GetRange(processed, currentChunkSize);

                            //var chunk = dynamicDataList.Skip(processed).Take(chunksize).ToList();
                            result.Data = await _unitOfWork.ITIStudentRevaluationRepository.DynamicUpdateExcelData(chunk, ActionType, intRowCounter);
                            await _unitOfWork.SaveChangesAsync();
                            intRowCounter++;
                            if (result.Data)
                            {
                                processed += currentChunkSize;
                                //processed += chunk.Count;
                            }
                            else
                            {
                                allChunksSucceeded = false;
                                break;
                            }
                        }

                        if (allChunksSucceeded)
                        {
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = Constants.MSG_UPDATE_ERROR;

                        }

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


        #endregion
    }
}
