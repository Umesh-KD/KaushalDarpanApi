using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Report.ITI;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class ItiExaminerController : BaseController
    {
        public override string PageName => "ItiExaminerController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ItiExaminerController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<DataTable>> GetAllData([FromBody] ItiExaminerSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetAllData(body));
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


        [HttpPost("GetStudentTheory")]
        public async Task<ApiResult<DataTable>> GetStudentTheory([FromBody] ITITeacherForExaminerSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetStudentTheory(body));
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




        [HttpPost("GetITIExaminer")]
        public async Task<ApiResult<DataTable>> GetITIExaminer([FromBody] ItiExaminerSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetITIExaminer(body));
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
        public async Task<ApiResult<int>> SaveData([FromBody] ITIExaminerModel request)
        {
            ActionName = "SaveData([FromBody] ItiExaminerModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }
                     

                    result.Data = await _unitOfWork.ItiExaminerRepository.SaveData(request);
             
                    if (result.Data>0)
                    {
                       
                        result.State = EnumStatus.Success;
                        if (request.ExaminerID == 0)
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
                        if (request.ExaminerID == 0)
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

        [HttpGet("GetByID/{PK_ID}/{StaffSubjectID}/{DepartmentID}")]
        public async Task<ApiResult<ITIExaminerModel>> GetByID(int PK_ID, int StaffSubjectID, int DepartmentID)
        {
            ActionName = "GetByID(int PK_ID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ITIExaminerModel>();
                try
                {
                    var data = await _unitOfWork.ItiExaminerRepository.GetById(PK_ID, StaffSubjectID, DepartmentID);
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

        [HttpDelete("DeleteDataByID/{PK_ID}/{ModifyBy}")]
        public async Task<ApiResult<bool>> DeleteDataByID(int PK_ID, int ModifyBy)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    int ExaminerID = PK_ID;
                    result.Data = await _unitOfWork.ItiExaminerRepository.DeleteDataByID(ExaminerID, ModifyBy);
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

        [HttpPost("GetTeacherForExaminer")]
        public async Task<ApiResult<DataTable>> GetTeacherForExaminer([FromBody] ITITeacherForExaminerSearchModel body)
        {
            ActionName = "GetTeacherForExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetTeacherForExaminer(body));
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



        [HttpPost("GetTeacherForExaminerById")]
        public async Task<ApiResult<DataTable>> GetTeacherForExaminerById([FromBody] ITITeacherForExaminerSearchModel body)
        {
            ActionName = "GetTeacherForExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetTeacherForExaminerById(body));
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



        //[HttpPost("SaveExaminerData")]
        //public async Task<ApiResult<int>> SaveExaminerData([FromBody] ITIExaminerMaster request)
        //{
        //    ActionName = " SaveExaminerData([FromBody] ExaminerMaster request)";
        //    return await Task.Run(async () =>
        //    {
        //        var result = new ApiResult<int>();
        //        try
        //        {
        //            request.IPAddress = CommonFuncationHelper.GetIpAddress();
        //            result.Data = await _unitOfWork.ItiExaminerRepository.SaveExaminerData(request);
        //            _unitOfWork.SaveChanges();
        //            if (result.Data > 0)
        //            {
        //                result.State = EnumStatus.Success;
        //                if (request.ExaminerID == 0)
        //                {
        //                    result.Message = Constants.MSG_SAVE_SUCCESS;
        //                }
        //                else
        //                {
        //                    result.Message = Constants.MSG_UPDATE_SUCCESS;
        //                }
        //            }
        //            else if (result.Data == -2)
        //            {
        //                result.State = EnumStatus.Warning;
        //                result.ErrorMessage = "UserID Does Not Exist";
        //            }
        //            else
        //            {
        //                result.State = EnumStatus.Error;
        //                if (request.ExaminerID == 0)
        //                {
        //                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
        //                }
        //                else
        //                {
        //                    result.ErrorMessage = Constants.MSG_UPDATE_ERROR;
        //                }
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

        [HttpPost("GetItiExaminerDashboardTiles")]
        public async Task<ApiResult<DataTable>> GetItiExaminerDashboardTiles([FromBody] ITI_ExaminerDashboardModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetItiExaminerDashboardTiles(body));
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

        [HttpPost("GetItiAppointExaminerDetails")]
        public async Task<ApiResult<DataTable>> GetItiAppointExaminerDetails([FromBody] ITI_AppointExaminerDetailsModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetItiAppointExaminerDetails(body));
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


        [HttpPost("GetItiExaminerBundleDetails")]
        public async Task<ApiResult<DataTable>> GetItiExaminerBundleDetails([FromBody] ITI_AppointExaminerDetailsModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetItiExaminerBundleDetails(body));
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



        [HttpPost("GetItiRemunerationExaminerDetails")]
        public async Task<ApiResult<DataTable>> GetItiRemunerationExaminerDetails([FromBody] ITI_AppointExaminerDetailsModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetItiRemunerationExaminerDetails(body));
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

        [HttpPost("SaveStudent")]
        public async Task<ApiResult<bool>> SaveStudent([FromBody] List<ItiAssignStudentExaminer> request)
        {
            ActionName = "SaveStudent([FromBody] List<ItiAssignStudentExaminer> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {


                    //validation
                    //if (request.Any(x => x.RoleId != (int)EnumRole.Admin))
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_UNAUTHORIZED_ACCESS_FOR_ROLE;
                    //    return result;
                    //}
                    if (request.Count == 0)
                    {
                        result.State = EnumStatus.Error;
                        result.Message = Constants.MSG_VALIDATION_FAILED;
                        return result;
                    }
                    //ipaddress
                    request.ForEach(x =>
                    {
                        x.IPAddress = CommonFuncationHelper.GetIpAddress();
                    });
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.ItiExaminerRepository.SaveStudent(request);
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

        [HttpDelete("DeleteAssignedStudents/{examinerId}")]
        public async Task<ApiResult<DataTable>> DeleteAssignedStudents(int examinerId)
        {
            ActionName = "DeleteAssignedStudents";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ItiExaminerRepository.DeleteAssignStudentByExaminerID(examinerId);
                _unitOfWork.SaveChanges();
                result.State = EnumStatus.Success;
                result.Message = "Student assignments deleted successfully.";
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpPost("SaveExaminerdata")]
        public async Task<ApiResult<int>> SaveExaminerData([FromBody] ITITheoryExaminerModel request)
        {
            ActionName = " SaveExaminerData([FromBody] ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    
                    result.Data = await _unitOfWork.ItiExaminerRepository.SaveExaminerData(request);
                    _unitOfWork.SaveChanges();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.ExaminerID == 0)
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
                        result.ErrorMessage = "Application is already assign to other user";
                    }

                    else if (result.Data == -3)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "This Examiner Code is already assigned to other user";
                    }

                    else
                    {
                        result.State = EnumStatus.Error;
                        if (request.ExaminerID == 0)
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



        [HttpGet("GetITIAssignedExaminerInstituteDetails/{BundelID}")]
        public async Task<ApiResult<DataTable>> GetITIAssignedExaminerInstituteDetails(int BundelID)
        {
            ActionName = "GetITIAssignedExaminerInstituteDetails(int BundelID)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.ItiExaminerRepository.ITIAssignedExaminerInstituteDetailbyID(BundelID);
                    result.State = EnumStatus.Success;
                    if (result.Data.Rows.Count == 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "No record found.!";
                        return result;
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


        [HttpPost("Iti_RemunerationGenerateAndViewPdf")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<IActionResult> Iti_RemunerationGenerateAndViewPdf([FromBody] ITI_AppointExaminerDetailsModel filterModel)
        {
            ActionName = "GenerateAndViewPdf([FromBody] RenumerationExaminerRequestModel filterModel)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            try
            {
                var data = await _unitOfWork.ItiExaminerRepository.Iti_RemunerationGenerateAndViewPdf(filterModel);
                if (data?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIRemunerationExaminer.rdlc");
                    //save file
                    int id = Convert.ToInt32(data.Rows[0]["AppointExaminerID"]);
                    //int adminstatus = Convert.ToInt32(data.Rows[0]["adminstatus"]);
                    int adminstatus = data.Rows[0].Field<int?>("adminstatus") ?? 0;

                    var newFileName = $"RemunerationExaminer{id}_{DateTime.Now.ToString("MMMddyyyyhhmmss")}.pdf";
                    string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{newFileName}";

                    //rpt
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    LocalReport localReport = new LocalReport(rdlcPath);
                    localReport.AddDataSource("Remuneration", data);
                    var reportResult = localReport.Execute(RenderType.Pdf);
                    //file stream

                    //if (adminstatus!=0)
                    //{
                    //    //check file exists
                    //    if (!System.IO.Directory.Exists(folderPath))
                    //    {
                    //        Directory.CreateDirectory(folderPath);
                    //    }

                    //    System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                    //}

                    return File(reportResult.MainStream, "application/pdf", newFileName );
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
                //var data = await _unitOfWork.ItiExaminerRepository.SaveDataSubmitAndForwardToAdmin(filterModel);
                //_unitOfWork.SaveChanges();
                //var objData = CommonFuncationHelper.ConvertDataTable<RenumerationExaminerPDFModel>(data);
                
                    var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";

                    var data1 = await _unitOfWork.ItiExaminerRepository.Iti_RemunerationGenerateAndViewPdf(filterModel);
                if (data1?.Rows?.Count > 0)
                {
                    //rdlc
                    string rdlcPath = Path.Combine(ConfigurationHelper.RootPath, Constants.RDLCFolderITI, "ITIRemunerationExaminer.rdlc");
                    //save file
                    int id = Convert.ToInt32(data1.Rows[0]["AppointExaminerID"]);
                    int adminstatus = Convert.ToInt32(data1.Rows[0]["adminstatus"]);

                    var newFileName = $"RemunerationExaminer{id}_{DateTime.Now.ToString("MMMddyyyyhhmmss")}.pdf";
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
                 var data = await _unitOfWork.ItiExaminerRepository.SaveDataSubmitAndForwardToAdmin(filterModel);
                 _unitOfWork.SaveChanges();
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
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetItiRemunerationAdminDetails(body));
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
                var data = await _unitOfWork.ItiExaminerRepository.UpdateToApprove(filterModel);
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