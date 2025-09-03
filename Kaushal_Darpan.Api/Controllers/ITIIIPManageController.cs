using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.IDfFundDetailsModel;
using Kaushal_Darpan.Models.ITIIIPManageDataModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ITIIIPManageController : BaseController
    {
        public override string PageName => "ITI_InspectionController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIIIPManageController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataSet>> GetAllData([FromBody] ITIIIPManageDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIIIPManageRepository.GetAllData(body));
                result.State = EnumStatus.Success;
                if (result.Data != null)
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

        [HttpPost("SaveIMCReg")]
        public async Task<ApiResult<int>> SaveIMCReg([FromBody] ITIIIPManageDataModel request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.SaveIMCReg(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // Log the error
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

        [HttpGet("GetById_IMC/{ID}")]
        public async Task<ApiResult<ITIIIPManageDataModel>> GetById_IMC(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIIIPManageDataModel>();
                try
                {
                    var data = await _unitOfWork.ITIIIPManageRepository.GetById_IMC(ID);
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

        [HttpGet("GetIMCHistory_ById/{RegID}")]
        public async Task<ApiResult<DataTable>> GetIMCHistory_ById(int RegID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ITIIIPManageRepository.GetIMCHistory_ById(RegID);
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

        [HttpPost("SaveIMCFund")]
        public async Task<ApiResult<int>> SaveIMCFund([FromBody] IIPManageFundSearchModel request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.SaveIMCFund(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // Log the error
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

        [HttpPost("GetAllIMCFundData")]
        public async Task<ApiResult<DataSet>> GetAllIMCFundData([FromBody] IIPManageFundSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIIIPManageRepository.GetAllIMCFundData(body));
                result.State = EnumStatus.Success;
                if (result.Data != null)
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

        #region "Fund Detaails"

        [HttpPost("SaveFundDetails")]
        public async Task<ApiResult<int>> SaveFundDetails([FromBody] IDfFundDetailsModel request)
        {
            ActionName = "Task<ApiResult<int>> SaveFundDetails([FromBody] ITIIIPManageDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.SaveFundDetails(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // Log the error
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

        [HttpPost("GetFundDetailsData")]
        public async Task<ApiResult<DataTable>> GetFundDetailsData([FromBody] IDfFundSearchDetailsModel body)
        {
            ActionName = "GetFundDetailsData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIIIPManageRepository.GetFundDetailsData(body));
                result.State = EnumStatus.Success;
                if (result.Data != null)
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

        [HttpGet("GetById_FundDetails/{ID}")]
        public async Task<ApiResult<IDfFundDetailsModel>> GetById_FundDetails(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<IDfFundDetailsModel>();
                try
                {
                    var data = await _unitOfWork.ITIIIPManageRepository.GetById_FundDetails(ID);
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
        #endregion

        [HttpGet("GetById_IMCFund/{ID}")]
        public async Task<ApiResult<IIPManageFundSearchModel>> GetById_IMCFund(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<IIPManageFundSearchModel>();
                try
                {
                    var data = await _unitOfWork.ITIIIPManageRepository.GetById_IMCFund(ID);
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

        [HttpGet("GetQuaterlyProgressData/{ID}")]
        public async Task<ApiResult<DataTable>> GetQuaterlyProgressData(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.GetQuaterlyProgressData(ID);

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

        [HttpPost("SaveQuaterlyProgressData")]
        public async Task<ApiResult<int>> SaveQuaterlyProgressData([FromBody] IMCFundRevenue? request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.SaveQuaterlyProgressData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // Log the error
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

        [HttpPost("FinalSubmitUpdate/{ID}")]
        public async Task<ApiResult<int>> FinalSubmitUpdate(int id)
        {
            //ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITIIIPManageRepository.FinalSubmitUpdate(id);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;

                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // Log the error
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

        [HttpGet("GetIIPQuaterlyFundReport/{Id}")]
        public async Task<ApiResult<string>> GetIIPQuaterlyFundReport(int Id)
        {
            
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITIIIPManageRepository.GetIIPQuaterlyFundReport(Id);

                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        data.Tables[0].TableName = "IMCReg_Details";

                        //data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];


                        data.Tables[1].TableName = "IMC_Members";
                        data.Tables[2].TableName = "IMC_FundDetails";
                        data.Tables[3].TableName = "IMC_QuaterProgressDetails";

                        string devFontSize = "12px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();


                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/GetIIPQuaterlyFundReport.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);
                        //System.IO.File.WriteAllText("debug.html", html);
                        sb1.Append(html);


                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", watermarkImagePath);

                        result.Data = Convert.ToBase64String(pdfBytes);
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
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }
    }
}
