using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using ExcelDataReader;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]

    public class CounsellingMasterController : BaseController
    {
        public override string PageName => "CounsellingMasterController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CounsellingMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("SavePersonalData")]
        public async Task<ApiResult<int>> SaveData([FromBody] ApplicationDataModel request)
        {
            ActionName = "SaveData([FromBody] HRMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingMasterRepository.SaveData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ApplicationID == 0)
                        {
                            result.Message = Constants.MSG_SAVE_SUCCESS;
                        }
                        else
                        {
                            result.Message = Constants.MSG_UPDATE_SUCCESS;
                        }
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ApplicationID == 0)
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



        //[HttpPost("MapCandidateSSO")]
        //public async Task<ApiResult<DataTable>> MapCandidateSSO([FromBody] CounsellingApplicationSearchModel body)
        //{
        //    ActionName = "MapCandidateSSO()";
        //    var result = new ApiResult<DataTable>();
        //    try
        //    {
        //        // Pass the entire model to the repository
        //        result.Data = await _unitOfWork.CounsellingApplicationFormRepository.MapCandidateSSO(body);
        //        if (result.Data.Rows.Count > 0)
        //        {
        //            result.State = EnumStatus.Success;
        //            result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //        }
        //        else
        //        {
        //            result.State = EnumStatus.Warning;
        //            result.Message = Constants.MSG_DATA_NOT_FOUND;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.State = EnumStatus.Error;
        //        result.ErrorMessage = ex.Message;
        //        // Log the error
        //        await _unitOfWork.DisposeAsync();
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


        //[HttpPost("UpdateStudentSsoMapping")]
        //public async Task<ApiResult<int>> UpdateCandidateSsoMapping([FromBody] CounsellingApplicationSearchModel model)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {

        //            var data = await _unitOfWork.CounsellingApplicationFormRepository.UpdateStudentSsoMapping(model);
        //            await _unitOfWork.SaveChangesAsync();
        //            if (data > 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                result.Data = data;
        //                result.Message = "Student Mapped Successfully";

        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                result.ErrorMessage = "Something went wrong";
        //                result.Data = data;
        //            }
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;

        //            // Log the error
        //            await _unitOfWork.DisposeAsync();
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



        [HttpPost("GetCounsellingAllotmentList")]
        public async Task<ApiResult<DataTable>> GetCounsellingAllotmentList([FromBody] CounsellingAllotmentListModel body)
        
        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetCounsellingAllotmentList(body);

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
        }


        [HttpPost("GetCandidateList")]
        public async Task<ApiResult<DataTable>> GetCandidateList([FromBody] CounsellingAllotmentListModel body)

        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetCandidateList(body);

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
        }

        [HttpPost("SaveCandidateAllotment_Counselling/{TradeID}")]
        public async Task<ApiResult<int>> SaveCandidateAllotment_Counselling([FromRoute] int TradeID, [FromBody] List<Counselling_AllotmentDataModel> request)
        {
            ActionName = "Task<ApiResult<int>> SaveCandidateAllotment_Counselling([FromRoute] int TradeID, [FromBody] List<Counselling_AllotmentDataModel> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var isSave = await _unitOfWork.CounsellingMasterRepository.SaveCandidateAllotment_Counselling(TradeID, request);
                    await _unitOfWork.SaveChangesAsync(); 

                    if (isSave == -1)
                    {
                        //result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        // result.Data = true;
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

        [HttpPost("GetAllottedCandidateList_Counselling")]
        public async Task<ApiResult<DataTable>> GetAllottedCandidateList_Counselling([FromBody] CounsellingAllottedListSearchModel body)

        {
            ActionName = "GetAllottedCandidateList_Counselling([FromBody] CounsellingAllottedListSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CounsellingMasterRepository.GetAllottedCandidateList_Counselling(body);

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
        }

        [HttpPost("SaveFinalInstituteAllotment_Counselling")]
        public async Task<ApiResult<bool>> SaveFinalInstituteAllotment_Counselling(EditInstituteDataModel_Counselling model)
        {
            ActionName = "SaveFinalInstituteAllotment_Counselling(EditInstituteDataModel_Counselling model)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.CounsellingMasterRepository.SaveFinalInstituteAllotment_Counselling(model);
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

        //[HttpPost("GenerateAllotmentOrder_Counselling")]
        //public async Task<ApiResult<string>> GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> model)
        //{
        //    ActionName = "GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> model)";
        //    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<string>();
        //        try
        //        {
        //            var data = await _unitOfWork.CounsellingMasterRepository.GenerateAllotmentOrder_Counselling(model);
        //            if (data != null)
        //            {
        //                string guid = Guid.NewGuid().ToString().ToUpper();
        //                var fileName = $"CounsellingAllotmentOrder_{guid}.pdf";
        //                string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
        //                string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderITI}/CounsellingAllotmentOrder.rdlc";

        //                model.ForEach(x =>
        //                {
        //                    x.AllotmentOrderPath = filepath;
        //                    x.AllotmentOrder = fileName;
        //                });

        //                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //                LocalReport localReport = new LocalReport(rdlcpath);

        //                localReport.AddDataSource("CounsellingAllotmentOrderTable", data.Tables[0]);
        //                var reportResult = localReport.Execute(RenderType.Pdf);

        //                //check file exists
        //                if (!System.IO.Directory.Exists(folderPath))
        //                {
        //                    Directory.CreateDirectory(folderPath);
        //                }

        //                System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

        //                //result.Data = fileName;
        //                result.State = EnumStatus.Success;
        //                result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            }

        //            //var Issuccess = await _unitOfWork.CenterObserverRepository.UpdateDutyOrder(model);
        //            //if (Issuccess > 0)
        //            //{
        //            //    result.Data = Issuccess.ToString();
        //            //    result.State = EnumStatus.Success;
        //            //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
        //            //}
        //            //else
        //            //{
        //            //    result.State = EnumStatus.Warning;
        //            //    result.Message = Constants.MSG_DATA_NOT_FOUND;
        //            //}

        //        }
        //        catch (Exception ex)
        //        {
        //            await _unitOfWork.DisposeAsync();
        //            // Write error log
        //            var nex = new NewException
        //            {
        //                PageName = PageName,
        //                ActionName = ActionName,
        //                Ex = ex,
        //            };
        //            await CreateErrorLog(nex, _unitOfWork);
        //            //
        //            result.State = EnumStatus.Error;
        //            result.ErrorMessage = ex.Message;
        //        }
        //        return result;
        //    });
        //}

        [HttpPost("GenerateAllotmentOrder_Counselling")]
        public async Task<ApiResult<string>> GenerateAllotmentOrder_Counselling([FromBody] List<EditInstituteDataModel_Counselling> body)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await Task.Run(() => _unitOfWork.CounsellingMasterRepository.GenerateAllotmentOrder_Counselling(body));

                    if (data?.Tables?.Count == 1)
                    {
                        var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";

                        string guid = Guid.NewGuid().ToString().ToUpper();
                        var fileName = $"CounsellingAllotmentOrder_{guid}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";

                        //provider                      
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        data.Tables[0].TableName = "AllottedCandidateList";

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.ReportFolderITI}/CounsellingAllotmentOrder.html";

                        string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        html = Utility.PDFWorks.ReplaceCustomTag(html);

                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        if (System.IO.File.Exists(filepath))
                        {
                            System.IO.File.Delete(filepath);
                        }
                        if (Utility.PDFWorks.GeneratePDF(sb1, filepath, ""))
                        {
                            //byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                            //string file_Name = filepath.Split('/')[filepath.Split('/').Length - 1];
                            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file_Name);
                        }
                        else
                        {
                            //return null;
                        }


                        ////check file exists
                        //if (!System.IO.Directory.Exists(folderPath))
                        //{
                        //    Directory.CreateDirectory(folderPath);
                        //}

                        result.Data = fileName;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";

                        if (result.State == EnumStatus.Success)
                        {
                            foreach (var item in body)
                            {
                                item.AllotmentOrder = fileName;
                            }
                            var updateData = new ApiResult<bool>();

                            updateData.Data = await _unitOfWork.CounsellingMasterRepository.UpdateAllotmentOrder_Counselling(body);
                            await _unitOfWork.SaveChangesAsync();
                            if (updateData.Data)
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

        [HttpGet("GetSampleExcelFile_CounsellingVacant")]
        public async Task<ApiResult<DataTable>> GetSampleExcelFile_CounsellingVacant()
        {
            ActionName = "GetSampleExcelFile_CounsellingVacant()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CounsellingMasterRepository.GetSampleExcelFile_CounsellingVacant());
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

        [HttpPost("ImportExcelFile_CounsellingVacant"), DisableRequestSizeLimit]
        public async Task<ApiResult<List<ImportCounsellingVacancyDataModel>>> ImportExcelFile_CounsellingVacant([FromForm] UploadFileModel model)
        {
            ActionName = "ImportExcelFile_CounsellingVacant([FromForm] UploadFileModel model)";
            var result = new ApiResult<List<ImportCounsellingVacancyDataModel>>();

            try
            {
                //  Validate file presence
                if (model.file == null || model.file.Length == 0)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_INVALID_REQUEST;
                    return result;
                }

                //  Read the Excel file
                using (var stream = model.file.OpenReadStream())
                {
                    // Prepare StringWriter for logging or debugging purposes (Optional)
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    StringWriter swSQL = new StringWriter(sb);

                    // Register CodePagesEncodingProvider for reading older Excel formats
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    // Read Excel file into DataSet
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Treat first row as headers
                            }
                        });

                        // Get the first sheet as DataTable
                        DataTable dt = ds.Tables[0];

                        //  Convert DataTable to your specific model list
                        var dataTime = CommonFuncationHelper.ConvertExcelData<List<ImportCounsellingVacancyDataModel>>(dt);
                        var data = await _unitOfWork.CounsellingMasterRepository.ImportExcelFile_CounsellingVacant(dataTime);

                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;

                    }
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
        }
    }
}
