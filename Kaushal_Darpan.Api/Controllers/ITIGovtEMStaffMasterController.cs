using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTERIMCAllocationModel;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.DispatchMaster;
using Kaushal_Darpan.Models.HrMaster;

using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIGovtEMStaffMasterController : BaseController
    {
        public override string PageName => "ITIGovtEMStaffMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIGovtEMStaffMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork; 
        }
        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetAllData(body);

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
       
        [HttpPost("StaffBasicDetails")]
        public async Task<ApiResult<int>> SaveBasicData([FromBody] ITIGovtEMAddStaffBasicDetailDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.SaveBasicData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                       

                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
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
                        if (request.ProfileID == 0)
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
       
        [HttpPost("StaffDetails")]
        public async Task<ApiResult<bool>> SaveData([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID  == 0)
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
       
        [HttpGet("GetByID/{PK_ID}/{DepartmentID}")]
        public async Task<ApiResult<ITIGovtEMStaffMasterModel>> GetByID(int PK_ID, int DepartmentID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIGovtEMStaffMasterModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetById(PK_ID, DepartmentID);
                    result.Data = data;
                    if (data != null)
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


        [HttpPost("GetAllStudentPersentData")]
        public async Task<ApiResult<DataTable>> GetAllStudentPersentData([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {
            ActionName = "GetAllStudentPersentData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.ITIGovtEMGetAllStudentPersentData(body);

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

        [HttpPost("ITIGovtEMGetAllTotalExaminerData")]
        public async Task<ApiResult<DataTable>> ITIGovtEMGetAllTotalExaminerData([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {
            ActionName = "ITIGovtEMGetAllTotalExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.ITIGovtEMGetAllTotalExaminerData(body);

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

        [HttpPost("LockandSubmit")]
        public async Task<ApiResult<bool>> LockandSubmit([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.LockandSubmit(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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

        [HttpPost("UnlockStaff")]
        public async Task<ApiResult<bool>> UnlockStaff([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.UnlockStaff(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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

        [HttpPost("ApproveStaff")]
        public async Task<ApiResult<bool>> ApproveStaff([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ApproveStaff(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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


        #region  check sso Details

        private static string GetSSOUrl
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                return root.GetSection("SSOLanding:SSOServiceurl").Value;
            }
        }


        [HttpGet]
        [Route("getSsoDetaislBySSOId")]
        public async Task<ITIGovtEMStaffMasterModel> GetSsoDetaislBySSOId(string SSOId)
        {
            try
            {

                ITIGovtEMStaffMasterModel data = new ITIGovtEMStaffMasterModel();

                SSOUser ssoInfo = new SSOUser();
                SSOUserResponse objServiceResponse = new SSOUserResponse();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetSSOUrl); //("http://ssotest.rajasthan.gov.in:8888/");
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    string WSUSERNAME = "KAUSHALDARPAN.TEST";
                    string WSPASSWORD = "R@jS$okau29#";
                    HttpResponseMessage Res = await client.GetAsync("/SSOREST/GetUserDetailJSON/" + SSOId + "/" + WSUSERNAME + "/" + WSPASSWORD);
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list  
                        objServiceResponse = JsonConvert.DeserializeObject<SSOUserResponse>(EmpResponse);
                        if (objServiceResponse != null)
                        {
                            data = new ITIGovtEMStaffMasterModel()
                            {
                                Email = objServiceResponse.mailPersonal,
                                MobileNumber = objServiceResponse.mobile,
                                //Gender = objServiceResponse.gender,
                                Dis_ProfileName = objServiceResponse.displayName,
                                SSOID = objServiceResponse.SSOID
                            };
                        }
                    }
                    return data;
                }
            }
            catch(Exception ex)
            {
                
                return null;
            }



        }
        #endregion check sso Details





        [HttpPost("StaffLevelType")]
        public async Task<ApiResult<DataTable>> StaffLevelType([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.StaffLevelType(body);

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
        
        [HttpPost("StaffLevelChild")]
        public async Task<ApiResult<DataTable>> StaffLevelChild([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.StaffLevelChild(body);

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




        [HttpPost("IsDownloadCertificate")]
        public async Task<ApiResult<bool>> IsDownloadCertificate([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "IsDownloadCertificate([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.IsDownloadCertificate(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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


        [HttpDelete("IsDeleteHostelWarden/{SSOID}")]
        public async Task<ApiResult<int>> IsDeleteHostelWarden(string SSOID)
        {
            ActionName = "IsDeleteHostelWarden(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.IsDeleteHostelWarden(SSOID);
                    result.Data = data;
                    if (data != null)
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

        [HttpPost("ChangeWorkingInstitute")]
        public async Task<ApiResult<bool>> ChangeWorkingInstitute([FromBody] ITIGovtEMStaffMasterModel request)
        {
            ActionName = "ChangeWorkingInstitute([FromBody] StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ChangeWorkingInstitute(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StaffID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StaffID == 0)
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

        [HttpPost("GetCurrentWorkingInstitute_ByID")]
        public async Task<ApiResult<DataTable>> GetCurrentWorkingInstitute_ByID([FromBody] ITIGovtEMStaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetCurrentWorkingInstitute_ByID(body);

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


        [HttpPost("UpdateSSOIDByPriciple")]
        public async Task<ApiResult<int>> UpdateSSOIDByPriciple([FromBody] UpdateSSOIDByPricipleModel request)
        {
            ActionName = "UpdateSSOIDByPriciple([FromBody] UpdateSSOIDByPricipleModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.UpdateSSOIDByPriciple(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {


                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
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
                        if (request.StaffID == 0)
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


        [HttpPost("GetPrincipleList")]
        public async Task<ApiResult<DataTable>> GetPrincipleList([FromBody] ITIGovtStudentSearchModel body)
        {
            ActionName = "GetPrincipleList([FromBody] ITIGovtStudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIGovtEMStaffMasterRepository.GetPrincipleList(body));
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


        [HttpPost("ITIGovtEM_OfficeGetAllData")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_OfficeGetAllData([FromBody] ITIGovtEM_OfficeSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeGetAllData(body);

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

        [HttpPost("ITIGovtEM_OfficeSaveData")]
        public async Task<ApiResult<int>> ITIGovtEM_OfficeSaveData([FromBody] ITIGovtEM_OfficeSaveDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeSaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {


                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -1)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.OfficeID == 0)
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


        [HttpGet("ITIGovtEM_OfficeGetByID/{ID:int}")]
        public async Task<ApiResult<ITIGovtEM_OfficeSearchModel>> ITIGovtEM_OfficeGetByID(int ID)
        {
            ActionName = "ITIGovtEM_OfficeGetByID(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIGovtEM_OfficeSearchModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeGetByID(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITIGovtEM_OfficeSearchModel>(data);
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


        [HttpDelete("ITIGovtEM_OfficeDeleteById/{ID}/{userId}")]
        public async Task<ApiResult<bool>> ITIGovtEM_OfficeDeleteById(int ID, int userId)
        {
            ActionName = "ITIGovtEM_OfficeDeleteById(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeDeleteById(ID, userId);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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







        [HttpPost("GetAllITI_Govt_EM_OFFICERS")]
        public async Task<ApiResult<DataTable>> GetAllITI_Govt_EM_OFFICERS([FromBody] ITI_Govt_EM_OFFICERSSearchDataModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetAllITI_Govt_EM_OFFICERS(body);

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


        [HttpPost("ZonalOfficeCreateSSOID")]
        public async Task<ApiResult<int>> ZonalOfficeCreateSSOID([FromBody] UpdateSSOIDByPricipleModel request)
        {
            ActionName = "ZonalOfficeCreateSSOID([FromBody] UpdateSSOIDByPricipleModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ZonalOfficeCreateSSOID(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {


                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
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
                        if (request.StaffID == 0)
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


        [HttpPost("ITIGovtEM_PostGetAllData")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_PostGetAllData([FromBody] ITIGovtEM_PostSearchModel body)
        {

            ActionName = "ITIGovtEM_PostGetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_PostGetAllData(body);

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

        [HttpPost("ITIGovtEM_PostSaveData")]
        public async Task<ApiResult<int>> ITIGovtEM_PostSaveData([FromBody] ITIGovtEM_PostSaveDataModel request)
        {
            ActionName = "ITIGovtEM_PostSaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_PostSaveData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {


                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -1)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
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


        [HttpGet("ITIGovtEM_PostGetByID/{ID:int}")]
        public async Task<ApiResult<ITIGovtEM_PostSearchModel>> ITIGovtEM_PostGetByID(int ID)
        {
            ActionName = "ITIGovtEM_PostGetByID(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIGovtEM_PostSearchModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_PostGetByID(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITIGovtEM_PostSearchModel>(data);
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


        [HttpDelete("ITIGovtEM_PostDeleteById/{ID}/{userId}")]
        public async Task<ApiResult<bool>> ITIGovtEM_PostDeleteById(int ID, int userId)
        {
            ActionName = "ITIGovtEM_PostDeleteById(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_PostDeleteById(ID, userId);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_DELETE_ERROR;
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


        [HttpPost("ITIGovtEM_Govt_AdminT2Zonal_Save")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_Govt_AdminT2Zonal_Save([FromBody] List<ITI_Govt_EM_ZonalOFFICERSDataModel> body)
        {

            ActionName = "ITIGovtEM_Govt_AdminT2Zonal_Save()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_AdminT2Zonal_Save(body);
                _unitOfWork.SaveChanges();
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_SAVE_Duplicate;
                }
                else
                {                  

                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
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

        [HttpGet("ITIGovtEM_SSOIDCheck/{SSOID}")]
        public async Task<ApiResult<ITI_Govt_EM_ZonalOFFICERSDataModel>> ITIGovtEM_SSOIDCheck(string SSOID)
        {
            ActionName = "ITIGovtEM_SSOIDCheck(string SSOID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITI_Govt_EM_ZonalOFFICERSDataModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_SSOIDCheck(SSOID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITI_Govt_EM_ZonalOFFICERSDataModel>(data);
                        result.Data = mappedData;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        [HttpPost("ITIGovtEM_Govt_AdminT2Zonal_GetAllData")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_Govt_AdminT2Zonal_GetAllData([FromBody] ITI_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {

            ActionName = "ITIGovtEM_Govt_AdminT2Zonal_GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_AdminT2Zonal_GetAllData(body);

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


        [HttpPost("ITIGovtEM_Govt_StaffProfileQualification")]
        public async Task<ApiResult<int>> ITIGovtEM_Govt_StaffProfileQualification([FromBody] List<ITIGovtEMStaff_EducationalQualificationAndTechnicalQualificationModel> body)
        {

            ActionName = "ITIGovtEM_Govt_AdminT2Zonal_Save()";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_StaffProfileQualification(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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

        [HttpPost("ITIGovtEM_Govt_StaffProfileStaffPosting")]
        public async Task<ApiResult<int>> ITIGovtEM_Govt_StaffProfileStaffPosting([FromBody] List<StaffPostingData> body)
        {

            ActionName = "ITIGovtEM_Govt_AdminT2Zonal_Save()";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_StaffProfileStaffPosting(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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


        [HttpPost("ITIGovtEM_Govt_StaffProfileUpdate")]
        public async Task<ApiResult<int>> ITIGovtEM_Govt_StaffProfileUpdate([FromBody] ITIGovtEMStaff_PersonalDetailsModel body)
        {

            ActionName = "ITIGovtEM_Govt_StaffProfileUpdate([FromBody] ITIGovtEMStaff_PersonalDetailsModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_StaffProfileUpdate(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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



        [HttpPost("ITI_GOVT_EM_ApproveRejectStaff")]
        public async Task<ApiResult<bool>> ITI_GOVT_EM_ApproveRejectStaff([FromBody] RequestUpdateStatus request)
        {
            ActionName = "ITI_GOVT_EM_ApproveRejectStaff([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITI_GOVT_EM_ApproveRejectStaff(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StatusIDs == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StatusIDs == 0)
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


        [HttpPost("ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save([FromBody] List<ITI_Govt_EM_SanctionedPostBasedInstituteModel> body)
        {

            ActionName = "ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save([FromBody] List<ITI_Govt_EM_SanctionedPostBasedInstituteModel>   body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_PersonnelDetailsInstitutionsAccordingBudget_Save(body);
                _unitOfWork.SaveChanges();
                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_SAVE_Duplicate;
                }
                else
                {

                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
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

        [HttpPost("ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData([FromBody] ITI_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {

            ActionName = "ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData([FromBody] ITI_Govt_EM_ZonalOFFICERSSearchDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(body);

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


        [HttpPost("ITIGovtEM_Govt_RoleOfficeMapping_GetAllData")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_Govt_RoleOfficeMapping_GetAllData([FromBody] ITI_Govt_EM_RoleOfficeMappingSearchDataModel body)
        {

            ActionName = "ITIGovtEM_Govt_RoleOfficeMapping_GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_Govt_RoleOfficeMapping_GetAllData(body);

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


        [HttpPost("ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID")]
        public async Task<ApiResult<PersonalDetailByUserIDModel>> ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID([FromBody]  PersonalDetailByUserIDSearchModel body)
        {
            ActionName = "ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID([FromBody]  PersonalDetailByUserIDSearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<PersonalDetailByUserIDModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_ITI_Govt_Em_PersonalDetailByUserID(body);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<PersonalDetailByUserIDModel>(data);
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

        //[HttpDelete("ITIGovtEM_DeleteByIdStaffPromotionHistory/{ID}/{ModifyBy}")]
        //public async Task<ApiResult<bool>> ITIGovtEM_DeleteByIdStaffPromotionHistory(int ID, int ModifyBy)
        //{
        //    ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //var DeleteData_Request = new ScholarshipMaster
        //            //{
        //            //    ScholarshipID = ID,
        //            //    ModifyBy = ModifyBy,
        //            //};
        //            result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_DeleteByIdStaffPromotionHistory(ID, ModifyBy);
        //            _unitOfWork.SaveChanges();

        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DELETE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_DELETE_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}

        [HttpPost("ITIGovtEM_DeleteByIdStaffPromotionHistory")]
        public async Task<ApiResult<int>> ITIGovtEM_DeleteByIdStaffPromotionHistory([FromBody] ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body)
        {

            ActionName = "ITIGovtEM_DeleteByIdStaffPromotionHistory([FromBody] ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_DeleteByIdStaffPromotionHistory(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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


        [HttpGet("ITIGovtEM_ITI_Govt_EM_GetUserLevel/{ID}")]
        public async Task<ApiResult<DataTable>> ITIGovtEM_ITI_Govt_EM_GetUserLevel(int ID)
        {

            ActionName = "ITIGovtEM_ITI_Govt_EM_GetUserLevel()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_ITI_Govt_EM_GetUserLevel(ID);

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


        //[HttpDelete("ITI_Govt_EM_EducationalQualificationDeleteByID/{ID}/{ModifyBy}")]
        //public async Task<ApiResult<bool>> ITI_Govt_EM_EducationalQualificationDeleteByID(int ID, int ModifyBy)
        //{
        //    ActionName = "ITI_Govt_EM_EducationalQualificationDeleteByID(int PK_ID, int ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //var DeleteData_Request = new ScholarshipMaster
        //            //{
        //            //    ScholarshipID = ID,
        //            //    ModifyBy = ModifyBy,
        //            //};
        //            result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITI_Govt_EM_EducationalQualificationDeleteByID(ID, ModifyBy);
        //            _unitOfWork.SaveChanges();

        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DELETE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_DELETE_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}



        [HttpPost("ITI_Govt_EM_EducationalQualificationDeleteByID")]
        public async Task<ApiResult<int>> ITI_Govt_EM_EducationalQualificationDeleteByID([FromBody] ITIGovtEM_DeleteByIdStaffEducationDelete body)
        {

            ActionName = "ITI_Govt_EM_EducationalQualificationDeleteByID([FromBody] ITIGovtEM_DeleteByIdStaffPromotionHistoryDelete body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITI_Govt_EM_EducationalQualificationDeleteByID(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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



        //[HttpDelete("ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID/{ID}/{ModifyBy}")]
        //public async Task<ApiResult<bool>> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(int ID, int ModifyBy)
        //{
        //    ActionName = "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(int PK_ID, int ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
        //            //var DeleteData_Request = new ScholarshipMaster
        //            //{
        //            //    ScholarshipID = ID,
        //            //    ModifyBy = ModifyBy,
        //            //};
        //            result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(ID, ModifyBy);
        //            _unitOfWork.SaveChanges();

        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DELETE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_DELETE_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}


        [HttpPost("ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID")]
        public async Task<ApiResult<int>> ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID([FromBody] ITIGovtEM_DeleteByIdStaffServiceDelete body)
        {

            ActionName = "ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID([FromBody] ITIGovtEM_DeleteByIdStaffServiceDelete body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITI_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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






        [HttpGet("GetITI_Govt_EM_GetUserProfileStatus/{ID}")]
        public async Task<ApiResult<DataTable>> GetITI_Govt_EM_GetUserProfileStatus(int ID)
        {

            ActionName = "ITIGovtEM_ITI_Govt_EM_GetUserLevel()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetITI_Govt_EM_GetUserProfileStatus(ID);

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


        [HttpPost("FinalSubmitUpdateStaffProfileStatus")]
        public async Task<ApiResult<bool>> FinalSubmitUpdateStaffProfileStatus([FromBody] RequestUpdateStatus request)
        {
            ActionName = "FinalSubmitUpdateStaffProfileStatus([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.FinalSubmitUpdateStaffProfileStatus(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.StatusIDs == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.StatusIDs == 0)
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


        [HttpGet("ITIGovtEM_GetSSOID/{StaffId}")]
        public async Task<ApiResult<ITI_Govt_EM_ZonalOFFICERSDataModel>> ITIGovtEM_GetSSOID(int StaffId)
        {
            ActionName = "ITIGovtEM_GetSSOID(int StaffId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITI_Govt_EM_ZonalOFFICERSDataModel>();
                try
                {
                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_GetSSOID(StaffId);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITI_Govt_EM_ZonalOFFICERSDataModel>(data);
                        result.Data = mappedData;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_ADD_ERROR;
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



        [HttpPost("GetJoiningLetter")]
        public async Task<ApiResult<string>> GetJoiningLetter([FromBody] JoiningLetterSearchModel model)
        {
            ActionName = "GetJoiningLetter(string ApplicationID)";
            
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetJoiningLetter(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        ////report
                        //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images


                        


                        /*define table name for read and replace column from table*/
                        data.Tables[0].TableName = "Joining_Details";
                        


                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.JoiningLetterITI}/JoiningLetterForm.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1);

                        // Example: Send in API
                        //return File(pdfBytes, "application/pdf", "Generated.pdf");


                        ///string dataUri = "data:application/pdf;base64," + base64String;
                        result.Data =  Convert.ToBase64String(pdfBytes); ;
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

            [HttpPost("GetRelievingLetter")]
        public async Task<ApiResult<string>> GetRelievingLetter([FromBody] RelievingLetterSearchModel model)
        {
            ActionName = "GetRelievingLetter(string ApplicationID)";
            
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetRelievingLetter(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {
                        //var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                        ////report
                        //var fileName = $"JoiningLetter_{model.UserID}_{model.StaffID}.pdf";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ApplicationFormPreview.rdlc";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //images


                        


                        /*define table name for read and replace column from table*/
                        data.Tables[0].TableName = "Relieving_Details";
                        


                        string devFontSize = "15px";
                        /*default font size for kruti dev*/
                        //string fontSize = "font-size: 10px;";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.JoiningLetterITI}/RelievingLetterForm.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1);

                        // Example: Send in API
                        //return File(pdfBytes, "application/pdf", "Generated.pdf");


                        ///string dataUri = "data:application/pdf;base64," + base64String;
                        result.Data =  Convert.ToBase64String(pdfBytes); ;
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


        [HttpPost("GetITI_Govt_CheckDistrictNodalOffice")]
        public async Task<ApiResult<DataTable>> GetITI_Govt_CheckDistrictNodalOffice(CheckDistrictNodalOfficeSearchModel model)
        {


            ActionName = "GetITI_Govt_CheckDistrictNodalOffice()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetITI_Govt_CheckDistrictNodalOffice(model);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = result.Data.Rows[0]["ErrorMessage"].ToString() ?? ""; 
                }
                else
                {                    
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
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


        //[HttpDelete("ITIGovtEM_OfficeDelete/{ID}/{ModifyBy}")]
        //public async Task<ApiResult<bool>> ITIGovtEM_OfficeDelete(int ID, int ModifyBy)
        //{
        //    ActionName = "ITIGovtEM_OfficeDelete(int PK_ID, int ModifyBy)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {
                    
        //            result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeDelete(ID, ModifyBy);
        //            _unitOfWork.SaveChanges();

        //            if (result.Data)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DELETE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_DELETE_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}


        [HttpPost("ITIGovtEM_OfficeDelete")]
        public async Task<ApiResult<int>> ITIGovtEM_OfficeDelete([FromBody] ITIGovtEM_OfficeDeleteModel body)
        {

            ActionName = "ITIGovtEM_OfficeDelete([FromBody] ITIGovtEM_OfficeDelete body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.ITIGovtEM_OfficeDelete(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
                    }
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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


        [HttpPost("GetITI_Govt_EM_UserProfileStatusHt")]
        public async Task<ApiResult<DataTable>> GetITI_Govt_EM_UserProfileStatusHt([FromBody] ITI_Govt_EM_UserRequestHistoryListSearchDataModel request)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIGovtEMStaffMasterRepository.GetITI_Govt_EM_UserProfileStatusHt(request);

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


    }

}
