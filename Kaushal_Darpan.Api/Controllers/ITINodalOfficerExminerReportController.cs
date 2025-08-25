using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra;
using Kaushal_Darpan.Models.ITINodalOfficerExminerReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class ITINodalOfficerExminerReportController : BaseController
    {
        public override string PageName => "ITINodalOfficerExminerReportController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITINodalOfficerExminerReportController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("ITINodalOfficerExminerReportSave")]
        public async Task<ApiResult<int>> ITINodalOfficerExminerReportSave([FromBody] ITINodalOfficerExminerReport body)
        {

            ActionName = "ITINodalOfficerExminerReportSave([FromBody] ITINodalOfficerExminerReport body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReportSave(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
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


        [HttpPost("ITINodalOfficerExminerReport_GetAllData")]
        public async Task<ApiResult<DataTable>> ITINodalOfficerExminerReport_GetAllData([FromBody] ITINodalOfficerExminerReportSearch body)
        {

            ActionName = "ITINodalOfficerExminerReport_GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReport_GetAllData(body);

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


        [HttpPost("ITINodalOfficerExminerReport_GetAllDataByID")]
        public async Task<ApiResult<DataTable>> ITINodalOfficerExminerReport_GetAllDataByID([FromBody] ITINodalOfficerExminerReportSearch body)
        {

            ActionName = "ITINodalOfficerExminerReport_GetAllDataByID()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReport_GetAllDataByID(body);

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


         [HttpPost("ITINodalOfficerExminerReport_GetDataByID")]
        public async Task<ApiResult<ITINodalOfficerExminerReportByID>> ITINodalOfficerExminerReport_GetDataByID([FromBody] ITINodalOfficerExminerReportSearch body)
        {
            ActionName = "ITINodalOfficerExminerReport_GetDataByID([FromBody]  ITINodalOfficerExminerReportSearch body)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITINodalOfficerExminerReportByID>();
                try
                {
                    var data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReport_GetDataByID(body);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITINodalOfficerExminerReportByID>(data);
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

        [HttpGet("ITINodalOfficerExminerReportDetails_GetByID/{ID:int}")]
        public async Task<ApiResult<ITIInspectExaminationCenters>> ITINodalOfficerExminerReportDetails_GetByID(int ID)
        {
            ActionName = "ITINodalOfficerExminerReportDetails_GetByID(int ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIInspectExaminationCenters>();
                try
                {
                    var data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReportDetails_GetByID(ID);
                    if (data != null)
                    {
                        var mappedData = _mapper.Map<ITIInspectExaminationCenters>(data);
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

        [HttpPost("ITINodalOfficerExminerReportDetailsUpdate")]
        public async Task<ApiResult<int>> ITINodalOfficerExminerReportDetailsUpdate([FromBody] ITIInspectExaminationCenters body)
        {

            ActionName = "ITINodalOfficerExminerReportDetailsUpdate([FromBody] ITIInspectExaminationCenters body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReportDetailsUpdate(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
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

        [HttpPost("ITINodalOfficerExminerReportDetailsDelete")]
        public async Task<ApiResult<int>> ITINodalOfficerExminerReportDetailsDelete([FromBody] ITIInspectExaminationCenters body)
        {

            ActionName = "ITINodalOfficerExminerReportDetailsDelete([FromBody] ITIInspectExaminationCenters body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.ITINodalOfficerExminerReportDetailsDelete(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_DELETE_SUCCESS;
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

        [HttpPost("Generate_ITINodalOfficerExminerReport_ByID/{id}/{InstituteID}/{ExamDateTime}")]
        public async Task<ApiResult<string>> Generate_ITINodalOfficerExminerReport_ByID(int id,int InstituteID,string ExamDateTime)
        {
            ActionName = "Generate_ITINodalOfficerExminerReport_ByID()";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {

                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITINodalOfficerExminerReport.Generate_ITINodalOfficerExminerReport_ByID(id,InstituteID, ExamDateTime);
                    if (data != null)
                    {
                        var fileName = $"ITINodalOfficerExminerReport.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITINodalOfficerExminerReport.rdlc";
                        string pdfpath = "http://localhost:5230/Kaushal_Darpan.Api/StaticFiles/Reports/" + fileName;
                       


                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("ITINodalOfficerExminerReport_ByID", data.Tables[0]);
                        localReport.AddDataSource("ITINodalOfficerExminerReportTable2", data.Tables[1]);
                        localReport.AddDataSource("ItinodalcenterList", data.Tables[2]);
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

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] Nodalsearchmodel body)
        {

            ActionName = "ITINodalOfficerExminerReport_GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.GetAllData(body);

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

        [HttpPost("SaveAllData")]
        public async Task<ApiResult<int>> SaveAllData([FromBody] NodalExamMapping body)
        {

            ActionName = "SaveAllData([FromBody] NodalExamMapping body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITINodalOfficerExminerReport.SaveAllData(body);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {


                    result.State = EnumStatus.Success;
                    if (result.Data == 1)
                    {
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.Message = Constants.MSG_UPDATE_SUCCESS;
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
    }
}
