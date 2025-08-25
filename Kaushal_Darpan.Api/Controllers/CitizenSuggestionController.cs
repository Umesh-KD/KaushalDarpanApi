using AutoMapper;
using Kaushal_Darpan.Api.Email;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CitizenSuggestion;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.Examiners;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ValidationActionFilter]
    public class CitizenSuggestionController : BaseController
    {
        public override string PageName => "AbcIdStudentDetailsController";
        public override string ActionName { get; set; }
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CitizenSuggestionController(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] CitizenSuggestionSearchModel model)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CitizenSuggestionRepository.GetAllData(model);
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


        //[HttpPost("SaveData")]
        //public async Task<ApiResult<DataTable>> SaveData([FromBody] CitizenSuggestion request)
        //{
        //    ActionName = "SaveData([FromBody] CitizenSuggestion request)";
        //    var result = new ApiResult<DataTable>();

        //    try
        //    {

        //        // Call the repository method to save data
        //        result.Data = await _unitOfWork.CitizenSuggestionRepository.SaveData(request);

        //        //await _emailService.SendEmailAsync(request.Email, "test", "test");

        //        // Save changes to the database
        //        _unitOfWork.SaveChanges();

        //        // Set response message based on whether the data was saved or updated
        //        if (result.Data.Rows.Count>0)
        //        {
        //            var data = result.Data.AsEnumerable()
        //               .Select(row => row[1].ToString())
        //               .FirstOrDefault();

        //            string email = request.Email;
        //            string userName = request.Name;
        //            string comment = request.Comment;
        //            string MobileNo = request.MobileNo;
        //            string srNo = data;
        //            string emailBody = @"<!DOCTYPE html>
        //                                <html>
        //                                <head>
        //                                    <meta charset='UTF-8'>
        //                                    <style>
        //                                        body {
        //                                            font-family: Arial, sans-serif;
        //                                            background-color: #f4f4f4;
        //                                            margin: 0;
        //                                            padding: 0;
        //                                        }
        //                                        .container {
        //                                            background-color: #ffffff;
        //                                            max-width: 600px;
        //                                            margin: 40px auto;
        //                                            padding: 20px;
        //                                            border-radius: 8px;
        //                                            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        //                                        }
        //                                        .header {
        //                                            background-color: #007bff;
        //                                            color: white;
        //                                            padding: 10px 20px;
        //                                            border-radius: 8px 8px 0 0;
        //                                            font-size: 20px;
        //                                        }
        //                                        .content {
        //                                            padding: 20px;
        //                                            color: #333;
        //                                        }
        //                                        .footer {
        //                                            font-size: 12px;
        //                                            color: #999;
        //                                            text-align: center;
        //                                            padding: 10px 20px;
        //                                            border-top: 1px solid #eee;
        //                                        }
        //                                        .button {
        //                                            display: inline-block;
        //                                            margin-top: 20px;
        //                                            padding: 12px 25px;
        //                                            background-color: #28a745;
        //                                            color: #fff !important;
        //                                            text-decoration: none;
        //                                            border-radius: 5px;
        //                                        }
        //                                    </style>
        //                                </head>
        //                                <body>
        //                                    <div class='container'>
        //                                        <div class='header'>
        //                                            Kaushal Darpan Notification
        //                                        </div>
        //                                        <div class='content'>
        //                                            <p>Hello,{userName}</p>
        //                                            <p>Email: {email}</p>
        //                                            <p>Mobile No: {MobileNo}</p>
        //                                            <p>Comment: {comment}</p>
        //                                            <p>Generated SR No: {srNo}</p>
        //                                        </div>
        //                                        <div class='footer'>
        //                                            &copy; 2025 Kaushal Darpan. All rights reserved.
        //                                        </div>
        //                                    </div>
        //                                </body>
        //                                </html>
        //                                ";

        //            await _emailService.SendEmail(emailBody, request.Email);
        //            result.State = EnumStatus.Success;
        //            result.Message = request.Pk_ID == 0 ? Constants.MSG_SAVE_SUCCESS : Constants.MSG_UPDATE_SUCCESS;
        //        }
        //        else
        //        {
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = request.Pk_ID == 0 ? Constants.MSG_ADD_ERROR : Constants.MSG_UPDATE_ERROR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Dispose the unit of work on error
        //        _unitOfWork.Dispose();

        //        // Handle the exception and return a generic error message
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = "An unexpected error occurred: " + ex.Message;

        //        // Log the exception
        //        var nex = new NewException
        //        {
        //            PageName = PageName,
        //            ActionName = ActionName,
        //            Ex = ex,
        //        };
        //        await CreateErrorLog(nex, _unitOfWork);
        //    }

        //    return result;
        //}

        [HttpPost("SaveData")]
        public async Task<ApiResult<DataTable>> SaveData([FromBody] CitizenSuggestion request)
        {
            ActionName = "SaveData([FromBody] CitizenSuggestion request)";
            var result = new ApiResult<DataTable>();

            try
            {
                // Save to database via repository
                result.Data = await _unitOfWork.CitizenSuggestionRepository.SaveData(request);
                _unitOfWork.SaveChanges();

                // If data was inserted/updated
                if (result.Data?.Rows.Count > 0)
                {
                    // Get generated SR No. (assumed to be in second column [1])
                    string srNo = result.Data.Rows[0][1]?.ToString();

                    // Build email body
                    string userName = WebUtility.HtmlEncode(request.Name);
                    string email = WebUtility.HtmlEncode(request.Email);
                    string comment = WebUtility.HtmlEncode(request.Comment);
                    string mobileNo = WebUtility.HtmlEncode(request.MobileNo);

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
                                                    <p>Hello, <strong>{userName}</strong></p>
                                                    <p><strong>Email:</strong> {email}</p>
                                                    <p><strong>Mobile No:</strong> {mobileNo}</p>
                                                    <p><strong>Comment:</strong> {comment}</p>
                                                    <p><strong>Generated SR No:</strong> {srNo}</p>
                                                </div>
                                                <div class='footer'>
                                                    &copy; 2025 Kaushal Darpan. All rights reserved.
                                                </div>
                                            </div>
                                        </body>
                                        </html>";

                    // Send email
                    await _emailService.SendEmail(emailBody, email, "CitizenSuggestion");

                    result.State = EnumStatus.Success;
                    result.Message = request.Pk_ID == 0 ? Constants.MSG_SAVE_SUCCESS : Constants.MSG_UPDATE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = request.Pk_ID == 0 ? Constants.MSG_ADD_ERROR : Constants.MSG_UPDATE_ERROR;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                result.State = EnumStatus.Error;
                result.ErrorMessage = "An unexpected error occurred: " + ex.Message;

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


        [HttpPost("SaveReplayData")]
        public async Task<ApiResult<bool>> SaveReplayData([FromBody] ReplayQuery request)
        {
            ActionName = "SaveReplayData([FromBody] ReplayQuery request)";
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


                    result.Data = await _unitOfWork.CitizenSuggestionRepository.SaveReplayData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.Pk_ID == 0)
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
                        if (request.Pk_ID == 0)
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
        public async Task<ApiResult<CitizenSuggestion>> GetByID(int PK_ID,int DepartmentID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CitizenSuggestion>();
                try
                {
                    var data = await _unitOfWork.CitizenSuggestionRepository.GetByID(PK_ID);
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



        [HttpPost("GetSRNumberData")]
        public async Task<ApiResult<DataTable>> GetSRNumberData([FromBody] CitizenSuggestionSearchSRModel model)
        {
            ActionName = "GetSRNumberData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CitizenSuggestionRepository.GetSRNumberData(model);
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

        [HttpPost("GetSRNDataList")]
        public async Task<ApiResult<DataTable>> GetSRNDataList([FromBody] CitizenSuggestionSearchModel model)
        {
            ActionName = "GetSRNDataList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.CitizenSuggestionRepository.GetSRNDataList(model);
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



        [HttpGet("GetByMobileNo/{Mobile}")]
        public async Task<ApiResult<CitizenSuggestion>> GetByMobileNo(string Mobile)
        {
            ActionName = "GetByMobileNo(int Mobile)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CitizenSuggestion>();
                try
                {
                    var data = await _unitOfWork.CitizenSuggestionRepository.GetByMobileNo(Mobile);
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

        [HttpPost("SaveUserRating")]
        public async Task<ApiResult<bool>> SaveUserRating([FromBody] UserRatingDataModel request)
        {
            ActionName = "SaveReplayData([FromBody] ReplayQuery request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CitizenSuggestionRepository.SaveUserRating(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        if (request.Pk_ID == 0)
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
                        if (request.Pk_ID == 0)
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


    }
}
