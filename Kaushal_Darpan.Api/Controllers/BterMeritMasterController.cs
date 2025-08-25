using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BterMeritMaster;
using Kaushal_Darpan.Models.ItiMerit;
using Kaushal_Darpan.Models.ITIResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class BterMeritMasterController : BaseController
    {
        public override string PageName => "BterMeritMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BterMeritMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] BterMeritSearchModel model)
        {
            ActionName = "GetAllData([FromBody] BterMeritSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BterMeritMasterRepository.GetAllData(model);
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

        [HttpPost("GenerateMerit")]
        public async Task<ApiResult<DataTable>> GenerateMerit([FromBody] BterMeritSearchModel model)
        {
            ActionName = "GenerateMerit([FromBody] BterMeritSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BterMeritMasterRepository.GenerateMerit(model);
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

        [HttpPost("PublishMerit")]
        public async Task<ApiResult<DataTable>> PublishMerit([FromBody] BterMeritSearchModel model)
        {
            ActionName = "PublishMerit([FromBody] BterMeritSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BterMeritMasterRepository.PublishMerit(model);
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


        [HttpPost("UploadMeritdata")]
        public async Task<ApiResult<DataTable>> UploadMeritdata([FromBody] BterUploadMeritDataModel model)
        {
            ActionName = "UploadMeritdata([FromBody] BterMeritSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BterMeritMasterRepository.UploadMeritdata(model);
                _unitOfWork.SaveChanges();
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


        [HttpPost("MeritFormateData")]
        public async Task<ApiResult<DataTable>> MeritFormateData([FromBody] BterMeritSearchModel model)
        {
            ActionName = "PublishMerit([FromBody] BterMeritSearchModel model)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.BterMeritMasterRepository.MeritFormateData(model);
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

        [HttpPost("DowanloadMeritDataPDF")]
        public async Task<ApiResult<string>> DowanloadMeritDataPDF([FromBody] BterMeritSearchModel model)
        {
            ActionName = "DowanloadMeritData(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    // var data = await _unitOfWork.ITIResultRepository.GetCFormReport(request);
                    var Data = await _unitOfWork.BterMeritMasterRepository.MeritReport(model);
                    if (Data.Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //data.Tables[0].TableName = "StateTradeCertificate";

                        //data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        //string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        //html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append("<table style='border-collapse: collapse; width: 100%; font-family: Arial; font-size:14px' border='0' cellpadding='5' cellspacing='0'>");
                        sb1.Append("<tr>");
                        sb1.Append("<th style='text-align: center; font-size: 12px;border:1px solid gray; '>" + Data.Rows[0]["Heading"] + "  </th>");
                        sb1.Append("</tr>");              
                        sb1.Append("</table>");


                        sb1.Append("<table style='border-collapse: collapse; width: 100%; font-family: Arial; font-size:14px' border='0' cellpadding='5' cellspacing='0'>");
                        sb1.Append("<tr>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>ApplicationNo  </b>       </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>ApplicantName  </b>       </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>Gender         </b>       </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>Category       </b>       </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>Pref Cat.      </b>      </th>");

                        if (Data.Rows[0]["CourseType"].ToString() != "3")
                        {
                            sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>% 10</b>  </th>");
                        }

                        if (Data.Rows[0]["CourseType"].ToString() == "2" || Data.Rows[0]["CourseType"].ToString() == "3" || Data.Rows[0]["CourseType"].ToString() == "4" || Data.Rows[0]["CourseType"].ToString() == "5")
                        {
                            sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>Qua. Exm.</b>    </th>");
                            sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>% </b>   </th>");
                        }

                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>common merit </b>         </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>general M    </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>general F    </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>ews M        </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>ews F        </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>sc M         </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>sc F         </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>st M         </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>st F         </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>tsp M        </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>tsp F        </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>obc M        </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>obc F        </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>mbc M        </b>      </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>mbc F        </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>cat B M      </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>cat B F      </b>  </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>PH           </b>    </th>");
                        sb1.Append("<th  style='writing-mode: vertical-lr; transform: rotate(180deg); white-space: nowrap; text-align: center; vertical-align: middle;font-size: 8px;border:1px solid gray; '><b>SMD          </b>    </th>");
                        sb1.Append("</tr>");

                        foreach (DataRow row in Data.Rows)
                        {

                            sb1.Append("<tr>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["ApplicationNo"] + "         </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["ApplicantName"] + "         </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["Gender"] + "                </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["Category"] + "              </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["PrefCategoryType"] + "   </th>");
                            if (Data.Rows[0]["CourseType"].ToString() != "3")
                            {
                                sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["TenthPer"] + "            </th>");
                            }
                            if (Data.Rows[0]["CourseType"].ToString() == "2" || Data.Rows[0]["CourseType"].ToString() == "3" || Data.Rows[0]["CourseType"].ToString() == "4" || Data.Rows[0]["CourseType"].ToString() == "5")
                            {
                                sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["QualifyingExamination"] + " </th>");

                                sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["Percentage"] + "  </th>");
                            }

                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["common_merit"] + "   </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["general_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["general_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["ews_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["ews_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["sc_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["sc_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["st_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["st_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["tsp_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["tsp_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["obc_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["obc_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["mbc_male"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["mbc_female"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["catB_MP_male_merit"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["catB_MP_female_merit"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["catC_PH_merit"] + " </th>");
                            sb1.Append("<th  style='text-align: left;font-size: 8px;border:1px solid gray; '>" + row["catE_SMD_merit"] + "   </th>");
                            sb1.Append("</tr>");
                        }

                        sb1.Append("</table>");






                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4");

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

        [HttpPost("DowanloadMeritDataExcel")]
        public async Task<ApiResult<DataTable>> DowanloadMeritDataExcel([FromBody] BterMeritSearchModel model)
        {
            ActionName = "DowanloadMeritDataExcel(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    // var data = await _unitOfWork.ITIResultRepository.GetCFormReport(request);
                    result.Data = await _unitOfWork.BterMeritMasterRepository.MeritReport(model);
                    if (result.Data.Rows.Count > 0)
                    {                   
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


    }
}
