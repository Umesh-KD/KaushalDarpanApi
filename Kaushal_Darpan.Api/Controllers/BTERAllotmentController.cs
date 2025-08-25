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
using Kaushal_Darpan.Models.Allotment;
using AspNetCore.Reporting;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BTERAllotmentController : BaseController
    {
        public override string PageName => "BTERAllotment";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BTERAllotmentController(IMapper  mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetGenerateAllotment")]
        public async Task<ApiResult<DataTable>> GetGenerateAllotment([FromBody] BTERAllotmentdataModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetGenerateAllotment(body);

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
        
        [HttpPost("GetPublishAllotment")]
        public async Task<ApiResult<DataTable>> GetPublishAllotment([FromBody] BTERAllotmentdataModel body)
        {
            ActionName = "GetPublishAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetPublishAllotment(body);

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
        public async Task<ApiResult<DataTable>> AllotmentCounter([FromBody] BTERSearchModelCounter body)
        {
            ActionName = "AllotmentCounter()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BTERAllotmentRepository.AllotmentCounter(body);
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
        public async Task<ApiResult<DataTable>> GetShowSeatMetrix([FromBody] BTERSearchModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetShowSeatMetrix(body);

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
        public async Task<ApiResult<List<OptionDetailsDataModel>>> GetOptionDetailsbyID(BTERSearchModel request)
        {
            ActionName = "GetOptionDetailsbyID(int ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<OptionDetailsDataModel>>();
                try
                {
                    var data = await _unitOfWork.BTERAllotmentRepository.GetOptionDetailsbyID(request);
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
        public async Task<ApiResult<DataTable>> GetStudentSeatAllotment([FromBody] BTERSearchModel body)
        {
            ActionName = "GetGenerateAllotment()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetStudentSeatAllotment(body);

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
        public async Task<ApiResult<DataTable>> GetAllotmentData([FromBody] BTERAllotmentModel body)
        {
            ActionName = "GetAllotmentData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetAllotmentData(body);

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
        public async Task<ApiResult<DataTable>> GetAllotmentReport([FromBody] BTERAllotmentModel body)
        {
            ActionName = "GetAllotmentReport()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.GetAllotmentReport(body);

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


        [HttpPost("UploadAllotmentData")]
        public async Task<ApiResult<DataTable>> UploadAllotmentData([FromBody] BterUploadAllotmentDataModel body)
        {
            ActionName = "UploadAllotmentData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.UploadAllotmentData(body);
                _unitOfWork.SaveChanges();
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

        [HttpPost("AllotmnetFormateData")]
        public async Task<ApiResult<DataTable>> AllotmnetFormateData([FromBody] BterUploadAllotmentDataModel body)
        {
            ActionName = "AllotmnetFormateData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTERAllotmentRepository.AllotmnetFormateData(body);
                _unitOfWork.SaveChanges();
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

        [HttpGet("GetStudentAllotmentReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentAllotmentReceipt(int ApplicationID)
        {
            ActionName = "GetStudentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAllotmentReceipt(ApplicationID);
                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/CEGlogo.png";                       
                        
                        string devFontSize = "16px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/AllotmentLetter.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);                      
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"),true,devFontSize));                       

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", "");

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


        [HttpGet("GetStudentReportingReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentReportingReceipt(int ApplicationID)
        {
            ActionName = "GetStudentReportingReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAllotmentReceipt(ApplicationID);
                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllotmentData";

                        data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/CEGlogo.png";

                        string devFontSize = "16px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/ReportingReceipt.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", "");

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

        [HttpGet("GetStudentAllotmentFeeReceipt/{ApplicationID}")]
        public async Task<ApiResult<string>> GetStudentAllotmentFeeReceipt(int ApplicationID)
        {
            ActionName = "GetStudentAllotmentFeeReceipt(string EnrollmentNo)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ReportRepository.GetStudentAllotmentReceipt(ApplicationID);
                    if (data != null)
                    {
                        string EnrollmentNo = data.Tables[0].Rows[0]["AllotmentId"].ToString();
                        var feeData = await _unitOfWork.ReportRepository.GetStudentAllotmentFeeReceipt(EnrollmentNo);

                        if (feeData.Tables.Count > 0 && feeData.Tables[0].Rows.Count > 0)
                        {
                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                            data.Tables[0].TableName = "AllotmentData";

                            data.Tables[0].Rows[0]["logo_left"] = $"{ConfigurationHelper.StaticFileRootPath}/CEGlogo.png";

                            data.Tables[0].Rows[0]["ReceiptNo"] = feeData.Tables[0].Rows[0]["ReceiptNo"];
                            data.Tables[0].Rows[0]["Transactionld"] = feeData.Tables[0].Rows[0]["Transactionld"];
                            data.Tables[0].Rows[0]["TransactionDate"] = feeData.Tables[0].Rows[0]["TransactionDate"];
                            data.Tables[0].Rows[0]["TransactionDate"] = feeData.Tables[0].Rows[0]["TransactionDate"];

                            string devFontSize = "16px";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();

                            string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderBTER}/AllotmentFeeReceipt.html";

                            string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                            html = Utility.PDFWorks.ReplaceCustomTag(html);
                            sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                            //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                            byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", "");

                            result.Data = Convert.ToBase64String(pdfBytes); ;
                            result.State = EnumStatus.Success;
                            result.Message = "Success";
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
