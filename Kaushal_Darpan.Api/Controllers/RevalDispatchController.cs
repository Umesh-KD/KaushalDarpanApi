using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.DispatchFormDataModel;
using Kaushal_Darpan.Models.DispatchMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.IssuedItems;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class RevalDispatchController : BaseController
    {
        public override string PageName => "RevalDispatchController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RevalDispatchController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] DispatchSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetAllData(body));
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



        [HttpPost("GetBundelDataAllData")]
        public async Task<ApiResult<DataTable>> GetBundelDataAllData([FromBody] BundelDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                if (body.ExamDate== "NaN-NaN-NaN")
                {
                    body.ExamDate = "";
                }
               
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetBundelDataAllData(body));
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


        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] DispatchFormDataModel request)
        {
            ActionName = "SaveData([FromBody] DispatchFormDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}
                    var jsonData = JsonConvert.SerializeObject(request);

                    result.Data = await _unitOfWork.RevalDispatchRepository.SaveData(request);
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
                        else if (result.Data == 3)
                        {
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = "Quantity is Not Available";
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


        [HttpPost("SaveDispatchReceived")]
        public async Task<ApiResult<int>> SaveDispatchReceived([FromBody] List<DispatchReceivedModel> bundelDataModels)
        {
            ActionName = "SaveDispatchReceived([FromBody] BundelDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //ip
                    bundelDataModels.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });

                    result.Data = await _unitOfWork.RevalDispatchRepository.SaveDispatchReceived(bundelDataModels);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (result.Data == 1)
                        {
                            result.Message = "send data successfully !";
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





        [HttpPost("GetGroupDataAllData")]
        public async Task<ApiResult<InstituteGroupDetail>> GetGroupDataAllData(DispatchGroupSearchModel searchRequest)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<InstituteGroupDetail>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetGroupDataAllData(searchRequest);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<InstituteGroupDetail>(data);
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
            


        

       
        


        [HttpGet("GetGroupdetailsId/{ID:int}")]
        public async Task<ApiResult<DispatchGroupModel>> GetByID(int ID)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DispatchGroupModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetGroupdetailsId(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DispatchGroupModel>(data);
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


        #region Download Dispatch Received Report
        [HttpPost("GetDownloadDispatchReceived")]
        public async Task<ApiResult<string>> GetDownloadDispatchReceived([FromBody] DownloadDispatchReceivedSearchModel body)
        {
            ActionName = "GetDownloadDispatchReceived(DownloadDispatchReceivedSearchModel body)";
            var result = new ApiResult<string>();
            try
            {
                var fileName = $"ReceivedAtBterFromCenterReport{body.DispatchID}.pdf";
                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_ReceivingReceipt.rdlc";

                var data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetDownloadDispatchReceived(body, fileName, rdlcpath));
                if (data.Rows?.Count >= 1)
                {
                    //report

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("Dispatch_Receive_Receipt", data);
                    //localReport.AddDataSource("Dispatch_Bundle_Table", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    //bool Issuccess = await _unitOfWork.RevalDispatchRepository.UpdateDownloadFileDispatchMaster(fileName, body.DispatchID);
                    //if (Issuccess)
                    //{
                    //    result.Data = fileName;
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //}



                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
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




        [HttpDelete("DeleteGroupById/{ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteGroupById(int ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //var DeleteData_Request = new ScholarshipMaster
                    //{
                    //    ScholarshipID = ID,
                    //    ModifyBy = ModifyBy,
                    //};
                    result.Data = await _unitOfWork.RevalDispatchRepository.DeleteGroupById(ID, ModifyBy);
                    _unitOfWork.SaveChanges();

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

       


        //[HttpPost("OnSTatusUpdate")]
        //public async Task<ApiResult<bool>> OnSTatusUpdate([FromBody] List<DispatchStatusUpdate> request)
        //{
        //    ActionName = "SaveEnrolledData([FromBody] List<DispatchStatusUpdate> request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<bool>();
        //        try
        //        {

        //            // Pass the list to the repository for batch update
        //            var isSave = await _unitOfWork.RevalDispatchRepository.OnSTatusUpdate(request);
        //            _unitOfWork.SaveChanges();  // Commit changes if everything is successful

        //            if (isSave == -1)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_NO_DATA_SAVE;
        //            }
        //            else if (isSave > 0)
        //            {
        //                result.Data = true;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_SAVE_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _unitOfWork.Dispose();
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
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



       

        [HttpPost("OnSTatusUpdateByBTER")]
        public async Task<ApiResult<bool>> OnSTatusUpdateByBTER([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "OnSTatusUpdateByExaminer([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnSTatusUpdateByBTER(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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



       



        [HttpPost("GetDispatchGroupcodeDetails")]
        public async Task<ApiResult<DataTable>> GetDispatchGroupcodeDetails([FromBody] DispatchPrincipalGroupCodeSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetDispatchGroupcodeDetails(body));
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


        [HttpPost("GetDispatchGroupcodeList")]
        public async Task<ApiResult<DataTable>> GetDispatchGroupcodeList([FromBody] DispatchPrincipalGroupCodeSearchModel body)
        {
            ActionName = "GetDispatchGroupcodeList([FromBody] DispatchPrincipalGroupCodeSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetDispatchGroupcodeList(body));
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


        [HttpGet("GetDispatchGroupcodeId/{ID:int}")]
        public async Task<ApiResult<DispatchPrincipalGroupCodeDataModel>> GetDispatchGroupcodeId(int ID)
        {
            ActionName = "GetDispatchGroupcodeId(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DispatchPrincipalGroupCodeDataModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetDispatchGroupcodeId(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DispatchPrincipalGroupCodeDataModel>(data);
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



       
        [HttpPost("DownloadAckReportPri")]
        public async Task<ApiResult<string>> DownloadAckReportPri([FromBody] DispatchSearchModel model)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.DownloadAckReportPri(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"CenterObserverDutyOrder_{model.DispatchID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/Dispatch_AckByTheExaminer.rdlc";


                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("DispatchAck_OtherDetails", data.Tables[0]);
                        localReport.AddDataSource("DispatchAck_DataTable", data.Tables[1]);

                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

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


        [HttpPost("GetAllReceivedData")]
        public async Task<ApiResult<DataTable>> GetAllReceivedData([FromBody] DispatchSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetAllReceivedData(body));
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




        [HttpDelete("DeleteDispatchPrincipalGroupCodeById/{ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDispatchPrincipalGroupCodeById(int ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //var DeleteData_Request = new ScholarshipMaster
                    //{
                    //    ScholarshipID = ID,
                    //    ModifyBy = ModifyBy,
                    //};
                    result.Data = await _unitOfWork.RevalDispatchRepository.DeleteDispatchPrincipalGroupCodeById(ID, ModifyBy);
                    _unitOfWork.SaveChanges();

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


        [HttpGet("GetByIdDispatchMaster/{ID:int}")]
        public async Task<ApiResult<DispatchFormDataModel>> GetById(int ID)
        {
            ActionName = "GetByIdDispatchMaster(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DispatchFormDataModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetById(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DispatchFormDataModel>(data);
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


        [HttpDelete("DeleteDispatchMasterById/{ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDispatchMasterById(int ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //var DeleteData_Request = new ScholarshipMaster
                    //{
                    //    ScholarshipID = ID,
                    //    ModifyBy = ModifyBy,
                    //};
                    result.Data = await _unitOfWork.RevalDispatchRepository.DeleteDispatchMasterById(ID, ModifyBy);
                    _unitOfWork.SaveChanges();

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



        [HttpPost("GetAllDataDispatchMaster")]
        public async Task<ApiResult<DataTable>> GetAllDataDispatchMaster([FromBody] DispatchSearchModel body)
        {
            ActionName = "GetAllDataDispatchMaster()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetAllDataDispatchMaster(body));
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


        [HttpPost("OnSTatusUpdateDispatchMaster")]
        public async Task<ApiResult<bool>> OnSTatusUpdateDispatchMaster([FromBody] List<DispatchMasterStatusUpdate> request)
        {
            ActionName = "OnSTatusUpdateDispatchMaster([FromBody] List<DispatchMasterStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnSTatusUpdateDispatchMaster(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("CheckDateDispatchSearch")]
        public async Task<ApiResult<DataTable>> CheckDateDispatchSearch([FromBody] CheckDateDispatchSearchModel body)
        {
            ActionName = "CheckDateDispatchSearch([FromBody] CheckDateDispatchSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.CheckDateDispatchSearch(body));
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


       

        

        [HttpPost("DispatchSuperintendentAllottedExamDateList")]
        public async Task<ApiResult<DataTable>> DispatchSuperintendentAllottedExamDateList([FromBody] DispatchSearchModel body)
        {
            ActionName = "DispatchSuperintendentAllottedExamDateList([FromBody] DispatchSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.DispatchSuperintendentAllottedExamDateList(body));
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

        [HttpPost("UpdateRemarkImageHandedOverToExaminerByPrincipal")]
        public async Task<ApiResult<int>> UpdateRemarkImageHandedOverToExaminerByPrincipal([FromBody] UpdateFileHandovertoExaminerByPrincipalModel request)
        {
            ActionName = "UpdateRemarkImageHandedOverToExaminerByPrincipal([FromBody] DispatchFormDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.RevalDispatchRepository.UpdateRemarkImageHandedOverToExaminerByPrincipal(request);
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

        
        [HttpPost("GetAllDataCompanyDispatch")]
        public async Task<ApiResult<DataTable>> GetAllDataCompanyDispatch([FromBody] CompanyDispatchMasterSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetAllDataCompanyDispatch(body));
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

        [HttpPost("SaveDataCompanyDispatch")]
        public async Task<ApiResult<int>> SaveDataCompanyDispatch([FromBody] CompanyDispatchMasterModel request)
        {
            ActionName = "SaveDataCompanyDispatch([FromBody] CompanyDispatchMasterModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.RevalDispatchRepository.SaveDataCompanyDispatch(request);
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

        [HttpGet("GetByIdCompanyDispatch/{ID:int}")]
        public async Task<ApiResult<CompanyDispatchMasterModel>> GetByIdCompanyDispatch(int ID)
        {
            ActionName = "GetByIdCompanyDispatch(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<CompanyDispatchMasterModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetByIdCompanyDispatch(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<CompanyDispatchMasterModel>(data);
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

        [HttpDelete("DeleteDataCompanyDispatchByID/{ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataCompanyDispatchByID(int ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //var DeleteData_Request = new ScholarshipMaster
                    //{
                    //    ScholarshipID = ID,
                    //    ModifyBy = ModifyBy,
                    //};
                    result.Data = await _unitOfWork.RevalDispatchRepository.DeleteDataCompanyDispatchByID(ID, ModifyBy);
                    _unitOfWork.SaveChanges();

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


        [HttpPost("GetDispatchGroupcodeDetailsCheck")]
        public async Task<ApiResult<DataTable>> GetDispatchGroupcodeDetailsCheck([FromBody] DispatchPrincipalGroupCodeSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetDispatchGroupcodeDetailsCheck(body));
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
        
        
        //---------------bter-reval-----------------


        [HttpPost("GetRevalDispatchInstituteWiseExaminerBundle")]
        public async Task<ApiResult<DataTable>> GetRevalDispatchInstituteWiseExaminerBundle([FromBody] DispatchGroupSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetRevalDispatchInstituteWiseExaminerBundle(body));
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


        [HttpPost("SaveRevalDispatchInstituteWiseExaminerBundle")]
        public async Task<ApiResult<int>> SaveRevalDispatchInstituteWiseExaminerBundle([FromBody] RevalDispatchGroupModel request)
        {
            ActionName = "SaveRevalDispatchInstituteWiseExaminerBundle([FromBody] RevalDispatchGroupModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    //if (!ModelState.IsValid)
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Validation failed!";
                    //    return result;
                    //}


                    result.Data = await _unitOfWork.RevalDispatchRepository.SaveRevalDispatchInstituteWiseExaminerBundle(request);
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
                        else if (result.Data == 3)
                        {
                            result.State = EnumStatus.Warning;
                            result.ErrorMessage = "Quantity is Not Available";
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
                            result.ErrorMessage = "This college bundle is already assigned.";
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

        [HttpPost("GetB_RevalAllGroupData")]
        public async Task<ApiResult<DataTable>> GetB_RevalAllGroupData([FromBody] DispatchSearchModel body)
        {
            ActionName = "GetB_RevalAllGroupData([FromBody] DispatchSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetB_RevalAllGroupData(body));
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


        [HttpPost("OnSTatusUpdate")]
        public async Task<ApiResult<bool>> OnSTatusUpdate([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnSTatusUpdate(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpGet("GetRevalGroupdetailsId/{ID:int}")]
        public async Task<ApiResult<RevalDispatchGroupModel>> GetRevalGroupdetailsId(int ID)
        {
            ActionName = "GetByID(int AppointExaminerID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<RevalDispatchGroupModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetRevalGroupdetailsId(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<RevalDispatchGroupModel>(data);
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

        [HttpPost("getgroupteacherData")]
        public async Task<ApiResult<DataTable>> getgroupteacherData([FromBody] DispatchSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.getgroupteacherData(body));
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

        [HttpPost("UpdateBundleHandovertoExaminerByPrincipal")]
        public async Task<ApiResult<bool>> UpdateBundleHandovertoExaminerByPrincipal([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "OnSTatusUpdateByBTER([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.UpdateBundleHandovertoExaminerByPrincipal(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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



        [HttpPost("GetRevalDispatchGroupcodeDetailsCheck")]
        public async Task<ApiResult<DataTable>> GetRevalDispatchGroupcodeDetailsCheck([FromBody] DispatchPrincipalGroupCodeSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetRevalDispatchGroupcodeDetailsCheck(body));
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
        [HttpPost("GetRevalDispatchGroupcodeList")]
        public async Task<ApiResult<DataTable>> GetRevalDispatchGroupcodeList([FromBody] DispatchPrincipalGroupCodeSearchModel body)
        {
            ActionName = "GetRevalDispatchGroupcodeList([FromBody] DispatchPrincipalGroupCodeSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetRevalDispatchGroupcodeList(body));
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

        [HttpGet("GetRevalDispatchGroupcodeId/{ID:int}")]
        public async Task<ApiResult<DispatchPrincipalGroupCodeDataModel>> GetRevalDispatchGroupcodeId(int ID)
        {
            ActionName = "GetRevalDispatchGroupcodeId(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DispatchPrincipalGroupCodeDataModel>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.GetRevalDispatchGroupcodeId(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<DispatchPrincipalGroupCodeDataModel>(data);
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

        [HttpPost("SaveRevalDispatchPrincipalGroupCodeData")]
        public async Task<ApiResult<int>> SaveRevalDispatchPrincipalGroupCodeData([FromBody] DispatchPrincipalGroupCodeDataModel request)
        {
            ActionName = "SaveRevalDispatchPrincipalGroupCodeData([FromBody] DispatchPrincipalGroupCodeDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.RevalDispatchRepository.SaveRevalDispatchPrincipalGroupCodeData(request);
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


        [HttpPost("getRevalgroupExaminerData")]
        public async Task<ApiResult<DataTable>> getRevalgroupExaminerData([FromBody] DispatchSearchModel body)
        {
            ActionName = "getRevalgroupExaminerData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.getRevalgroupExaminerData(body));
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


        [HttpPost("OnRevalSTatusUpdateByExaminer")]
        public async Task<ApiResult<bool>> OnRevalSTatusUpdateByExaminer([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "OnSTatusUpdateByBTER([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnRevalSTatusUpdateByExaminer(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("RevalBundelNoSendToThePrincipalByTheExaminer")]
        public async Task<ApiResult<bool>> RevalBundelNoSendToThePrincipalByTheExaminer([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "RevalBundelNoSendToThePrincipalByTheExaminer([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.RevalBundelNoSendToThePrincipalByTheExaminer(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("RevalDeleteGroupById")]
        public async Task<ApiResult<int>> RevalDeleteGroupById([FromBody] RevalDispatchGroupModel body)
        {

            ActionName = "RevalDeleteGroupById([FromBody] RevalDispatchGroupModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.RevalDispatchRepository.RevalDeleteGroupById(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
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
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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

        [HttpPost("RevalDeleteDispatchPrincipalGroupCode")]
        public async Task<ApiResult<int>> RevalDeleteDispatchPrincipalGroupCode([FromBody] RevalDeleteDispatchPrincipalGroupCodeCModel body)
        {

            ActionName = "RevalDeleteDispatchPrincipalGroupCode([FromBody] RevalDeleteDispatchPrincipalGroupCodeCModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.RevalDispatchRepository.RevalDeleteDispatchPrincipalGroupCode(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_ERROR;
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
                    if (result.Data == 0)
                    {
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
                    else
                    {
                        result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
                    }
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

        [HttpPost("OnVerifiedtoPrincipalToAdmin")]
        public async Task<ApiResult<bool>> OnVerifiedtoPrincipalToAdmin([FromBody] List<DispatchStatusUpdate> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<DispatchStatusUpdate> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnVerifiedtoPrincipalToAdmin(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("RevalDownloadAckReportPri")]
        public async Task<ApiResult<string>> RevalDownloadAckReportPri([FromBody] DispatchSearchModel model)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.RevalDispatchRepository.RevalDownloadAckReportPri(model);
                    if (data != null)
                    {
                        //report
                        var fileName = $"RevalCenterObserverDutyOrder_{model.DispatchID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalDispatch_AckByTheExaminer.rdlc";


                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("DispatchAck_OtherDetails", data.Tables[0]);
                        localReport.AddDataSource("DispatchAck_DataTable", data.Tables[1]);

                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

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


        [HttpPost("GetRevalDownloadDispatchReceived")]
        public async Task<ApiResult<string>> GetRevalDownloadDispatchReceived([FromBody] DownloadDispatchReceivedSearchModel body)
        {
            ActionName = "GetRevalDownloadDispatchReceived(DownloadDispatchReceivedSearchModel body)";
            var result = new ApiResult<string>();
            try
            {
                var fileName = $"RevalReceivedAtBterFromCenterReport{body.DispatchID}.pdf";
                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/RevalDispatch_ReceivingReceipt.rdlc";

                var data = await Task.Run(() => _unitOfWork.RevalDispatchRepository.GetRevalDownloadDispatchReceived(body, fileName, rdlcpath));
                if (data.Rows?.Count >= 1)
                {
                    //report

                    LocalReport localReport = new LocalReport(rdlcpath);
                    localReport.AddDataSource("Dispatch_Receive_Receipt", data);
                    //localReport.AddDataSource("Dispatch_Bundle_Table", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    //check file exists
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    //save
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //end report
                    result.Data = fileName;
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;

                    //bool Issuccess = await _unitOfWork.RevalDispatchRepository.UpdateDownloadFileDispatchMaster(fileName, body.DispatchID);
                    //if (Issuccess)
                    //{
                    //    result.Data = fileName;
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //}



                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_DATA_NOT_FOUND;
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

        [HttpPost("OnSTatusUpdateDispatchl")]
        public async Task<ApiResult<bool>> OnSTatusUpdateDispatchl([FromBody] List<UpdateStatusDispatchPrincipalGroupCodeModel> request)
        {
            ActionName = "OnSTatusUpdateDispatchl([FromBody] List<UpdateStatusDispatchPrincipalGroupCodeModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.RevalDispatchRepository.OnSTatusUpdateDispatchl(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

    }
}


