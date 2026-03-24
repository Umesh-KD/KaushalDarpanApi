using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.EMMA;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.IDfFundDetailsModel;
using Kaushal_Darpan.Models.ITIIIPManageDataModel;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.SurveyPerformModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;
//vivek 
using WorkerDesignationTradeModel = Kaushal_Darpan.Models.SurveyPerformModel.WorkerDesignationTradeModel;
using WorkerDetailsOfExistingApprenticeshipModel = Kaushal_Darpan.Models.SurveyPerformModel.WorkerDetailsOfExistingApprenticeshipModel;
using WorkerDetalisOffacilitiesModel = Kaushal_Darpan.Models.SurveyPerformModel.WorkerDetalisOffacilitiesModel;

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
        private readonly IConverter _converter;

        public ITIIIPManageController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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

        [HttpPost("GetIIPQuaterlyFundReportData")]
        public async Task<ApiResult<string>> GetIIPQuaterlyFundReportData([FromBody] List<IdModel> mod)
        {

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                   
                    List<byte[]> pdfFiles = new List<byte[]>();

                    foreach (var x in mod)
                    {
                        var data = await _unitOfWork.ITIIIPManageRepository.GetIIPQuaterlyFundReport(x.id);

                        if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                        {
                            data.Tables[0].TableName = "IMCReg_Details";
                            data.Tables[1].TableName = "IMC_Members";
                            data.Tables[2].TableName = "IMC_FundDetails";
                            data.Tables[3].TableName = "IMC_QuaterProgressDetails";

                            //data.Tables[0].Rows[0]["ITILogo"] = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                            //data.Tables[0].Rows[0]["NE100"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                            //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/" + data.Tables[0].Rows[0]["signlogo"];



                            string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.GetITIStudent_MarksheetReport}/GetIIPQuaterlyFundReport.html";
                            string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);
                            html = Utility.PDFWorks.ReplaceCustomTag(html);

                            var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";
                            pdfFiles.Add(Utility.PDFWorks.GeneratePDFGetByte(new StringBuilder(html), "landscape", watermarkImagePath));
                        }
                    }

                    if (pdfFiles.Count > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            Document document = new Document();
                            PdfCopy copy = new PdfCopy(document, ms);
                            document.Open();

                            foreach (var pdf in pdfFiles)
                            {
                                PdfReader reader = new PdfReader(pdf);
                                for (int i = 1; i <= reader.NumberOfPages; i++)
                                {
                                    copy.AddPage(copy.GetImportedPage(reader, i));
                                }
                                reader.Close();
                            }

                            document.Close();
                            result.Data = Convert.ToBase64String(ms.ToArray());
                            result.State = EnumStatus.Success;
                            result.Message = "Success";
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

        [HttpPost("GetAllIMCFundDataforReport")]
        public async Task<ApiResult<DataSet>> GetAllIMCFundDataforReport([FromBody] IIPManageFundSearchModel body)
        {
            //ActionName = "GetAllData()";
            ActionName = "GetAllIMCFundDataforReport()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIIIPManageRepository.GetAllIMCFundDataforReport(body));
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


        [HttpPost("SavesurveyperformaReport")]
        public async Task<ApiResult<int>> SavesurveyperformaReport([FromBody] SurveyPerformModel request)
        {
            ActionName = "Task<ApiResult<int>> SavesurveyperformaReport([FromBody] SurveyPerformModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIIIPManageRepository.SavesurveyperformaReport(request);
                    await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.DisposeAsync();
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




        [HttpPost("GetAllsurveyperformaReport")]
        public async Task<ApiResult<DataSet>> GetAllsurveyperformaReport([FromBody] GetSurveyPerformModel body)
        {
            //ActionName = "GetAllData()";
            ActionName = "GetAllsurveyperformaReport()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIIIPManageRepository.GetAllsurveyperformaReport(body));
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




        

        [HttpPost("surveyperformaReportDownload")]
        //        public async Task<IActionResult> surveyperformaReportDownload([FromBody] GetSurveyPerformModel body)
        //        {
        //            try
        //            {
        //                var streams_data = await _unitOfWork.ITIIIPManageRepository.surveyperformaReportDownload(body);

        //                if (streams_data == null || streams_data.Tables.Count < 2)
        //                    return BadRequest("No data found");


        //                var headerData = CommonFuncationHelper
        //                   .ConvertDataTable<List<GetSurveyPerformModel>>(streams_data.Tables[0]);

        //                var WorkerDesignationTradeData = CommonFuncationHelper
        //                    .ConvertDataTable<List<WorkerDesignationTradeModel>>(streams_data.Tables[1]);

        //                var WorkerDetailsOfExistingApprenticeshipData = CommonFuncationHelper
        //                    .ConvertDataTable<List<WorkerDetailsOfExistingApprenticeshipModel>>(streams_data.Tables[2]);

        //                var WorkerDetalisOffacilitiesData = CommonFuncationHelper
        //                    .ConvertDataTable<List<WorkerDetalisOffacilitiesModel>>(streams_data.Tables[3]);

        //                var sb = new StringBuilder();

        //                sb.Append(@"

        //                    <!DOCTYPE html>
        //                    <html lang=""en"">
        //                    <head>
        //                        <meta charset=""UTF-8"">
        //                        <title>Survey Performa</title>
        //                        <style>
        //                            body {
        //                                font-family: ""Times New Roman"", serif;
        //                                margin: 30px;
        //                                color: #000;
        //                            }

        //                            h2, h3 {
        //                                text-align: center;
        //                                margin: 5px 0;
        //                            }

        //                            .subtitle {
        //                                text-align: center;
        //                                font-size: 14px;
        //                                margin-bottom: 20px;
        //                            }

        //                            .section {
        //                                margin-bottom: 20px;
        //                            }

        //                            .section p {
        //                                margin: 6px 0;
        //                                font-size: 14px;
        //                            }

        //                            table {
        //                                width: 100%;
        //                                border-collapse: collapse;
        //                                margin-top: 10px;
        //                                font-size: 14px;
        //                            }

        //                            table, th, td {
        //                                border: 1px solid #000;
        //                            }

        //                            th, td {
        //                                padding: 6px;
        //                                text-align: center;
        //                                vertical-align: middle;
        //                            }

        //                            th {
        //                                font-weight: bold;
        //                            }

        //                            .left {
        //                                text-align: left;
        //                            }

        //                            .signature {
        //                                margin-top: 40px;
        //                                display: flex;
        //                                justify-content: space-between;
        //                                font-size: 14px;
        //                            }

        //                            .signature div {
        //                                width: 40%;
        //                            }

        //                            .line {
        //                                display: inline-block;
        //                                width: 250px;
        //                                border-bottom: 1px dotted #000;
        //                            }
        //                        </style>
        //                    </head>
        //                    <body>

        //                        <h2>Survey Performa</h2>
        //                        <h3>Office of the Apprenticeship Adviser, State of</h3>
        //                        <div class=""subtitle"">
        //                            (The column for ‘Designated Trade’ is to be filled up keeping in view the type of work performed by
        //                            employees as classified in the National Classification of Occupation (N.C.O.) irrespective of their designation/post)
        //                        </div>

        //                        <div class=""section"">
        //<--    Call table 1  -->
        //                            <p>1. Name of the Establishment: ________________________________</p>
        //                            <p>2. Name, Designation and Address of the Head of the Establishment: ________________________________</p>
        //                            <p>3. Nature of business (Please describe what the establishment makes or does as its principal activity): ________________________________</p>
        //                            <p>4. Total no. of persons employed: ________________________________</p>
        //                            <p>5. Basic Training Facility: ________________________________</p>
        //                            <p>6. Occupational distribution of workers employed (other than unskilled workers) in the designated trade shown below
        //                               (please give below the number of employee in each trade separately).</p>
        //                        </div>

        //                        <table>
        //                            <tr>
        //                                <th>S. No.</th>
        //                                <th>Designated Trade</th>
        //                                <th>
        //                                    N.C.O. No. workers (Semi Skilled)<br>
        //                                    Engaged chiefly on repetitive or production lines process
        //                                </th>
        //                                <th>
        //                                    Less Skilled workers<br>
        //                                    Tradesman skilled those who are not included in col. 4
        //                                </th>
        //                                <th>Fully Skilled</th>
        //                                <th>Total</th>
        //                                <th>Remarks</th>
        //                            </tr>
        //                            <tr>
        //                                <td>1</td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                            </tr>
        //                        </table>

        //                        <div class=""section"">
        //                            <h3>ANNEXURE – II</h3>
        //                            <p>7. Details of the existing apprenticeship training programmes (other than scheme for training graduate and diploma apprentices referred to under item 8 below):</p>
        //                        </div>

        //                        <table>
        //                            <tr>
        //        	                    <th>S. No.</th>
        //                                <th>Trade Training</th>
        //                                <th>Duration of per last survey (if surveyed)</th>
        //                                <th>Number of seats located as</th>
        //                                <th>Number actually undergoing training</th>
        //                            </tr>
        //                            <tr>
        //                                <td>1</td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                            </tr>
        //                        </table>

        //                        <div class=""section"">
        //                            <p>8. Details of the facilities, if any, available for the training of ‘Graduate’ and ‘Diploma’ apprentices under any scheme framed by or with the approval of Central Government.</p>
        //                        </div>

        //                        <table>
        //                            <tr>
        //        	                    <th rowspan=""2"">S. No.</th>
        //                                <th rowspan=""2"">Trade</th>
        //                                <th rowspan=""2"">Duration of Training</th>
        //                                <th rowspan=""2"">Number of Training seats sanctioned</th>
        //                                <th colspan=""4"">Number actually undergoing training</th>
        //                            </tr>
        //                            <tr>
        //                                <th>Designate</th>
        //                                <th>Optional</th>
        //                                <th>NATS</th>
        //                                <th>Fresher</th>
        //                            </tr>
        //                            <tr>
        //                                <td>1</td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                                <td></td>
        //                            </tr>
        //                        </table>

        //                        <div class=""section"">
        //                            <p>9. Remarks, if any: ________________________________________________</p>
        //                        </div>

        //                        <div class=""signature"">
        //                            <div>
        //                                Signature of AAA: <span class=""line""></span>
        //                            </div>
        //                            <div>
        //                                Signature of the Employer: <span class=""line""></span><br><br>
        //                                Designation: <span class=""line""></span><br><br>
        //                                Office Seal: <span class=""line""></span>
        //                            </div>
        //                        </div>

        //                    </body>
        //                    </html>


        //                    ");

        //                var doc = new HtmlToPdfDocument
        //                {
        //                    GlobalSettings =
        //            {
        //                PaperSize = PaperKind.A4,
        //                Orientation = Orientation.Portrait
        //            },
        //                    Objects =
        //            {
        //                new ObjectSettings
        //                {
        //                    HtmlContent = sb.ToString(),
        //                    WebSettings = { DefaultEncoding = "utf-8" },
        //                    FooterSettings = new FooterSettings
        //                    {
        //                        FontName = "Arial",
        //                        FontSize = 9,
        //                        Right = "Page [page] of [toPage]",
        //                        Left = "Printed on: [date]",
        //                        Line = true
        //                    }
        //                }
        //            }
        //                };

        //                byte[] pdfBytes = _converter.Convert(doc);

        //                return File(pdfBytes, "application/pdf", "Center_Wise_Present_Absent_Report.pdf");
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(500, ex.Message);
        //            }
        //        }



        public async Task<IActionResult> surveyperformaReportDownload([FromBody] GetSurveyPerformModel body)
        {
            try
            {
                var streams_data = await _unitOfWork
                    .ITIIIPManageRepository
                    .surveyperformaReportDownload(body);

                if (streams_data == null || streams_data.Tables.Count < 4)
                    return BadRequest("No data found");

                var headerData = CommonFuncationHelper
                    .ConvertDataTable<List<GetSurveyPerformModel>>(streams_data.Tables[0]);

                var header = headerData.FirstOrDefault();

                var workerDesignationTradeData =
                    CommonFuncationHelper.ConvertDataTable<List<WorkerDesignationTradeModel>>(streams_data.Tables[1]);

                var workerExistingApprenticeshipData =
                    CommonFuncationHelper.ConvertDataTable<List<WorkerDetailsOfExistingApprenticeshipModel>>(streams_data.Tables[2]);

                var workerFacilitiesData =
                    CommonFuncationHelper.ConvertDataTable<List<WorkerDetalisOffacilitiesModel>>(streams_data.Tables[3]);

                var sb = new StringBuilder();

                sb.Append(@"
<!DOCTYPE html>
<html>
<head>
<meta charset='UTF-8'>
<title>Survey Performa</title>
<style>
body { font-family: 'Times New Roman'; font-size: 14px; margin: 30px; }
h2,h3 { text-align:center; margin:5px; }
.subtitle { text-align:center; margin-bottom:20px; }
table { width:100%; border-collapse:collapse; margin-top:10px; }
table, th, td { border:1px solid #000; }
th, td { padding:6px; text-align:center; }
.left { text-align:left; }
.section { margin-top:15px; }
.signature { margin-top:40px; display:flex; justify-content:space-between; }
.line { border-bottom:1px dotted #000; display:inline-block; width:250px; }
.center-title {
    text-align: center;
}
</style>
</head>
<body>

<h1>
    <span style=""display: block; text-align: center;"">
        Survey Performa
    </span>
</h1>
<h3>Office of the Apprenticeship Adviser, State of:--------------------------</h3>

<div class='subtitle'>
(The column for ‘Designated Trade’ is to be filled up keeping in view the type of work performed by employees as classified in the National
Classification of Occupation (N.C.O.) irrespective of their designation/post)
</div>
");

                sb.Append($@"
<div class='section'>
<p>1. Name of the Establishment: <b>{header?.NameofEstablishment}</b></p>
<p>2. Name, Designation and Address of the Head of the Establishment:
   <b>{header?.NameofDesignation}, {header?.HeadofEstablishmentAddress}</b>
</p>
<p>3. Nature of business: <b>{header?.NatureOfBusiness}</b></p>
<p>4. Total no. of persons employed: <b>{header?.TotalNoPersonEmployeed}</b></p>
<p>5. Basic Training Facility: <b>{header?.BasicTraningFacility}</b></p>
<p>6. Occupational distribution of workers employed:</p>
</div>
");

                sb.Append(@"
<table>
<tr>
<th>S.No</th>
<th>N.C.O. No. workers (Semi Skilled)
Engaged chiefly on repetitive or
production lines process</th>
<th>Less Skilled workers
Tradesman skilled those who are not
included in col. 4</th>
<th>Fully Skilled</th>
<th>Total</th>
<th>Remarks</th>
</tr>
");

                int i = 1;
                foreach (var row in workerDesignationTradeData)
                {
                    sb.Append($@"
<tr>
<td>{i++}</td>
<td>{row.NCONumberWorkers}</td>
<td>{row.LessSkilledWorker}</td>
<td>{row.FullySkilledWorker}</td>
<td>{row.TotalWorker}</td>
<td>{row.Remark}</td>
</tr>
");
                }

                sb.Append("</table>");

                sb.Append(@"
<h3 style='margin-top:25px;'>ANNEXURE – II</h3>
<p>7. Details of the existing apprenticeship training programmes (other than scheme for training graduate and diploma apprentices referred to under
item 8 below)</p>

<table>
<tr>
<th>S.No</th>
<th>Trade Training</th>
<th>Duration of per last survey (if surveyed)</th>
<th>Number of seats located as</th>
<th>Number actually undergoing training</th>
</tr>
");

                i = 1;
                foreach (var row in workerExistingApprenticeshipData)
                {
                    sb.Append($@"
<tr>
<td>{i++}</td>
<td>{row.TradeTraning}</td>
<td>{row.DurationofLastSurvey}</td>
<td>{row.NumberOfSeatsLocated}</td>
<td>{row.NumberActuallyUndergoingtraning}</td>
</tr>
");
                }

                sb.Append("</table>");

                sb.Append(@"
<p style='margin-top:20px;'>8. Details of the facilities, if any, available for the training of ‘Graduate’ and ‘Diploma’ apprentices under any scheme framed by or with the
approval of Central Government.</p>

<table>
 <tr>
        <th rowspan='2'>S. No.</th>
        <th rowspan='2'>Trade</th>
        <th rowspan='2'>Duration of Training</th>
        <th rowspan='2'>Number of Training seats sanctioned</th>
        <th colspan='4'>Number actually undergoing training</th>
    </tr>
    <tr>
        <th>Designate</th>
        <th>Optional</th>
        <th>NATS</th>
        <th>Fresher</th>
    </tr>
");

                i = 1;
                foreach (var row in workerFacilitiesData)
                {
                    sb.Append($@"
<tr>
<td>{i++}</td>
<td>{row.TradeName}</td>
<td>{row.DurationOfTraning}</td>
<td>{row.NumberOfSeatsSanctioned}</td>
<td>{row.NAUT_Deginate}</td>
<td>{row.NAUT_Optional}</td>
<td>{row.NAUT_NATS}</td>
<td>{row.NAUT_Fresher}</td>
</tr>
");
                }

                sb.Append("</table>");

                sb.Append($@"
<p style='margin-top:20px;'>
9. Remark:---------------------------.
</p>

<div class='signature'>
<div>Signature of AAA: <span class='line'></span></div>
<div>
Signature of Employer: <span class='line'></span><br><br>
Designation: <span class='line'></span><br><br>
Office Seal: <span class='line'></span>
</div>
</div>

</body>
</html>
");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(pdfBytes, "application/pdf", "Survey_Performa_Report.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
