using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.HrMaster;

using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.ViewPlacedStudents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

using System.Net.Http.Headers;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class StaffMasterController : BaseController
    {
        public override string PageName => "StaffMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StaffMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] StaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAllData(body);

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
       
        [HttpPost("StaffBasicDetails")]
        public async Task<ApiResult<int>> SaveBasicData([FromBody] AddStaffBasicDetailDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.SaveBasicData(request);
                    await _unitOfWork.SaveChangesAsync();
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
       
        [HttpPost("StaffDetails")]
        public async Task<ApiResult<bool>> SaveData([FromBody] StaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] StaffMasterModel request)";
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


                    result.Data = await _unitOfWork.StaffMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
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
       
        [HttpGet("GetByID/{PK_ID}/{DepartmentID}")]
        public async Task<ApiResult<StaffMasterModel>> GetByID(int PK_ID, int DepartmentID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<StaffMasterModel>();
                try
                {
                    var data = await _unitOfWork.StaffMasterRepository.GetById(PK_ID, DepartmentID);
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


        [HttpPost("GetAllStudentPersentData")]
        public async Task<ApiResult<DataTable>> GetAllStudentPersentData([FromBody] StaffMasterSearchModel body)
        {
            ActionName = "GetAllStudentPersentData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.GetAllStudentPersentData(body);

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


        [HttpPost("GetStudentEnrCancelRequestData")]
        public async Task<ApiResult<DataTable>> GetStudentEnrCancelRequestData([FromBody] StaffMasterSearchModel body)
        {
            ActionName = "GetStudentEnrCancelRequestData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.GetStudentEnrCancelRequestData(body);

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


        [HttpPost("ApporveOrRejectStudentEnrCancelRequest")]
        public async Task<ApiResult<DataTable>> ApporveOrRejectStudentEnrCancelRequest([FromBody] StudentEnrCancelReqModel body)
        {
            ActionName = "ApporveOrRejectStudentEnrCancelRequest()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.ApporveOrRejectStudentEnrCancelRequest(body);

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

        [HttpPost("GetAllTotalExaminerData")]
        public async Task<ApiResult<DataTable>> GetAllTotalExaminerData([FromBody] StaffMasterSearchModel body)
        {
            ActionName = "GetAllTotalExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffDashboardRepository.GetAllTotalExaminerData(body);

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

        [HttpPost("LockandSubmit")]
        public async Task<ApiResult<bool>> LockandSubmit([FromBody] StaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.LockandSubmit(request);
                    await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("UnlockStaff")]
        public async Task<ApiResult<bool>> UnlockStaff([FromBody] StaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.UnlockStaff(request);
                    await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("ApproveStaff")]
        public async Task<ApiResult<bool>> ApproveStaff([FromBody] StaffMasterModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.ApproveStaff(request);
                    await _unitOfWork.SaveChangesAsync();
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
        public async Task<StaffMasterModel> GetSsoDetaislBySSOId(string SSOId)
        {
            try
            {

                StaffMasterModel data = new StaffMasterModel();

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
                            data = new StaffMasterModel()
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
        public async Task<ApiResult<DataTable>> StaffLevelType([FromBody] StaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.StaffLevelType(body);

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
        
        [HttpPost("StaffLevelChild")]
        public async Task<ApiResult<DataTable>> StaffLevelChild([FromBody] StaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.StaffLevelChild(body);

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




        [HttpPost("IsDownloadCertificate")]
        public async Task<ApiResult<bool>> IsDownloadCertificate([FromBody] StaffMasterModel request)
        {
            ActionName = "IsDownloadCertificate([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.IsDownloadCertificate(request);
                    await _unitOfWork.SaveChangesAsync();
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


        [HttpDelete("IsDeleteHostelWarden/{SSOID}")]
        public async Task<ApiResult<int>> IsDeleteHostelWarden(string SSOID)
        {
            ActionName = "IsDeleteHostelWarden(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StaffMasterRepository.IsDeleteHostelWarden(SSOID);
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

        [HttpPost("ChangeWorkingInstitute")]
        public async Task<ApiResult<bool>> ChangeWorkingInstitute([FromBody] StaffMasterModel request)
        {
            ActionName = "ChangeWorkingInstitute([FromBody] StaffMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.ChangeWorkingInstitute(request);
                    await _unitOfWork.SaveChangesAsync();
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

        [HttpPost("GetCurrentWorkingInstitute_ByID")]
        public async Task<ApiResult<DataTable>> GetCurrentWorkingInstitute_ByID([FromBody] StaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetCurrentWorkingInstitute_ByID(body);

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


        [HttpPost("AllBranchHOD")]
        public async Task<ApiResult<DataTable>> SaveBranchHOD(BranchHODModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.SaveBranchHOD(body);

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
    
        [HttpPost("SaveBranchSectionData")]
        public async Task<ApiResult<bool>> SaveBranchSectionData([FromBody] SectionDataModel body)
        {
            ActionName = "SaveBranchSectionData()";
            var result = new ApiResult<bool>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.SaveBranchSectionData(body);

                if (result.Data)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }


                //GetSectionDataModel getSectionDataModel = new GetSectionDataModel();
                //getSectionDataModel.StreamID = body.StreamID;
                //getSectionDataModel.DepartmentID = body.DepartmentID;
                //getSectionDataModel.EndTermID = body.EndTermID;
                //getSectionDataModel.Eng_NonEng = body.Eng_NonEng;
                //getSectionDataModel.SemesterID = body.SemesterID;
                //getSectionDataModel.InstituteId = body.InstituteId;
                //getSectionDataModel.Action = "GET_BY_ID";

                //var BranchStudentList = new ApiResult<DataTable>();
                //BranchStudentList.Data = await _unitOfWork.StaffMasterRepository.GetBranchStudentData(getSectionDataModel);
                //List<GetSectionStudentDataModel> getSectionStudentDataModels = new List<GetSectionStudentDataModel>();

                //foreach (DataRow row in BranchStudentList.Data.Rows)
                //{
                //    var student = new GetSectionStudentDataModel
                //    {
                //        StudentID = row["StudentID"] != DBNull.Value ? Convert.ToInt32(row["StudentID"]) : 0,
                //        EnrollmentNo = row["EnrollmentNo"] != DBNull.Value ? Convert.ToString(row["EnrollmentNo"]) : string.Empty,
                //        StreamID = row["StreamID"] != DBNull.Value ? Convert.ToInt32(row["StreamID"]) : 0,
                //        ApplicationID = row["ApplicationID"] != DBNull.Value ? Convert.ToInt32(row["ApplicationID"]) : 0
                //    };

                //    getSectionStudentDataModels.Add(student);
                //}


                //var BranchSectionList = new ApiResult<DataTable>();
                //BranchSectionList.Data = await _unitOfWork.StaffMasterRepository.GetBranchSectionData(getSectionDataModel);
                //List<GetSectionDataModel> getSectionDataList = new List<GetSectionDataModel>();
                //foreach (DataRow row in BranchSectionList.Data.Rows)
                //{
                //    var section = new GetSectionDataModel
                //    {
                //        SectionID = row.Field<int?>("SectionID") ?? 0,
                //        DepartmentID = row.Field<int?>("DepartmentID") ?? 0,
                //        EndTermID = row.Field<int?>("EndTermID") ?? 0,
                //        Eng_NonEng = row.Field<int?>("Eng_NonEng") ?? 0,
                //        StreamID = row.Field<int?>("StreamID") ?? 0,
                //        SemesterID = row.Field<int?>("SemesterID") ?? 0,
                //        StudentCount = row.Field<int?>("StudentCount") ?? 0,
                //        ActiveStatus = row.Field<bool?>("ActiveStatus") ?? false,
                //        DeleteStatus = row.Field<bool?>("DeleteStatus") ?? false,
                //        CreatedBy = row.Field<int?>("CreatedBy") ?? 0,
                //        ModifyBy = row.Field<int?>("ModifyBy") ?? 0,
                //        CreatedDate = row.Field<DateTime?>("CreatedDate") ?? DateTime.MinValue
                //    };

                //    getSectionDataList.Add(section);
                //}


                //List<AllSectionBranchStudentDataModel> allSectionBranchStudentDataModel = new List<AllSectionBranchStudentDataModel>();


                //int totalStudents = getSectionStudentDataModels.Count;

                //List<int> studentIDsToAssign = getSectionStudentDataModels.Select(s => s.StudentID).ToList();






                //foreach (var section in getSectionDataList)
                //{
                //    int studentIndex = 0;


                //    if (section.StudentCount <= 0)
                //        continue;


                //    int remainingStudents = section.StudentCount - studentIndex;
                //    if (remainingStudents <= 0)
                //        break; 


                //    int assignCount = Math.Min(section.StudentCount, remainingStudents);

                //    for (int i = 0; i < assignCount; i++)
                //    {
                //        var student = getSectionStudentDataModels[studentIndex];
                //        var combined = new AllSectionBranchStudentDataModel
                //        {
                //            StudentID = student.StudentID,
                //            EnrollmentNo = student.EnrollmentNo,
                //            StreamID = student.StreamID,
                //            ApplicationID = student.ApplicationID,
                //            SectionID = section.SectionID,
                //            DepartmentID = section.DepartmentID,
                //            EndTermID = section.EndTermID,
                //            Eng_NonEng = section.Eng_NonEng,
                //            ActiveStatus = section.ActiveStatus ?? false,
                //            DeleteStatus = section.DeleteStatus ?? false,
                //            CreatedBy = section.CreatedBy,
                //            ModifyBy = section.ModifyBy,
                //            CreatedDate = section.CreatedDate
                //        };

                //        allSectionBranchStudentDataModel.Add(combined);

                //    }
                //    studentIndex++;
                //}

                //var Data = await _unitOfWork.StaffMasterRepository.SaveBranchSectionEnrollmentData(allSectionBranchStudentDataModel);
                //if (result.Data)
                //{
                //    result.State = EnumStatus.Success;
                //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                //}
                //else
                //{
                //    result.State = EnumStatus.Warning;
                //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                //}                
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

               
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

        [HttpPost("GetBranchSectionData")]
        public async Task<ApiResult<DataTable>> GetBranchSectionData([FromBody] GetSectionDataModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetBranchSectionData(body);

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
        
        [HttpPost("GetBranchSectionEnrollmentData")]
        public async Task<ApiResult<DataTable>> GetBranchSectionEnrollmentData([FromBody] GetSectionBranchStudentDataModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetBranchSectionEnrollmentData(body);

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

        

        [HttpPost("GetAllRosterDisplay")]
        public async Task<ApiResult<DataTable>> GetAllRosterDisplay([FromBody] GetAllRosterDisplayModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAllRosterDisplay(body);

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



        [HttpPost("GetAllRoomReport")]
        public async Task<ApiResult<DataTable>> GetAllRoomReport([FromBody] GetAllRosterDisplayModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAllRoomReport(body);

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



        [HttpPost("GetAllRoomUtilizationReport")]
        public async Task<ApiResult<DataTable>> GetAllRoomUtilizationReport([FromBody] GetAllRosterDisplayModel body)
        {
            ActionName = "SaveBranchHOD()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAllRoomUtilizationReport(body);

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




        [HttpPost("SaveRosterDisplay")]
        public async Task<ApiResult<int>> SaveRosterDisplay([FromBody] SaveRosterDisplayModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.SaveRosterDisplay(request);
                    await _unitOfWork.SaveChangesAsync();
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
                        result.ErrorMessage = "The selected Application range consist of Appication Number that is already assigned";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("GetStreamIDBySemester")]
        public async Task<ApiResult<DataTable>> GetStreamIDBySemester([FromBody] SearchBranchDataModel body)
        {

            ActionName = "GetStreamIDBySemester()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetStreamIDBySemester(body);

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

        [HttpPost("SaveRosterDisplayMultiple")]
        public async Task<ApiResult<int>> SaveRosterDisplayMultiple([FromBody] List<SaveRosterDisplayMultipleModel> request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.SaveRosterDisplayMultiple(request);
                    await _unitOfWork.SaveChangesAsync();
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
                        result.Message = "This record already exists in the selected application range.";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [HttpPost("DeleteRosterDisplay")]
        public async Task<ApiResult<int>> DeleteRosterDisplay([FromBody] SaveRosterDisplayMultipleModel request)
        {
            ActionName = "DeleteRosterDisplay([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StaffMasterRepository.DeleteRosterDisplay(request);
                    await _unitOfWork.SaveChangesAsync();
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
                    
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("GetAssignedTeacherForSubject_BySecctionID")]
        public async Task<ApiResult<DataTable>> GetAssignedTeacherForSubject_BySecctionID([FromBody] GetAssignedTeacherForSubjectDataModel body)
        {

            ActionName = " GetAssignedTeacherForSubject_BySecctionID([FromBody] GetAssignedTeacherForSubjectDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAssignedTeacherForSubject_BySecctionID(body);

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


        [HttpPost("GetAssignedTeacherForSubject")]
        public async Task<ApiResult<DataTable>> GetAssignedTeacherForSubject([FromBody] GetAssignedTeacherForSubjectDataModel body)
        {

            ActionName = " GetAssignedTeacherForSubject([FromBody] GetAssignedTeacherForSubjectDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAssignedTeacherForSubject(body);

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


        [HttpPost("GetBranchStudentData")]
        public async Task<ApiResult<List<GetSectionStudentDataModel>>> GetBranchStudentData([FromBody] SectionDataModel body)
        {
            ActionName = "GetBranchStudentData()";
            var result = new ApiResult<List<GetSectionStudentDataModel>>();
            try
            {
                GetSectionDataModel getSectionDataModel = new GetSectionDataModel
                {
                    StreamID = body.StreamID,
                    DepartmentID = body.DepartmentID,
                    EndTermID = body.EndTermID,
                    Eng_NonEng = body.Eng_NonEng,
                    SemesterID = body.SemesterID,
                    InstituteId = body.InstituteId,
                    Action = "GET_BY_ID"
                };

                var BranchStudentList = new ApiResult<DataTable>();
                BranchStudentList.Data = await _unitOfWork.StaffMasterRepository.GetBranchStudentData(getSectionDataModel);

                if (BranchStudentList.Data != null && BranchStudentList.Data.Rows.Count > 0)
                {
                    List<GetSectionStudentDataModel> getSectionStudentDataModels = new List<GetSectionStudentDataModel>();

                    foreach (DataRow row in BranchStudentList.Data.Rows)
                    {
                        var student = new GetSectionStudentDataModel
                        {
                            StudentID = row["StudentID"] != DBNull.Value ? Convert.ToInt32(row["StudentID"]) : 0,
                            EnrollmentNo = row["EnrollmentNo"] != DBNull.Value ? Convert.ToString(row["EnrollmentNo"]) : string.Empty,
                            StreamID = row["StreamID"] != DBNull.Value ? Convert.ToInt32(row["StreamID"]) : 0,
                            ApplicationID = row["ApplicationID"] != DBNull.Value ? Convert.ToInt32(row["ApplicationID"]) : 0
                        };

                        getSectionStudentDataModels.Add(student);
                    }
                    result.Data = getSectionStudentDataModels;
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


        [HttpPost("GetBranchSectionAcRosterData")]
        public async Task<ApiResult<DataTable>> GetBranchSectionAcRosterData([FromBody] GetDDlSectionDataModel body)
        {
            ActionName = "GetBranchSectionAcRosterData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetBranchSectionAcRosterData(body);

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


        [HttpPost("GetAssignedTeacher_SSOData")]
        public async Task<ApiResult<DataTable>> GetAssignedTeacher_SSOData([FromBody] GetDDlSectionDataModel body)
        {
            ActionName = "GetAssignedTeacher_SSOData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetAssignedTeacher_SSOData(body);

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


        [HttpPost("GetHODWiseSemester")]
        public async Task<ApiResult<DataTable>> GetHODWiseSemester([FromBody] GetHODWiseSemesterDataModel body)
        {
            ActionName = "GetHODWiseSemester()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StaffMasterRepository.GetHODWiseSemester(body);

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
        [HttpPost("InsertStaffAssignmentHierarchy")]
        public async Task<ApiResult<DataTable>> InsertStaffAssignmentHierarchy([FromBody] InsertStaffAssignmentHierarchyModel body)
        {
            ActionName = "InsertStaffAssignmentHierarchy()";

            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.StaffMasterRepository.InsertStaffAssignmentHierarchy(body);

                if (result.Data.Rows.Count > 0)
                {
                    var status = Convert.ToInt32(result.Data.Rows[0]["Status"]);
                    var message = result.Data.Rows[0]["Message"].ToString();

                    if (status == 1)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = message;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = message;
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
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

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
        [HttpPost("GetStaffAssignmentHierarchy")]
        public async Task<ApiResult<DataTable>> GetStaffAssignmentHierarchy([FromBody] GetStaffAssignmentHierarchyModel body)
        {
            ActionName = "GetStaffAssignmentHierarchy()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.StaffMasterRepository.GetStaffAssignmentHierarchy(body);

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

        [HttpPost("GetStaffAssignmentHistory")]
        public async Task<ApiResult<DataTable>> GetStaffAssignmentHistory([FromBody] StaffAssignmentHistoryModel body)
        {
            ActionName = "GetStaffAssignmentHistory()";

            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.StaffMasterRepository.GetStaffAssignmentHistory(body);

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

    }
}
