using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Api.Code.Helper;
using Kaushal_Darpan.Api.Report.ITI;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.ITI_Inspection;
using Kaushal_Darpan.Models.ITIAllotment;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO.Compression;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ITI_InspectionController : BaseController
    {
        public override string PageName => "ITI_InspectionController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITI_InspectionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITI_InspectionSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetAllData(body));
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

        [HttpPost("GetAllInspectedData")]
        public async Task<ApiResult<DataTable>> GetAllInspectedData([FromBody] ITI_InspectionSearchModel body)
        {
            ActionName = "GetAllInspectedData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetAllInspectedData(body));
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

        [HttpPost("GetITIInspectionDropdown")]
        public async Task<ApiResult<DataTable>> GetITIInspectionDropdown([FromBody] ITI_InspectionDropdownModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetITIInspectionDropdown(body));
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

        [HttpPost("SaveData")]
        public async Task<ApiResult<int>> SaveData([FromBody] ITI_InspectionDataModel request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_InspectionRepository.SaveData(request);
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


        [HttpPost("SaveInspectionDeploymentData")]
        public async Task<ApiResult<bool>> SaveInspectionDeploymentData([FromBody] List<InspectionDeploymentDataModel> request)
        {
            ActionName = "SaveInspectionDeploymentData([FromBody] List<InspectionDeploymentDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //request.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();

                    //});
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITI_InspectionRepository.SaveInspectionDeploymentData(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
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

        [HttpGet("GetById_Team/{ID}")]
        public async Task<ApiResult<ITI_InspectionDataModel>> GetById_Team(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITI_InspectionDataModel>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetById_Team(ID);
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

        [HttpPost("GetHistoryDataById_Team/{ID}")]
        public async Task<ApiResult<DataTable>> GetHistoryDataById_Team(int ID)
        {
            ActionName = "GetHistoryDataById_Team(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetHistoryDataById_Team(ID);
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


        [HttpGet("GetById_Deployment/{ID}")]
        public async Task<ApiResult<DataTable>> GetById_Deployment(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetById_Deployment(ID);
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

        [HttpPost("GetInspectionDataByID_Status")]
        public async Task<ApiResult<DataTable>> GetInspectionDataByID_Status([FromBody] ITI_InspectionSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetInspectionDataByID_Status(body));
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

        [HttpPost("GetAllData_GenerateOrder")]
        public async Task<ApiResult<DataTable>> GetAllData_GenerateOrder([FromBody] ITI_InspectionSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetAllData_GenerateOrder(body));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully.!";
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

        [HttpPost("GenerateInspectionDutyOrder")]
        public async Task<ApiResult<string>> GenerateInspectionDutyOrder([FromBody] List<CODeploymentDataModel> model)
        {
            ActionName = "GenerateCenterObserverDutyOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    model.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    var data = await _unitOfWork.ITI_InspectionRepository.GenerateInspectionDutyOrder(model);
                    if (data != null)
                    {
                        var fileName = $"CenterObserverDutyOrder_{data.Tables[0].Rows[0]["DeploymentDate"]}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/CenterObserverDutyOrderReport.rdlc";

                        model.ForEach(x =>
                        {
                            x.DutyOrderPath = filepath;
                            x.DutyOrder = fileName;
                        });

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("CenterObserverMember", data.Tables[0]);
                        localReport.AddDataSource("CenterObserverDeploymentDetails", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        //result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }
                    //foreach (var Team in model)
                    //{
                    //    var data = await _unitOfWork.CenterObserverRepository.GenerateCenterObserverDutyOrder(Team);
                    //    if (data != null)
                    //    {
                    //        //report
                    //        var fileName = $"CenterObserverDutyOrder_{Team.DeploymentID}_{Team.CenterObserverTeamID}.pdf";
                    //        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                    //        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/CenterObserverDutyOrderReport.rdlc";

                    //        Team.DutyOrderPath = filepath;
                    //        Team.DutyOrder = fileName;

                    //        //
                    //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    //        LocalReport localReport = new LocalReport(rdlcpath);

                    //        localReport.AddDataSource("CenterObserverMember", data.Tables[0]);
                    //        localReport.AddDataSource("CenterObserverDeploymentDetails", data.Tables[1]);
                    //        var reportResult = localReport.Execute(RenderType.Pdf);

                    //        //check file exists
                    //        if (!System.IO.Directory.Exists(folderPath))
                    //        {
                    //            Directory.CreateDirectory(folderPath);
                    //        }

                    //        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                    //        //end report

                    //        //result.Data = fileName;
                    //        //result.State = EnumStatus.Success;
                    //        //result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //    }
                    //    else
                    //    {
                    //        result.State = EnumStatus.Warning;
                    //        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //    }
                    //}

                   // var Issuccess = await _unitOfWork.ITI_InspectionRepository.UpdateDutyOrder(model);
                    //if (Issuccess > 0)
                    //{
                    //    result.Data = Issuccess.ToString();
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //}

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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("check_Engagement")]
        public async Task<ApiResult<Boolean>> check_Engagement([FromBody] InspectionMemberDetailsDataModel model)
        {
            //ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<Boolean>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.check_Engagement(model);
                 
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

        [HttpPost("SaveCheckSSODataModel")]
        public async Task<ApiResult<int>> SaveCheckSSODataModel([FromBody] SaveCheckSSODataModel request)
        {
            //ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_InspectionRepository.SaveCheckSSODataModel(request);
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
                    else if (result.Data == -10)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "This user already engage on this date range!";
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

        [HttpPost("UpdateDeployment/{ID}")]
        public async Task<ApiResult<int>> UpdateDeployment(int id)
        {
            //ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_InspectionRepository.UpdateDeployment(id);
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

        [HttpPost("GenerateInspectionDeploymentOrder/{id}")]
        public async Task<ApiResult<string>> GenerateInspectionDeploymentOrder(int id)
        {
            CODeploymentDataModel model = new CODeploymentDataModel();
            ActionName = "GenerateInspectionDeploymentOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {

                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GenerateInspectionDeploymentOrder(id);
                    if (data != null)
                    {
                        //var fileName = $"InspectionDutyOrder.pdf";
                        var fileName = $"InspectionDutyOrder_{data.Tables[0].Rows[0]["InstituteID"]}{Guid.NewGuid()}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/InspectionDutyOrder.rdlc";
                        //string pdfpath = "http://localhost:5230/Kaushal_Darpan.Api/StaticFiles/Reports/" + fileName;
                     
                            model.DutyOrderPath = filepath;
                            model.DutyOrder = fileName;
                            model.IPAddress = CommonFuncationHelper.GetIpAddress();
                            model.DeploymentID = Convert.ToInt32(data.Tables[0].Rows[0]["DeploymentID"]);
                            model.InspectionTeamID = id;

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("InspectionDeployInstitute", data.Tables[0]);
                        localReport.AddDataSource("InspectionMembers", data.Tables[1]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        //result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        result.PDFURL = fileName;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }

                    var Issuccess = await _unitOfWork.ITI_InspectionRepository.UpdateDutyOrder(model);
                    if (Issuccess > 0)
                    {
                        result.Data = Issuccess.ToString();
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
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetITIInspectionDashData")]
        public async Task<ApiResult<DataTable>> GetITIInspectionDashData([FromBody] InspectionDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetITIInspectionDashData(model));
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

        [HttpPost("GetITIInspectionMemeberTeamList")]
        public async Task<ApiResult<DataTable>> GetITIInspectionMemeberTeamList([FromBody] InspectionDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetITIInspectionMemeberTeamList(model));
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

        [HttpPost("GetITIInspectionInstituteList")]
        public async Task<ApiResult<DataTable>> GetITIInspectionInstituteList([FromBody] InspectionDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetITIInspectionInstituteList(model));
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

        [HttpPost("GetITIInspectionQuestionData")]
        public async Task<ApiResult<DataTable>> GetITIInspectionQuestionData([FromBody] InspectionDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetITIInspectionQuestionData(model));
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

        [HttpPost("SaveInspectionAnswersByInstitute")]
        public async Task<ApiResult<int>> SaveInspectionAnswersByInstitute([FromBody] ITI_InspectionAnswerModel request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
         
                var result = new ApiResult<int>();
                try
                {
                    
                    result.Data = await _unitOfWork.ITI_InspectionRepository.SaveInspectionAnswersByInstitute(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = "Inspection Already Completed";
                        //result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
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
         
        }

    

        [HttpPost("GenerateCOAnsweredReport/{id}")]
        public async Task<ApiResult<string>> GenerateCOAnsweredReport(int id)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GenerateCOAnsweredReport(id);
                    if (data != null)
                    {
                        ////report
                        //var fileName = $"ITIInspectionChekckListReport_{id}.pdf";
                        //string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        //string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIInspectionCheckListReport.rdlc";


                        ////
                        //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //LocalReport localReport = new LocalReport(rdlcpath);

                        //localReport.AddDataSource("ITICheckListDetails", data.Tables[0]);
                        //localReport.AddDataSource("ITIChecklistAnswers", data.Tables[1]);

                        //var reportResult = localReport.Execute(RenderType.Pdf);

                        ////check file exists
                        //if (!System.IO.Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}

                        //System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        ////end report
                        ///
                        // Assuming data gives you full file paths OR relative paths
                        //List<string> files = data.DataSetName.Select(x => Path.Combine("wwwroot/uploads", x.DocFile)).ToList();
                        
                        List<string> files = data.Tables[1].AsEnumerable().Select(x => Path.Combine($"{ConfigurationHelper.StaticFileRootPath}/InspectionManagerITI", x["Answer"].ToString())).ToList();
                       
                        byte[] mergedPdf = WordHelper.MergePdfFiles(files);

                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string fileName = $"MergedReport_{Guid.NewGuid()}.pdf";
                        string fullPath = Path.Combine(folderPath, fileName);

                        System.IO.File.WriteAllBytes(fullPath, mergedPdf);

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
                    await _unitOfWork.DisposeAsync();
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

        //DownloadZipDocumentModel request
        [HttpPost("Consolidate-Zip/{id}")]
        public async Task<IActionResult> DownloadConsolidate(int id)
        {
            try
            {

                //string rootPath = ConfigurationHelper.StaticFileRootPath
                //.Replace("/", "\\")
                //.TrimEnd('\\');

                string targetFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "ITI/InspectionManagerITI");

                //string targetFolder = Path.Combine(ConfigurationHelper.StaticFileRootPath, "Students", "BTER", request.FinancialYearID.ToString(), request.Eng_NonEng.ToString());
                //string zipFileName = "Students.zip";
                //string targetFolder = Path.Combine(
                //    ConfigurationHelper.StaticFileRootPath,
                //    "InspectionManagerITI"
                //);
                string zipFileName = "Consolidated.zip";

                //if (!Directory.Exists(targetFolder))
                //{
                //     return Content("Folder not found!");
                //}

                var data = await _unitOfWork.ITI_InspectionRepository.GenerateCOAnsweredReport(id);
                if (data != null)
                {

                    //var fileList = await _repository.GetStudentFiles(request);
                    //List<string> files = data.Tables[1].AsEnumerable().Select(x => Path.Combine($"{ConfigurationHelper.StaticFileRootPath}/InspectionManagerITI", x["Answer"].ToString())).ToList();
                    List<string> files = data.Tables[1].AsEnumerable()
                     .Select(x => Path.Combine(
                         ConfigurationHelper.StaticFileRootPath,
                         "ITI/InspectionManagerITI",
                         x["Answer"].ToString()
                     ))
                     .ToList();

                    //if (files.length == 0)
                    //{
                    //    return Content("File not found!");
                    //}

                    var imageFiles = files
                        .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                                 || f.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                                 || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    //using (MemoryStream zipStream = new MemoryStream())
                    //{
                    //    using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    //    {
                    //        foreach (var filePath in imageFiles)
                    //        {
                    //            if (System.IO.File.Exists(filePath)) // ✅ safe check
                    //            {
                    //                string fileName = Path.GetFileName(filePath);
                    //                zip.CreateEntryFromFile(filePath, fileName);
                    //            }
                    //        }
                    //    }

                    //    zipStream.Position = 0;
                    //    return File(zipStream.ToArray(), "application/zip", zipFileName);
                    //}

                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {
                            foreach (var filePath in imageFiles)
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    string fileName = Path.GetFileName(filePath);
                                    zip.CreateEntryFromFile(filePath, fileName);
                                }
                            }
                        }

                        zipStream.Position = 0;

                        HttpContext.Response.Clear();
                        HttpContext.Response.ContentType = "application/zip";
                        HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{zipFileName}\"");

                        await zipStream.CopyToAsync(HttpContext.Response.Body);

                        return new EmptyResult(); // Response is already written
                    }
                }

                return Content("No Data Found !");
            }
            catch (Exception ex)
            {
                await _unitOfWork.DisposeAsync();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                return Content("Error while downloading ZIP");
            }
        }

        [HttpPost("UpdateAttendance")]
        public async Task<ApiResult<bool>> UpdateAttendance(UpdateAttendance model)
        {
            ActionName = "DeleteDataByID(CenterObserverDataModel model)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InspectionRepository.UpdateAttendance(model);
                    await _unitOfWork.SaveChangesAsync();

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


        [HttpPost("IsRequestInspection")]
        public async Task<ApiResult<int>> IsRequestInspection([FromBody] PostIsRequestCenterObserver model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.IsRequestInspection(model);
                    //var data1 = await _unitOfWork.ITICenterObserverRepository.IsRequestHistoryCenterObserver(model);

                    await _unitOfWork.SaveChangesAsync();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Data = data;
                        result.Message = "Student Mapped Successfully";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                        result.Data = data;
                    }
                    return result;
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
            });
        }


        [HttpPost("RequestApprovedbyAdmin/{Deployeid}")]
        public async Task<ApiResult<int>> RequestApprovedbyAdmin(int Deployeid = 0, string Remark = "")
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.ApproveRequest(Deployeid);

                    await _unitOfWork.SaveChangesAsync();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Data = data;
                        result.Message = "Student Mapped Successfully";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Something went wrong";
                        result.Data = data;
                    }
                    return result;
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
            });
        }

        [HttpPost("GetDistrictMaster")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDistrictMaster([FromBody] ITI_InspectionSearchModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetDistrictMaster(body);
                    if (data.Count > 0)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("saveConsentData")]
        public async Task<ApiResult<int>> saveConsentData([FromBody] ConsentModel request)
        {
            ActionName = " saveConsentData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_InspectionRepository.saveConsentData(request);
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


        [HttpPost("GetAllConsentData")]
        public async Task<ApiResult<DataTable>> GetAllConsentData([FromBody] ConsentModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetAllConsentData(body));
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


        [HttpPost("saveConsent")]
        public async Task<ApiResult<bool>> saveConsent([FromBody] List<ConsentModel> request)
        {
            ActionName = "saveConsent([FromBody] List<ConsentModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    //request.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();

                    //});
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITI_InspectionRepository.saveConsent(request);
                    await _unitOfWork.SaveChangesAsync();  

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
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

        [HttpPost("GetDistrict")]
        public async Task<ApiResult<List<CommonDDLModel>>> GetDistrict([FromBody] ITI_ConsentSearchModel body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<CommonDDLModel>>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetDistrict(body);
                    if (data.Count > 0)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found!";
                    }
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetById_Consent/{ID}")]
        public async Task<ApiResult<DataTable>> GetById_Consent(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ITI_InspectionRepository.GetById_Consent(ID);
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



        [HttpPost("updateConsent")]
        public async Task<ApiResult<int>> updateConsent([FromBody] ConsentModel request)
        {
            ActionName = "updateConsent([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITI_InspectionRepository.updateConsent(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "update successfully .!";
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
            });
        }

        [HttpPost("GetAllConsentbyPrincipal")]
        public async Task<ApiResult<DataTable>> GetAllConsentbyPrincipal([FromBody] ConsentModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_InspectionRepository.GetAllConsentbyPrincipal(body));
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



    }
}
