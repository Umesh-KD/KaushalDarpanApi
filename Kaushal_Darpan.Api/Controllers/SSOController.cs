using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using EmitraEmitraEncrytDecryptClient;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.EmitraPayment;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.RPPPayment;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.UserMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.MenuMaster;
using Microsoft.AspNetCore.Http.HttpResults;
using Kaushal_Darpan.Models;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class SSOController : BaseController
    {
        public override string PageName => "SSOController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SSOController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // login process = /login
        [HttpGet("Login/{SSOID}/{Password}")]
        public async Task<ApiResult<SSOUserDetailsModel>> Login(string SSOID, string Password)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SSOUserDetailsModel>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.Login(SSOID, Password);
                    if (result.Data != null)
                    {


                        MenuByUserAndRoleWiseModel menuModel = new MenuByUserAndRoleWiseModel() { UserID = result.Data.UserID, RoleID = result.Data.RoleID, EndTermID = result.Data.EndTermID, Eng_NonEng = result.Data.Eng_NonEng, DepartmentID = result.Data.DepartmentID, FinancialYearID = result.Data.FinancialYearID };
                        result.Data.SSOMenu = await _unitOfWork.MenuMasterRepository.MenuUserandRoleWise(menuModel);

                        var userSessionModel = new UserSessionModel
                        {
                            Email = result.Data.SSOID,
                            UserName = result.Data.FirstName,
                            UserID = result.Data.UserID,
                            EndTermID = result.Data.EndTermID,
                            LevelId = result.Data.LevelId
                            //if want role base
                            //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                            //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                        };

                        // set auth and get token
                        var token = await CreateAuthentication(userSessionModel);
                        Response.Headers.Append("X-AuthToken", token);


                        result.State = EnumStatus.Success;
                        result.Message = "Login successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid SSOID or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        //[HttpGet("Login/{SSOID}/{Password}")]
        //public async Task<ApiResult<SSOUserDetailsModel>> Login(string SSOID, string Password)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<SSOUserDetailsModel>();
        //        try
        //        {
        //            // Fetch the data from the repository (which executes the stored procedure)
        //            result.Data = await _unitOfWork.SSORepository.Login(SSOID, Password);

        //            if (result.Data != null)
        //            {
        //                // Handle the different login statuses
        //                if (result.Data.LoginStatus == "Login Success")
        //                {
        //                    // Successful login, generate token
        //                    var userSessionModel = new UserSessionModel
        //                    {
        //                        Email = result.Data.SSOID,
        //                        UserName = result.Data.FirstName,
        //                        UserID = result.Data.UserID,
        //                    };

        //                    var token = await CreateAuthentication(userSessionModel);
        //                    Response.Headers.Append("X-AuthToken", token);

        //                    result.State = EnumStatus.Success;
        //                    result.Message = "Login successfully .!";
        //                }
        //                else if (result.Data.LoginStatus == "Invalid Password")
        //                {
        //                    // Invalid password case
        //                    result.State = EnumStatus.Warning;
        //                    result.Message = "Invalid Password.";
        //                }
        //                else if (result.Data.LoginStatus == "Not Exists")
        //                {
        //                    // SSOID does not exist case
        //                    result.State = EnumStatus.Warning;
        //                    result.Message = "SSOID does not exist.";
        //                }
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = "Invalid SSOID or Password.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result.State = EnumStatus.Warning;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        finally
        //        {
        //            _unitOfWork.Dispose();
        //        }

        //        return result;
        //    });
        //}


        [HttpPost("GetUserRequestList")]
        public async Task<ApiResult<DataTable>> GetUserRequestList([FromBody] UserSearchModel model)
        {
            ActionName = "GetUserRequestList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.SSORepository.GetUserRequestList(model);
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


        [HttpPost("SaveData")]
        public async Task<ApiResult<bool>> SaveData([FromBody] UserRequestModel request)
        {
            ActionName = "SaveData([FromBody] UserMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.UserID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.UserID == 0)
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

        //sso login process = /ssologin(redirected)
        [HttpGet("GetSSOUserDetails/{SearchRecordID}")]
        public async Task<ApiResult<SSOUserDetailsModel>> GetSSOUserDetails(string SearchRecordID)//encoded search id
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SSOUserDetailsModel>();
                try
                {
                    // decode sso search id
                    var ssoSearchId = CommonFuncationHelper.Decrypt(HttpUtility.UrlDecode(SearchRecordID));
                    var data = await _unitOfWork.SSORepository.GetSSOUserDetails(ssoSearchId);
                    result.State = EnumStatus.Success;
                    //
                    if (data != null)
                    {
                        result.Data = data;
                        var userSessionModel = new UserSessionModel
                        {
                            Email = result.Data.SSOID,
                            UserName = result.Data.FirstName,
                            UserID = result.Data.UserID,
                            //if want role base
                            //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                            //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                        };

                        // set auth and get token
                        var token = await CreateAuthentication(userSessionModel);
                        Response.Headers.Append("X-AuthToken", token);
                    }
                }
                catch (Exception ex)
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

        [HttpPost("GetUserRoleList")]
        public async Task<ApiResult<DataTable>> GetUserRoleList(RoleListRequestModel request)
        {
            ActionName = "GetAllData(RoleListRequestModel request)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.SSORepository.GetUserRoleList(request));
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
        [HttpGet("GetAcedmicYearList/{RoleID=0}/{DepartmentID=0}/{SessionTypeID=0}")]
        public async Task<ApiResult<DataTable>> GetAcedmicYearList(int RoleID = 0, int DepartmentID = 0 , int SessionTypeID =0)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.SSORepository.GetAcedmicYearList(RoleID, DepartmentID , SessionTypeID));
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

        #region SSO Landing and Other
        [HttpPost("SSOLandingThenRedirectToApp")]
        public async Task<IActionResult> SSOLandingThenRedirectToApp()
        {
            ActionName = "SSOLandingThenRedirectToApp()";
            try
            {
                CommonFuncationHelper.WriteTextLog("comes to sso landing");

                // user=token
                var tokenDetail = Request.Form["UserDetails"];
                CommonFuncationHelper.WriteTextLog($"get the tokenDetail : {tokenDetail}");

                //api token
                var ssoTokenDetails = ThirdPartyServiceHelper.GetSSOTokenDetails(tokenDetail);
                //CommonFuncationHelper.WriteTextLog($"get the ssoTokenDetails : {JsonConvert.SerializeObject(ssoTokenDetails)}");
                // validate token
                var ssoId = ssoTokenDetails.Data.sAMAccountName ?? string.Empty;
                if (ssoTokenDetails?.State == EnumStatus.Error || string.IsNullOrWhiteSpace(ssoId))
                {
                    CommonFuncationHelper.WriteTextLog("validate token failed or empty ssoid");
                    Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                    return Content("");
                }

                //api user profile
                var ssoUserDetailsApi = ThirdPartyServiceHelper.GetSSOUserProfileDetail(ssoId, tokenDetail);
                //var ssoUserDetailsApi = ThirdPartyServiceHelper.GetSSOUserDetail(ssoId);
                //CommonFuncationHelper.WriteTextLog($"get the ssoUserDetailsApi : {JsonConvert.SerializeObject(ssoUserDetailsApi)}");
                // validate user profile
                if (ssoUserDetailsApi == null)
                {
                    CommonFuncationHelper.WriteTextLog("user profile detail not found");
                    Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                    return Content(""); ;
                }

                // save
                var ssoSearchId = await _unitOfWork.SSORepository.AddSSOUserProfileDetails(ssoUserDetailsApi);
                _unitOfWork.SaveChanges();

                //encode search id
                ssoSearchId = HttpUtility.UrlEncode(CommonFuncationHelper.Encrypt(ssoSearchId));
                CommonFuncationHelper.WriteTextLog($"convert the ssoSearchId : {ssoSearchId}");

                // cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                    SameSite = SameSiteMode.None, // Important for cross-site cookies
                    Secure = true,                // Required when SameSite=None
                    HttpOnly = false,              // Optional: set to false to access via JS

                };
                Response.Cookies.Append(ConfigurationHelper.AppName, ssoSearchId, cookieOptions);

                // success url
                string appUrl = $"{ConfigurationHelper.ApplicationURL}/ssologin?id1={ssoSearchId}";// no need, pick from cookie
                //string appUrl = $"{ConfigurationHelper.ApplicationURL}/ssologin";

                return Redirect(appUrl);
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                // file
                CommonFuncationHelper.WriteTextLog($"error : {ex.Message}");
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                return Redirect(ConfigurationHelper.SSOBaseUrl);
            }
        }
        [HttpPost("SSOIncreaseSessionTime")]
        public async void SSOIncreaseSessionTime()
        {
            ActionName = "SSOIncreaseSessionTime()";
            try
            {
                // user=token
                var tokenDetail = Request.Form["UserDetails"];
                CommonFuncationHelper.WriteTextLog($"get the tokenDetail : {tokenDetail}");

                //api sso increase session time
                var resp = ThirdPartyServiceHelper.IncreaseSessionTimeSSO(tokenDetail);
                CommonFuncationHelper.WriteTextLog($"comes the IncreaseSessionTimeSSO : {resp}");
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                // file
                CommonFuncationHelper.WriteTextLog($"error : {ex.Message}");
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
        }
        [HttpPost("SSOLogout")]
        public async void SSOLogout()
        {
            ActionName = "SSOLogout()";
            try
            {
                // user=token
                var tokenDetail = Request.Form["UserDetails"];
                CommonFuncationHelper.WriteTextLog($"get the tokenDetail : {tokenDetail}");

                //api sso logout
                ThirdPartyServiceHelper.LogoutSSO(tokenDetail);
                CommonFuncationHelper.WriteTextLog("comes the LogoutSSO");
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                // file
                CommonFuncationHelper.WriteTextLog($"error : {ex.Message}");
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
        }
        [HttpPost("SSOBackTo")]
        public async void SSOBackTo()
        {
            ActionName = "SSOBackTo()";
            try
            {
                // user=token
                var tokenDetail = Request.Form["UserDetails"];
                CommonFuncationHelper.WriteTextLog($"get the tokenDetail : {tokenDetail}");

                //api sso logout
                ThirdPartyServiceHelper.BackToSSO(tokenDetail);
                CommonFuncationHelper.WriteTextLog("comes the BackToSSO");
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                // file
                CommonFuncationHelper.WriteTextLog($"error : {ex.Message}");
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
        }


        [HttpPost("UpdateStudentUserType")]
        public async Task<ApiResult<int>> UpdateStudentUserType([FromBody] UpdateStudentDetailsModel request)
        {
            ActionName = "SaveData([FromBody] UserMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.SSORepository.UpdateStudentUserType(request);
                    _unitOfWork.SaveChanges();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Student Mapped Successfully";
                    }
                    else if (data == -2)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = "SSO ID NOT FOUND";
                        result.ErrorMessage = "SSO ID NOT FOUND";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = "Something went wrong";
                        result.ErrorMessage = "Something went wrong";
                    }
                    return result;
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
            });




        }






        #endregion


        [HttpGet("StudentLogin/{SSOID}")]
        public async Task<ApiResult<SSOUserDetailsModel>> StudentLogin(string SSOID)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SSOUserDetailsModel>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.StudentLogin(SSOID);
                    if (result.Data != null)
                    {
                        MenuByUserAndRoleWiseModel menuModel = new MenuByUserAndRoleWiseModel() { UserID = result.Data.UserID, RoleID = result.Data.RoleID, EndTermID = result.Data.EndTermID, Eng_NonEng = result.Data.Eng_NonEng, DepartmentID = result.Data.DepartmentID, FinancialYearID = result.Data.FinancialYearID };
                        result.Data.SSOMenu = await _unitOfWork.MenuMasterRepository.MenuUserandRoleWise(menuModel);

                        var userSessionModel = new UserSessionModel
                        {
                            Email = result.Data.SSOID,
                            UserName = result.Data.FirstName,
                            UserID = result.Data.UserID,
                            //if want role base
                            //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                            //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                        };

                        // set auth and get token
                        var token = await CreateAuthentication(userSessionModel);
                        Response.Headers.Append("X-AuthToken", token);


                        result.State = EnumStatus.Success;
                        result.Message = "Login successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid SSOID or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        [HttpGet("MobileLogin/{SSOID}/{CourseType}")]
        public async Task<ApiResult<SSOUserDetailsModel>> MobileLogin(string SSOID, int CourseType)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SSOUserDetailsModel>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.MobileLogin(SSOID, CourseType);
                    if (result.Data != null)
                    {
                        var userSessionModel = new UserSessionModel
                        {
                            Email = result.Data.SSOID,
                            UserName = result.Data.FirstName,
                            UserID = result.Data.UserID,
                            //if want role base
                            //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                            //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                        };

                        // set auth and get token
                        var token = await CreateAuthentication(userSessionModel);
                        Response.Headers.Append("X-AuthToken", token);


                        result.State = EnumStatus.Success;
                        result.Message = "Login successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid SSOID or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }


        [HttpGet("ItiCollegeMap/{CollegeCode}/{Password}")]
        public async Task<ApiResult<ITICollegeSSoMAP>> ItiCollegeMap(string CollegeCode, string Password)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITICollegeSSoMAP>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.ItiCollegeMap(CollegeCode, Password);
                    if (result.Data != null)
                    {

                        if (result.Data.CollegeExists == 1)
                        {
                            result.State = EnumStatus.Success;
                            result.Message = "College Exists";
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = "Invalid CollegeCode or Password.!";
                        }

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid CollegeCode or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }

        [HttpGet("BterCollegeMap/{CollegeCode}/{Password}")]
        public async Task<ApiResult<BTERCollegeSSoMAP>> BterCollegeMap(string CollegeCode, string Password)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<BTERCollegeSSoMAP>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.BTERCollegeMap(CollegeCode, Password);
                    if (result.Data != null)
                    {

                        if (result.Data.CollegeExists == 1)
                        {
                            result.State = EnumStatus.Success;
                            result.Message = "College Exists";
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = "Invalid CollegeCode or Password.!";
                        }

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid CollegeCode or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }


        [HttpPost("CreateCollegePrincipal")]
        public async Task<ApiResult<int>> CreateCollegePrincipal([FromBody] CreatePrincipalModel request)
        {
            ActionName = "CreateCollegePrincipal([FromBody] CreatePrincipalModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.CreateCollegePrincipal(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = "Something went wrong";
                    }
                }
                catch (System.Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = ex.Message;
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

        [HttpPost("CreateBTERCollegePrincipal")]
        public async Task<ApiResult<int>> CreateBTERCollegePrincipal([FromBody] CreatePrincipalModel request)
        {
            ActionName = "CreateBTERCollegePrincipal([FromBody] CreatePrincipalModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.SSORepository.CreateBTERCollegePrincipal(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = "Something went wrong";
                    }
                }
                catch (System.Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.Message = ex.Message;
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


        [HttpPost("LandingPage")]
        public async Task<IActionResult> LandingPage()
        {
            ActionName = "LandingPage()";
            try
            {
                if (Convert.ToString(Request.Form["encData"]) != null)
                {
                    string encData = Request.Form["encData"].ToString();
                    string decryptData = EmitraHelper.Decrypt(encData, "E-m!tr@2016");
                    ARequestModel emitra = JsonConvert.DeserializeObject<ARequestModel>(decryptData);
                    //write logs
                    CommonFuncationHelper.WriteTextLog($"LandingPageEncData : {encData}");
                    if (emitra != null)
                    {
                        // cookie
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.UtcNow.AddDays(1),
                            SameSite = SameSiteMode.None, // Important for cross-site cookies
                            Secure = true,                // Required when SameSite=None
                            HttpOnly = false,              // Optional: set to false to access via JS

                        };


                        SSO_UserProfileDetailModel ssoUserDetailsApi = new SSO_UserProfileDetailModel();
                        ssoUserDetailsApi.SSOID = emitra.SSOID;
                        ssoUserDetailsApi.displayName = emitra.SSOID;
                        ssoUserDetailsApi.userType = "KIOSK";
                        ssoUserDetailsApi.KIOSKCODE = emitra.KIOSKCODE;
                        ssoUserDetailsApi.SSoToken = emitra.SSOTOKEN;
                        ssoUserDetailsApi.IsKiosk = string.IsNullOrEmpty(emitra.KIOSKCODE) == true ? false : true;
                        ssoUserDetailsApi.SERVICEID = emitra.SERVICEID;
                        // save
                        var ssoSearchId = await _unitOfWork.SSORepository.AddSSOUserProfileDetails(ssoUserDetailsApi);
                        _unitOfWork.SaveChanges();

                        //encode search id
                        ssoSearchId = HttpUtility.UrlEncode(CommonFuncationHelper.Encrypt(ssoSearchId));
                        CommonFuncationHelper.WriteTextLog($"convert the ssoSearchId : {ssoSearchId}");


                        //SSO Token Verification
                        // staging service id =1825 and production : 1835 
                        EmitraRequestDetailsModel Model = new EmitraRequestDetailsModel();
                        Model.ServiceID = emitra.SERVICEID;
                        Model.IsKiosk = true;
                        var EmitraServiceDetail = await _unitOfWork.CommonFunctionRepository.GetEmitraServiceDetails(Model);
                        if (EmitraServiceDetail != null)
                        {
                            emitra.SERVICEID = EmitraServiceDetail.SERVICEID;
                            emitra.DepartmentID = EmitraServiceDetail.DepartmentID;
                            //set cookiekes
                            Response.Cookies.Append("KDEmitraKiosk", JsonConvert.SerializeObject(emitra), cookieOptions);
                            //Response.Redirect(EmitraServiceDetail.ViewName, false);
                            //return Content("");
                            // success url
                            string appUrl = $"{ConfigurationHelper.ApplicationURL}/ssologin?id1={ssoSearchId}";// no need, pick from cookie
                                                                                                               //string appUrl = $"{ConfigurationHelper.ApplicationURL}/ssologin";
                                                                                                               //Response.Redirect(appUrl, false);
                                                                                                               //return Content(""); 
                                                                                                               //
                            return Redirect(appUrl);

                        }
                        else
                        {
                            //Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                            //return Content(""); 
                            return Redirect(ConfigurationHelper.SSOBaseUrl);
                        }
                    }
                    else
                    {
                        //Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                        //return Content(""); 
                        return Redirect(ConfigurationHelper.SSOBaseUrl);
                    }
                }
                else
                {
                    //Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                    //return Content(""); 
                    return Redirect(ConfigurationHelper.SSOBaseUrl);
                }
            }
            catch (System.Exception ex)
            {
                _unitOfWork.Dispose();
                // file
                CommonFuncationHelper.WriteTextLog($"error : {ex.Message}");
                // write error log
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //Response.Redirect(ConfigurationHelper.SSOBaseUrl, false);
                //return Content(""); 
                return Redirect(ConfigurationHelper.SSOBaseUrl);
            }
        }


        [HttpPost("SSOLoginWithIDPass")]
        public async Task<ApiResult<SSOUserDetailsModel>> SSOLoginWithIDPass(SsoLoginPassModel Model)
        {
            bool isValid=false;
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SSOUserDetailsModel>();
                try
                {
                    if (Model.Password.ToUpper() == Constants.Login_DefaultPassword) //default password user
                    {
                        isValid = true;
                    }
                    else
                    {
                        //validate and check 
                        var ssoUserDetailsApi = await ThirdPartyServiceHelper.SSOLoginWithIDPass(Model.SSOID, Model.Password);
                        if (ssoUserDetailsApi != null)
                        {
                            var data = ssoUserDetailsApi.FirstOrDefault();
                            if (data != null)
                            {
                                if (data.IsSuccess == "1") //"1" For Success
                                {
                                    Model.Password = Constants.Login_DefaultPassword;// when user authenthicate then
                                    isValid = true;
                                }
                            }
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    if (isValid) // when is valid
                    {
                        result.Data = await _unitOfWork.SSORepository.Login(Model.SSOID, Model.Password);
                        if (result.Data != null)
                        {
                            MenuByUserAndRoleWiseModel menuModel = new MenuByUserAndRoleWiseModel() { UserID = result.Data.UserID, RoleID = result.Data.RoleID, EndTermID = result.Data.EndTermID, Eng_NonEng = result.Data.Eng_NonEng, DepartmentID = result.Data.DepartmentID, FinancialYearID = result.Data.FinancialYearID };
                            result.Data.SSOMenu = await _unitOfWork.MenuMasterRepository.MenuUserandRoleWise(menuModel);

                            var userSessionModel = new UserSessionModel
                            {
                                Email = result.Data.SSOID,
                                UserName = result.Data.FirstName,
                                UserID = result.Data.UserID,
                                EndTermID = result.Data.EndTermID,
                                LevelId = result.Data.LevelId
                                //if want role base
                                //RoleIDs = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleId)),
                                //RoleNames = string.Join(',', userEntity.UserRoles?.Select(x => x.RoleName))
                            };

                            // set auth and get token
                            var token = await CreateAuthentication(userSessionModel);
                            Response.Headers.Append("X-AuthToken", token);


                            result.State = EnumStatus.Success;
                            result.Message = "Login successfully .!";
                        }
                        else
                        {
                            result.State = EnumStatus.Warning;
                            result.Message = "Invalid SSOID or Password.!";
                        }
                    }
                    else 
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Invalid SSOID or Password.!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                    result.Message = "Invalid SSOID or Password.!"; 
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
                return result;
            });
        }


        [HttpPost("GetAcedmicYearListbySessionTypeID")]
        public async Task<ApiResult<DataTable>> GetAcedmicYearListbyTypeID([FromBody] RequestBaseModel Model)
        {
            ActionName = "GetAcedmicYearListbyTypeID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.SSORepository.GetAcedmicYearListbyTypeID(Model));
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

    }
}