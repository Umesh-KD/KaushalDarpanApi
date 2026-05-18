using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.StaffMaster;
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
        private readonly IConverter _converter;
        public BTER_EM_StaffServiceDetailsController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
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

        [HttpPost("GetEM_RelievingTransferData")]
        public async Task<ApiResult<DataTable>> GetEM_RelievingTransferData([FromBody] EM_TransferSystemSearchModel body)
        {

            ActionName = "GetEM_RelievingTransferData([FromBody] EM_TransferSystemSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.GetEM_RelievingTransferData(body);

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


        [HttpPost("GetEM_TransferSystemEmployeeStatus")]
        public async Task<ApiResult<DataTable>> GetEM_TransferSystemEmployeeStatus([FromBody] EM_TransferSystemSearchModel body)
        {

            ActionName = "GetEM_TransferSystemEmployeeStatus([FromBody] EM_TransferSystemSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.GetEM_TransferSystemEmployeeStatus(body);

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
                            result.Message = Constants.MSG_DELETE_SUCCESS;
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


        [HttpPost("TransferSystemGeneratorUpdate")]
        public async Task<ApiResult<int>> TransferSystemGeneratorUpdate([FromBody] TransferSystemUpdateDataModel body)
        {

            ActionName = "TransferSystemGeneratorUpdate([FromBody] TransferSystemUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.TransferSystemGeneratorUpdate(body);
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


        [HttpPost("AddTransferSystemManualRequest")]
        public async Task<ApiResult<int>> AddTransferSystemManualRequest([FromBody] BTERStaffManualRequestModel body)
        {

            ActionName = "AddTransferSystemManualRequest([FromBody] StaffTrainingStatusUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.AddTransferSystemManualRequest(body);
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


        #region Relieving letter



        [HttpGet("DownloadRelievingLetterPDF/{TransferSystemID}/{StaffID}")]
        public async Task<IActionResult> DownloadRelievingLetterPDF(int TransferSystemID, int StaffID)
        {


            var body = new EM_TransferSystemSearchModel
            {
                TransferSystemID = TransferSystemID,
                StaffID = StaffID
            };

            // अब आप यहाँ बिना एरर के 'await' का उपयोग कर सकते हैं

            var ds = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.GetRelievingLetter(body);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return NotFound("Data not found in database");

            var model = CommonFuncationHelper
                .ConvertDataTable<List<TransferSystemShowDataModel>>(ds.Tables[0])
                .FirstOrDefault();


            if (model == null)
                return NotFound("Data mapping failed");

            string base64Logo = "";
            string path = Path.Combine(ConfigurationHelper.StaticFileRootPath, "kd.jpeg");

            if (System.IO.File.Exists(path))
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                base64Logo = $"data:image/jpeg;base64,{Convert.ToBase64String(imageArray)}";
            }




            var html = $@"
        <html>
        <head>
        <style>
            body {{
              font-family: 'Noto Sans Devanagari', 'Mangal', 'Arial Unicode MS', sans-serif;
                font-size: 11.5px;
                margin: 0;
                padding: 0;
                color: #000;
            }}

            .page {{
                width: 95%;
                margin: 0 auto;
                padding: 20px;
                background: #fff;
            }}

            table {{
                width: 100%;
                border-collapse: collapse;
                margin-bottom: -1px; /* Borders overlap avoid double lines */
            }}

            .main-table td {{
                border: 1px solid #777;
                padding: 6px 10px;
                vertical-align: middle;
            }}

            /* Header Styles */
            .logo-box {{
                width: 15%;
                text-align: center;
                color: #d32f2f;
                font-weight: bold;
                font-size: 14px;
                line-height: 1.2;
            }}

            .dept-box {{
                width: 70%;
                text-align: center;
                font-size: 18px;
                font-weight: bold;
                text-decoration: underline;
            }}

            .photo-box {{
                width: 15%;
                text-align: center;
            }}

            .photo-placeholder {{
                border: 1px solid #ccc;
                width: 65px;
                height: 75px;
                margin: 0 auto;
                line-height: 75px;
                font-size: 10px;
                color: #999;
            }}

            .school-info-section {{
                text-align: center;
                padding: 8px !important;
            }}

            .school-name {{
                font-size: 14px;
                font-weight: bold;
                color: #1a237e;
                margin-bottom: 2px;
            }}

            .order-title {{
                background-color: #f2f2f2;
                text-align: center;
                font-size: 16px;
                font-weight: bold;
                letter-spacing: 1px;
            }}

            /* Data Table Styles */
            .label {{
                background-color: #ffffff;
                width: 30%;
            }}

            .value {{
                font-weight: bold;
                width: 20%;
            }}

            .full-value {{
                font-weight: bold;
            }}

            /* Footer Styles */
            .dispatch-section {{
                margin-top: 25px;
                border: none !important;
            }}

            .dispatch-section td {{
                border: none !important;
                padding: 0;
            }}

            .copy-text {{
                margin-top: 20px;
                font-size: 10px;
                line-height: 1.6;
            }}

            .footer-stamp {{
                text-align: right;
                margin-top: 35px;
                font-weight: bold;
                font-size: 12px;
            }}

            .system-footer {{
                text-align: center;
                font-size: 9px;
                color: #666;
                border-top: 1px solid #eee;
                margin-top: 20px;
                padding-top: 5px;
            }}

        .footer {{position: absolute;
            bottom: 15mm;
            left: 15mm;
            right: 15mm;
            text-align: center;
            font-size: 12px;
            border-top: 1px solid #ddd;
            padding-top: 5px;
        }}
        </style>
        </head>
        <body>
        <div class='page'>

            <table class='main-table'>
         <tr>
            <td class='logo-box' style='width: 10%; text-align: center;'>
                <img src='{base64Logo}' style='height: 60px; width: auto;' alt='Logo' />
            </td>

            <td class='dept-box' style='width: 70%; text-align: center;'>
                <div style='font-size: 18px; font-weight: bold; text-decoration: underline;'>
                    शिक्षा विभाग - राजस्थान
                </div>
            </td>


        </tr>
                <tr>
                    <td colspan='3' class='school-info-section'>
                        <div class='school-name'>{model.InstituteName}</div>

                    </td>
                </tr>
                <tr>
                    <td colspan='3' class='order-title'>कार्यमुक्ति आदेश</td>
                </tr>
            </table>

            <table class='main-table'>
                <tr>
                    <td class='label'>कौशल दर्पण आदेश क्रमांक</td>
                    <td class='value'>{model.OrderNo}</td>
                    <td style='width:15%;'>दिनांक</td>
                    <td class='value'>{model.OrderDate}</td>
                </tr>
                <tr>
                    <td>अधिकारी/कर्मचारी का नाम</td>
                    <td colspan='3' class='full-value'>{model.NAME}</td>
                </tr>
                <tr>
                    <td>अधिकारी / कर्मचारी का एम्प्लाई आईडी</td>
                    <td colspan='3' class='full-value'>{model.EmployeeID}</td>
                </tr>
                <tr>
                    <td>जन्म दिनांक</td>
                    <td class='value'>{model.DateOfBirth}</td>
                    <td>मो.नं.</td>
                    <td class='value'>{model.MobileNumber}</td>
                </tr>
                <tr>
                    <td>वर्तमान पद का नाम</td>
                    <td colspan='3' class='full-value'>{model.TransferPostName}</td>
                </tr>
                <tr>
                    <td>आदेश का कारण</td>
                    <td colspan='3' class='full-value'>{model.RequestRemarks}</td>
                </tr>
                <tr>
                    <td>आदेशकर्ता अधिकारी</td>
                    <td colspan='3' class='full-value'>{model.ApproveName}</td>
                </tr>
                <tr>
                    <td>आदेश क्रमांक (आदेशकर्ता अधिकारी)</td>
                    <td class='value'>{model.OrderNo}</td>
                    <td>दिनांक</td>
                    <td class='value'>{model.OrderDate}</td>
                </tr>
                <tr>
                    <td>पद जिस हेतु कार्यमुक्त हुआ है</td>
                    <td colspan='3' class='full-value'>{model.LastPostName}</td>
                </tr>
                <tr>
                    <td>स्थान जिसके लिये कार्यमुक्त किया गया है</td>
                    <td colspan='3' class='full-value'>{model.TransferOfficeName}</td>
                </tr>

                <tr>
                    <td>कार्यमुक्ति दिनांक</td>
                    <td class='value'>{model.RelievingDate}</td>
                    <td>समय</td>
                    <td class='value'>{model.RelivingTime}</td>
                </tr>
            </table>

          <br/>
        <br/>
        <br/>
        <br/>
        <br/>
        <table class='footer-sign' style='font-size:15px';margin-top:40px;>
            <tr>
                <td style='width:50%; text-align:left; vertical-align:top;'>
                    प्रतिलिपि एवं प्रेषण क्रमांक<br>
                    दिनांक
                </td>
                <td style='width:50%; text-align:right; vertical-align:top;'>
                    {model.TransferPostName}<br>
                   {model.InstituteName}
                </td>
            </tr>
        </table>



          <div class='footer'>
            This order is system-generated and does not require an e-signature!
        </div>

        </div>
        </body>
        </html>";

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings
                    {
                        Top = 4,
                        Bottom = 4,
                        Left = 4,
                        Right = 4
                    }
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        WebSettings = new WebSettings
                        {
                            DefaultEncoding = "utf-8",
                            LoadImages = true
                        }
                    }
                }
            };




            var pdf = _converter.Convert(doc);


            return File(pdf, "application/pdf", "Relieving_Letter.pdf");
        }
        #endregion


        [HttpPost("TransferSystemRetievingUpdateStatus")]
        public async Task<ApiResult<int>> TransferSystemRetievingUpdateStatus([FromBody] EM_TransferSystemSearchModel body)
        {

            ActionName = "TransferSystemRetievingUpdateStatus([FromBody] TransferSystemUpdateDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.TransferSystemRetievingUpdateStatus(body);
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
