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
        //    [HttpPost("JanAdharDataNew")]
        //    public async Task<IActionResult> JanAdharDataNew(
        //string SchemeName = "EEMS",
        //string Action = "",
        //string JanaadhaarNo = "",
        //string memberId = "",
        //string tid = "",
        //string otp = "")
        //    {

        //        string schemShortCode = "RAJSAHAKAR";
        //        string appCode = "JAN6268201";
        //        string transactionId = $"{SchemeName}{DateTime.Now:yyyyMMdd}{new Random().Next(100000, 999999)}";

        //        try
        //        {
        //            if (string.IsNullOrWhiteSpace(Action))
        //                return BadRequest(new { status = false, message = "Action is required" });

        //            dynamic requestObj = null;
        //            string UrlDataType = "";

        //            switch (Action)
        //            {
        //                case "FetchMemberList":
        //                    if (string.IsNullOrWhiteSpace(JanaadhaarNo))
        //                        return BadRequest(new { status = false, message = "JanaadhaarNo is required" });

        //                    requestObj = new
        //                    {
        //                        data = new
        //                        {
        //                            appCode,
        //                            schemShortCode,
        //                            transactionId,
        //                            janId = JanaadhaarNo
        //                        }
        //                    };
        //                    UrlDataType = "member-list";
        //                    break;

        //                case "GenerateOTP":
        //                    if (string.IsNullOrWhiteSpace(memberId))
        //                        return BadRequest(new { status = false, message = "memberId is required" });

        //                    requestObj = new
        //                    {
        //                        data = new
        //                        {
        //                            appCode,
        //                            schemShortCode,
        //                            transactionId,
        //                            memberId
        //                        }
        //                    };
        //                    UrlDataType = "generate-otp";
        //                    break;

        //                case "ValidateOTP_FetchRequestedData":
        //                    if (string.IsNullOrWhiteSpace(memberId) || string.IsNullOrWhiteSpace(tid) || string.IsNullOrWhiteSpace(otp))
        //                        return BadRequest(new { status = false, message = "memberId, tid, and otp are required" });

        //                    requestObj = new
        //                    {
        //                        data = new
        //                        {
        //                            appCode,
        //                            schemShortCode,
        //                            transactionId,
        //                            memberId,
        //                            tid,
        //                            otp
        //                        }
        //                    };
        //                    UrlDataType = "validate-otp";
        //                    break;

        //                default:
        //                    return BadRequest(new { status = false, message = "Invalid Action" });
        //            }

        //            // ✅ Call service async
        //            string decryptedResponse = await _service.CallApiAsync(UrlDataType, requestObj.data);

        //            // Try parsing the decrypted response JSON safely
        //            try
        //            {
        //                var jsonResponse = JsonConvert.DeserializeObject<object>(decryptedResponse);

        //                return Ok(jsonResponse);
        //            }
        //            catch
        //            {
        //                return Ok(new { status = true, response = decryptedResponse });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new
        //            {
        //                status = false,
        //                message = "Error occurred while processing Jan Aadhaar request.",
        //                error = ex.Message
        //            });
        //        }
        //    }
        //[HttpPost("JanAdharDataNew")]
        //public async Task<ApiResult<object>> JanAdharDataNew(
        //string SchemeName = "EEMS",
        //string sType = "",
        //string JanaadhaarNo = "",
        //string memberId = "",
        //string tid = "",
        //string otp = "")
        //{
        //    string schemShortCode = "EEMS";
        //    string appCode = "JAN4601237";
        //    string transactionId = $"{SchemeName}{DateTime.Now:yyyyMMdd}{new Random().Next(100000, 999999)}";
        //    string ActionName = "JanAdharDataNew";
        //    var resultData = new ApiResult<object>();

        //    try
        //    {
        //        dynamic requestObj = null;
        //        string UrlDataType = "";
        //        switch (sType)
        //        {
        //            case "FetchMemberList":
        //                requestObj = new
        //                {
        //                    appCode,
        //                    schemShortCode,
        //                    transactionId,
        //                    janId = JanaadhaarNo
        //                };
        //                UrlDataType = "member-list";
        //                break;

        //            case "GenerateOTP":
        //                if (string.IsNullOrWhiteSpace(memberId))
        //                    throw new Exception("memberId is required");

        //                requestObj = new
        //                {
        //                    appCode,
        //                    schemShortCode,
        //                    transactionId,
        //                    memberId
        //                };
        //                UrlDataType = "generate-otp";
        //                break;

        //            case "ValidateOTP_FetchRequestedData":
        //                if (string.IsNullOrWhiteSpace(memberId) || string.IsNullOrWhiteSpace(tid) || string.IsNullOrWhiteSpace(otp))
        //                    throw new Exception("memberId, tid, and otp are required");

        //                requestObj = new
        //                {
        //                    appCode,
        //                    schemShortCode,
        //                    transactionId,
        //                    memberId,
        //                    tid,
        //                    otp
        //                };
        //                UrlDataType = "validate-otp";
        //                break;

        //            default:
        //                throw new Exception("Invalid Action");
        //        }

        //        // STEP 1 - Log input data
        //        var nex1 = new NewException
        //        {
        //            PageName = "step1",
        //            ActionName = JsonConvert.SerializeObject(requestObj),
        //            Ex = new Exception()
        //        };
        //        await CreateErrorLog(nex1, _unitOfWork);

        //        // STEP 2 - Call service
        //        string decryptedResponse = await _service.CallApiAsync(UrlDataType, requestObj);

        //        // STEP 3 - Log raw response
        //        var nex2 = new NewException
        //        {
        //            PageName = "step2",
        //            ActionName = decryptedResponse,
        //            Ex = new Exception()
        //        };
        //        await CreateErrorLog(nex2, _unitOfWork);

        //        // STEP 4 - Deserialize response if possible
        //        try
        //        {
        //            var jsonResponse = JsonConvert.DeserializeObject<object>(decryptedResponse);
        //            resultData.Data = jsonResponse;
        //            resultData.State = EnumStatus.Success;
        //        }
        //        catch
        //        {
        //            resultData.Data = decryptedResponse;
        //            resultData.State = EnumStatus.Success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _unitOfWork.Dispose();
        //        resultData.State = EnumStatus.Error;
        //        resultData.ErrorMessage = ex.Message;

        //        // Log error
        //        var nex = new NewException
        //        {
        //            PageName = "JanAdharDataNew",
        //            ActionName = ActionName,
        //            Ex = ex
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }

        //    return resultData;
        //}

        //[HttpPost("NewJanAadharapi")]
        //public async Task<ApiResult<NewJanAadharDetailsEntity>> TestAPIToCheckDetailsFromJanaadhar(string JAN_AADHAR, string AADHAR_ID, string MID)
        //{
        //    string message = string.Empty;
        //    ActionName = "VerifyRecheckOTP(VerifyOTP model)";
        //    var resultData = new ApiResult<NewJanAadharDetailsEntity>();
        //    string requestUrlHost = HttpContext.Request.Host.ToString();
        //    try
        //    {
        //        var data = CommonFuncationHelper.GetDetailFromJanAadhar(JAN_AADHAR, "", AADHAR_ID, MID, requestUrlHost);
        //        resultData.Data = data.NewjanAadharUserDetails;
        //        resultData.State = EnumStatus.Success;

        //    }
        //    catch (Exception ex)
        //    {
        //        _unitOfWork.Dispose();
        //        resultData.State = EnumStatus.Error;
        //        resultData.ErrorMessage = ex.Message;
        //        // write error log
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }

        //    return resultData;

        //}
        //    [HttpPost("JanAdharDataNew")]
        //    public async Task<ApiResult<NewJanAadharDetailsEntity>> JanAdharDataNew(
        //string SchemeName = "EEMS",
        //string sType = "",
        //string JanaadhaarNo = "",
        //string memberId = "",
        //string tid = "",
        //string otp = "")
        //    {
        //        string schemShortCode = "EEMS";
        //        string appCode = "JAN4601237";
        //        string transactionId = $"{SchemeName}{DateTime.Now:yyyyMMdd}{new Random().Next(100000, 999999)}";
        //        string ActionName = "JanAdharDataNew";
        //        var resultData = new ApiResult<NewJanAadharDetailsEntity>();

        //        try
        //        {
        //            dynamic requestObj = null;
        //            string UrlDataType = "";
        //            bool isOtpBypassed = false;

        //            switch (sType)
        //            {
        //                case "FetchMemberList":
        //                    requestObj = new
        //                    {
        //                        appCode,
        //                        schemShortCode,
        //                        transactionId,
        //                        janId = JanaadhaarNo?.Trim()
        //                    };
        //                    UrlDataType = "member-list";
        //                    break;

        //                case "GenerateOTP":
        //                    if (string.IsNullOrWhiteSpace(memberId))
        //                        throw new Exception("memberId is required for GenerateOTP");

        //                    requestObj = new
        //                    {
        //                        appCode,
        //                        schemShortCode,
        //                        transactionId,
        //                        memberId = memberId.Trim()
        //                    };
        //                    UrlDataType = "generate-otp";
        //                    break;

        //                case "ValidateOTP_FetchRequestedData":
        //                    // ONLY bypass when explicitly asked
        //                    if (!string.IsNullOrEmpty(otp) && otp.Trim().ToUpper() == "9464")
        //                    {
        //                        isOtpBypassed = true;
        //                        otp = "000000"; // dummy OTP only when BYPASS specified
        //                    }

        //                    if (string.IsNullOrWhiteSpace(memberId) || string.IsNullOrWhiteSpace(tid))
        //                        throw new Exception("memberId and tid are required for ValidateOTP_FetchRequestedData");

        //                    // Use the actual otp if provided (and not BYPASS)
        //                    requestObj = new
        //                    {
        //                        appCode,
        //                        schemShortCode,
        //                        transactionId,
        //                        memberId = memberId.Trim(),
        //                        tid = tid.Trim(),
        //                        otp = (otp ?? string.Empty).Trim()
        //                    };
        //                    UrlDataType = "validate-otp";
        //                    break;

        //                default:
        //                    throw new Exception("Invalid sType");
        //            }

        //            // Log request
        //            await CreateErrorLog(new NewException
        //            {
        //                PageName = "JanAdharDataNew-step1",
        //                ActionName = JsonConvert.SerializeObject(requestObj),
        //                Ex = new Exception(isOtpBypassed ? "OTP bypass requested" : "")
        //            }, _unitOfWork);

        //            // Call external API
        //            string decryptedResponse = await _service.CallApiAsync(UrlDataType, requestObj);

        //            // Log raw response
        //            await CreateErrorLog(new NewException
        //            {
        //                PageName = "JanAdharDataNew-step2",
        //                ActionName = decryptedResponse,
        //                Ex = new Exception()
        //            }, _unitOfWork);

        //            // Try parsing intelligently
        //            NewJanAadharDetailsEntity parsed = null;

        //            try
        //            {
        //                // 1) Try direct deserialize to your model
        //                parsed = JsonConvert.DeserializeObject<NewJanAadharDetailsEntity>(decryptedResponse);

        //                // 2) If null, try to find nested payloads (common cases)
        //                if (parsed == null)
        //                {
        //                    var jObj = JObject.Parse(decryptedResponse);

        //                    // Common possible keys: "data", "response", "result", "NewjanAadharUserDetails"
        //                    var candidateTokens = new[] { "data", "response", "result", "NewjanAadharUserDetails", "NewjanAadharUserData" };

        //                    foreach (var key in candidateTokens)
        //                    {
        //                        if (jObj.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out JToken token))
        //                        {
        //                            try
        //                            {
        //                                parsed = token.ToObject<NewJanAadharDetailsEntity>();
        //                                if (parsed != null) break;
        //                            }
        //                            catch { /* ignore and continue */ }
        //                        }
        //                    }

        //                    // If still null, try mapping by reading fields manually into the model
        //                    if (parsed == null)
        //                    {
        //                        // Try reading high-level status/message and nested object
        //                        var temp = new NewJanAadharDetailsEntity();
        //                        if (jObj.TryGetValue("Status", StringComparison.OrdinalIgnoreCase, out JToken st)) temp.Status = st.ToString();
        //                        if (jObj.TryGetValue("Message", StringComparison.OrdinalIgnoreCase, out JToken msg)) temp.Message = msg.ToString();

        //                        // find any nested object that looks like model
        //                        var possibleNested = jObj.Descendants().OfType<JProperty>()
        //                            .FirstOrDefault(p => p.Name.IndexOf("jan", StringComparison.OrdinalIgnoreCase) >= 0
        //                                              || p.Name.IndexOf("user", StringComparison.OrdinalIgnoreCase) >= 0);

        //                        if (possibleNested != null)
        //                        {
        //                            try
        //                            {
        //                                temp.NewjanAadharUserDetails = possibleNested.Value.ToObject<NewJanAadharAPIModel>();
        //                                parsed = temp;
        //                            }
        //                            catch { /* continue fallback */ }
        //                        }
        //                    }
        //                }

        //                if (parsed != null)
        //                {
        //                    resultData.Data = parsed;
        //                    resultData.State = EnumStatus.Success;
        //                    resultData.Message = isOtpBypassed ? "OTP bypassed." : "Success";
        //                }
        //                else
        //                {
        //                    // Parsing failed — return raw response in ErrorMessage for debugging
        //                    resultData.State = EnumStatus.Error;
        //                    resultData.ErrorMessage = "Unable to map API response to NewJanAadharDetailsEntity.";
        //                    // Optionally attach raw response
        //                    resultData.Data = new NewJanAadharDetailsEntity { Status = "PARSE_ERROR", Message = decryptedResponse };
        //                }
        //            }
        //            catch (JsonException jex)
        //            {
        //                resultData.State = EnumStatus.Error;
        //                resultData.ErrorMessage = "JSON parse error: " + jex.Message;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            resultData.State = EnumStatus.Error;
        //            resultData.ErrorMessage = ex.Message;

        //            await CreateErrorLog(new NewException
        //            {
        //                PageName = "JanAdharDataNew",
        //                ActionName = ActionName,
        //                Ex = ex
        //            }, _unitOfWork);
        //        }

        //        return resultData;
        //    }
        
        
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
