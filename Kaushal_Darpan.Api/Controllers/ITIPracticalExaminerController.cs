using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITIPracticalExaminerController : BaseController
    {
        public override string PageName => "PracticalExaminerController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIPracticalExaminerController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetPracticalExamCenter")]
        public async Task<ApiResult<DataTable>> GetPracticalExamCenter([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetPracticalExamCenter(filterModel);
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

        [HttpPost("GetParcticalStudentCenterWise")]
        public async Task<ApiResult<DataTable>> GetParcticalStudentCenterWise([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetParcticalStudentCenterWise()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetParcticalStudentCenterWise(filterModel);
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


        [HttpPost("ParcticalExaminerDashboard")]
        public async Task<ApiResult<DataTable>> ParcticalExaminerDashboard([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.ParcticalExaminerDashboard(filterModel);
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

        [HttpPost("AssignPracticalExaminer")]
        public async Task<ApiResult<bool>> AssignPracticalExaminer([FromBody] PracticalExaminerDetailsModel request)
        {
            ActionName = "UpdateCCCode([FromBody] AssignCenterSuperintendent request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    var isSave = await _unitOfWork.ITIPracticalExaminerRepository.AssignPracticalExaminer(request);
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



        [HttpPost("Getstaffpractical")]
        public async Task<ApiResult<DataTable>> Getstaffpractical([FromBody] ItiPracticalExaminerDDLDataModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIPracticalExaminerRepository.Getstaffpractical(body));
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


        [HttpPost("DownloadItiPracticalExaminer/{id}")]
        public async Task<ApiResult<string>> DownloadItiPracticalExaminer(int id)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITIPracticalExaminerRepository.GetUndertakingExaminerDetailsByIdAsync(id);
                    if (data != null)
                    {
                        //report
                        var fileName = $"UndertakingExaminerDetails.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/ITIPracticalExaminerDetails.rdlc";

                        LocalReport localReport = new LocalReport(rdlcpath);
                        localReport.AddDataSource("UndertakingExaminerDetails", data);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        //end report

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;


                        //DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                        //ModInsert.FileName = fileName;
                        //ModInsert.PDFType = (int)EnumPdfType.CenterSuperintendent;
                        //ModInsert.Status = 11;
                        //ModInsert.DepartmentID=1;
                        ////ModInsert.Eng_NonEng=filterModel.Eng_NonEng;
                        //ModInsert.EndTermID=filterModel.EndTermID;



                        //var isSave = await _unitOfWork.ReportRepository.SaveRollNumbePDFData(ModInsert);
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


        [HttpPost("GetStudentExamReport")]
        public async Task<ApiResult<DataTable>> GetStudentExamReport([FromBody] ITIPracticalExaminerSearchFilter filterModel, [FromQuery] string subjectCode)
        {
            ActionName = "GetStudentExamReport()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetStudentExamReportAsync(filterModel, subjectCode);
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found!";
                }
                else
                {
                    result.Message = "Successfull";
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }





        [HttpPost("GetStudentExamReportForITI")]
        public async Task<ApiResult<DataTable>> GetStudentExamReportForITI([FromBody] ITIExaminerDataModel filterModel)
        {
            ActionName = "GetStudentExamReportForITI()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetStudentExamReportGetStudentExamReportForITIAsync(filterModel);
                result.State = EnumStatus.Success;

                if (result.Data.Rows.Count == 0)
                {
                    result.Message = "No record found!";
                }
                else
                {
                    result.Message = "Successfull";
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };

                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GetAssignedCentersAndTimetable")]
        public async Task<ApiResult<DataTable>> GetAssignedCentersAndTimetable([FromBody] PracticalExaminerDetailsModel model)
        {
            ActionName = "GetAssignedCentersAndTimetable";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetAssignedCentersAndTimetableAsync(model);
                result.State = EnumStatus.Success;

                result.Message = result.Data.Rows.Count > 0 ? "Data fetched successfully." : "No data found.";
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var log = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(log, _unitOfWork);
            }

            return result;
        }


        [HttpPost("UpdateStudentExamMarksBulk")]
        public async Task<ApiResult<string>> UpdateStudentExamMarksBulk([FromBody] StudentExamMarksUpdateModel modelList)
        {
            ActionName = "UpdateStudentExamMarksBulk";
            var result = new ApiResult<string>();

            try
            {
                int responseCode = await _unitOfWork.ITIPracticalExaminerRepository.UpdateStudentExamMarks(modelList);
                _unitOfWork.SaveChanges();
                if (responseCode == 1)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Marks updated successfully.";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = "Update failed or no records were processed.";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var log = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(log, _unitOfWork);
            }

            return result;
        }


        [HttpPost("UpdateStudentExamMarksBulkData")]
        public async Task<ApiResult<string>> UpdateStudentExamMarksBulkData([FromBody] List<StudentExamMarksUpdateModel> modelList)
        {
            ActionName = "UpdateStudentExamMarksBulk";
            var result = new ApiResult<string>();

            try
            {
                if (modelList == null || !modelList.Any())
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No data received.";
                    return result;
                }

                if (modelList.Any(m => m.UserID == 0 || string.IsNullOrWhiteSpace(m.IPAddress)))
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "UserID and IPAddress must be provided for all entries.";
                    return result;
                }

                int responseCode = await _unitOfWork.ITIPracticalExaminerRepository.UpdateStudentExamMarksData(modelList);
                _unitOfWork.SaveChanges();

                if (responseCode == 1)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Marks updated successfully.";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = "Update failed or no records were processed.";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var log = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(log, _unitOfWork);
            }

            return result;
        }


        [HttpPost("NcvtUpdateStudentExamMarksData")]
        public async Task<ApiResult<string>> NcvtUpdateStudentExamMarksBulkData([FromBody] List<StudentExamMarksUpdateModel> modelList)
        {
            ActionName = "UpdateStudentExamMarksBulk";
            var result = new ApiResult<string>();

            try
            {
                if (modelList == null || !modelList.Any())
                {
                    result.State = EnumStatus.Warning;
                    result.Message = "No data received.";
                    return result;
                }

                

                int responseCode = await _unitOfWork.ITIPracticalExaminerRepository.NcvtUpdateStudentExamMarksData(modelList);
                _unitOfWork.SaveChanges();

                if (responseCode == 1)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Marks updated successfully.";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = "Update failed or no records were processed.";
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                var log = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(log, _unitOfWork);
            }

            return result;
        }



        [HttpPost("GetCenterPracticalexaminer")]
        public async Task<ApiResult<DataTable>> GetCenterPracticalexaminer([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetCenterPracticalexaminer(filterModel);

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



        [HttpPost("GetCenterPracticalexaminerReliving")]
        public async Task<ApiResult<DataTable>> GetCenterPracticalexaminerReliving([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetCenterPracticalexaminerReliving(filterModel);
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


        [HttpPost("GetPracticalExaminerRelivingByUserId")]
        public async Task<ApiResult<DataTable>> GetPracticalExaminerRelivingByUserId([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            ActionName = "GetPracticalExaminerRelivingByUserId()";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ITIPracticalExaminerRepository.GetPracticalExaminerRelivingByUserId(filterModel);

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

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex
                };
                await CreateErrorLog(nex, _unitOfWork);
            }

            return result;
        }


        [HttpPost("GeneratePracticalExamAssignData")]
        public async Task<ApiResult<string>> GeneratePracticalExamAssignData([FromBody] ITIPracticalExaminerSearchFilter filterModel)
        {
            //ActionName = "GenerateInspectionDeploymentOrder()";
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
                    var data = await _unitOfWork.ITIPracticalExaminerRepository.GetPracticalExamCenter_Report(filterModel);
                    if (data != null)
                    {
                        var fileName = $"PracticalExamAssignData.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/PracticalExamAssignData.rdlc";
                        string pdfpath = "http://localhost:5230/Kaushal_Darpan.Api/StaticFiles/Reports/" + fileName;
                        //model.ForEach(x =>
                        //{
                        //    x.DutyOrderPath = filepath;
                        //    x.DutyOrder = fileName;
                        //});


                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("PracticalExamAssignData", data);
                        //localReport.AddDataSource("InspectionMembers", data.Tables[1]);
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


                        DownloadnRollNoModel ModInsert = new DownloadnRollNoModel();
                        ModInsert.FileName = fileName;
                        ModInsert.PDFType = (int)EnumPdfType.PracticalExaminer;
                        ModInsert.Status = 11;
                        ModInsert.DepartmentID=2;
                        ModInsert.Eng_NonEng=filterModel.Eng_NonEng.Value;
                        ModInsert.EndTermID=filterModel.EndTermID;
                        ModInsert.InstituteID=filterModel.InstituteID;



                        var isSave = await _unitOfWork.ReportRepository.ITISaveRollNumbePDFData(ModInsert);

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }

                    //var Issuccess = await _unitOfWork.CenterObserverRepository.UpdateDutyOrder(model);
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


        [HttpPost("GetItiRemunerationExaminerDetails")]
        public async Task<ApiResult<DataTable>> GetItiRemunerationExaminerDetails([FromBody] ITI_AppointExaminerDetailsModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIPracticalExaminerRepository.GetItiRemunerationExaminerDetails(body));
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


        [HttpPost("Iti_RemunerationGenerateAndViewPdf")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<IActionResult> Iti_RemunerationGenerateAndViewPdf([FromBody] ITI_AppointExaminerDetailsModel filterModel)
        {
            ActionName = "GenerateAndViewPdf([FromBody] RenumerationExaminerRequestModel filterModel)";
            try
            {
                var data = await _unitOfWork.ITIPracticalExaminerRepository.Iti_RemunerationGenerateAndViewPdf(filterModel);
                if (data?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIRemunerationPractical.rdlc");
                    //save file
                    int id = Convert.ToInt32(data.Rows[0]["CenterAssignedID"]);

                    var newFileName = $"RemunerationPractical{id}_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";


                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //file stream
                    return File(reportResult.MainStream, "application/pdf", newFileName);
                }
                else
                {
                    return Content("No data available to generate the PDF.");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
                //
                return Content("An error occurred while generating the PDF.");
            }
        }

        [HttpPost("SavePDFSubmitAndForwardToAdmin")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<ApiResult<bool>> SavePDFSubmitAndForwardToAdmin([FromBody] ITI_AppointExaminerDetailsModel filterModel)
        {
            ActionName = "SavePDFSubmitAndForwardToJD([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<bool>();
            try
            {

                var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
                var data1 = await _unitOfWork.ITIPracticalExaminerRepository.Iti_RemunerationGenerateAndViewPdf(filterModel);
                if (data1?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIRemunerationPractical.rdlc");
                    //save file
                    int id = Convert.ToInt32(data1.Rows[0]["CenterAssignedID"]);
                    int adminstatus = Convert.ToInt32(data1.Rows[0]["adminstatus"]);
                    var newFileName = $"RemunerationPractical{id}_{DateTime.Now.ToString("MMMddyyyyhhmmssffffff")}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{newFileName}";

                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data1);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //file stream
                    if (adminstatus == 0)
                    {
                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);
                        filterModel.filename = newFileName;
                    }
                }
                var data = await _unitOfWork.ITIPracticalExaminerRepository.SaveDataSubmitAndForwardToAdmin(filterModel);
                _unitOfWork.SaveChanges();
                //var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                if (data == 1)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Forwarded To Admin Successfully";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

        [HttpPost("GetItiRemunerationAdminDetails")]
        public async Task<ApiResult<DataTable>> GetItiRemunerationAdminDetails([FromBody] ITI_AppointExaminerDetailsModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIPracticalExaminerRepository.GetItiRemunerationAdminDetails(body));
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

        [HttpPost("UpdateToApprove")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<ApiResult<bool>> UpdateToApprove([FromBody] ITI_AppointExaminerDetailsModel filterModel)
        {
            ActionName = "SavePDFSubmitAndForwardToJD([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<bool>();
            try
            {
                var data = await _unitOfWork.ITIPracticalExaminerRepository.UpdateToApprove(filterModel);
                _unitOfWork.SaveChanges();
                //var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                if (data == 1)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "Update Successfully";
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();

                result.State = EnumStatus.Error;
                result.Message = Constants.MSG_ERROR_OCCURRED;
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

    }
}
