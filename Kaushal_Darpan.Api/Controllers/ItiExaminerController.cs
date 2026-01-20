using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Report.ITI;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Data;
using System.Text;

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
        private readonly IConverter _converter;

        public ItiExaminerController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _converter = converter;
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
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else if (result.Data == -2)
                    {
                        result.State = EnumStatus.Warning;
                        result.ErrorMessage = "Examiner Already Exist with this SSOID";
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
                    await _unitOfWork.SaveChangesAsync();

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
        //            await _unitOfWork.SaveChangesAsync();
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
        //            await _unitOfWork.DisposeAsync();
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
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

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

        [HttpDelete("DeleteAssignedStudents/{examinerId}")]
        public async Task<ApiResult<DataTable>> DeleteAssignedStudents(int examinerId)
        {
            ActionName = "DeleteAssignedStudents";
            var result = new ApiResult<DataTable>();

            try
            {
                result.Data = await _unitOfWork.ItiExaminerRepository.DeleteAssignStudentByExaminerID(examinerId);
                await _unitOfWork.SaveChangesAsync();
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
                    await _unitOfWork.SaveChangesAsync();
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
                await _unitOfWork.DisposeAsync();

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
                //await _unitOfWork.SaveChangesAsync();
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
                 await _unitOfWork.SaveChangesAsync();
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
                await _unitOfWork.DisposeAsync();

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

        [HttpPost("UpdateToApprove")]
        //[RoleActionFilter(EnumRole.Examiner_Eng, EnumRole.Examiner_NonEng)]
        public async Task<ApiResult<bool>> UpdateToApprove([FromBody] ITI_AppointExaminerDetailsModel filterModel)
        {
            ActionName = "SavePDFSubmitAndForwardToJD([FromBody] RenumerationExaminerRequestModel filterModel)";
            var result = new ApiResult<bool>();
            try
            {
                var data = await _unitOfWork.ItiExaminerRepository.UpdateToApprove(filterModel);
                await _unitOfWork.SaveChangesAsync();
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
                await _unitOfWork.DisposeAsync();

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




        [HttpPost("CheckExaminerProfileCompleted")]
        public async Task<ApiResult<DataTable>> CheckExaminerProfileCompleted([FromBody] ItiExaminerSearchModel body)
        {
            ActionName = "GetAllData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.CheckExaminerProfileCompleted(body));
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


        [HttpPost("RemoveStudent")]
        public async Task<ApiResult<bool>> RemoveStudent(ItiExaminerSearchModel model)
        {
            ActionName = "DeleteDataByID(int PK_ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                    result.Data = await _unitOfWork.ItiExaminerRepository.RemoveStudent(model);
                    await _unitOfWork.SaveChangesAsync();
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



        [HttpPost("GetTeacherForExaminerReport")]
        public async Task<ApiResult<DataTable>> GetTeacherForExaminerReport([FromBody] ITITeacherForExaminerSearchModel body)
        {
            ActionName = "GetTeacherForExaminerReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.GetTeacherForExaminerReport(body));
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

        [HttpPost("TeacherForExaminerReportDewnloadPdf")]
        public async Task<IActionResult> TeacherForExaminerReportDewnloadPdf(
    [FromBody] ITITeacherForExaminerSearchModel body)
        {
            try
            {
                var streams_data =
                    await _unitOfWork.ItiExaminerRepository.TeacherForExaminerReportDewnloadPdf(body);

                var dataList =
                    CommonFuncationHelper.ConvertDataTable<List<ITITeacherForExaminerSearchModel>>(streams_data);

                var groupedData = dataList
                    .GroupBy(x => new { x.SubjectCode, x.StreamID, x.SemesterID,x.CenterCode })
                    .ToList();

                var sb = new StringBuilder();

                // ================= HTML HEADER =================
                sb.Append(@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Teacher Examiner Report</title>
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            margin: 20px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
        }
        table, th, td {
            border: 1px solid #000;
        }
        th, td {
            padding: 5px;
            text-align: center;
        }
        th {
            font-weight: bold;
        }
        .text-left {
            text-align: left;
        }
        .line {
            border-bottom: 1px solid #000;
            display: inline-block;
            width: 200px;
        }

 .footer-table {{
        width: 100%;
        border-collapse: collapse;
        font-size: 10px;
    }}
    .footer-table td {{
        border: 1px solid #000;
        padding: 6px;
        vertical-align: top;
    }}
    .line {{
        display: inline-block;
        border-bottom: 1px solid #000;
        width: 150px;
    }}


        .page-break {
            --page-break-after: always;
--border-bottom: 1px dashed #000;
        }

  .table-wrapper {
        width: 100%;
      
    }

    .half-table {
        width: 50%;
        display: inline-block;
        vertical-align: top;
        font-size: 12px;
        box-sizing: border-box;
        padding-right: 10px;
    }

    </style>
</head>
<body>
");

                // ================= BODY =================
                int groupIndex = 0;
                int totalGroups = groupedData.Count;

                foreach (var gitem in groupedData)
                {
                    var header = gitem.First();

                   sb.Append($@"<div  style=""border:1px solid #ddd; padding:25px; margin-bottom:25px;"">
<div {(groupIndex < totalGroups - 1 ? "class='page-break'" : "")}>


<table width=""100%%"" style=""border:none; margin-bottom:10px;"">
    <tr>
        <td style=""border:none; text-align:left;"">
            <b>{header.ExamName}</b>
        </td>
        <td style=""border:none; text-align:right;"">
            Center Code: <b>{header.CenterCode}</b>
        </td>
    </tr>

    <tr>
        <td style=""border:none; text-align:left;"">
            Examiner Code: <b>{header.ExaminerCode}</b>
        </td>
        <td style=""border:none; text-align:right;"">
            Subject: <b>{header.SubjectName}</b>
        </td>
    </tr>

    <tr>
        <td style=""border:none; text-align:left;"">
            Trade: <b>{header.StreamName}</b>
        </td>
        <td style=""border:none; text-align:right;"">
            Maximum Marks: <b>{header.MaxMarks}</b>
        </td>
    </tr>
</table>




    <table>
        <thead>
            <tr>
                <th>S.No.</th>
                <th>Roll No</th>
                <th>Marks (In Words)</th>
                <th>Marks (In Fig.)</th>
            </tr>
        </thead>
        <tbody>
");

                    int sno = 1;

                    foreach (var item in gitem)
                    {
                        sb.Append($@"
            <tr>
                <td>{sno++}</td>
                <td>{item.RollNo}</td>
                <td>{item.ObtainedMarks_inWords}</td>
                <td>{item.ObtainedMarks}</td>
            </tr>");
                    }

            //        for (int i = sno; i <= 30; i++)
            //        {
            //            sb.Append($@"
            //<tr>
            //    <td>{i}</td>
            //    <td></td>
            //    <td></td>
            //    <td></td>
            //</tr>");
            //        }

                    sb.Append($@"
        </tbody>
    </table>

    <br/><br/>

    <table width='100%' style='border:none'>
        <tr>
            <td class='text-left' style='border:none'>
                Name: <b>{header.ExaminerName}</b>
            </td>
            <td class='text-left' style='border:none'>
                Date: <span class='line'></span>
            </td>
        </tr>
        <tr>
            <td class='text-left' style='border:none'>
                Post: <span class='line'></span>
            </td>
            <td class='text-left' style='border:none'>
                Signature: <span class='line'></span>
            </td>
        </tr>
        <tr>
            <td class='text-left' style='border:none'>
                Mobile No: <b>{header.MobileNo}</b>
            </td>
            <td style='border:none'></td>
        </tr>
    </table>


</div></div>
");

                    groupIndex++;
                }

                // ================= HTML FOOTER =================
                sb.Append(@"
</body>
</html>
");

                // ================= PDF SETTINGS =================
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
            },
                    Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8" },
                    FooterSettings = new FooterSettings
                    {
                        FontName = "Arial",
                        FontSize = 9,
                        Left = "Printed on: [date]",
                        Right = "Page [page] of [toPage]",
                        Line = true
                    },
                }
            }
                };

                byte[] pdfBytes = _converter.Convert(doc);

                return File(
                    pdfBytes,
                    "application/pdf",
                    "Teacher_For_Examiner_Report.pdf"
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






    }
}