using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Net;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class CampusPostMasterController : BaseController
    {
        public override string PageName => "CampusPostMasterController";
        public override string ActionName { get; set; }
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CampusPostMasterController(IMapper mapper, IUnitOfWork unitOfWork , IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        [HttpGet("GetAllData/{SSOID}/{DepartmentID:int}")]
        public async Task<ApiResult<DataTable>> GetAllData(string SSOID, int DepartmentID)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusPostMasterRepository.GetAllData(SSOID, DepartmentID));
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
        }


      

        [HttpGet("GetNameWiseData/{PK_ID}/{DepartmentID}")]
        public async Task<ApiResult<List<CampusPostMasterModel>>> GetNameWiseData(int PK_ID, int DepartmentID)
        {
            ActionName = "GetNameWiseData()";
            var result = new ApiResult<List<CampusPostMasterModel>>();
            try
            {
                result.Data = await _unitOfWork.CampusPostMasterRepository.GetNameWiseData(PK_ID, DepartmentID);
                if (result.Data.Count > 0)
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
        }

        [HttpGet("GetByID/{PK_ID}")]
        public async Task<ApiResult<CampusPostMasterModel>> GetByID(int PK_ID)
        {

            ActionName = " GetByID(int PK_ID)";
            var result = new ApiResult<CampusPostMasterModel>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusPostMasterRepository.GetById(PK_ID));
                result.State = EnumStatus.Success;
                if (result.Data == null)
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
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<DataTable>> SaveData([FromBody] CampusPostMasterModel request)
        {
            ActionName = "SaveData([FromBody] CampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.CampusPostMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data.Rows.Count>0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.PostID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.PostID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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
        [HttpPost("Save_CampusValidation_NodalAction")]
        public async Task<ApiResult<bool>> Save_CampusValidation_NodalAction([FromBody] CampusPostMaster_Action request)
        {
            ActionName = "Save_CampusValidation_NodalAction([FromBody] CampusPostMaster_Action request)";
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


                    result.Data = await _unitOfWork.CampusPostMasterRepository.Save_CampusValidation_NodalAction(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {

                        // email content
                        string emailContent = "your email code is working";
                        string email = "divya.sharma@corpnet.co.in";

                        string emailBody = $@"
                                        <!DOCTYPE html>
                                        <html>
                                        <head>
                                            <meta charset='UTF-8'>
                                            <style>
                                                body {{
                                                    font-family: Arial, sans-serif;
                                                    background-color: #f4f4f4;
                                                    margin: 0;
                                                    padding: 0;
                                                }}
                                                .container {{
                                                    background-color: #ffffff;
                                                    max-width: 600px;
                                                    margin: 40px auto;
                                                    padding: 20px;
                                                    border-radius: 8px;
                                                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                                                }}
                                                .header {{
                                                    background-color: #007bff;
                                                    color: white;
                                                    padding: 10px 20px;
                                                    border-radius: 8px 8px 0 0;
                                                    font-size: 20px;
                                                }}
                                                .content {{
                                                    padding: 20px;
                                                    color: #333;
                                                }}
                                                .footer {{
                                                    font-size: 12px;
                                                    color: #999;
                                                    text-align: center;
                                                    padding: 10px 20px;
                                                    border-top: 1px solid #eee;
                                                }}
                                            </style>
                                        </head>
                                        <body>
                                            <div class='container'>
                                                <div class='header'>Kaushal Darpan Notification</div>
                                                <div class='content'>
                                                    <p>Hello, <strong>{emailContent}</strong></p>
                                                </div>
                                                <div class='footer'>
                                                    &copy; 2025 Kaushal Darpan. All rights reserved.
                                                </div>
                                            </div>
                                        </body>
                                        </html>";

                        // Send email
                        await _emailService.SendEmail(emailBody, email, "Campus Post Status");

                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                        result.ErrorMessage = "There was an error updating data.!";
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

        /*put is used to full update the existing record*/
        [HttpPut("UpdateData")]
        public async Task<ApiResult<bool>> UpdateData(CampusPostMasterModel request)
        {
            ActionName = "UpdateData(CampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CampusPostMasterRepository.UpdateData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.RoleID == 0)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.RoleID == 0)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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

        /*delete is used to remove the existing record*/
        [HttpDelete("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new CampusPostMasterModel
                    {
                        PostID = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.CampusPostMasterRepository.DeleteDataByID(DeleteData_Request);
                    await _unitOfWork.SaveChangesAsync();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Deleted successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error deleting data.!";
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
    

        [HttpGet("CampusValidationList/{CompanyID}/{CollegeID}/{Status}/{DepartmentID}/{CompanyTypeID?}/{Flag?}/{FinancialYearID?}/{post?}")]
        public async Task<ApiResult<DataTable>> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID, int CompanyTypeID = 0, string Flag = "", int FinancialYearID = 0, int postId = 0)
        {
            ActionName = "CampusValidationList";

            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await Task.Run(() =>
                    _unitOfWork.CampusPostMasterRepository.CampusValidationList( CompanyID, CollegeID,Status,DepartmentID, CompanyTypeID,Flag, FinancialYearID, postId));

                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }

                result.State = EnumStatus.Success;
                result.Message = "Data load successfully.!";
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

        [HttpPost("GetCampusSMSDataByID")]
        public async Task<ApiResult<DataTable>> GetCampusSMSDataByID(SmsDataModel reuqest )
        {
            ActionName = "GetCampusSMSDataByID(SmsDataModel reuqest)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusPostMasterRepository.GetCampusSMSDataByID(reuqest));
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
        }


        [HttpPost("SaveSignedCopyData")]
        public async Task<ApiResult<int>> SaveSignedCopyData([FromBody] SignedCopyOfResultModel request)
        {
            ActionName = "SaveSignedCopyData([FromBody] SignedCopyOfResultModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.CampusPostMasterRepository.SaveSignedCopyData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.SignedCopyOfResultID == 0)
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
                        if (request.SignedCopyOfResultID == 0)
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


        [HttpDelete("DeleteSignedCopyDataByID/{ID:int}/{ModifyBy:int}")]
        public async Task<ApiResult<bool>> DeleteSignedCopyDataByID(int ID, int ModifyBy)
        {
            ActionName = "DeleteSignedCopyDataByID(int ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var mappedData = new SignedCopyOfResultSearchModel
                    {
                        SignedCopyOfResultID = ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.CampusPostMasterRepository.DeleteSignedCopyDataByID(mappedData);
                    await _unitOfWork.SaveChangesAsync();

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


        [HttpPost("GetAllSignedCopyData")]
        public async Task<ApiResult<DataTable>> GetAllSignedCopyData([FromBody] SignedCopyOfResultSearchModel body)
        {
            ActionName = "GetAllSignedCopyData([FromBody] SignedCopyOfResultSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CampusPostMasterRepository.GetAllSignedCopyData(body);

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


        [HttpGet("GetSignedCopyById/{PK_ID}")]
        public async Task<ApiResult<SignedCopyOfResultModel>> GetSignedCopyById(int PK_ID)
        {
            ActionName = "GetSignedCopyById(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<SignedCopyOfResultModel>();
                try
                {
                    var data = await _unitOfWork.CampusPostMasterRepository.GetSignedCopyById(PK_ID);
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

        [HttpGet("CampusHistoryList/{CompanyID}/{CollegeID}/{Status}/{DepartmentID}")]
        public async Task<ApiResult<DataTable>> CampusHistoryList(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            ActionName = "CampusHistoryList(int CollegeID,string Status)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusPostMasterRepository.CampusHistoryList(CompanyID, CollegeID, Status, DepartmentID));
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
        }


        [HttpPost("CampusPost_UpdateStatus")]
        public async Task<ApiResult<int>> CampusPost_UpdateStatus([FromBody] CampusPost_UpdateStatus_Model request)
        {
            ActionName = "CampusPost_UpdateStatus([FromBody] SignedCopyOfResultModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.CampusPostMasterRepository.CampusPost_UpdateStatus(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.PostID == 0)
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
                        if (request.PostID == 0)
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

        [HttpPost("GetMinMaxAgeDate")]
        public async Task<ApiResult<MinMaxAgeDateDataModel>> GetMinMaxAgeDate(MinMaxAgeDateDataModel model)
        {
            ActionName = "GetMinMaxAgeDate(MinMaxAgeDateDataModel model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<MinMaxAgeDateDataModel>();
                try
                {
                    var data = await _unitOfWork.CampusPostMasterRepository.GetMinMaxAgeDate(model);
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
    }
}
