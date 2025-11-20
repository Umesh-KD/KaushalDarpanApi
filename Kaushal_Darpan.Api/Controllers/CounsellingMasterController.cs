using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.EMMA;
using ExcelDataReader;
using iTextSharp.tool.xml.html;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]

    public class CounsellingMasterController : BaseController
    {
        public override string PageName => "CounsellingMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;

        public CounsellingMasterController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter, IPrintHtmlFile printHtmlFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
        }

        [HttpPost("SavePersonalData")]
        public async Task<ApiResult<int>> SaveData([FromBody] ApplicationDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
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
                        if (request.ApplicationID == 0)
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



        //[HttpPost("MapCandidateSSO")]
        //public async Task<ApiResult<DataTable>> MapCandidateSSO([FromBody] CounsellingApplicationSearchModel body)
        //{
        //    ActionName = "MapCandidateSSO()";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        // Pass the entire model to the repository
        //        result.Data = await _unitOfWork.CounsellingApplicationFormRepository.MapCandidateSSO(body);
        //        if (result.Data.Rows.Count > 0)
        //        {
        //            result.State = EnumStatus.Success;
        //            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //        }
        //        else
        //        {
        //            result.State = EnumStatus.Warning;
        //            result.Message = Constants.MSG_DATA_NOT_FOUND;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //        // Log the error
        //        await _unitOfWork.DisposeAsync();
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }
        //    return result;
        //}


        //[HttpPost("UpdateStudentSsoMapping")]
        //public async Task<ApiResult<int>> UpdateCandidateSsoMapping([FromBody] CounsellingApplicationSearchModel model)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {

        //            var data = await _unitOfWork.CounsellingApplicationFormRepository.UpdateStudentSsoMapping(model);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (data > 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Data = data;
        //                result.Message = "Student Mapped Successfully";

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Something went wrong";
        //                result.Data = data;
        //            }
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            await _unitOfWork.DisposeAsync();
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



        [HttpPost("GetCounsellingAllotmentList")]
        public async Task<ApiResult<DataTable>> GetCounsellingAllotmentList([FromBody] CounsellingAllotmentListModel body)
        
        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetCounsellingAllotmentList(body);

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


        [HttpPost("GetCandidateList")]
        public async Task<ApiResult<DataTable>> GetCandidateList([FromBody] CounsellingAllotmentListModel body)

        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetCandidateList(body);

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

        [HttpPost("SaveCandidateAllotment_Counselling/{TradeID}")]
        public async Task<ApiResult<int>> SaveCandidateAllotment_Counselling([FromRoute] int TradeID, [FromBody] List<Counselling_AllotmentDataModel> request)
        {
            ActionName = "Task<ApiResult<int>> SaveCandidateAllotment_Counselling([FromRoute] int TradeID, [FromBody] List<Counselling_AllotmentDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var isSave = await _unitOfWork.CounsellingMasterRepository.SaveCandidateAllotment_Counselling(TradeID, request);
                    await _unitOfWork.SaveChangesAsync(); 

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

        [HttpPost("GetAllottedCandidateList_Counselling")]
        public async Task<ApiResult<DataTable>> GetAllottedCandidateList_Counselling([FromBody] CounsellingAllottedListSearchModel body)

        {
            ActionName = "GetAllottedCandidateList_Counselling([FromBody] CounsellingAllottedListSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetAllottedCandidateList_Counselling(body);

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
        [HttpPost("GetAllottedCandidateList_CounsellingReport")]
        public async Task<ApiResult<DataTable>> GetAllottedCandidateList_CounsellingReport([FromBody] CounsellingAllottedListSearchModel body)

        {
            ActionName = "GetAllottedCandidateList_CounsellingReport([FromBody] CounsellingAllottedListSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetAllottedCandidateList_CounsellingReport(body);

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
        [HttpPost("SaveFinalInstituteAllotment_Counselling")]
        public async Task<ApiResult<bool>> SaveFinalInstituteAllotment_Counselling(EditInstituteDataModel_Counselling model)
        {
            ActionName = "SaveFinalInstituteAllotment_Counselling(EditInstituteDataModel_Counselling model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingMasterRepository.SaveFinalInstituteAllotment_Counselling(model);
                    await _unitOfWork.SaveChangesAsync();

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

        //[HttpPost("GenerateAllotmentOrder_Counselling")]
        //public async Task<ApiResult<string>> GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> model)
        //{
        //    ActionName = "GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> model)";
        //    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            var data = await _unitOfWork.CounsellingMasterRepository.GenerateAllotmentOrder_Counselling(model);
        //            if (data != null)
        //            {
        //                string guid = Guid.NewGuid().ToString().ToUpper();
        //                var fileName = $"CounsellingAllotmentOrder_{guid}.pdf";
        //                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/CounsellingAllotmentOrder.rdlc";

        //                model.ForEach(x =>
        //                {
        //                    x.AllotmentOrderPath = filepath;
        //                    x.AllotmentOrder = fileName;
        //                });

        //                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                LocalReport localReport = new LocalReport(rdlcpath);

        //                localReport.AddDataSource("CounsellingAllotmentOrderTable", data.Tables[0]);
        //                var reportResult = localReport.Execute(RenderType.Pdf);

        //                //check file exists
        //                if (!System.IO.Directory.Exists(folderPath))
        //                {
        //                    Directory.CreateDirectory(folderPath);
        //                }

        //                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

        //                //result.Data = fileName;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }

        //            //var Issuccess = await _unitOfWork.CenterObserverRepository.UpdateDutyOrder(model);
        //            //if (Issuccess > 0)
        //            //{
        //            //    result.Data = Issuccess.ToString();
        //            //    result.State = EnumStatus.Success;
        //            //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            //}
        //            //else
        //            //{
        //            //    result.State = EnumStatus.Warning;
        //            //    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            //}

        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            //
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}

        [HttpPost("GenerateAllotmentOrder_Counselling")]
        public async Task<ApiResult<string>> GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await Task.Run(() => _unitOfWork.CounsellingMasterRepository.GenerateAllotmentOrder_Counselling(body));

                    if (data?.Tables?.Count == 1)
                    {

                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        string guid = Guid.NewGuid().ToString().ToUpper();
                        var fileName = $"CounsellingAllotmentOrder_{guid}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITI_CounsellingAllotmentOrder.rdlc";

                        body.ForEach(x =>
                        {
                            x.AllotmentOrderPath = filepath;
                            x.AllotmentOrder = fileName;
                        });

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("ITI_CounsellingAllotmentOrderTable", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        if (result.State == EnumStatus.Success)
                        {
                            foreach (var item in body)
                            {
                                item.AllotmentOrder = result.Data;
                            }
                            var updateData = new ApiResult<bool>();

                            updateData.Data = await _unitOfWork.CounsellingMasterRepository.UpdateAllotmentOrder_Counselling(body);
                            await _unitOfWork.SaveChangesAsync();
                            if (updateData.Data)
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }

                return result;
            });
        }

        ////tabluation umesh
        //[HttpPost("TabulationDataReport")]
        //public async Task<ApiResult<string>> TabulationDataReport([FromBody] TabluationDataModel body)
        //{
        //    ActionName = "TabulationDataReport([FromBody] TabluationDataModel body)";
        //    var result = new ApiResult<string>();
        //    try
        //    {
        //        // get all streams
        //        var streams_data = await _unitOfWork.ReportRepository.GetStreamResultRptTabulation(body);

        //        if (streams_data?.Rows?.Count == 0)
        //        {
        //            result.State = EnumStatus.Warning;
        //            result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            return result;
        //        }

        //        StringBuilder sb = new StringBuilder();

        //        // start
        //        sb.AppendLine("<!DOCTYPE html>");
        //        sb.AppendLine("<html lang=\"en\">");
        //        sb.AppendLine("<head>");
        //        sb.AppendLine("    <meta charset=\"UTF-8\">");
        //        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        //        sb.AppendLine("    <title>Tabulation Register</title>");
        //        sb.AppendLine("</head>");
        //        sb.AppendLine("<body>");
        //        sb.AppendLine("    <div style=\"width: 98%; margin: auto;\">");

                
        //        // get consolidate summary
        //        var consolidate_data = await _unitOfWork.ReportRepository.GetConsolidatedDetailsResultRptTabulation(body);
        //        if (consolidate_data?.Rows.Count > 0)
        //        {
        //            //get html
        //            var _sb1 = _printHtmlFile.GetHtmlOfConsolidateForTabulation(consolidate_data);
        //            sb.AppendJoin("</br>", _sb1);
        //        }

        //        // end
        //        sb.AppendLine("    </div>");
        //        sb.AppendLine("</body>");
        //        sb.AppendLine("</html>");

        //        var doc = new HtmlToPdfDocument()
        //        {
        //            GlobalSettings = {
        //                PaperSize = PaperKind.A3,
        //                Orientation = Orientation.Landscape
        //            },
        //            Objects = {
        //                new ObjectSettings()
        //                {
        //                    HtmlContent = sb.ToString(),
        //                    WebSettings = { DefaultEncoding = "utf-8" }
        //                }
        //            }
        //        };

        //        byte[] pdfBytes = _converter.Convert(doc);

        //        result.Data = Convert.ToBase64String(pdfBytes);
        //        result.State = EnumStatus.Success;
        //        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //        //return File(pdfBytes, "application/pdf", "tabulationresult.pdf");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        await _unitOfWork.DisposeAsync();
        //        result.State = EnumStatus.Error;
        //        result.Message = Constants.MSG_ERROR_OCCURRED;
        //        result.ErrorMessage = ex.Message;
        //        // write error log
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //        //return StatusCode(500, ex.Message);
        //    }
        //    return result;
        //}

        [HttpGet("GetSampleExcelFile_CounsellingVacant")]
        public async Task<ApiResult<DataTable>> GetSampleExcelFile_CounsellingVacant()
        {
            ActionName = "GetSampleExcelFile_CounsellingVacant()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CounsellingMasterRepository.GetSampleExcelFile_CounsellingVacant());
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

        [HttpPost("ImportExcelFile_CounsellingVacant"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<ImportCounsellingVacancyDataModel>>> ImportExcelFile_CounsellingVacant([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile_CounsellingVacant([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<ImportCounsellingVacancyDataModel>>();

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
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<ImportCounsellingVacancyDataModel>>(dt);
                        var data = await _unitOfWork.CounsellingMasterRepository.ImportExcelFile_CounsellingVacant(dataTime);

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

        [HttpPost("SaveExcelData_CounsellingVacant")]
        public async Task<ApiResult<bool>> SaveExcelData_CounsellingVacant([FromBody] List<ImportCounsellingVacancyDataModel> request)
        {
            ActionName = "SaveExcelData_CounsellingVacant([FromBody] List<ImportCounsellingVacancyDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.CounsellingMasterRepository.SaveExcelData_CounsellingVacant(request);
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

        [HttpPost("GetCounsellingVacancyData")]
        public async Task<ApiResult<DataTable>> GetCounsellingVacancyData([FromBody] CounsellingVacancySearchModel body)
        {
            ActionName = "GetCounsellingVacancyData([FromBody] CounsellingVacancySearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetCounsellingVacancyData(body);

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

        [HttpPost("EditVacancyData_Counselling")]
        public async Task<ApiResult<bool>> EditVacancyData_Counselling([FromBody] EditVacancyDataModel request)
        {
            ActionName = "EditVacancyData_Counselling([FromBody] EditVacancyDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var isSave = await _unitOfWork.CounsellingMasterRepository.EditVacancyData_Counselling(request);
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

        [HttpGet("GetVacancyDetailsById_Counselling/{TradeInstituteID}")]
        public async Task<ApiResult<EditVacancyDataModel>> GetVacancyDetailsById_Counselling(int TradeInstituteID)
        {
            ActionName = " EditVacancyData_Counselling(int TradeInstituteID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<EditVacancyDataModel>();
                try
                {
                    var data = await _unitOfWork.CounsellingMasterRepository.GetVacancyDetailsById_Counselling(TradeInstituteID);
                    if (data != null)
                    {
                        result.Data = data;
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
    }
}
