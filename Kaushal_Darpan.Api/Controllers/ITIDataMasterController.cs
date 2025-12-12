using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using ExcelDataReader;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIDataMasterController : BaseController
    {
        public override string PageName => "ITIDataMasterController   ";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIDataMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("GetAllData")]
        public async Task<ApiResult<string>> GetAllData(DataListSearchModel request)
        {
            // (`ITIINSTITUE:DSP@@pMzxalWNz77kZXXW8hQ==`)
            // 'SVRJSU5TVElUVUU6RFNQQEBwTXp4YWxXTno3N2taWFhXOGhRPT0='

            ActionName = "GetAllData(SeatIntakeSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Unauthorized Request";
                    return result;
                }

                // Ensure it starts with "Basic "
                var authHeaderValue = authHeader.ToString();
                if (!authHeaderValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Unauthorized Request";
                    return result;
                }

                // Extract base64 part (after "Basic ")
                var base64Part = authHeaderValue.Substring("Basic ".Length).Trim();

                try
                {
                    var decodedBytes = Convert.FromBase64String(base64Part);
                    var decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);

                    // Split into username & password
                    var parts = decodedString.Split(':', 2);
                    var username = parts.Length > 0 ? parts[0] : string.Empty;
                    var password = parts.Length > 1 ? parts[1] : string.Empty;
                    if (username != "ITIINSTITUE" && password != "DSP@@pMzxalWNz77kZXXW8hQ==")
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "User not Valid";
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "Unauthorized Request";
                    return result;
                }
                try
                {
                    var data = await _unitOfWork.ITIDataMasterRepository.GetAllData(request);

                    if (data.Rows[0]["data"] != null)
                    {

                        if (!string.IsNullOrEmpty(Convert.ToString(data.Rows[0]["data"])))
                        {

                            result.Data = data.Rows[0]["data"].ToString();
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                        }
                        else
                        {
                            if (request.RequestType == "UserNotValid")
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = "User not Valid";
                            }
                            else
                            {
                                result.State = EnumStatus.Warning;
                                result.Message = Constants.MSG_DATA_NOT_FOUND;
                            }

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


        #region ncvt student corrected master api's

        [HttpPost("GetStudentCorrectionListData")]
        public async Task<ApiResult<DataTable>> GetStudentCorrectionListData([FromBody] StudentCorrectionMasterSearchModel body)
        {
            ActionName = "GetStudentCorrectionListData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIDataMasterRepository.GetStudentCorrectionListData(body);

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

        [HttpPost("GetStudentCorrectionDataByID")]
        public async Task<ApiResult<DataTable>> GetStudentCorrectionDataByID([FromBody] StudentCorrectionMasterSearchModel body)

        {
            ActionName = "GetStudentCorrectionDataByID()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIDataMasterRepository.GetStudentCorrectionDataByID(body);

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


        [HttpPost("SaveStudentCorrectionData")]
        public async Task<ApiResult<bool>> SaveStudentCorrectionData([FromBody] StudentCorrectionMasterSearchModel request)
        {
            ActionName = "SaveStudentCorrectionData([FromBody] ItiCompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.ITIDataMasterRepository.SaveStudentCorrectionData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.CandidateID == 0)
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
                        if (request.CandidateID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else
                        {
                            //result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                            result.ErrorMessage = "Email or MobileNo. alredy exists";
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

        [HttpPost("GetTraineeLogsList")]
        public async Task<ApiResult<DataTable>> GetTraineeLogsList([FromBody] UploadTrainee_LogsModel body)

        {
            ActionName = "GetTraineeLogsList([FromBody] UploadTrainee_LogsModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIDataMasterRepository.GetTraineeLogsList(body);

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
        #endregion


        [HttpPost("GetBTERStudentDetailsList")]
        public async Task<ApiResult<DataTable>> GetBTERStudentDetailsList([FromBody] BTERStudentDetailsMasterSearchModel body)
        {
            ActionName = "GetBTERStudentDetailsList([FromBody] BTERStudentDetailsMasterSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIDataMasterRepository.GetBTERStudentDetailsList(body);

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


        [HttpPost("GetStudentDetailsBYID")]
        public async Task<ApiResult<DataTable>> GetStudentDetailsBYID([FromBody] BTERStudentDetailsMasterSearchModel body)
        {
            ActionName = "GetStudentDetailsBYID([FromBody] BTERStudentDetailsMasterSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITIDataMasterRepository.GetStudentDetailsBYID(body);

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


        [HttpPost("UploadStatusCheckNew")]
        public async Task<ApiResult<RootUploadStatusCheckDataModel>> UploadStatusCheckNew([FromBody] List<NCVTUploadStatusCheckDataModel> request)
        {
            var result = new ApiResult<RootUploadStatusCheckDataModel>();

            try
            {
                if (request.Count > 0)
                {
                    var apidetails = await _unitOfWork.ITIDataMasterRepository.GetNcvt_APIDetails();
                    NCVT_APIDetailsModel resultList = CommonFuncationHelper.ConvertDataTable<NCVT_APIDetailsModel>(apidetails);

                    if (resultList == null)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Service details not found.";
                        return result;
                    }

                    foreach (var items in request)
                    {
                        var token = await ThirdPartyServiceHelper.GetAccessTokenAsync(resultList);
                        if (token == null || token.status != "success" || token.data == null)
                        {
                            result.State = EnumStatus.Error;
                            result.ErrorMessage = "Failed to generate access token.";
                            result.Message = token?.message ?? "No token response.";
                            return result;
                        }
                        resultList.log_Id = items.Log_id;
                        resultList.TokenNo = token.data;
                        var response = await ThirdPartyServiceHelper.CheckUploadStatusNew(resultList);
                        result.Data = response;
                        
                        var responseDataList = new List<ResponseData>();
                        if (result?.Data?.results != null)
                        {
                            foreach (var item in result.Data.results)
                            {
                                responseDataList.Add(new ResponseData
                                {
                                    ErrorDescription = item.ErrorDescription,
                                    MISITICode = item.MISITICode,
                                    MobileNumber = item.MobileNumber,
                                    RecordStatus = item.RecordStatus,
                                    Shift = item.Shift,
                                    StateRegNumber = item.StateRegNumber,
                                    Trade = item.Trade,
                                    TraineeName = item.TraineeName,
                                    Unit = item.Unit
                                });
                            }
                        }
                      
                        if (responseDataList.Count > 0)
                        {
                            var saveResult = await _unitOfWork.ITIStudentEnrollmentRepository.updateOnResponseData(responseDataList);

                            if (saveResult > 0)
                                result.Message = "Response data updated successfully.";
                            else
                                result.Message = "No data updated.";
                        }
                        else
                        {
                            result.Message = "No records found in API response.";
                        }
                        result.State = EnumStatus.Success;
                    }
                }
                else 
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage ="nodata found";
                }
                
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();
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



    }
}
