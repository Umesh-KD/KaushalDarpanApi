using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.PaperSetter;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidationActionFilter]
    public class MarksheetDownloadController : BaseController
    {
        public override string PageName => "MarksheetDownloadController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;

        public MarksheetDownloadController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter, IPrintHtmlFile printHtmlFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
        }

        [HttpPost("GetStudents")]
        public async Task<ApiResult<DataTable>> GetStudents([FromBody] MarksheetDownloadSearchModel body)
        {
            ActionName = "GetStudents([FromBody] MarksheetDownloadSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudents(body));
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

        [HttpPost("MarksheetLetterDownload")]
        public async Task<ApiResult<string>> MarksheetLetterDownload([FromBody] MarksheetDownloadSearchModel model)
        {
            ActionName = "MarksheetLetterDownload([FromBody] MarksheetDownloadSearchModel model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.MarksheetDownloadRepository.MarksheetLetterDownload(model);
                    if (data != null)
                    {
                        var fileName = $"MarksheetLetter_{data.Tables[0].Rows[0]["InstituteCode"]}_{data.Tables[0].Rows[0]["SemesterCode"]}_{data.Tables[0].Rows[0]["EndTermName"]}_{System.DateTime.Now:MMMddyyyyhhmmssffffff}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/MarksheetLetter.rdlc";


                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("MarksheetLetterDetails", data.Tables[0]);
                        localReport.AddDataSource("MarksheetLetterTableDetails", data.Tables[1]);
                        localReport.AddDataSource("MarksheetLetterAdditionalDetails", data.Tables[2]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        result.Data = fileName;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("Get5thSemBackPaperReport")]
        public async Task<ApiResult<DataTable>> Get5thSemBackPaperReport([FromBody] BackPaperReportDataModel body)
        {
            ActionName = "Get5thSemBackPaperReport([FromBody] BackPaperReportDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.Get5thSemBackPaperReport(body));
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

        [HttpPost("GetStudentResult_public")]
        public async Task<ApiResult<DataSet>> GetStudentResult_public(StudentResultSearchModel model)
        {
            ActionName = "GetStudentResult_public(StudentResultSearchModel model)";
            var result = new ApiResult<DataSet>();
            try
            {
                // handle to show result or not
                var ValidateModel = new ValidateOrStudentsWithMsgRequestModel
                {
                    SemesterID = model.SemesterID,
                    EndTermID = model.EndTermID,
                    RollNo = model.RollNo,
                    DOB = model.DOB,
                    ResultTypeID = model.ResultType
                };
                var validateResult = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetValidateOrStudentsWithMsg(ValidateModel));
                if (validateResult.ValidateStatus != 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = validateResult.Msg;
                    return result;
                }

                result.Data = new DataSet();
                // different type of result
                if (model.ResultType == (int)EnumResultType.MainResult)
                {
                    result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResult_public(model));
                }
                else if (model.ResultType == (int)EnumResultType.RwhResult || model.ResultType == (int)EnumResultType.RwhRevalEffected)
                {
                    result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResultRWH_public(model));
                }
                else if (model.ResultType == (int)EnumResultType.RevaluationResult)
                {
                    result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResultReval_public(model));
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_INVALID_REQUEST;
                    return result;
                }
                // check data found or not
                if (result.Data.Tables.Count < 3 || result.Data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                //success
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
            }
            catch (System.Exception ex)
            {
                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;

                // write error log
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

        [HttpPost("GetResultEndTermDDLList")]
        public async Task<ApiResult<DataTable>> GetResultEndTermDDLList()
        {
            ActionName = "GetResultEndTermDDLList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetResultEndTermDDLList());
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

        #region Theory Marks Report BTER
        [HttpPost("DownloadStudentResult_Public")]
        public async Task<ApiResult<string>> DownloadStudentResult_Public(StudentResultSearchModel model)
        {
            ActionName = "DownloadStudentResult_Public(StudentResultSearchModel model)";
            var result = new ApiResult<string>();
            try
            {
                // handle to show result or not
                var ValidateModel = new ValidateOrStudentsWithMsgRequestModel
                {
                    SemesterID = model.SemesterID,
                    EndTermID = model.EndTermID,
                    RollNo = model.RollNo,
                    DOB = model.DOB,
                    ResultTypeID = model.ResultType
                };
                var validateResult = await Task.Run(() => _unitOfWork.CommonFunctionRepository.GetValidateOrStudentsWithMsg(ValidateModel));
                if (validateResult.ValidateStatus != 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = validateResult.Msg;
                    return result;
                }

                var data = new DataSet();
                // different type of result
                if (model.ResultType == (int)EnumResultType.MainResult)
                {
                    data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResult_public(model));
                }
                else if (model.ResultType == (int)EnumResultType.RwhResult || model.ResultType == (int)EnumResultType.RwhRevalEffected)
                {
                    data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResultRWH_public(model));
                }
                else if (model.ResultType == (int)EnumResultType.RevaluationResult)
                {
                    data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudentResultReval_public(model));
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_INVALID_REQUEST;
                    return result;
                }
                // check data found or not
                if (data.Tables.Count < 3 || data.Tables[0].Rows.Count == 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }

                var sb = await _printHtmlFile.StudentResult_Public_GetHtml(data , (int)model.ResultType);
                var _html = sb.ToString();

                // remove last blank page
                string endTag = "<div class='page-break'></div></body></html>";
                if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                {
                    _html = _html.Substring(0, _html.Length - endTag.Length)
                                 + "</body></html>";
                }

                // pdf document setting
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        PaperSize = PaperKind.A4,
                        Orientation = Orientation.Portrait,
                        Margins = new MarginSettings
                        {
                            Top = 10,
                            Bottom = 10,
                            Left = 5,
                            Right = 5
                        }
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = _html,
                            WebSettings = { DefaultEncoding = "utf-8" },

                            FooterSettings = new FooterSettings
                            {
                                FontName = "Arial",
                                FontSize = 7,
                                Center = "Page [page] of [toPage]",
                                Line = true
                            }
                        }
                    }
                };

                // return
                byte[] pdfBytes = await Task.Run(() => _converter.Convert(doc));

                result.Data = Convert.ToBase64String(pdfBytes);
                result.State = EnumStatus.Success;
                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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
                result.Message = Constants.MSG_ERROR_OCCURRED;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
