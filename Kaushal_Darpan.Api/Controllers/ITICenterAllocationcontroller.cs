using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.ItiStudentActivities;
using Kaushal_Darpan.Models.TheoryMarks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class ITICenterAllocationcontroller : BaseController
    {
        public override string PageName => "CenterAllocationController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITICenterAllocationcontroller(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetAllData(filterModel);
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

        [HttpPost("GetInstituteByCenterID")]
        public async Task<ApiResult<DataTable>> GetInstituteByCenterID([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetInstituteByCenterID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetInstituteByCenterID(filterModel);
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




        [HttpPost("SaveAllData")]
        public async Task<ApiResult<bool>> SaveAllData([FromBody] List<ITICenterAllocationModel> request)
        {
            ActionName = "SaveAllData([FromBody] List<PlacementShortListStudentResponseModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();

                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITICenterAllocationRepository.SaveData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -2)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_NO_DATA_UPDATE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
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

        [HttpPost("GetCenterSuperintendent")]
        public async Task<ApiResult<DataTable>> GetCenterSuperintendent([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITICenterAllocationRepository.CenterSuperintendent(filterModel);
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

        [HttpPost("AssignCenterSuperintendent")]
        public async Task<ApiResult<bool>> AssignCenterSuperintendent([FromBody] CenterSuperintendentDetailsModel request)
        {
            ActionName = "UpdateCCCode([FromBody] AssignCenterSuperintendent request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    var isSave = await _unitOfWork.ITICenterAllocationRepository.AssignCenterSuperintendent(request);
                    _unitOfWork.SaveChanges();

                    if (isSave == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
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

        [HttpPost("DownloadCenterSuperintendent")]
        public async Task<ApiResult<string>> DownloadCenterSuperintendent([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                    try
                    {
                    var data = await _unitOfWork.ITICenterAllocationRepository.DownloadCenterSuperintendent(filterModel);
                    if (data != null)
                    { 
                        //report

                        var fileName = $"CenterSuperintendentReport_SCVT.pdf";
                        string folderName = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.CenterSuperintendentFolder_ITI}";
                        string filepath = Path.Combine(folderName, fileName);


                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/CenterSuperintendentReport_SCVT.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("CenterSuperintendentReport_SCVT", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;


                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                        ModInsert.FileName = fileName;
                        ModInsert.PDFType = (int)EnumPdfType.CenterSuperintendent;
                        ModInsert.Status = 11;
                        ModInsert.DepartmentID=2;
                        ModInsert.Eng_NonEng=filterModel.Eng_NonEng.Value;
                        ModInsert.EndTermID=filterModel.EndTermID;



                        var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
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



        [HttpPost("SaveWorkflow")]
        public async Task<ApiResult<bool>> SaveWorkflow([FromBody] DownloadnRollNoModel request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<VerifyRollNumberList> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ITICenterAllocationRepository.ITISaveWorkflow(request);
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

        [HttpPost("GetExamCoordinatorData")]
        public async Task<ApiResult<DataTable>> GetExamCoordinatorData([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetExamCoordinatorData(filterModel);
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


        [HttpPost("AssignExamCoordinatorData")]
        public async Task<ApiResult<bool>> AssignExamCoordinatorData([FromBody] PracticalExaminerDetailsModel request)
        {
            ActionName = "UpdateCCCode([FromBody] AssignCenterSuperintendent request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    var isSave = await _unitOfWork.ITICenterAllocationRepository.AssignExamCoordinatorData(request);
                    _unitOfWork.SaveChanges();

                    if (isSave == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
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


        [HttpPost("GetExamCoordinatorDataByInstitute")]
        public async Task<ApiResult<DataTable>> GetExamCoordinatorDataByInstitute([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetExamCoordinatorDataByInstitute()";
            var result = new ApiResult<DataTable>();

            try
            {
                if (filterModel == null)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Invalid search parameters.";
                    return result;
                }

                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetExamCoordinatorData_ByInstitute(filterModel);

                if (result.Data != null && result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
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
        }


        [HttpPost("GetExamCoordinatorDataByUserId")]
        public async Task<ApiResult<DataTable>> GetExamCoordinatorDataByUserId([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            ActionName = "GetExamCoordinatorDataByInstitute()";
            var result = new ApiResult<DataTable>();

            try
            {
                if (filterModel == null)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = "Invalid search parameters.";
                    return result;
                }

                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetExamCoordinatorData_ByUserId(filterModel);

                if (result.Data != null && result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
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
        }

        [HttpPost("GetCenterSuperDashboard")]
        public async Task<ApiResult<DataTable>> GetCenterSuperDashboard([FromBody] ExaminerDashboardModel filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITICenterAllocationRepository.GetCenterSuperDashboard(filterModel);
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

        [HttpPost("DownloadExamCoordinate")]
        public async Task<ApiResult<string>> DownloadExamCoordinate([FromBody] ITICenterAllocationSearchFilter filterModel)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITICenterAllocationRepository.DownloadExamCoordinate(filterModel);
                    if (data != null)
                    {
                        //report

                        var fileName = $"ExamCoordinatorReport_NCVT.pdf";
                        string folderName = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder_ITI}{Constants.CenterSuperintendentFolder_ITI}";
                        string filepath = Path.Combine(folderName, fileName);


                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ExamCoordinatorReport_NCVT.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("CenterSuperintendentReport_SCVT", data.Tables[0]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderName))
                        {
                            Directory.CreateDirectory(folderName);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;


                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                        ModInsert.FileName = fileName;
                        ModInsert.PDFType = (int)EnumPdfType.ExamCoordinator;
                        ModInsert.Status = 11;
                        ModInsert.DepartmentID=2;
                        ModInsert.Eng_NonEng=filterModel.Eng_NonEng.Value;
                        ModInsert.EndTermID=filterModel.EndTermID;
                        ModInsert.InstituteID=filterModel.DistrictID;



                        var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);
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
