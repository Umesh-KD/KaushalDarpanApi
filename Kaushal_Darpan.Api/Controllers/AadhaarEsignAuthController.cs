using AspNetCore.Reporting;
using AutoMapper;
using Azure.Core;
using DocumentFormat.OpenXml.Office2016.Excel;
using Kaushal_Darpan.Api.Validators;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using static System.Net.WebRequestMethods;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AadhaarEsignAuthController : BaseController
    {
        public override string PageName => "AadhaarEsignAuthController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AadhaarEsignAuthController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetEsignAuthData")]
        public async Task<ApiResult<string>> GetEsignAuthData(EsignAuthRequestModel esignAuth)
        {
            ActionName = "GetEsignAuthData(EsignAuthRequestModel esignAuth)";
            var result = new ApiResult<string>();
            try
            {
                result.Data = await Task.Run(() => AadhaarEsignAuthValidator.GetEsignAuthData(esignAuth));
                result.State = EnumStatus.Success;
                result.Message = result.Data.Contains("NO#") ? "Error" : "Success";
                return result;
            }
            catch (System.Exception ex)
            {
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
        }
        [HttpGet("GetDecryptedResponseData")]
        public async Task<ApiResult<string>> GetDecryptedResponseData(string encryptedData)
        {
            ActionName = "GetDecryptedResponseData(string encryptedData)";
            var result = new ApiResult<string>();
            try
            {
                var data = await Task.Run(() => AadhaarEsignAesEncryption.Decrypt(encryptedData, Constants.AADHAAR_ESIGN_AUTH_ENC_KEY));
                result.Data = data.DecriptedData;
                result.State = data.IsSuccess ? EnumStatus.Success : EnumStatus.Error;
                result.Message = data.IsSuccess ? "Success" : "Error";
                return result;
            }
            catch (System.Exception ex)
            {
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
        }

        [HttpPost("GetSignedXML")]
        public async Task<ApiResult<GetSignedXMLResponseModel>> GetSignedXML(GetSignedXMLModel request)
        {
            ActionName = "GetSignedXML(GetSignedXMLModel request)";
            var result = new ApiResult<GetSignedXMLResponseModel>();
            try
            {
                //service
                request.SignatureOnPageNumber = "0";//0=all
                request.Xcord = "400";
                request.Ycord = "30";
                request.ResponseUrl = $"{ConfigurationHelper.BaseURL}AadhaarEsignAuth/CallBackToGetEsignData";
                request.Sigsize = "medium";//small/medium/large
                var responseData = AadharEsignServiceBus.GetSignedXML(request);
                //
                if (responseData == null)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                else
                {
                    var data = JsonConvert.DeserializeObject<GetSignedXMLResponseModel>(responseData);
                    //save
                    var saveData = new EsignDataHistoryRequestModel
                    {
                        ApiType = "GetSignedXML",
                        Response = responseData,
                        Txn = data?.txn,
                        ModifyBy = request.ModifyBy,
                        UserNameInAadhar = request.UserNameInAadhar
                    };
                    var retval = await _unitOfWork.AadharEsignRepository.SaveEsignDataHistory(saveData);
                    _unitOfWork.SaveChanges();

                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                return result;
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;

                result.ErrorMessage = ex.Message;
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                return result;
            }
        }

        [HttpPost("GetSignedPDF")]
        public async Task<ApiResult<GetSignedPDFResponseModel>> GetSignedPDF(GetSignedPDFModel request)
        {
            ActionName = "GetSignedPDF(GetSignedPDFModel request)";
            var result = new ApiResult<GetSignedPDFResponseModel>();
            try
            {
                var esignDataReqObj = new EsignDataHistoryRequestModel
                {
                    Txn = request.Txn,
                    ApiType = "esignData"
                };
                //get
                var esignDataFromDB = await _unitOfWork.AadharEsignRepository.GetEsignDataHistory(esignDataReqObj);
                var esignData = esignDataFromDB.Response;

                //get
                var getXmlReqObj = new EsignDataHistoryRequestModel
                {
                    Txn = request.Txn,
                    ApiType = "GetSignedXML"
                };
                var dataFromDB = await _unitOfWork.AadharEsignRepository.GetEsignDataHistory(getXmlReqObj);
                
                //service
                request.Txn = request.Txn;
                request.esignData = esignData;
                request.UserNameInAadhar = dataFromDB.UserNameInAadhar;
                var responseData = AadharEsignServiceBus.GetSignedPDF(request);//call
                //
                if (responseData == null)
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    return result;
                }
                else
                {
                    var data = JsonConvert.DeserializeObject<GetSignedPDFResponseModel>(responseData);//response
                    if (data?.responseCode == "REA_001" && !string.IsNullOrEmpty(data?.signedPDFUrl))
                    {
                        string fileUrl = data.signedPDFUrl;
                        string filePath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.EsignedPdfFolder}";
                        if (!Directory.Exists(filePath))//check folder
                            Directory.CreateDirectory(filePath);
                        var fileName = $"signed_{data.txn}.pdf";
                        string savePath = Path.Combine(filePath, fileName);
                        await CommonFuncationHelper.DownloadAndSaveFileInFolderAsync(fileUrl, savePath);//download from url and save

                        //save
                        var saveData = new EsignDataHistoryRequestModel
                        {
                            ApiType = "GetSignedPDF",
                            Response = responseData,
                            Txn = data?.txn,
                            ModifyBy = request.ModifyBy,
                            SignedPDFUrl = data.signedPDFUrl
                        };
                        var objDbUpdate = await _unitOfWork.AadharEsignRepository.SaveEsignDataHistory(saveData);
                        _unitOfWork.SaveChanges();

                        //response
                        data.fileName = $"{Constants.EsignedPdfFolder}/{fileName}";
                    }
                    result.Data = data;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                    return result;
                }
            }
            catch (System.Exception ex)
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
        }

        [HttpPost("CallBackToGetEsignData")]
        public async Task<IActionResult> CallBackToGetEsignData()
        {
            ActionName = "CallBackToGetEsignData()";
            string appUrl = string.Empty;
            try
            {
                var esignData = Request.Form["esignData"];
                var txn = string.Empty;
                try
                {
                    System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(esignData);
                    txn = doc.Root?.Attribute("txn")?.Value; // Get root element and read the 'txn' attribute
                }
                catch { }

                //save
                var saveData = new EsignDataHistoryRequestModel
                {
                    ApiType = "esignData",
                    Response = esignData,
                    Txn = txn
                };
                var objDbUpdate = await _unitOfWork.AadharEsignRepository.SaveEsignDataHistory(saveData);
                _unitOfWork.SaveChanges();

                // cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    SameSite = SameSiteMode.None, // Important for cross-site cookies
                    Secure = true,                // Required when SameSite=None
                    HttpOnly = false,              // Optional: set to false to access via JS

                };
                Response.Cookies.Append("kd-esign", txn, cookieOptions);

                // success url
                //appUrl = $"{ConfigurationHelper.ApplicationURL}/aadhar-esign?txn={txn}";
                appUrl = $"{ConfigurationHelper.ApplicationURL}/aadhar-esign";
                return Redirect(appUrl);
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            // failed url
            appUrl = $"{ConfigurationHelper.ApplicationURL}/aadhar-esign";
            return Redirect(appUrl);
        }

    }
}
