using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    [ValidationActionFilter]
    public class ITINodalReportController : BaseController
    {
        public override string PageName => "ITINodalReportController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITINodalReportController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpPost("Save_PMNAM_melaRPT_BeforeAfter")]
        public async Task<ApiResult<DataTable>> SaveData([FromBody] ITIPMNAM_MelaReportBeforeAfterModal body)
        {

            ActionName = "SaveData([FromBody] ITIPMNAM_MelaReportBeforeAfterModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.SaveData(body);
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




        [HttpPost("GetPMNAM_BeforeAfterAllData")]
        public async Task<ApiResult<DataTable>> GetPMNAM_BeforeAfterAllData([FromBody] ITIPMNAM_Report_SearchModal body)
        {

            ActionName = "GetPMNAM_BeforeAfterAllData([FromBody] ITIPMNAM_MelaReportBeforeAfterModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetAllData(body);
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



        [HttpPost("PMNAM_report_DeletebyID/{PKid}")]
        public async Task<ApiResult<DataTable>> PMNAM_report_DeletebyID(int PKid = 0)
        {

            ActionName = "PMNAM_report_DeletebyID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.PMNAM_report_DeletebyID(PKid);
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



        [HttpGet("GetReportDatabyID/{PKid}")]
        public async Task<ApiResult<DataTable>> GetReportDatabyID(int PKid = 0)
        {

            ActionName = "PMNAM_report_DeletebyID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetReportDatabyID(PKid);
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


        [HttpPost("Save_PMNUM_Mela_Report")]
        public async Task<ApiResult<DataTable>> SaveDataMelaReportCount([FromBody] ITIPMNAMAppApprenticeshipReportEntity body)
        {

            ActionName = "SaveDataMelaReportCount([FromBody] ITIPMNAMAppApprenticeshipReportEntity body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.SaveDataMelaReportCount(body);
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

        [HttpGet("GetAllData/{UserID}/{DistrictID}")]
        public async Task<ApiResult<DataTable>> GetAllData(int UserID, int DistrictID)
        {

            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetAllData(UserID, DistrictID);
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
        [HttpPost("DeleteData_Pmnam_mela_Report")]
        public async Task<ApiResult<DataTable>> DeleteData_Pmnam_mela_Report([FromBody] ITIPMNAMAppApprenticeshipReportEntity body)
        {

            ActionName = "DeleteData_Pmnam_mela_Report([FromBody] ITIPMNAMAppApprenticeshipReportEntity body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.DeleteData_Pmnam_mela_Report(body);
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

        [HttpPost("Save_QuaterReport")]
        public async Task<ApiResult<int>> Save_QuaterReport([FromBody] ITIApprenticeshipWorkshop request)
        {
            ActionName = "SaveData([FromBody] ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {




                    result.Data = await _unitOfWork.ITINodalReportRepository.Save_QuaterReport(request);

                    if (result.Data > 0)
                    {

                        result.State = EnumStatus.Success;
                        if (request.ID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }



                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_SAVE_SUCCESS;
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


        [HttpPost("GetQuaterProgressList")]
        public async Task<ApiResult<DataTable>> GetQuaterProgressList([FromBody] ITIApprenticeshipWorkshop body)
        {

            ActionName = "GetQuaterProgressList([FromBody] ITIPMNAM_MelaReportBeforeAfterModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetQuaterProgressList(body);
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


        [HttpGet("GetQuaterReportById/{PKid}")]
        public async Task<ApiResult<DataTable>> GetQuaterReportById(int PKid = 0)
        {

            ActionName = "PMNAM_report_DeletebyID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetQuaterReportById(PKid);
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

        [HttpGet("GetAAADetailsById/{PKid}")]
        public async Task<ApiResult<DataTable>> GetAAADetailsById(int PKid = 0)
        {

            ActionName = "GetAAADetailsById()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.GetAAADetailsById(PKid);
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

        [HttpPost("QuaterListDelete/{PKid}")]
        public async Task<ApiResult<DataTable>> QuaterListDelete(int PKid = 0)
        {

            ActionName = "PMNAM_report_DeletebyID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.QuaterListDelete(PKid);
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


        [HttpPost("Save_ITIWorkshopProgressRPT")]
        public async Task<ApiResult<DataTable>> Save_ITIWorkshopProgressRPT([FromBody] List<workshopProgressRPTList> body)  // [FromBody] List<ListModal> body
        {

            ActionName = "SaveData([FromBody] ITIPMNAM_MelaReportBeforeAfterModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.Save_ITIWorkshopProgressRPT(body);
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

        [HttpPost("Submit_Apprenticeship_data")]
        public async Task<ApiResult<DataTable>> Submit_Apprenticeship_data([FromBody] ApprenticeshipEntriesList body)
        {

            ActionName = "Submit_Apprenticeship_data([FromBody] ApprenticeshipEntriesList body)";
            var result = new ApiResult<DataTable>();
            try
            {

                foreach (var entry in body.ApprenticeshipEntries)
                {
                    string businessNameCsv = entry.BusinessName != null
                        ? string.Join(",", entry.BusinessName)
                        : string.Empty;

                    result.Data = await _unitOfWork.ITINodalReportRepository.Submit_Apprenticeship_data(entry, businessNameCsv);

                }
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


        [HttpPost("Get_WorkshopProgressReportAllData")]
        public async Task<ApiResult<DataTable>> Get_WorkshopProgressReportAllData([FromBody] WorkshopProgressRPTSearchModal body)
        {

            ActionName = "Get_WorkshopProgressReportAllData([FromBody] WorkshopProgressRPTSearchModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.Get_WorkshopProgressRPT_AllData(body);
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

        [HttpPost("WorkshopProgressRPTDelete_byID/{PKid}")]
        public async Task<ApiResult<DataTable>> WorkshopProgressRPTDelete_byID(int PKid = 0)
        {

            ActionName = "WorkshopProgressRPTDelete_byID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.WorkshopProgressRPTDelete_byID(PKid);
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



        [HttpPost("SampleImportExcelFilePassout")]
        public async Task<ApiResult<DataTable>> GetSamplePassoutStudent([FromBody] ITITimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITINodalReportRepository.GetSamplePassoutStudent(body));
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
        [HttpPost("SampleImportExcelFileFresher")]
        public async Task<ApiResult<DataTable>> SampleImportExcelFileFresher([FromBody] ITITimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITINodalReportRepository.SampleImportExcelFileFresher(body));
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

        [HttpPost("SavePassoutReport")]
        public async Task<ApiResult<int>> SavePassoutReport([FromBody] ITIApprenticeshipRegPassOutModel request)
        {
            ActionName = "SaveData([FromBody] ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {




                    result.Data = await _unitOfWork.ITINodalReportRepository.SavePassoutReport(request);

                    if (result.Data > 0)
                    {

                        result.State = EnumStatus.Success;
                        if (request.ID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }



                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_SAVE_SUCCESS;
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

        [HttpPost("SaveFresherReport")]
        public async Task<ApiResult<int>> SaveFresherReport([FromBody] ITIApprenticeshipRegPassOutModel request)
        {
            ActionName = "SaveFresherReport([FromBody] ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {




                    result.Data = await _unitOfWork.ITINodalReportRepository.SaveFresherReport(request);

                    if (result.Data > 0)
                    {

                        result.State = EnumStatus.Success;
                        if (request.ID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }



                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ID == 0)
                        {
                            result.ErrorMessage = Constants.MSG_SAVE_SUCCESS;
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





        [HttpPost("Get_ApprenticeshipRegistrationReportAllData")]
        public async Task<ApiResult<DataTable>> Get_ApprenticeshipRegistrationReportAllData([FromBody] ApprenticeshipRegistrationSearchModal body)
        {

            ActionName = "Get_ApprenticeshipRegistrationReportAllData([FromBody] WorkshopProgressRPTSearchModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.Get_ApprenticeshipRegistrationReportAllData(body);
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

        [HttpPost("ApprenticeshipRegistrationRPTDelete_byID/{PKid}")]
        public async Task<ApiResult<DataTable>> ApprenticeshipRegistrationRPTDelete_byID(int PKid = 0)
        {

            ActionName = "ApprenticeshipRegistrationRPTDelete_byID()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.ApprenticeshipRegistrationRPTDelete_byID(PKid);
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

        [HttpPost("Get_PassingRegistrationReportAllData")]
        public async Task<ApiResult<DataTable>> Get_PassingRegistrationReportAllData([FromBody] ApprenticeshipRegistrationSearchModal body)
        {

            ActionName = "Get_PassingRegistrationReportAllData([FromBody] WorkshopProgressRPTSearchModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.Get_PassingRegistrationReportAllData(body);
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

        [HttpPost("Get_FresherRegistrationReportAllData")]
        public async Task<ApiResult<DataTable>> Get_FresherRegistrationReportAllData([FromBody] ApprenticeshipRegistrationSearchModal body)
        {

            ActionName = "Get_PassingRegistrationReportAllData([FromBody] WorkshopProgressRPTSearchModal body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITINodalReportRepository.Get_FresherRegistrationReportAllData(body);
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

        [HttpPost("PassoutRegistrationRPTDelete_byID/{PKid}")]
        public async Task<ApiResult<int>> PassoutRegistrationRPTDelete_byID(int PKid = 0)
        {

            ActionName = "PassoutRegistrationRPTDelete_byID( PkId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITINodalReportRepository.PassoutRegistrationRPTDelete_byID(PKid);

                    if (result.Data > 0)
                    {

                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == 0)
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
            });

        }
        [HttpPost("FresherRegistrationRPTDelete_byID/{PKid}")]
        public async Task<ApiResult<int>> FresherRegistrationRPTDelete_byID(int PKid = 0)
        {

            ActionName = "PassoutRegistrationRPTDelete_byID(PkId)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ITINodalReportRepository.FresherRegistrationRPTDelete_byID(PKid);

                    if (result.Data > 0)
                    {

                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DELETE_SUCCESS;
                        _unitOfWork.SaveChanges();
                    }
                    else if (result.Data == 0)
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
            });
        }

        [HttpPost("MelaSampleImportExcelFile")]
        public async Task<ApiResult<DataTable>> MelaSampleImportExcelFile([FromBody] ITITimeTableSearchModel body)
        {
            ActionName = "GetSampleTimeTable()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITINodalReportRepository.MelaSampleImportExcelFile(body));
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

    }
}


