using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Infra.Repositories;
using Org.BouncyCastle.Utilities.Encoders;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.ITIIMCAllocation;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using Kaushal_Darpan.Models.ITIIIPManageDataModel;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using DinkToPdf;
using Kaushal_Darpan.Models.TheoryMarks;
using System.Text;
using Kaushal_Darpan.Models.ITIPlacementStudentMaster;
using iTextSharp.tool.xml.html;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidationActionFilter]
    public class ITIAllotmentController : BaseController
    {
        public override string PageName => "ITIAllotment";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;

        public ITIAllotmentController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter, IPrintHtmlFile printHtmlFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
            _printHtmlFile = printHtmlFile;

        }

        [HttpPost("GetGenerateAllotment")]
        public async Task<ApiResult<DataTable>> GetGenerateAllotment([FromBody] AllotmentdataModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetGenerateAllotment(body);

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

        [HttpPost("AllotmentCounter")]
        public async Task<ApiResult<DataTable>> AllotmentCounter([FromBody] SearchModel body)
        {
            ActionName = "AllotmentCounter()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIAllotmentRepository.AllotmentCounter(body);
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

                //Log the error
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


        [HttpPost("GetShowSeatMetrix")]
        public async Task<ApiResult<DataTable>> GetShowSeatMetrix([FromBody] SearchModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetShowSeatMetrix(body);

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


        [HttpPost("GetOptionDetailsbyID")]
        public async Task<ApiResult<List<OptionDetailsDataModel>>> GetOptionDetailsbyID(SearchModel request)
        {
            ActionName = "GetOptionDetailsbyID(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<OptionDetailsDataModel>>();
                try
                {
                    var data = await _unitOfWork.ITIAllotmentRepository.GetOptionDetailsbyID(request);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<List<OptionDetailsDataModel>>(data);
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

        [HttpPost("GetStudentSeatAllotment")]
        public async Task<ApiResult<DataTable>> GetStudentSeatAllotment([FromBody] SearchModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetStudentSeatAllotment(body);

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


        [HttpPost("GetAllotmentData")]
        public async Task<ApiResult<DataTable>> GetAllotmentData([FromBody] SearchModel body)
        {
            ActionName = "GetAllotmentData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetAllotmentData(body);

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

        [HttpPost("GetAllotmentStatusList")]
        public async Task<ApiResult<DataTable>> GetAllotmentStatusList([FromBody] AllotmentStatusSearchModel body)
        {
            ActionName = " GetAllotmentStatusList([FromBody] AllotmentStatusSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetAllotmentStatusList(body);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;

                    result.Data = new DataTable();
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

        [HttpPost("GetPublishAllotment")]
        public async Task<ApiResult<DataTable>> GetPublishAllotment([FromBody] AllotmentdataModel body)
        {
            ActionName = " GetPublishAllotment([FromBody] AllotmentStatusSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIAllotmentRepository.GetPublishAllotment(body);

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


        [HttpPost("GetAllotmentReport")]
        public async Task<ApiResult<DataTable>> GetAllotmentReport([FromBody] SearchModel body)
        {
            ActionName = " GetPublishAllotment([FromBody] AllotmentStatusSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                result.Data = await _unitOfWork.ITIAllotmentRepository.GetAllotmentReport(body);

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

        [HttpGet("GetAllotmentLetter/{AllotmentId}")]
        public async Task<ApiResult<string>> GetAllotmentLetter(string AllotmentId)
        {
            ActionName = "GetAllotmentLetter(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);

                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";
                        data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentPhoto"];
                        data.Tables[0].Rows[0]["StudentSign"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentSign"];

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/Allotmentletter.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        //html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(html);


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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

        [HttpGet("GetAllotmentReportingReceipt/{AllotmentId}")]
        public async Task<ApiResult<string>> GetAllotmentReportingReceipt(string AllotmentId)
        {
            ActionName = "GetAllotmentReportingReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);

                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/AllotmentReportingReceipt.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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

        [HttpGet("GetAllotmentFeeReceipt/{AllotmentId}")]
        public async Task<ApiResult<string>> GetAllotmentFeeReceipt(string AllotmentId)
        {
            ActionName = "GetAllotmentReportingReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);

                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/AllotmentFeeReceipt.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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

        [HttpGet("GetDirectAdmissionAllotmentLetter/{AllotmentId}")]
        public async Task<ApiResult<string>> GetDirectAdmissionAllotmentLetter(string AllotmentId)
        {
            ActionName = "GetAllotmentLetter(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);

                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";
                        data.Tables[0].Rows[0]["Principal_sign"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["Principal_sign"]}";
                        data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentPhoto"];
                        data.Tables[0].Rows[0]["StudentSign"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentSign"];


                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/DirectAdmissionAllotmentReceipt.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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

        #region Direct Admission

        [HttpGet("GetDirectAdmissionReceipt/{AllotmentId}")]
        public async Task<ApiResult<string>> GetAllotmentReceipt(string AllotmentId)
        {
            ActionName = "GetAllotmentReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetAllotmentReceipt(AllotmentId);

                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";
                        data.Tables[0].Rows[0]["Principal_sign"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["Principal_sign"]}";
                        data.Tables[0].Rows[0]["StudentPhoto"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentPhoto"];
                        data.Tables[0].Rows[0]["StudentSign"] = $"{ConfigurationHelper.StaticFileRootPath}/{data.Tables[0].Rows[0]["StudentPhotoFolder"]}/" + data.Tables[0].Rows[0]["StudentSign"];

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/DirectAdmission.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", watermarkImagePath, true);

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetAllData(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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
        }

        [HttpPost("StudentDetailsList")]
        public async Task<ApiResult<DataTable>> StudentDetailsList([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "StudentDetailsList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.StudentDetailsList(body));
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

        [HttpPost("GetAllDataPhoneVerify")]
        public async Task<ApiResult<DataTable>> GetAllDataPhoneVerify([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetAllDataPhoneVerify(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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
        }

        [HttpPost("GetStudentDetails")]
        public async Task<ApiResult<DataSet>> GetStudentDetails([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetStudentDetails(body));
                result.State = EnumStatus.Success;
                if (result.Data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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
        }



        [HttpPost("GetJailStudentDetails")]
        public async Task<ApiResult<DataSet>> GetJailStudentDetails([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetJailStudentDetails(body));
                result.State = EnumStatus.Success;
                if (result.Data.Tables.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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
        }



        [HttpPost("UpdateAllotments")]
        public async Task<ApiResult<int>> UpdateAllotments([FromBody] ITIDirectAllocationDataModel request)
        {
            ActionName = "UpdateAllotments([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIAllotmentRepository.UpdateAllotments(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
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
                        if (request.ApplicationID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
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



        [HttpPost("UpdateJailAllotments")]
        public async Task<ApiResult<int>> UpdateJailAllotments([FromBody] ITIDirectAllocationDataModel request)
        {
            ActionName = "UpdateAllotments([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIAllotmentRepository.UpdateJailAllotments(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
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
                        if (request.ApplicationID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
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





        [HttpPost("GetTradeListByCollege")]
        public async Task<ApiResult<DataTable>> GetTradeListByCollege([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetTradeListByCollege()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetTradeListByCollege(body));
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


        [HttpPost("GetStudentOptionByApplicationNo")]
        public async Task<ApiResult<DataTable>> GetStudentOptionByApplicationNo([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetTradeListByCollege()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetStudentOptionByApplicationNo(body));
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



        [HttpPost("ShiftUnitList")]
        public async Task<ApiResult<DataTable>> ShiftUnitList([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetTradeListByCollege()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.ShiftUnitList(body));
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

        [HttpPost("RevertAllotments")]

        public async Task<ApiResult<int>> RevertAllotments([FromBody] ITIDirectAllocationDataModel request)
        {
            ActionName = "RevertAllotments([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIAllotmentRepository.RevertAllotments(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data == 3)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Data Revert Successfull .!";

                    }
                    else if (result.Data > 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "There was an error updating data.!";

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



        [HttpPost("RevertJailAllotments")]

        public async Task<ApiResult<int>> RevertJailAllotments([FromBody] ITIDirectAllocationDataModel request)
        {
            ActionName = "RevertAllotments([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIAllotmentRepository.RevertJailAllotments(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data == 3)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Data Revert Successfull .!";

                    }
                    else if (result.Data > 0)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "There was an error updating data.!";

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


        [HttpPost("DownloadCollegeAllotmentData")]
        public async Task<ApiResult<string>> DownloadCollegeAllotmentData([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetAllotmentReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var resultData = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetSeatAllotmentData(body));

                    if (resultData != null)
                    {
                        DataSet data = new DataSet();
                        data.Tables.Add(resultData);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "StudentAllotment";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/CollegeAllotmentdata.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();


                        //html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4", "");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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


        [HttpPost("DownloadCollegeJailAllotmentData")]
        public async Task<ApiResult<string>> DownloadCollegeJailAllotmentData([FromBody] StudentsJoiningStatusMarksSearchModel body)
        {
            ActionName = "GetAllotmentReceipt(string AllotmentId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var resultData = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.DownloadCollegeJailAllotmentData(body));

                    if (resultData != null)
                    {
                        DataSet data = new DataSet();
                        data.Tables.Add(resultData);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "StudentAllotment";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/JailAllotmentData.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();


                        //html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4", "");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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





        [HttpPost("DownloadCollegeAdminData")]
        public async Task<ApiResult<string>> DownloadCollegeAdminData([FromBody] ReportCollegeForAdminModel body)
        {
            ActionName = "DownloadCollegeAdminData([FromBody] ReportCollegeForAdminModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var resultData = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetCollegeAdminData(body));

                    if (resultData != null)
                    {
                        DataSet data = new DataSet();
                        data.Tables.Add(resultData);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "StudentAllotment";

                        //data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        //data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/CollegeAllotmentAdminReport.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();


                        //html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4", "");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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



        [HttpPost("DownloadForCollegeDataOLD")]
        public async Task<ApiResult<string>> DownloadForCollegeDataOLD([FromBody] ReportCollegeModel body)
        {
            ActionName = "DownloadCollegeAdminData([FromBody] ReportCollegeForAdminModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var resultData = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetCollegeData(body));

                    if (resultData != null)
                    {
                        DataSet data = new DataSet();
                        data.Tables.Add(resultData);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "StudentAllotment";

                        //data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.jpeg";
                        //data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/CollegeAllotmentDetail.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();


                        //html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));
                        sb1.Append(html);

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4", "");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

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


        //tabluation umesh
        //        [HttpPost("DownloadForCollegeData")]
        //        public async Task<ApiResult<string>> DownloadForCollegeData([FromBody] ReportCollegeModel body)
        //        {
        //            ActionName = "TabulationDataReport([FromBody] TabluationDataModel body)";
        //            var result = new ApiResult<string>();
        //            try
        //            {
        //                // get all streams
        //                var streams_data = await Task.Run(() => _unitOfWork.StudentsJoiningStatusMarksRepository.GetCollegeData(body));

        //                if (streams_data?.Rows?.Count == 0)
        //                {
        //                    result.State = EnumStatus.Warning;
        //                    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //                    return result;
        //                }


        //                var data = new List<StudentAllotmentReportDataModel>();
        //                if (streams_data != null)
        //                {
        //                    data = CommonFuncationHelper.ConvertDataTable<List<StudentAllotmentReportDataModel>>(streams_data);
        //                }

        //                StringBuilder sb = new StringBuilder();

        //                // Add a style block at the top of your HTML
        //                sb.Append(@"
        //<style>
        //    body {
        //         font-family: Arial, sans-serif;
        //        font-size: 10pt; /* Global font size */
        //    }
        //    table {
        //        border-collapse: collapse;
        //        width: 100%;
        //    }
        //    th, td {
        //        border: 1px solid #494949;
        //        padding: 4px;
        //        font-size: 10pt; /* Cell font size */

        //    }
        //    b {
        //        font-size: 12pt; /* Trade name header */
        //    }
        //</style>
        //");

        //                foreach (var collegeGroup in data.GroupBy(f => f.CollegeId))
        //                {

        //                    sb.Append($@"
        //<table id='pdf-header' style='width:100%'>
        //    <tr>
        //        <td style='text-align:center'>
        //            {collegeGroup.FirstOrDefault()?.CollegeName}<br /><br />
        //            Reported Applicant List
        //        </td>
        //    </tr>
        //    <tr>
        //        <th colspan='3' style='border-bottom: 1px solid #494949; padding-top:1px;'>Total Applicant {data.Count}</th>
        //    </tr>
        //</table>");
        //                    int rowTradeC = 1;
        //                    foreach (var tradeGroup in collegeGroup.GroupBy(f => f.BranchName))
        //                    {

        //                        sb.Append($@"
        //<div style='margin-top:3px;'>&nbsp;</div>
        //<b>  {tradeGroup.Key}</b>

        //<table cellpadding='2' cellspacing='0'>
        //    <tr>
        //        <th>Sr No</th>
        //        <th>Application No</th>
        //        <th>Name</th>
        //        <th>Father Name</th>
        //        <th>Shift</th>
        //        <th>Unit</th>
        //        <th>Allotted Category</th>
        //        <th>Reported Date and Time</th>
        // <th>Admission Round</th>
        //    </tr>");

        //                        int rowNumber = 1;
        //                        foreach (var s in tradeGroup.OrderBy(f => f.Shift).ThenBy(f => f.UnitNo))
        //                        {
        //                           sb.Append($@"
        //                                <tr>
        //                                    <td>{rowNumber}</td>
        //                                    <td>{s.ApplicationNo}</td>
        //                                    <td>{s.Name}</td>
        //                                    <td>{s.FatherName}</td>
        //                                    <td>{s.Shift}</td>
        //                                    <td>{s.UnitNo}</td>
        //                                    <td>{s.AllotedCategory}</td>
        //                                    <td>{s.ReportingDate}</td>
        //                                    <td>{s.AdmissionRound}</td>
        //                                </tr>");
        //                             rowNumber++;
        //                        }
        //                        sb.Append("</table>");
        //                        rowTradeC++;
        //                    }
        //                }
        //                var doc = new HtmlToPdfDocument()
        //                {
        //                    GlobalSettings = {
        //                   PaperSize = PaperKind.A4,
        //                   Orientation = Orientation.Portrait, // ✅ spelling fixed

        //                    },
        //                    Objects = {
        //        new ObjectSettings()
        //        {
        //            HtmlContent = sb.ToString(),
        //            WebSettings = { DefaultEncoding = "utf-8" },
        //            FooterSettings = new FooterSettings
        //            {
        //                FontName = "Arial",
        //                FontSize = 9,
        //                Right = "Page [page] of [toPage]",
        //                Left = "Printed on: [date]",
        //                Line = true // Adds a line above footer
        //            }
        //        }
        //    }
        //                };
        //                byte[] pdfBytes = _converter.Convert(doc);

        //                result.Data = Convert.ToBase64String(pdfBytes);
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //                //return File(pdfBytes, "application/pdf", "tabulationresult.pdf");
        //            }
        //            catch (System.Exception ex)
        //            {
        //                await _unitOfWork.DisposeAsync();
        //                result.State = EnumStatus.Error;
        //                result.Message = Constants.MSG_ERROR_OCCURRED;
        //                result.ErrorMessage = ex.Message;
        //                // write error log
        //                var nex = new NewException
        //                {
        //                    PageName = PageName,
        //                    ActionName = ActionName,
        //                    Ex = ex,
        //                };
        //                await CreateErrorLog(nex, _unitOfWork);
        //                //return StatusCode(500, ex.Message);
        //            }
        //            return result;
        //        }



        [HttpPost("DownloadForCollegeData")]
        public async Task<IActionResult> DownloadForCollegeData([FromBody] ReportCollegeModel body)
        {
            try
            {
                var streams_data = await Task.Run(() =>
                    _unitOfWork.StudentsJoiningStatusMarksRepository.GetCollegeData(body));

                if (streams_data == null || streams_data.Rows.Count == 0)
                {
                    return BadRequest("No data found");
                }

                var data = CommonFuncationHelper
                            .ConvertDataTable<List<StudentAllotmentReportDataModel>>(streams_data);

                var sb = new StringBuilder();

                // HTML CONTENT
                sb.Append(@"<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<title>SCVT Annual Examination July 2025</title>
 
<style>

    body {

        font-family: ""Times New Roman"", serif;

        margin: 30px;

        color: #000;

    }
 
    
 
    .header {

        text-align: center;

        line-height: 1.4;

        font-size: 14px;

    }
 
    .header h2 {

        margin: 5px 0;

        font-size: 16px;

        font-weight: bold;

    }
 
    .row {
    display: flex;
    justify-content: space-between;
    font-size: 13px;
}
    h3 {

        text-align: center;

        margin: 15px 0;
.

        font-size: 15px;

        text-decoration: underline;

    }
 
    table {

        width: 100%;

        border-collapse: collapse;

        font-size: 13px;

    }
 
    th, td {

        border: 1px solid #000;

        padding: 6px;

        text-align: center;

        vertical-align: middle;

    }
 
    th {

        font-weight: bold;

    }
 
    @media print {

        body {

            margin: 0;

        }

        .page {

            margin: 15mm;

        }

    }
.top-row {
    display: flex;
    justify-content: space-between;
    font-size: 13px;
}
</style>
</head>
 
<body>
 
<div class=""page"">
 
    <div class=""header"">
<div>राजस्थान सरकार</div>
<div>कौशल, नियोजन एवं उद्यमिता विभाग</div>
<div>राजस्थान व्यावसायिक शिक्षा एवं प्रशिक्षण परिषद, जोधपुर</div>
<div>E-Mail: rcvtexam.raj@gmail.com</div>
</div>
<hr>
<div class=""row"">
<span>क्रमांक : प.श.9(5)/प-5/अनुसूची/परीक्षा/2025/77503</span>
<span style=""margin-right: 95px;"">दिनांक : 25/6/25</span>
</div>
 
    <h3>

        PROGRAMME FOR SCVT ANNUAL SYSTEM EXAMINATION JULY, 2025<br>

        (MAIN EXAMINATION)
</h3>
 
    <table>
<tr>
<th>Date / Day</th>
<th>Time of Commencement</th>
<th>Year</th>
<th>Annual System<br>All Trades of One Year and Two Year Duration</th>
</tr>
 
        <tr>
<td rowspan=""2"">28.07.2025<br>(MONDAY)</td>
<td>10.00 AM</td>
<td>I</td>
<td>Paper-I (Trade Theory) – All Trades</td>
</tr>
<tr>
<td>02.30 PM</td>
<td>II</td>
<td>Paper-I (Trade Theory) – All Trades</td>
</tr>
 
        <tr>
<td rowspan=""2"">29.07.2025<br>(TUESDAY)</td>
<td>10.00 AM</td>
<td>I</td>
<td>Paper-II (Employability Skills) – All Trades</td>
</tr>
<tr>
<td>02.30 PM</td>
<td>II</td>
<td>Paper-II (Employability Skills) – All Trades</td>
</tr>
 
        <tr>
<td rowspan=""2"">30.07.2025<br>(WEDNESDAY)</td>
<td>10.00 AM</td>
<td>I</td>
<td>Paper-III (Workshop Calculation & Science) – All Trades</td>
</tr>
<tr>
<td>02.30 PM</td>
<td>II</td>
<td>Paper-III (Workshop Calculation & Science) – All Trades</td>
</tr>
 
        <tr>
<td rowspan=""2"">31.07.2025<br>(THURSDAY)</td>
<td>10.00 AM</td>
<td>I</td>
<td>Paper-IV (Engineering Drawing) – All Trades</td>
</tr>
<tr>
<td>02.30 PM</td>
<td>II</td>
<td>Paper-IV (Engineering Drawing) – All Trades</td>
</tr>
 
        <tr>
<td>01.08.2025<br>(FRIDAY)</td>
<td>09.30 AM</td>
<td>I</td>
<td>Trade Practical – All Trades</td>
</tr>
 
        <tr>
<td>02.08.2025<br>(SATURDAY)</td>
<td>09.30 AM</td>
<td>II</td>
<td>Trade Practical – All Trades</td>
</tr>
</table>
 
</div>
 
</body>
</html>

 ");

                

                sb.Append(@"</tbody></table></body></html>");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                
                return File(
                    pdfBytes,
                    "application/pdf",
                    "College_Report.pdf"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        //IIPManageReport

        [HttpPost("StudentSeatWithdrawRequest")]
        public async Task<ApiResult<int>> StudentSeatWithdrawRequest([FromBody] StudentthdranSeatModel request)
        {
            ActionName = "StudentSeatWithdrawRequest([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIAllotmentRepository.StudentSeatWithdrawRequest(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ApplicationID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
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


        [HttpPost("GetJailAllData")]
        public async Task<ApiResult<DataTable>> GetJailAllData([FromBody] ITIDirectAllocationSearchModel body)
        {
            ActionName = "GetAllDataPhoneVerify()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIAllotmentRepository.GetJailAllData(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";
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
        }

        // Download IIP manage Report
        [HttpPost("downloadIIPManageReportPDF")]
        public async Task<ApiResult<string>> downloadIIPManageReportPDF([FromBody] ITIIIPManageDataModel body)
        {

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.StudentsJoiningStatusMarksRepository.downloadIIPManageReportPDF(body);

                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        //data.Tables[0].TableName = "IMCReg_Details";
                        data.Tables[0].TableName = "ITI_IIP_IMCFund";
                        //data.Tables[0].TableName = "ITI_IIP_IMCFund";

                        //data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        //data.Tables[1].TableName = "IMC_Members";
                        //data.Tables[2].TableName = "IMC_FundDetails";
                        //data.Tables[3].TableName = "IMC_QuaterProgressDetails";
                        //data.Tables[1].TableName = "ITI_IIP_IMCFund";

                        string devFontSize = "12px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/GetIIPQuaterlyFundReportdata.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //System.IO.File.WriteAllText("debug.html", html);
                        sb1.Append(html);


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
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
