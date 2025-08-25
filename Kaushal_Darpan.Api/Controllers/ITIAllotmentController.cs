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

        public ITIAllotmentController(IMapper  mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.Dispose();
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
                    _unitOfWork.SaveChanges();
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
                    _unitOfWork.SaveChanges();
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
                    _unitOfWork.Dispose();
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


    }
}
