using AutoMapper;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Code.PlaywrightPdf;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CertificateDownload;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class CertificateController : BaseController
    {
        public override string PageName => "Certificate";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;
        private readonly IPlaywrightPdfService _pdfService;

        public CertificateController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter, IPrintHtmlFile printHtmlFile, IPlaywrightPdfService pdfService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
            _pdfService = pdfService;
        }

        [HttpPost("GetAllMigrationCertificateData")]
        public async Task<ApiResult<DataTable>> GetAllMigrationCertificateData([FromBody] CertificateSearchModel body)
        {
            ActionName = "GetAllMigrationCertificateData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CertificateRepository.GetAllMigrationCertificateData(body);

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

        [HttpPost("GetAllProvisionalCertificateData")]
        public async Task<ApiResult<DataTable>> GetAllProvisionalCertificateData([FromBody] CertificateSearchModel body)
        {
            ActionName = "GetAllMigrationCertificateData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CertificateRepository.GetAllProvisionalCertificateData(body);

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

        [HttpPost("DownloadDiplomaForwardingLetter")]
        public async Task<ApiResult<string>> DownloadDiplomaForwardingLetter([FromBody] DiplomaCertificateModel model)
        {
            ActionName = "DownloadDiplomaForwardingLetter([FromBody] DiplomaCertificateModel model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.CertificateRepository.DownloadDiplomaForwardingLetter(model);

                    if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                        return result;
                    }

                    var sb = await _printHtmlFile.DiplomaForwardingLetterHtml(data);
                    var _html = sb.ToString();

                    // remove last blank page
                    string endTag = "<div class='page-break'></div></body></html>";
                    if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                    {
                        _html = _html.Substring(0, _html.Length - endTag.Length)
                                     + "</body></html>";
                    }

                    var pdfBytes = await _pdfService.GenerateAsync(_html,
                                            new PdfOptions
                                            {
                                                Format = "A4",
                                                MarginTop = "5mm",
                                                MarginBottom = "5mm",
                                                MarginLeft = "5mm",
                                                MarginRight = "5mm",
                                                PrintBackground = true,
                                                DisplayHeaderFooter = true,
                                                //PrintFooterPageNo = true
                                            });

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
            });
        }

        [HttpPost("DownloadDiplomaPassedStudentRegisterReport")]
        public async Task<ApiResult<string>> DownloadDiplomaPassedStudentRegisterReport([FromBody] DiplomaCertificateModel model)
        {
            ActionName = "DownloadDiplomaForwardingLetter([FromBody] DiplomaCertificateModel model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.CertificateRepository.DownloadDiplomaPassedStudentRegisterReport(model);

                    if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                        return result;
                    }

                    var sb = await _printHtmlFile.DiplomaPassedStudentRegisterReportHtml(data);
                    var _html = sb.ToString();

                    // remove last blank page
                    string endTag = "<div class='page-break'></div></body></html>";
                    if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                    {
                        _html = _html.Substring(0, _html.Length - endTag.Length)
                                     + "</body></html>";
                    }

                    var pdfBytes = await _pdfService.GenerateAsync(_html,
                                            new PdfOptions
                                            {
                                                Format = "A4",
                                                MarginTop = "5mm",
                                                MarginBottom = "5mm",
                                                MarginLeft = "5mm",
                                                MarginRight = "5mm",
                                                PrintBackground = true,
                                                DisplayHeaderFooter = true,
                                                //PrintFooterPageNo = true
                                            });

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
            });
        }

        [HttpPost("DownloadPendingDiplomaCertificateReport")]
        public async Task<ApiResult<string>> DownloadPendingDiplomaCertificateReport([FromBody] DiplomaCertificateModel model)
        {
            ActionName = "DownloadDiplomaForwardingLetter([FromBody] DiplomaCertificateModel model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.CertificateRepository.DownloadPendingDiplomaCertificateReport(model);

                    if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                        return result;
                    }

                    var sb = await _printHtmlFile.PendingDiplomaCertificateReportHtml(data);
                    var _html = sb.ToString();

                    // remove last blank page
                    string endTag = "<div class='page-break'></div></body></html>";
                    if (_html.EndsWith(endTag, StringComparison.OrdinalIgnoreCase))
                    {
                        _html = _html.Substring(0, _html.Length - endTag.Length)
                                     + "</body></html>";
                    }

                    var pdfBytes = await _pdfService.GenerateAsync(_html,
                                            new PdfOptions
                                            {
                                                Format = "A4",
                                                MarginTop = "5mm",
                                                MarginBottom = "5mm",
                                                MarginLeft = "5mm",
                                                MarginRight = "5mm",
                                                PrintBackground = true,
                                                DisplayHeaderFooter = true,
                                                PrintFooterPageNo = true
                                            });

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
            });
        }
    }
}
