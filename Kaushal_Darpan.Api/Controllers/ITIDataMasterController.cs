using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.Student;

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

                    if (data.Rows[0]["data"]!=null)
                    { 
       
                        if (!string.IsNullOrEmpty( Convert.ToString(data.Rows[0]["data"])))
                        {
                          
                            result.Data = data.Rows[0]["data"].ToString();
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                     
                        }
                        else {
                            if(request.RequestType== "UserNotValid")
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




    }
}
