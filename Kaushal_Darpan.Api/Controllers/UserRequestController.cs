using System.Data;
using AutoMapper;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.DispatchFormDataModel;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.UserMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class UserRequestController : BaseController
    {

        public override string PageName => "UserLoginController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserRequestController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("UserRequest")]
        public async Task<ApiResult<DataTable>> UserRequest([FromBody] RequestSearchModel request)
     {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.UserRequest(request);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_UPDATE_SUCCESS;
                   
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

        [HttpPost("UserRequestUpdateStatus")]
        public async Task<ApiResult<bool>> UserRequestUpdateStatus([FromBody] RequestUpdateStatus request)
        {
            ActionName = "UserRequestUpdateStatus([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.UsersRequest.UserRequestUpdateStatus(request);
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

        [HttpPost("UserRequestHistory")]
        public async Task<ApiResult<DataTable>> UserRequestHistory([FromBody] RequestUserRequestHistory request)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.UserRequestHistory(request);

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


        [HttpPost("StafffJoiningRequestUpdateAndPromotions")]
        public async Task<ApiResult<int>> StafffJoiningRequestUpdateAndPromotions([FromBody] RequestUpdateStatus request)
        {
            ActionName = "StafffJoiningRequestUpdateAndPromotions([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.UsersRequest.StafffJoiningRequestUpdateAndPromotions(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else if (result.Data == 2)
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                       
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (result.Data == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else if (result.Data == -1)
                        {
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
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



        //------------------Bter User Request -------------------------

       


        [HttpPost("BterEmUserRequest")]
        public async Task<ApiResult<DataTable>> BterEmUserRequest([FromBody] BterRequestSearchModel request)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.BterEmUserRequest(request);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_UPDATE_SUCCESS;

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

        [HttpPost("BterEmUserRequestUpdateStatus")]
        public async Task<ApiResult<bool>> BterEmUserRequestUpdateStatus([FromBody] BterRequestUpdateStatus request)
        {
            ActionName = "UserRequestUpdateStatus([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.UsersRequest.BterEmUserRequestUpdateStatus(request);
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

        [HttpPost("BterEmUserRequestHistory")]
        public async Task<ApiResult<DataTable>> BterEmUserRequestHistory([FromBody] BterRequestUserRequestHistory request)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.BterEmUserRequestHistory(request);

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


        [HttpPost("BterEmStafffJoiningRequestUpdateAndPromotions")]
        public async Task<ApiResult<int>> BterEmStafffJoiningRequestUpdateAndPromotions([FromBody] BterRequestUpdateStatus request)
        {
            ActionName = "StafffJoiningRequestUpdateAndPromotions([FromBody] RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.UsersRequest.BterEmStafffJoiningRequestUpdateAndPromotions(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else if (result.Data == 2)
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (result.Data == 0)
                        {
                            result.ErrorMessage = Constants.MSG_ADD_ERROR;
                        }
                        else if (result.Data == -1)
                        {
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
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




        [HttpPost("GetJoiningLetter")]
        public async Task<ApiResult<string>> GetJoiningLetter([FromBody] JoiningLetterSearchModel model)
        {
            ActionName = "GetJoiningLetter(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {

                    var data = await _unitOfWork.UsersRequest.GetJoiningLetter(model);
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


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.JoiningRelivingLetterBTER}/JoiningLetterForm.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1);

                        // Example: Send in API
                        //return File(pdfBytes, "application/pdf", "Generated.pdf");


                        ///string dataUri = "data:application/pdf;base64," + base64String;
                        result.Data = Convert.ToBase64String(pdfBytes); ;
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

                    var data = await _unitOfWork.UsersRequest.GetRelievingLetter(model);
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


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.JoiningRelivingLetterBTER}/RelievingLetterForm.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1);

                        // Example: Send in API
                        //return File(pdfBytes, "application/pdf", "Generated.pdf");


                        ///string dataUri = "data:application/pdf;base64," + base64String;
                        result.Data = Convert.ToBase64String(pdfBytes); ;
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


        [HttpPost("BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData")]
        public async Task<ApiResult<DataTable>> BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData([FromBody] Bter_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {

            ActionName = "BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData([FromBody] Bter_Govt_EM_ZonalOFFICERSSearchDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(body);

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


        [HttpPost("BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing")]
        public async Task<ApiResult<DataTable>> BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing([FromBody] BterStaffUserRequestReportSearchModel body)
        {

            ActionName = "BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing([FromBody] Bter_Govt_EM_ZonalOFFICERSSearchDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing(body);

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
        //-------------VRS----------------

        [HttpPost("GetBter_GetStaffDetailsVRS")]
        public async Task<ApiResult<DataTable>> GetBter_GetStaffDetailsVRS([FromBody] BTER_EM_UnlockProfileDataModel body)
        {

            ActionName = "GetBter_GetStaffDetailsVRS([FromBody] BTER_EM_UnlockProfileDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.UsersRequest.GetBter_GetStaffDetailsVRS(body);

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
