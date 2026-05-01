using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class BTER_EM_StaffServiceDetailsController : BaseController
    {
        public override string PageName => "BTER_EM_StaffServiceDetailsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BTER_EM_StaffServiceDetailsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Save_StaffTrainingDetails")]
        public async Task<ApiResult<int>> Save_StaffTrainingDetails([FromBody] StaffTrainingDetailDataModel body)
        {

            ActionName = "Save_StaffTrainingDetails([FromBody] StaffTrainingDetailDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.Save_StaffTrainingDetails(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("StaffTrainingDetails_GetData")]
        public async Task<ApiResult<DataTable>> StaffTrainingDetails_GetData([FromBody] StaffTrainingDetailSearchData body)
        {
            ActionName = "StaffTrainingDetails_GetData([FromBody] StaffTrainingDetailSearchData body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingDetails_GetData(body);

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

        [HttpPost("StaffTrainingDetails_DeleteById")]
        public async Task<ApiResult<bool>> StaffTrainingDetails_DeleteById([FromBody] StaffTrainingDetailSearchData request)
        {
            ActionName = " StaffTrainingDetails_DeleteById([FromBody] StaffTrainingDetailSearchData request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingDetails_DeleteById(request);
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

        [HttpPost("StaffTrainingStatusUpdate")]
        public async Task<ApiResult<int>> StaffTrainingStatusUpdate([FromBody] StaffTrainingStatusUpdateDataModel body)
        {

            ActionName = "StaffTrainingStatusUpdate([FromBody] StaffTrainingStatusUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingStatusUpdate(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("StaffTrainingHTS_GetData")]
        public async Task<ApiResult<DataTable>> StaffTrainingHTS_GetData([FromBody] StaffTrainingDetailSearchData body)
        {
            ActionName = "StaffTrainingHTS_GetData([FromBody] StaffTrainingDetailSearchData body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingHTS_GetData(body);

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

        [HttpPost("StaffTrainingDocUpdate")]
        public async Task<ApiResult<int>> StaffTrainingDocUpdate([FromBody] StaffTrainingDetailDataModel body)
        {

            ActionName = "StaffTrainingDocUpdate([FromBody] StaffTrainingDetailDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingDocUpdate(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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



        //// BTER Staff Transfer System

        [HttpPost("GetStaffPersonalDetails")]
        public async Task<ApiResult<DataTable>> GetStaffPersonalDetails([FromBody] BTER_GetStaffPersonalDetailsModel body)
        {

            ActionName = "GetStaffPersonalDetails([FromBody] BTER_GetStaffPersonalDetailsModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.GetStaffPersonalDetails(body);

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


        [HttpPost("BTER_EM_TransferSystem_IU")]
        public async Task<ApiResult<int>> BTER_EM_TransferSystem_IU([FromBody] BTER_EM_TransferSystemModule body)
        {
            ActionName = "BTER_EM_TransferSystem_IU([FromBody] BTER_EM_TransferSystemModule body)";
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.BTER_EM_TransferSystem_IU(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("GetEM_TransferSystemData")]
        public async Task<ApiResult<DataTable>> GetEM_TransferSystemData([FromBody] EM_TransferSystemSearchModel body)
        {

            ActionName = "GetEM_TransferSystemData([FromBody] EM_TransferSystemSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.GetEM_TransferSystemData(body);

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


        [HttpPost("EM_TransferSystemUpdatePocessManage")]
        public async Task<ApiResult<bool>> EM_TransferSystemUpdatePocessManage([FromBody] EM_TransferSystemSearchModel request)
        {
            ActionName = " EM_TransferSystemUpdatePocessManage([FromBody] EM_TransferSystemSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.EM_TransferSystemUpdatePocessManage(request);
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


        [HttpPost("EM_TransferSystemUpdateStatus")]
        public async Task<ApiResult<int>> EM_TransferSystemUpdateStatus([FromBody] TransferSystemUpdateDataModel body)
        {

            ActionName = "EM_TransferSystemUpdateStatus([FromBody] StaffTrainingStatusUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.EM_TransferSystemUpdateStatus(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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


        [HttpPost("TransferSystemEXTStatusUpdate")]
        public async Task<ApiResult<int>> TransferSystemEXTStatusUpdate([FromBody] TransferSystemUpdateDataModel body)
        {

            ActionName = "TransferSystemEXTStatusUpdate([FromBody] TransferSystemUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.TransferSystemEXTStatusUpdate(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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
    }
}
