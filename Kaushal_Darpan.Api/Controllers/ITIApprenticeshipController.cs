using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.ITI_Apprenticeship;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITIFlyingSquad;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ITIApprenticeshipController : BaseController
    {
        public override string PageName => "ITI_ApprenticeshipController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIApprenticeshipController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITI_ApprenticeshipSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetAllData(body));
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

        [HttpPost("GetAllInspectedData")]
        public async Task<ApiResult<DataTable>> GetAllInspectedData([FromBody] ITI_ApprenticeshipSearchModel body)
        {
            ActionName = "GetAllInspectedData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetAllInspectedData(body));
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

        [HttpPost("GetITIApprenticeshipDropdown")]
        public async Task<ApiResult<DataTable>> GetITIApprenticeshipDropdown([FromBody] ITI_ApprenticeshipDropdownModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetITIApprenticeshipDropdown(body));
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
        public async Task<ApiResult<int>> SaveData([FromBody] ITI_ApprenticeshipDataModel request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.SaveData(request);
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


        [HttpPost("SaveApprenticeshipDeploymentData")]
        public async Task<ApiResult<bool>> SaveApprenticeshipDeploymentData([FromBody] List<ApprenticeshipDeploymentDataModel> request)
        {
            ActionName = "SaveApprenticeshipDeploymentData([FromBody] List<ApprenticeshipDeploymentDataModel> request)";
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
                    var isSave = await _unitOfWork.ITI_ApprenticeshipRepository.SaveApprenticeshipDeploymentData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

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

        [HttpGet("GetById_Team/{ID}")]
        public async Task<ApiResult<ITI_ApprenticeshipDataModel>> GetById_Team(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITI_ApprenticeshipDataModel>();
                try
                {
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.GetById_Team(ID);
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

        [HttpGet("GetById_Deployment/{ID}")]
        public async Task<ApiResult<DataTable>> GetById_Deployment(int ID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.GetById_Deployment(ID);
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

        [HttpPost("GetApprenticeshipDataByID_Status")]
        public async Task<ApiResult<DataTable>> GetApprenticeshipDataByID_Status([FromBody] ITI_ApprenticeshipSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetApprenticeshipDataByID_Status(body));
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

        [HttpPost("GetAllData_GenerateOrder")]
        public async Task<ApiResult<DataTable>> GetAllData_GenerateOrder([FromBody] ITI_ApprenticeshipSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetAllData_GenerateOrder(body));
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

        [HttpPost("GenerateApprenticeshipDutyOrder")]
        public async Task<ApiResult<string>> GenerateApprenticeshipDutyOrder([FromBody] List<CODeploymentDataModel> model)
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
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.GenerateApprenticeshipDutyOrder(model);
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

                    var Issuccess = await _unitOfWork.CenterObserverRepository.UpdateDutyOrder(model);
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

        [HttpPost("check_Engagement")]
        public async Task<ApiResult<Boolean>> check_Engagement([FromBody] ApprenticeshipMemberDetailsDataModel model)
        {
            //ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<Boolean>();
                try
                {
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.check_Engagement(model);
                 
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

        [HttpPost("SaveCheckSSODataModel")]
        public async Task<ApiResult<int>> SaveCheckSSODataModel([FromBody] Apprenticeship_SaveCheckSSODataModel request)
        {
            //ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    //request.IPAddress = CommonFuncationHelper.GetIpAddress();
                    result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.SaveCheckSSODataModel(request);
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
                   // result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateDeployment(id);
                    result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateDutyOrder(id);
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

        [HttpPost("GenerateApprenticeshipDeploymentOrder/{id}")]
        public async Task<ApiResult<string>> GenerateApprenticeshipDeploymentOrder(int id)
        {
            CODeploymentDataModel model = new CODeploymentDataModel();
            ActionName = "GenerateApprenticeshipDeploymentOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {

                var result = new ApiResult<string>();
                try
                {
                    //model.ForEach(x =>
                    //{
                    //    x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    //});
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.GenerateApprenticeshipDeploymentOrder(id);
                    if (data != null)
                    {
                        //var fileName = $"ApprenticeshipDutyOrder.pdf";
                        var fileName = $"ApprenticeshipDutyOrder_{data.Tables[0].Rows[0]["InstituteID"]}{Convert.ToDateTime(data.Tables[0].Rows[0]["DeploymentDateFrom"]).ToString("dd/MM/yyyy")}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ApprenticeshipDutyOrder.rdlc";
                        //string pdfpath = "http://localhost:5230/Kaushal_Darpan.Api/StaticFiles/Reports/" + fileName;
                        //model.ForEach(x =>
                        //{
                        //    x.DutyOrderPath = filepath;
                        //    x.DutyOrder = fileName;
                        //});
                        model.DutyOrderPath = filepath;
                        model.DutyOrder = fileName;
                        model.IPAddress = CommonFuncationHelper.GetIpAddress();
                        model.DeploymentID = Convert.ToInt32(data.Tables[0].Rows[0]["DeploymentID"]);
                        model.InspectionTeamID = id;

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("ApprenticeshipDeployInstitute", data.Tables[0]);
                        localReport.AddDataSource("ApprenticeshipMembers", data.Tables[1]);
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

                    //var Issuccess = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateDutyOrder(model);
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


        [HttpPost("GetITIApprenticeshipDashData")]
        public async Task<ApiResult<DataTable>> GetITIApprenticeshipDashData([FromBody] ApprenticeshipDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetITIApprenticeshipDashData(model));
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

        [HttpPost("GetITIApprenticeshipMemeberTeamList")]
        public async Task<ApiResult<DataTable>> GetITIApprenticeshipMemeberTeamList([FromBody] ApprenticeshipDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetITIApprenticeshipMemeberTeamList(model));
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

        [HttpPost("GetITIApprenticeshipIndustryList")]
        public async Task<ApiResult<DataTable>> GetITIApprenticeshipIndustryList([FromBody] ApprenticeshipDeploymentDataModel model)
        {
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetITIApprenticeshipIndustryList(model));
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

        [HttpPost("GetITIApprenticeshipQuestionData")]
        public async Task<ApiResult<DataSet>> GetITIApprenticeshipQuestionData([FromBody] ApprenticeshipDeploymentDataModel model)
        {
            var result = new ApiResult<DataSet>();
            var data = new DataSet();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetITIApprenticeshipQuestionData(model));
                result.State = EnumStatus.Success;
                if (result.Data != null)
                {
                    data = result.Data;

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

        [HttpPost("SaveApprenticeshipAnswersByIndustry")]
        public async Task<ApiResult<int>> SaveApprenticeshipAnswersByIndustry([FromBody] ITI_ApprenticeshipAnswerModel? request)
        {
            ActionName = " SaveAllData([FromBody] AdminUserDetailModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    int DeploymentID = Convert.ToInt32(request.MainData[0]?.DeploymentID);


                    result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.SaveApprenticeshipAnswersByIndustry(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        var reportPath = await GetReportById(DeploymentID);
                        result.State = EnumStatus.Success;
                        result.Message = "Saved Answer Successfully";

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

        [HttpPost("GenerateCOAnsweredReport/{DeploymentID}")]
        public async Task<ApiResult<string>> GenerateCOAnsweredReport(int DeploymentID)
        {
            ActionName = "GetFlyingSquadDutyOrder()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.GenerateCOAnsweredReport(DeploymentID);
                    if (data != null)
                    {
                        //report
                        var fileName = $"ITIApprenticeshipChekckListReport_{DeploymentID}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIApprenticeshipCheckListReport.rdlc";


                        //
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        DataTable dt = new DataTable();

                        dt.Columns.Add("AnswerText1");
                        dt.Columns.Add("AnswerText2");
                        dt.Columns.Add("AnswerText3");

                        DataRow row = dt.NewRow();
                        row["AnswerText1"] = data.Tables[1].Rows[0]["Answer"];
                        row["AnswerText2"] = data.Tables[1].Rows[1]["Answer"];
                        row["AnswerText3"] = data.Tables[1].Rows[2]["Answer"];
                        dt.Rows.Add(row);

                        localReport.AddDataSource("ApprenticeshipBasic", data.Tables[0]);
                        localReport.AddDataSource("ApprenticeshipAnsMain", dt);
                        localReport.AddDataSource("ApprenticeshipAnsTradeWise", data.Tables[2]);
                        localReport.AddDataSource("ApprenticeshipAnsTrainers", data.Tables[3]);
                        localReport.AddDataSource("ApprenticeshipAnsFacility", data.Tables[4]);

                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        var Issuccess = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateReport(fileName, DeploymentID);
                        _unitOfWork.SaveChanges();  // Commit changes if everything is successful
                        if (Issuccess > 0)
                        {
                            result.Data = fileName;
                            result.State = EnumStatus.Success;
                            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                        }
                        else
                        {
                            result.State = EnumStatus.Error;
                            result.Message = "Report Not Updated Successfully";
                        }

                        //end report

                       
                        
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

        [HttpPost("GetAllInspectedDataByID")]
        public async Task<ApiResult<DataTable>> GetAllInspectedDataByID([FromBody] ITI_ApprenticeshipSearchModel body)
        {
            ActionName = "GetAllInspectedData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITI_ApprenticeshipRepository.GetAllInspectedDataByID(body));
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


        //Shared logic
        private async Task<string> GetReportById(int DeploymentID)
        {
            ActionName = "GetFlyingSquadDutyOrder";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            var fileName = $"ITIApprenticeshipCheckListReport_{DeploymentID}.pdf";
            var filepath = Path.Combine(folderPath, fileName);

            try
            {
                var data = await _unitOfWork.ITI_ApprenticeshipRepository.GenerateCOAnsweredReport(DeploymentID);
                if (data != null && data.Tables.Count >= 5)
                {
                    // Prepare Report
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIApprenticeshipCheckListReport.rdlc");

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);

                    // Custom DataTable for AnswerText1-3
                    DataTable dt = new DataTable();
                    dt.Columns.Add("AnswerText1");
                    dt.Columns.Add("AnswerText2");
                    dt.Columns.Add("AnswerText3");

                    DataRow row = dt.NewRow();
                    row["AnswerText1"] = data.Tables[1].Rows[0]["Answer"];
                    row["AnswerText2"] = data.Tables[1].Rows[1]["Answer"];
                    row["AnswerText3"] = data.Tables[1].Rows[2]["Answer"];
                    dt.Rows.Add(row);

                    // Add data sources
                    localReport.AddDataSource("ApprenticeshipBasic", data.Tables[0]);
                    localReport.AddDataSource("ApprenticeshipAnsMain", dt);
                    localReport.AddDataSource("ApprenticeshipAnsTradeWise", data.Tables[2]);
                    localReport.AddDataSource("ApprenticeshipAnsTrainers", data.Tables[3]);
                    localReport.AddDataSource("ApprenticeshipAnsFacility", data.Tables[4]);

                    // Execute report
                    var reportResult = localReport.Execute(RenderType.Pdf);

                    // Ensure folder exists
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    // Write file
                    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                    // Update database with report filename
                    var isSuccess = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateReport(fileName, DeploymentID);
                    _unitOfWork.SaveChanges();

                    if (isSuccess > 0)
                        return filepath;
                    else
                        return null; // Failed to update DB
                }
                else
                {
                    return null; // No data
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                // Log exception
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);

                return null; // Error occurred
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
                    result.Data = await _unitOfWork.ITI_ApprenticeshipRepository.UpdateAttendance(model);
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


        [HttpPost("IsRequestInspection")]
        public async Task<ApiResult<int>> IsRequestApprenticeship([FromBody] PostIsRequestCenterObserver model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.IsRequestApprenticeship(model);
                    //var data1 = await _unitOfWork.ITICenterObserverRepository.IsRequestHistoryCenterObserver(model);

                    _unitOfWork.SaveChanges();
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
                    var data = await _unitOfWork.ITI_ApprenticeshipRepository.ApproveRequest(Deployeid);

                    _unitOfWork.SaveChanges();
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
            });
        }

    }
}
