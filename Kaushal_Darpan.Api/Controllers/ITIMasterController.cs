using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.NodalOfficer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Data;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]
    public class ITIMasterController : BaseController
    {
        public override string PageName => "ITIMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] ITIMasterModel request)
        {
            ActionName = "SaveData([FromBody] ITIMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIMasterRepository.SaveData(request);
                    _unitOfWork.SaveChanges();

                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = request.TradeId == 0 ? "Saved successfully!" : "Updated successfully!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Trade Code already exists.";
                    }
                    else if (result.Data == -3)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Trade Name already exists.";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = request.TradeId == 0
                            ? "There was an error adding the trade."
                            : "There was an error updating the trade.";
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Exception: " + ex.Message;

                    await CreateErrorLog(new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex
                    }, _unitOfWork);
                }
                return result;
            });
        }


        /*put is used to full update the existing record*/


        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITISearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetAllData(body));
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


        [HttpGet("GetByID/{TradeID:int}")]
        public async Task<ApiResult<ITIMasterModel>> GetByID(int TradeID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIMasterModel>();
                try
                {
                    var data = await _unitOfWork.ITIMasterRepository.GetById(TradeID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITIMasterModel>(data);
                        result.Data = mappedData;
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



        /*delete is used to remove the existing record*/
        [HttpPost("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    var DeleteData_Request = new ITIMasterModel
                    {
                        TradeId = PK_ID,
                        ModifyBy = ModifyBy,
                    };
                    result.Data = await _unitOfWork.ITIMasterRepository.DeleteDataByID(DeleteData_Request);
                    _unitOfWork.SaveChanges();

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

        [HttpPost("GetAllPaperUploadData")]
        public async Task<ApiResult<DataTable>> GetAllPaperUploadData([FromBody] ITIPaperUploadSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetAllPaperUploadData(body));
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


        [HttpPost("SavePaperUploadData")]
        public async Task<ApiResult<int>> SavePaperUploadData([FromBody] ITIPaperUploadModel request)
        {
            ActionName = "SavePaperUploadData([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIMasterRepository.SavePaperUploadData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.PaperUploadID == null)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.PaperUploadID == null)
                            result.ErrorMessage = "There was an error adding data.!";
                        else
                            result.ErrorMessage = "There was an error updating data.!";
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
        
        //[HttpPost("GetITIFeesPerYearList")]
        //public async Task<ApiResult<DataTable>> GetITIFeesPerYearList([FromBody] ITIFeesPerYearSearchModel request)
        //{
        //    ActionName = "GetITIFeesPerYearList([FromBody] ITIFeesPerYearSearchModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            result.Data = await _unitOfWork.ITIMasterRepository.GetITIFeesPerYearList(request);
        //            _unitOfWork.SaveChanges();
        //            if (result.Data > 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.TradeId == null)
        //                    result.Message = "Saved successfully .!";
        //                else
        //                    result.Message = "Updated successfully .!";
        //            }
        //            else if (result.Data == -2)
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = "Duplicate Entry";

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.TradeId == null)
        //                    result.ErrorMessage = "There was an error adding data.!";
        //                else
        //                    result.ErrorMessage = "There was an error updating data.!";
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //            // write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //        }
        //        return result;
        //    });
        //}


        [HttpPost("GetITIFeesPerYearList")]
        public async Task<ApiResult<DataTable>> GetITIFeesPerYearList([FromBody] ITIFeesPerYearSearchModel body)
        {
            ActionName = "GetITIFeesPerYearList()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetITIFeesPerYearList(body));
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

        [HttpPost("ItiFeesPerYearListDownloadOld")]
        public async Task<ApiResult<string>> ItiFeesPerYearListDownloadOld([FromBody] ITIFeesPerYearSearchModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await Task.Run(() => _unitOfWork.ITIMasterRepository.ItiFeesPerYearListDownload(body));
                    if (data != null)
                    {
                        //report
                        var fileName = $"ITITradesFeeInformation.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/ITITradesFeeInformation.rdlc";


                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("ITITradesFeeInformation", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("ItiFeesPerYearListDownload")]
        public async Task<ApiResult<string>> ItiFeesPerYearListDownload([FromBody] ITIFeesPerYearSearchModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await Task.Run(() => _unitOfWork.ITIMasterRepository.ItiFeesPerYearListDownload(body));
                    if (data != null)
                    {



                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "TradeWiseFee";

                        data.Tables[0].Rows[0]["logo_left"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_logo.png";
                        data.Tables[0].Rows[0]["logo_right"] = $"{ConfigurationHelper.StaticFileRootPath}/dte_logo.png";

                        //data.Tables[0].Rows[0]["signlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";
                        //data.Tables[0].Rows[0]["mainlogo"]=$"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";
                        //data.Tables[1].TableName = "Consolidated_Marksheet";
                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ItiFeesPerYearListDownload.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(html);

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogoWaterMark.png";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "", "");

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




        [HttpPost("unlockfee/{id}/{ModifyBy}/{FeePdf}")]
        public async Task<ApiResult<bool>> unlockfee(int id, int ModifyBy, int FeePdf)
        {
            ActionName = "ResetSSOID(int id, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    result.Data = await _unitOfWork.ITIMasterRepository.unlockfee(id, ModifyBy, FeePdf);
                    _unitOfWork.SaveChanges();

                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
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

        [HttpPost("GetITI_CollegeLoginInfoMaster")]
        public async Task<ApiResult<DataTable>> GetITI_CollegeLoginInfoMaster([FromBody] CollegeLoginInfoSearchModel body)
        {
            ActionName = "GetITI_CollegeLoginInfoMaster()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetITI_CollegeLoginInfoMaster(body));
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


        [HttpPost("Update_CollegeLoginInfo")]
        public async Task<ApiResult<int>> Update_CollegeLoginInfo([FromBody] CollegeLoginInfoSearchModel request)
        {
            ActionName = "Update_CollegeLoginInfo([FromBody] GroupMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITIMasterRepository.Update_CollegeLoginInfo(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.CollegeId == null)
                            result.Message = "Saved successfully .!";
                        else
                            result.Message = "Updated successfully .!";
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "Duplicate Entry";

                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.CollegeId == null)
                            result.ErrorMessage = "There was an error updating data.!";
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


        [HttpPost("GetCollegeLoginInfoByCode")]
        public async Task<ApiResult<DataTable>> GetCollegeLoginInfoByCode([FromBody] CollegeLoginInfoSearchModel body)
        {
            ActionName = "GetITI_CollegeLoginInfoMaster()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetCollegeLoginInfoByCode(body));
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

        [HttpGet("GetCenterDetailByPaperUploadID/{PaperUploadID:int}/{Userid:int}/{Roleid:int}")]
        public async Task<ApiResult<DataTable>> GetCenterDetail(int PaperUploadID , int Userid , int Roleid)
        {
            ActionName = "GetCenterDetailByPaperUploadID(int PaperUploadID ,int Userid , int Roleid )";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                      result.Data = await _unitOfWork.ITIMasterRepository.GetCenterDetailByPaperUploadID(PaperUploadID, Userid , Roleid);
                    
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

        [HttpPost("GetCenterWisePaperDetail")]
        public async Task<ApiResult<DataTable>> GetCenterWisePaperDetail([FromBody] CenterWisePaperDetailModal body)
        {
            ActionName = "GetCenterWisePaperDetail([FromBody] CenterWisePaperDetailModal)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetCenterWisePaperDetail(body));
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

        [HttpPost("PaperDownloadValidationCheck")]
        public async Task<ApiResult<DataTable>> PaperDownloadValidationCheck([FromBody] DownloadPaperValidationModal body)
        {
            ActionName = "GetCenterWisePaperDetail([FromBody] CenterWisePaperDetailModal)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.PaperDownloadValidationCheck(body));
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

        [HttpPost("UpdatePaperDownloadFalg")]
        public async Task<ApiResult<DataTable>> UpdatePaperDownloadFalg([FromBody] UpdateDownloadPaperFalgModal body)
        {
            ActionName = "UpdatePaperDownloadFalg([FromBody] CenterWisePaperDetailModal)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.UpdatePaperDownloadFalg(body));
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

        //[HttpPost("GetCenterIDByLoginUser")]
        //public async Task<ApiResult<DataTable>> GetCenterID([FromBody] CenterWisePaperDetailModal body)
        //{
        //    ActionName = "GetCenterID([FromBody] CenterWisePaperDetailModal)";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        result.Data = await Task.Run(() => _unitOfWork.ITIMasterRepository.GetCenterIDByLoginUser(body));
        //        result.State = EnumStatus.Success;
        //        if (result.Data.Rows.Count == 0)
        //        {
        //            result.State = EnumStatus.Success;
        //            result.Message = "No record found.!";
        //            return result;
        //        }
        //        result.State = EnumStatus.Success;
        //        result.Message = "Data load successfully .!";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _unitOfWork.Dispose();
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //        // write error log
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

    }
}
