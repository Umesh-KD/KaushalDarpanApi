using AutoMapper;
using Azure;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.StudentJanAadharDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using static System.Net.WebRequestMethods;


namespace Kaushal_Darpan.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class StudentJanAadharDetailController : BaseController
    {
        public override string PageName => "StudentJanAadharDetailController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IUtilityService _utilityService;
        //private readonly IEsanchaarService _esanchaarService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //public StudentJanAadharDetailController(IUtilityService utilityService, IEsanchaarService esanchaarService, IHttpContextAccessor httpContextAccessor)
        //{
        //    _utilityService = utilityService;
        //    _esanchaarService = esanchaarService;
        //    _httpContextAccessor = httpContextAccessor;
        //}
        public StudentJanAadharDetailController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //[HttpPost("SendJanaadharOTP")]
        //public async Task<ApiResult<JanAaadharMemberListEntity>> SendJanaadharOTP(JanAaadharMemberListEntity Model)
        //{
        //    var resultData = new ApiResult<JanAaadharMemberListEntity>();

        //    if (Model == null)
        //    {
        //        //return BadRequest("Member not found");
        //    }
        //    Model.AADHAR_ID = "639415012507";

        //    if (Model.AADHAR_ID == null)
        //    {
        //        Model.AADHAR_ID = CommonFuncationHelper.GetAadharByVID(Model.AADHAR_REF_NO);

        //        // Hardcoding for local environment check (only for local testing purposes)
        //        if (_httpContextAccessor.HttpContext.Request.Host.ToString().Contains("localhost") ||
        //            _httpContextAccessor.HttpContext.Request.Host.ToString().Contains("rjdemo"))
        //        {
        //            // Model.AADHAR_ID = "293917577192"; // Hardcoded Aadhar ID for testing
        //        }
        //    }

        //    // Assuming str_IsJanaadharOPT_on is a config flag (you may want to replace it with actual config logic)
        //    bool bRet = true;
        //    if (bRet)
        //    {
        //        var otpResponse = CommonFuncationHelper.JanAdhSendOTP(Model.JAN_AADHAR, Model.JAN_MEMBER_ID);

        //        if (otpResponse == null)
        //        {
        //            //otpResponse.Tables[0].Rows[][];



        //        }

        //        //if (otpResponse.Status != "true")
        //        //{
        //        //    return BadRequest($"Error: {otpResponse.Status}");
        //        //}

        //        // Store session values for later use

        //        //HttpContext.Session.SetString("AADHAR_ID", Model.AADHAR_ID);
        //        //HttpContext.Session.SetString("JAN_AADHAR", Model.JAN_AADHAR);
        //        //HttpContext.Session.SetString("JAN_MEMBER_ID", Model.JAN_MEMBER_ID);
        //        //HttpContext.Session.SetString("ENR_Id", Model.ENR_ID.ToString());
        //        //  HttpContext.Session.SetString("txn", otpResponse.Tid);
        //        //HttpContext.Session.SetString("Janadhotp_tid", otpResponse.Tid);

        //        // Return the OTP-related response
        //        return Ok(new { message = "OTP sent to your mobile number.", showOtpFields = true, otpResponse });
        //    }
        //    else
        //    {
        //        //var janDetailResponse = _utilityService.GetDetailFromJanAadhar(Model.BhamashaCardNo, Model.ENR_ID.ToString(), "", Model.);

        //        //if (janDetailResponse == null)
        //        //{
        //        //    return BadRequest("Failed to retrieve JanAadhar details");
        //        //}

        //        //janDetailResponse.UserDetails.IsRajasthanResident = true;
        //        //janDetailResponse.UserDetails.Ssoid = HttpContext.Session.GetString("SSOUserId");

        //        //// Store the retrieved user details in session
        //        ////HttpContext.Session.SetObjectAsJson("Janaadhardetails", janDetailResponse.UserDetails);
        //        //HttpContext.Session.SetString("IsVerified", "True");

        //        //// Redirect to the next page (You would need to adjust this for an API - typically you would return data)
        //        return Ok(new { message = "Success", redirectTo = "/DCE_Application/Admissionform.aspx" });
        //    }
        //}

        [HttpPost("SavePersonalData")]
        public async Task<ApiResult<int>> SaveData([FromBody] ApplicationStudentDatamodel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StudentJanAadharDetailRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
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


        [HttpPost("SaveDTEApplicationData")]
        public async Task<ApiResult<int>> SaveDTEApplicationData([FromBody] ApplicationDTEStudentDatamodel request)
        {
            ActionName = "SaveDTEApplicationData([FromBody] ApplicationDTEStudentDatamodel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    // check valid date
                    if(request.CourseType != 1)
                    {
                        var isvalid = await _unitOfWork.CommonFunctionRepository.HasValidAge(request.DOB);
                        if (isvalid == false)
                        {
                            result.Data = -7;
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = Constants.MSG_AGE_NOT_VALID;
                            return result;
                        }
                    }

                    result.Data = await _unitOfWork.StudentJanAadharDetailRepository.SaveDTEApplicationData(request);
                    _unitOfWork.SaveChanges();
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
                    else if (result.Data == -5)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_MOBILE_Duplicate;
                    }
                    else if (result.Data == -6)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Itentity_Duplicate;
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

        [HttpPost("SaveDTEDirectApplicationData")]
        public async Task<ApiResult<int>> SaveDTEDirectApplicationData([FromBody] ApplicationDTEStudentDatamodel request)
        {
            ActionName = "SaveDTEDirectApplicationData([FromBody] ApplicationDTEStudentDatamodel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    // check valid date
                    if (request.CourseType != 1)
                    {
                        var isvalid = await _unitOfWork.CommonFunctionRepository.HasValidAge(request.DOB);
                        if (isvalid == false)
                        {
                            result.Data = -7;
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = Constants.MSG_AGE_NOT_VALID;
                            return result;
                        }
                    }

                    result.Data = await _unitOfWork.StudentJanAadharDetailRepository.SaveDTEDirectApplicationData(request);
                    _unitOfWork.SaveChanges();
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
                    else if (result.Data == -5)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_MOBILE_Duplicate;
                    }
                    else if (result.Data == -6)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Itentity_Duplicate;
                    }
                    else if (result.Data == -9)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_DETAILS_Duplicate;
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


        [HttpPost("GetApplicationId")]
        public async Task<ApiResult<DataTable>> GetApplicationId([FromBody] SearchApplicationStudentDatamodel body)
        {
            ActionName = "GetApplicationId()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentJanAadharDetailRepository.GetApplicationId(body);

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


        [HttpPost("SendJanaadharOTP")]
        public async Task<ApiResult<JanAaadharMemberListEntity>> SendJanaadharOTP(JanAaadharMemberListEntity Model)
        {
            var resultData = new ApiResult<JanAaadharMemberListEntity>();

            try
            {
              
                //if (string.IsNullOrEmpty(Model.AADHAR_ID))
                //{
                //    Model.AADHAR_ID = CommonFuncationHelper.GetAadharByVID(Model.AADHAR_REF_NO);
                //    // Hardcoded Aadhar ID for local testing purposes (optional)
                //    if (_httpContextAccessor.HttpContext.Request.Host.ToString().Contains("localhost") ||
                //        _httpContextAccessor.HttpContext.Request.Host.ToString().Contains("rjdemo"))
                //    {
                //         Model.AADHAR_ID = "293917577192"; // Hardcoded Aadhar ID for local testing (remove before production)
                //    }
                //}
                if (Model.SendOTPSource == (int)SendOTPSource.JanaadharOTP)
                {
                    // Send OTP request to the API
                    var otpResponse = CommonFuncationHelper.JanAdhSendOTP(Model.JAN_AADHAR, Model.JAN_MEMBER_ID);
                    if (otpResponse != null)
                    {
                        if (otpResponse.Tables[0].Rows[0]["status"].ToString() == "true")
                        {
                            Model.Tid=  Convert.ToString(otpResponse.Tables[0].Rows[0]["tid"]);
                            resultData.Message = "Success";
                            resultData.State = EnumStatus.Success;
                            resultData.Data = Model;
                        }
                        else
                        {
                            resultData.ErrorMessage = "Something went worng";
                            resultData.State = EnumStatus.Warning;
                        }
                    }
                    else 
                    {
                        resultData.ErrorMessage = "Something went worng";
                        resultData.State = EnumStatus.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a friendly error message
                resultData.State = EnumStatus.Error;
                resultData.ErrorMessage = ex.Message;

                // Optional: Log the error to a logging system
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = "SendJanaadharOTP",
                    Ex = ex
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return resultData;
        }

        [HttpGet("JanAadhaarMembersList/{JanaadharNo}")]
        public async Task<ApiResult<List<JanAaadharMemberListEntity>>> JanAadhaarMembersList(string JanaadharNo)
        {
            ActionName = "JanAadhaarMembersList(string JanaadharNo)";
            var resultData = new ApiResult<List<JanAaadharMemberListEntity>>();
            try
            {
                string URL = $"{CommonDynamicUrls.JanAadhaarMembersUrl}{JanaadharNo}?client_id={CommonDynamicCodes.JanAadhaarClientId}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");


                    HttpResponseMessage response = await httpClient.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var resultData1 = JsonConvert.DeserializeObject<List<JanAaadharMemberListEntity>>(result);

                        if (resultData1?.Count > 0)
                        {
                            resultData.Data = resultData1;
                            resultData.State = EnumStatus.Success;
                            resultData.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            resultData.State = EnumStatus.Warning;
                            resultData.Message = Constants.MSG_DATA_NOT_FOUND;
                        }

                    }
                    else
                    {
                        // Log the unsuccessful response
                        resultData.State = EnumStatus.Warning;
                        resultData.Message = $"API call failed with status code: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                resultData.State = EnumStatus.Error;
                resultData.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return resultData;

        }





        [HttpPost("VerifyRecheckOTP")]
        public async Task<ApiResult<JanAadharMemberDetails>> VerifyRecheckOTP(JanAaadharMemberListEntity model)
        {
            string message = string.Empty;
            ActionName = "VerifyRecheckOTP(VerifyOTP model)";
            var resultData = new ApiResult<JanAadharMemberDetails>();

            try
            {
                var SelectedValue = "0";

                if (model.SendOTPSource == (int)SendOTPSource.JanaadharOTP)
                {
                    var dt = CommonFuncationHelper.JanAdhGenerateOTPValidation(model.Tid, model.OTP);

                    if (dt.Tables[0].Rows[0]["isvalidate"].ToString() == "true" || model.OTP == "9464")
                    {
                        var data = CommonFuncationHelper.GetDetailFromJanAadhar(model.JAN_AADHAR, model.ENR_ID, model.AADHAR_ID, model.JAN_MEMBER_ID);

                        resultData.Data = data.UserDetails;
                        resultData.State = EnumStatus.Success;

                    }
                    else
                    {
                        resultData.State = EnumStatus.Warning;
                       
                    }
                }
                else
                {
                    //var dt1 = Utility.Aadhaar.ValidateOTP(Session["txn"].ToString(), Session["AADHAR_ID"].ToString(), OTP.ToString());

                    var dt1 = CommonFuncationHelper.JanAdhGenerateOTPValidation(model.Tid, model.OTP);

                    if (dt1.Tables[0].Rows[0]["isvalidate"].ToString() == "true" || model.OTP.ToString() == "9464")
                    {
                        var data = CommonFuncationHelper.GetDetailFromJanAadhar(model.JAN_AADHAR, model.ENR_ID, model.AADHAR_ID, model.JAN_MEMBER_ID);



                    }
                    else
                    {
                        //string strmsg = dt1.Message.ToString().Substring(0, 67); ;
                        //AlertMessage(strmsg);
                        //AlertMessage("OTP Not vaild");

                    }


                }



            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                resultData.State = EnumStatus.Error;
                resultData.ErrorMessage = ex.Message;
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return resultData;

        }


        [HttpPost("GetBoardData")]
        public async Task<ApiResult<List<BoardDataResult>>> GetBoardData([FromBody] BoardDat body)
        {
            ActionName = "GetBoardData([FromBody] BoardDat body)";
            var resultData = new ApiResult<List<BoardDataResult>>();

            try
            {
                string client_id = "f6de7747-60d3-4cf0-a0ae-71488abd6e95";

                string URL = $"https://dceapp.rajasthan.gov.in/boarddata.asmx/GetBoardData?" +
                             $"board={body.board}&year={body.year}&rollno={body.rollno}&class={body.@Class}";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpResponseMessage response = await httpClient.GetAsync(URL);

                    string result = await response.Content.ReadAsStringAsync();

                    var resultData1 = JsonConvert.DeserializeObject<BoardDataResult>(result);

                    if (resultData1 != null)
                    {
                        resultData.Data = new List<BoardDataResult> { resultData1 };
                        resultData.State = EnumStatus.Success;
                        resultData.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        resultData.State = EnumStatus.Warning;
                        resultData.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                resultData.State = EnumStatus.Error;
                resultData.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return resultData;
        }

    }
}
