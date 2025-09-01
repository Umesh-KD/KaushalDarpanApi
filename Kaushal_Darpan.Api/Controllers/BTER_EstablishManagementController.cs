using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.StaffDashboard;
using Kaushal_Darpan.Models.StaffMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class BTER_EstablishManagementController : BaseController
    {
        public override string PageName => "BTER_EstablishManagementController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BTER_EstablishManagementController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("BTER_EM_AddStaffInitialDetails")]
        public async Task<ApiResult<int>> BTER_EM_AddStaffInitialDetails([FromBody] BTER_EM_AddStaffInitialDetailsDataModel body)
        {

            ActionName = "ITIGovtEM_Govt_AdminT2Zonal_Save()";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_AddStaffInitialDetails(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -2)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Principle Already Exists for Selected Institute";
                }
                else if (result.Data == -5)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "SSOID or Added User Already Exists in system";
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

        [HttpPost("BTER_EM_GetStaffList")]
        public async Task<ApiResult<DataTable>> BTER_EM_GetStaffList([FromBody] BTER_EM_GetStaffListDataModel body)
        {

            ActionName = "BTER_EM_GetStaffList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_GetStaffList(body);

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

        [HttpPost("BTER_EM_AddStaffPrinciple")]
        public async Task<ApiResult<int>> BTER_EM_AddStaffPrinciple([FromBody] BTER_EM_AddStaffPrincipleDataModel request)
        {
            ActionName = "BTER_EM_AddStaffPrinciple([FromBody] BTER_EM_AddStaffPrincipleDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_AddStaffPrinciple(request);
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

        [HttpPost("BTER_EM_GetPrincipleStaff")]
        public async Task<ApiResult<DataTable>> BTER_EM_GetPrincipleStaff([FromBody] BTER_EM_StaffMasterSearchModel body)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_GetPrincipleStaff(body);

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

        [HttpPost("BTER_EM_GetPersonalDetailByUserID")]
        public async Task<ApiResult<DataTable>> BTER_EM_GetPersonalDetailByUserID([FromBody] BTER_EM_GetPersonalDetailByUserID body)
        {

            ActionName = "BTER_EM_GetStaffList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_GetPersonalDetailByUserID(body);

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

        [HttpPost("BTER_EM_AddStaffDetails")]
        public async Task<ApiResult<int>> BTER_EM_AddStaffDetails([FromBody] BTER_EM_AddStaffDetailsDataModel body)
        {

            ActionName = "BTER_EM_AddStaffBasicDetails([FromBody] BTER_EM_AddStaffDetailsDataModel body)";
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_AddStaffDetails(body);
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
                    if (body.UserID == 0)
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
            
        }

        [HttpPost("BTER_EM_DeleteStaff")]
        public async Task<ApiResult<int>> BTER_EM_DeleteStaff([FromBody] ITIGovtEM_OfficeDeleteModel body)
        {

            ActionName = " BTER_EM_DeleteStaff([FromBody] ITIGovtEM_OfficeDeleteModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_DeleteStaff(body);
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

        [HttpPost("BTER_EM_ApproveStaffProfile")]
        public async Task<ApiResult<int>> BTER_EM_ApproveStaffProfile([FromBody] BTER_EM_ApproveStaffDataModel body)
        {

            ActionName = "BTER_EM_ApproveStaffProfile([FromBody] BTER_EM_ApproveStaffDataModel body)";
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_ApproveStaffProfile(body);
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
                    if (body.UserID == 0)
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

        }

        [HttpPost("BTER_EM_UnlockProfile")]
        public async Task<ApiResult<bool>> BTER_EM_UnlockProfile([FromBody] BTER_EM_UnlockProfileDataModel request)
        {
            ActionName = " BTER_EM_UnlockProfile([FromBody] BTER_EM_UnlockProfileDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_UnlockProfile(request);
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

        [HttpGet("BTER_EM_InstituteDDL/{DepartmentID}/{InsType}/{DistrictId}")]
        public async Task<ApiResult<DataTable>> BTER_EM_InstituteDDL(int DepartmentID, int InsType, int DistrictId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_InstituteDDL(DepartmentID, InsType, DistrictId);
                    if (data.Rows.Count > 0)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
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





        // BTER New

        [HttpPost("BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID")]
        public async Task<ApiResult<int>> BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID([FromBody] BTERGovtEM_DeleteByIdStaffServiceDelete body)
        {

            ActionName = "BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID([FromBody] BTERGovtEM_DeleteByIdStaffServiceDelete body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(body);
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

        [HttpGet("GetBTER_Govt_EM_GetUserProfileStatus/{ID}")]
        public async Task<ApiResult<DataTable>> GetBTER_Govt_EM_GetUserProfileStatus(int ID)
        {

            ActionName = "BTERGovtEM_BTER_Govt_EM_GetUserLevel()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.GetBTER_Govt_EM_GetUserProfileStatus(ID);

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

        [HttpPost("BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID")]
        public async Task<ApiResult<BTERPersonalDetailByUserIDSearchModel>> BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID([FromBody] BTERPersonalDetailByUserIDSearchModel body)
        {
            ActionName = "BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID([FromBody]  PersonalDetailByUserIDSearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<BTERPersonalDetailByUserIDSearchModel>();
                try
                {
                    var data = await _unitOfWork.BTER_EstablishManagementRepository.BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID(body);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<BTERPersonalDetailByUserIDSearchModel>(data);
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
        [HttpPost("BTERGovtEM_BTER_Govt_Em_PersonalDetailList")]
        public async Task<ApiResult<BTERPersonalDetailByUserIDSearchModel>> BTERGovtEM_BTER_Govt_Em_PersonalDetailList([FromBody] BTERPersonalDetailByUserIDSearchModel body)
        {
            ActionName = "BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID([FromBody]  PersonalDetailByUserIDSearchModel body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<BTERPersonalDetailByUserIDSearchModel>();
                try
                {
                    var data = await _unitOfWork.BTER_EstablishManagementRepository.BTERGovtEM_BTER_Govt_Em_PersonalDetailList(body);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<BTERPersonalDetailByUserIDSearchModel>(data);
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


        [HttpPost("BTERGovtEM_Govt_StaffProfileStaffPosting")]
        public async Task<ApiResult<int>> BTERGovtEM_Govt_StaffProfileStaffPosting([FromBody] List<StaffPostingData> body)
        {

            ActionName = "BTERGovtEM_Govt_AdminT2Zonal_Save()";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.BTERGovtEM_Govt_StaffProfileStaffPosting(body);
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

        [HttpPost("FinalSubmitUpdateStaffProfileStatus")]
        public async Task<ApiResult<bool>> FinalSubmitUpdateStaffProfileStatus([FromBody] RequestUpdateStatus request)
        {
            ActionName = "FinalSubmitUpdateStaffProfileStatus([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EstablishManagementRepository.FinalSubmitUpdateStaffProfileStatus(request);
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

        [HttpPost("GetBter_Govt_EM_UserProfileStatusHt")]
        public async Task<ApiResult<DataTable>> GetBter_Govt_EM_UserProfileStatusHt([FromBody] Bter_Govt_EM_UserRequestHistoryListSearchDataModel request)
        {
            ActionName = "GetBter_Govt_EM_UserProfileStatusHt([FromBody] Bter_Govt_EM_UserRequestHistoryListSearchDataModel request)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.GetBter_Govt_EM_UserProfileStatusHt(request);

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


        [HttpPost("Bter_GOVT_EM_ApproveRejectStaff")]
        public async Task<ApiResult<bool>> Bter_GOVT_EM_ApproveRejectStaff([FromBody] RequestUpdateStatus request)
        {
            ActionName = "Bter_GOVT_EM_ApproveRejectStaff([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EstablishManagementRepository.Bter_GOVT_EM_ApproveRejectStaff(request);
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

        [HttpGet("BterStaffSubjectListModel/{PK_ID}/{DepartmentID}")]
        public async Task<ApiResult<BTER_EM_AddStaffDetailsDataModel>> BterStaffSubjectListModel(int PK_ID, int DepartmentID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<BTER_EM_AddStaffDetailsDataModel>();
                try
                {
                    var data = await _unitOfWork.BTER_EstablishManagementRepository.BTER_EM_GetBterStaffSubjectListModelStaffID(PK_ID, DepartmentID);
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

        [HttpPost("GetHODDash")]
        public async Task<ApiResult<DataTable>> GetHODDash([FromBody] HODDashboardSearchModel model)

        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.GetHODDash(model);
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

        [HttpPost("GetStaff_HostelIDs")]
        public async Task<ApiResult<DataTable>> GetStaff_HostelIDs(StaffHostelSearchModel model)
        {
            ActionName = "GetStaff_HostelIDs(StaffHostelSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EstablishManagementRepository.GetStaff_HostelIDs(model);

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

        [HttpPost("SaveStaff_HostelIDs")]
        public async Task<ApiResult<bool>> SaveStaff_HostelIDs([FromBody] StaffHostelSearchModel request)
        {
            ActionName = "Bter_GOVT_EM_ApproveRejectStaff([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EstablishManagementRepository.SaveStaff_HostelIDs(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
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
    }
}
