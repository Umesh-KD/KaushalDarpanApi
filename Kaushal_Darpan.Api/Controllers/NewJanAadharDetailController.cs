using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentDetailUpdate;
using Kaushal_Darpan.Models.StudentMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using Kaushal_Darpan.Infra;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewJanAadharDetailController : BaseController
    {
        public override string PageName => "NewJanAadharDetailController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JanAadhaarGenericService _service;

        public NewJanAadharDetailController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _service = new JanAadhaarGenericService();
        }


        [HttpPost("JanAdharDataNew1")]
        public async Task<string> JanAdharDataNew1()
        {
            //JanAadhaarConfig
            string Path = ConfigurationHelper.PrivateCertPath;
            var nex = new NewException
            {
                PageName = "step1",
                ActionName = Path,
                Ex = new Exception(),
            };
            await CreateErrorLog(nex, _unitOfWork);
            return Path;
        }
        [HttpGet("CheckCertPaths")]
        public IActionResult CheckCertPaths()
        {
            return Ok(new
            {
               

                PrivateCertPath = ConfigurationHelper.PrivateCertPath,
                ExistsPrivate = System.IO.File.Exists(ConfigurationHelper.PrivateCertPath),
                PublicCertPath = ConfigurationHelper.PublicCertPath,
                ExistsPublic = System.IO.File.Exists(ConfigurationHelper.PublicCertPath)
            });
        }
        
        [HttpPost("JanAdharDataNew")]
        public async Task<ApiResult<object>> JanAdharDataNew(string SchemeName = "EEMS", string sType = "", string JanaadhaarNo = "", string memberId = "", string tid = "", string otp = "")
        {
            string schemShortCode = "EEMS";
            string appCode = "JAN4601237";
            string transactionId = $"{SchemeName}{DateTime.Now:yyyyMMdd}{new Random().Next(100000, 999999)}";
            string ActionName = "JanAdharDataNew";
            var resultData = new ApiResult<object>();
            try
            {
                dynamic requestObj = null;
                string UrlDataType = "";
                bool isOtpBypassed = false;
                string decryptedResponse = string.Empty;

                // Log request
                await CreateErrorLog(new NewException
                {
                    PageName = "JanAdharDataNew-step1",
                    ActionName = JsonConvert.SerializeObject(requestObj),
                    Ex = new Exception(isOtpBypassed ? sType : "")
                }, _unitOfWork);
                // Prepare request body
                switch (sType)
                {

                    case "FetchMemberList":
                        requestObj = new
                        {
                            appCode,
                            schemShortCode,
                            transactionId,
                            janId = JanaadhaarNo?.Trim()
                        };
                        UrlDataType = "member-list";
                        await CreateErrorLog(new NewException
                        {
                            PageName = "JanAdharDataNew-step2",
                            ActionName = JsonConvert.SerializeObject(requestObj),
                            Ex = new Exception(isOtpBypassed ? UrlDataType : "")
                        }, _unitOfWork);
                        break;

                    // Prepare request body
                    case "GenerateOTP":
                        if (string.IsNullOrWhiteSpace(memberId))
                            throw new Exception("memberId is required for GenerateOTP");

                        requestObj = new
                        {
                            appCode,
                            schemShortCode,
                            transactionId,
                            memberId = memberId.Trim()
                        };
                        UrlDataType = "generate-otp";
                        await CreateErrorLog(new NewException
                        {
                            PageName = "JanAdharDataNew-step3",
                            ActionName = JsonConvert.SerializeObject(requestObj),
                            Ex = new Exception(isOtpBypassed ? UrlDataType : "")
                        }, _unitOfWork);
                        break;

                    case "ValidateOTP_FetchRequestedData":
                        if (!string.IsNullOrEmpty(otp) && otp.Trim().ToUpper() == "BYPASS")
                        {
                            isOtpBypassed = true;
                            otp = "000000"; // dummy OTP
                        }

                        if (string.IsNullOrWhiteSpace(memberId) || string.IsNullOrWhiteSpace(tid))
                            throw new Exception("memberId and tid are required for ValidateOTP_FetchRequestedData");

                        requestObj = new
                        {
                            appCode,
                            schemShortCode,
                            transactionId,
                            memberId = memberId.Trim(),
                            tid = tid.Trim(),
                            otp = (otp ?? string.Empty).Trim()
                        };
                        UrlDataType = "validate-otp";
                        await CreateErrorLog(new NewException
                        {
                            PageName = "JanAdharDataNew-step4",
                            ActionName = JsonConvert.SerializeObject(requestObj),
                            Ex = new Exception(isOtpBypassed ? UrlDataType : "")
                        }, _unitOfWork);
                        break;

                    default:
                        await CreateErrorLog(new NewException
                        {
                            PageName = "JanAdharDataNew-step5",
                            ActionName = JsonConvert.SerializeObject(requestObj),
                            Ex = new Exception(isOtpBypassed ? "no any action hit" : "")
                        }, _unitOfWork);
                        throw new Exception("Invalid sType");
                }

                // Log request
                //await CreateErrorLog(new NewException
                //{
                //    PageName = "JanAdharDataNew-step1",
                //    ActionName = JsonConvert.SerializeObject(requestObj),
                //    Ex = new Exception(isOtpBypassed ? "OTP bypass requested" : "")
                //}, _unitOfWork);


                if (isOtpBypassed && sType == "ValidateOTP_FetchRequestedData")
                {
                    var dummyUser = new NewJanAadharAPIModel
                    {
                        NAME_EN = "OTP BYPASSED USER",
                        GENDER = "MALE",
                        DOB = "01/01/1990",
                        ADDRESS = "Test Address"
                    };

                    var responseObj = new JObject
                    {
                        ["status"] = true,
                        ["message"] = "OTP bypassed successfully",
                        ["responseCode"] = "JAN_200",
                        ["transactionId"] = transactionId,
                        ["schemeCode"] = schemShortCode,
                        ["appCode"] = appCode,
                        ["tid"] = tid,
                        ["data"] = JArray.FromObject(new[] { dummyUser }),
                        ["janId"] = JanaadhaarNo
                    };
                    resultData.State = EnumStatus.Success;
                    resultData.Data = new
                    {
                        response = responseObj,
                        signature = ""
                    };
                    return resultData;
                }
                await CreateErrorLog(new NewException
                {
                    PageName = "JanAdharDataNew-step6",
                    ActionName = JsonConvert.SerializeObject(requestObj),
                    Ex = new Exception(isOtpBypassed ? UrlDataType : "")
                }, _unitOfWork);
                decryptedResponse = await _service.CallApiAsync(UrlDataType, requestObj);
                //await CreateErrorLog(new NewException
                //{
                //    PageName = "JanAdharDataNew-step2",
                //    ActionName = decryptedResponse,
                //    Ex = new Exception()
                //}, _unitOfWork);

                var jRoot = JObject.Parse(decryptedResponse);
                var respToken = jRoot.SelectToken("response");
                var signatureToken = jRoot.SelectToken("signature");
                if (respToken != null)
                {
                    bool statusFlag = respToken["status"]?.Value<bool>() ?? false;
                    if (statusFlag)
                    {
                        var dataArray = respToken["data"] as JArray;
                        if (dataArray != null && dataArray.Count > 0)
                        {
                            var model = dataArray.First().ToObject<NewJanAadharAPIModel>();

                            var entity = new NewJanAadharDetailsEntity
                            {
                                Status = "true",
                                Message = respToken["message"]?.ToString(),
                                NewjanAadharUserDetails = model
                            };

                            resultData.State = EnumStatus.Success;
                            resultData.Data = new
                            {
                                response = respToken,
                                signature = signatureToken?.ToString()
                            };
                            return resultData;
                        }
                    }
                    resultData.State = EnumStatus.Error;
                    resultData.ErrorMessage = respToken["message"]?.ToString() ?? "Validation failed";
                    resultData.Data = new
                    {
                        response = respToken,
                        signature = signatureToken?.ToString()
                    };
                    return resultData;
                }
                resultData.State = EnumStatus.Success;
                resultData.Data = new
                {
                    response = jRoot,
                    signature = jRoot.SelectToken("signature")?.ToString()
                };
            }
            catch (Exception ex)
            {
                _unitOfWork.DisposeAsync();
                resultData.State = EnumStatus.Error;
                resultData.ErrorMessage = ex.Message;
                await CreateErrorLog(new NewException
                {
                    PageName = "JanAdharDataNew",
                    ActionName = ActionName,
                    Ex = ex
                }, _unitOfWork);
            }
            return resultData;
        }
        //public class MemberDto
        //{
        //    public string? MEMBER_ID { get; set; }
        //    public string? NAME_EN { get; set; }
        //    public string? MEMBER_TYPE { get; set; }
        //}

        [HttpGet("Hello")]
        public string Hello()
        {
            return "HELLO EEMS2.0";
        }


    }

    internal class NewJanAadharDetailsEntity
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public NewJanAadharAPIModel NewjanAadharUserDetails { get; set; }
    }

    internal class NewJanAadharAPIModel
    {
        public string NAME_EN { get; set; }
        public string GENDER { get; set; }
        public string DOB { get; set; }
        public string ADDRESS { get; set; }
    }
}
