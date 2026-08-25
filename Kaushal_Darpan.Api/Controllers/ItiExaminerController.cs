using AspNetCore.Reporting;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Report.ITI;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Org.BouncyCastle.Utilities;
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

        [HttpGet("DeleteDataByID/{PK_ID}/{ModifyBy}")]
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

        //    [HttpPost("TeacherForExaminerReportDewnloadPdf")]
        //    public async Task<IActionResult> TeacherForExaminerReportDewnloadPdf(
        //[FromBody] ITITeacherForExaminerSearchModel body)
        //    {
        //        try
        //        {
        //            var streams_data =
        //                await _unitOfWork.ItiExaminerRepository.TeacherForExaminerReportDewnloadPdf(body);
        //            var dataList =
        //                CommonFuncationHelper.ConvertDataTable<List<ITITeacherForExaminerSearchModel>>(streams_data);
        //            var groupedData = dataList
        //                .GroupBy(x => new { x.SubjectCode, x.StreamID, x.SemesterID,x.CenterCode })
        //                .ToList();
        //            var sb = new StringBuilder();
        //            // ================= HTML HEADER =================
        //            sb.Append(@"
        //            <!DOCTYPE html>
        //            <html lang='en'>
        //            <head>
        //                <meta charset='UTF-8'>
        //                <title>Teacher Examiner Report</title>
        //                <style>
        //                    body {
        //                        font-family: Arial, Helvetica, sans-serif;
        //                        font-size: 12px;
        //                        margin: 20px;
        //                    }
        //                    table {
        //                        width: 100%;
        //                        border-collapse: collapse;
        //                        margin-top: 10px;
        //                    }
        //                    table, th, td {
        //                        border: 1px solid #000;
        //                    }
        //                    th, td {
        //                        padding: 5px;
        //                        text-align: center;
        //                    }
        //                    th {
        //                        font-weight: bold;
        //                    }
        //                    .text-left {
        //                        text-align: left;
        //                    }
        //                    .line {
        //                        border-bottom: 1px solid #000;
        //                        display: inline-block;
        //                        width: 200px;
        //                    }

        //             .footer-table {{
        //                    width: 100%;
        //                    border-collapse: collapse;
        //                    font-size: 10px;
        //                }}
        //                .footer-table td {{
        //                    border: 1px solid #000;
        //                    padding: 6px;
        //                    vertical-align: top;
        //                }}
        //                .line {{
        //                    display: inline-block;
        //                    border-bottom: 1px solid #000;
        //                    width: 150px;
        //                }}


        //                    .page-break {
        //                        --page-break-after: always;
        //                        --border-bottom: 1px dashed #000;
        //                    }

        //              .table-wrapper {
        //                    width: 100%;

        //                }

        //                .half-table {
        //                    width: 50%;
        //                    display: inline-block;
        //                    vertical-align: top;
        //                    font-size: 12px;
        //                    box-sizing: border-box;
        //                    padding-right: 10px;
        //                }
        //            .page-col {
        //                    width: 50%;
        //                    display: inline-block;
        //                    vertical-align: top;
        //                    font-size: 12px;
        //                    box-sizing: border-box;
        //                    padding: 10px;
        //                }
        //            .page-row {
        //                    width: 100%;
        //                    font-size: 0; /* remove inline-block gap */
        //                }


        //                </style>
        //            </head>
        //            <body>
        //            ");

        //            // ================= BODY =================
        //            //<td style=""border:none; text-align:left;"">
        //            //    Examiner Code: <b>{header.ExaminerCode}</b>
        //            //</td>
        //            int groupIndex = 0;
        //            int totalGroups = groupedData.Count;

        //            foreach (var gitem in groupedData)
        //            {
        //                var header = gitem.First();

        //               sb.Append($@"<div   style=""border:1px solid #ddd; padding:35px; margin-bottom:25px;"">
        //                <div {(groupIndex < totalGroups - 1 ? "class='page-break'" : "")}>

        //                <table width=""100%"" style=""border:none; margin-bottom:10px;"">
        //                    <tr>
        //                        <td style=""border:none; text-align:left;"">
        //                            <b>{header.ExamName}</b>
        //                        </td>
        //                        <td style=""border:none; text-align:right;"">
        //                            Center Code: <b>{header.CenterCode}</b>
        //                        </td>
        //                    </tr>

        //                    <tr>
        //                        <td style=""border:none; text-align:left;"" colspan=""2"">
        //                            Subject: <b>{header.SubjectName}</b>
        //                        </td> 
        //                    </tr>

        //                    <tr>
        //                        <td style=""border:none; text-align:left;"">
        //                            Trade: <b>{header.StreamName}</b>
        //                        </td>
        //                        <td style=""border:none; text-align:right;"">
        //                            Maximum Marks: <b>{header.MaxMarks}</b>
        //                        </td>
        //                    </tr>
        //                </table>


        //                <table>
        //                    <thead>
        //                        <tr>
        //                            <th>S.No.</th>
        //                            <th>Roll No</th>
        //                            <th>Marks (In Words)</th>
        //                            <th>Marks (In Fig.)</th>
        //                        </tr>
        //                    </thead>
        //                    <tbody>
        //            ");

        //                                int sno = 1;

        //                                foreach (var item in gitem)
        //                                {
        //                                    sb.Append($@"
        //                        <tr>
        //                            <td>{sno++}</td>
        //                            <td>{item.RollNo}</td>
        //                            <td>{item.ObtainedMarks_inWords}</td>
        //                            <td>{item.ObtainedMarks}</td>
        //                        </tr>");
        //                                }

        //                        //        for (int i = sno; i <= 30; i++)
        //                        //        {
        //                        //            sb.Append($@"
        //                        //<tr>
        //                        //    <td>{i}</td>
        //                        //    <td></td>
        //                        //    <td></td>
        //                        //    <td></td>
        //                        //</tr>");
        //                        //        }

        //                                sb.Append($@"
        //                    </tbody>
        //                </table>

        //                <br/><br/>

        //                <table width='100%' style='border:none'>
        //                    <tr>
        //                        <td class='text-left' style='border:none'>
        //                            Name: <b>{header.ExaminerName}</b>
        //                        </td>
        //                        <td class='text-left' style='border:none'>
        //                            Date: <span class='line'></span>
        //                        </td>
        //                    </tr>
        //                    <tr>
        //                        <td class='text-left' style='border:none'>
        //                            Post: <span class='line'></span>
        //                        </td>
        //                        <td class='text-left' style='border:none'>
        //                            Signature: <span class='line'></span>
        //                        </td>
        //                    </tr>
        //                    <tr>
        //                        <td class='text-left' style='border:none'>
        //                            Mobile No: <b>{header.MobileNo}</b>
        //                        </td>
        //                        <td style='border:none'></td>
        //                    </tr>
        //                </table>


        //            </div></div></div>
        //            ");

        //                groupIndex++;
        //            }

        //            // ================= HTML FOOTER =================
        //            sb.Append(@"
        //            </body>
        //            </html>
        //            ");

        //            // ================= PDF SETTINGS =================
        //            var doc = new HtmlToPdfDocument
        //            {
        //                GlobalSettings =
        //        {
        //            PaperSize = PaperKind.A4,
        //            Orientation = Orientation.Portrait
        //        },
        //                Objects =
        //        {
        //            new ObjectSettings
        //            {
        //                HtmlContent = sb.ToString(),
        //                WebSettings = { DefaultEncoding = "utf-8" },
        //                FooterSettings = new FooterSettings
        //                {
        //                    FontName = "Arial",
        //                    FontSize = 9,
        //                    Left = "Printed on: [date]",
        //                    Right = "Page [page] of [toPage]",
        //                    Line = true
        //                },
        //            }
        //        }
        //            };

        //            byte[] pdfBytes = _converter.Convert(doc);

        //            return File(
        //                pdfBytes,
        //                "application/pdf",
        //                "Teacher_For_Examiner_Report.pdf"
        //            );
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, ex.Message);
        //        }
        //    }



        #region upated TeacherForExaminerReportDewnloadPdf
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
                    .GroupBy(x => new { x.SubjectCode, x.StreamID, x.SemesterID, x.CenterCode })
                    .ToList();

                const int rowsPerPage = 1000; // only matters for very large groups now

                var sb = new StringBuilder();

                // ================= HTML HEADER =================
                sb.Append(@"
        <!DOCTYPE html>
        <html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Teacher Examiner Report</title>

    <style>
        * {
            box-sizing: border-box;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

        body {
            margin: 0;
            padding: 0;
            background: #fff;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            page-break-inside: auto;
        }

        table,
        th,
        td {
            border: 1px solid #000;
        }

        th,
        td {
            padding: 5px;
            text-align: center;
        }

        th {
            font-weight: bold;
        }

        tr {
            page-break-inside: avoid;
            page-break-after: auto;
        }

        .text-left {
            text-align: left;
        }

        .line {
            border-bottom: 1px solid #000;
            display: inline-block;
            width: 200px;
        }

        .footer-table {
            width: 100%;
            border-collapse: collapse;
            font-size: 10px;
        }

        .footer-table td {
            border: 1px solid #000;
            padding: 6px;
            vertical-align: top;
        }

        /* Force new PDF page */
        .page-break {
            page-break-after: always;
            break-after: page;
        }

        /*
         * Each group will stay together as much as possible.
         * If the group cannot fit on the remaining page,
         * it will move to the next page.
         */
        .group-block {
            border: 1px solid #ddd;
            padding: 35px;
            margin-bottom: 25px;
            page-break-inside: avoid;
            break-inside: avoid;
        }

        /* PDF page settings */
        @page {
            size: A4;
            margin: 20px;
        }
    </style>
</head>
        </head>
        <body>
        ");

                // ================= BODY =================
                int groupIndex = 0;
                int totalGroups = groupedData.Count;

                foreach (var gitem in groupedData)
                {
                    var header = gitem.First();
                    var rows = gitem.ToList();

                    var rowChunks = rows
                        .Select((item, idx) => new { item, idx })
                        .GroupBy(x => x.idx / rowsPerPage)
                        .Select(g => g.Select(x => x.item).ToList())
                        .ToList();

                    for (int chunkIndex = 0; chunkIndex < rowChunks.Count; chunkIndex++)
                    {
                        var chunkRows = rowChunks[chunkIndex];
                        bool isLastChunkOfGroup = chunkIndex == rowChunks.Count - 1;

                        sb.Append(@"<div class='group-block'>");

                        sb.Append($@"
                <table width=""100%"" style=""border:none; margin-bottom:10px;"">
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

                        int sno = chunkIndex * rowsPerPage + 1;
                        foreach (var item in chunkRows)
                        {
                            sb.Append($@"
                    <tr>
                        <td>{sno++}</td>
                        <td>{item.RollNo}</td>
                        <td>{item.ObtainedMarks_inWords}</td>
                        <td>{item.ObtainedMarks}</td>
                    </tr>");
                        }

                        sb.Append(@"
                    </tbody>
                </table>
                ");

                        if (isLastChunkOfGroup)
                        {
                            sb.Append($@"
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
                    ");
                        }

                        sb.Append(@"</div>");

                        // Force a break ONLY if this group itself continues into another chunk
                        if (!isLastChunkOfGroup)
                        {
                            sb.Append(@"<div class='page-break'></div>");
                        }
                    }

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
                        Left = "Printed on: " + DateTime.Now.ToString("dd-MM-yyyy"),
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





        [HttpPost("TeacherForExaminerReportDewnloadPdf4")]
        public IActionResult TeacherForExaminerReportDewnloadPdf4()
        {
            try
            {
                var sb = new StringBuilder();

                sb.Append(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>SCVT Examination Marks Sheet</title>
    <style>
        * {
            box-sizing: border-box;
            font-family: Arial, sans-serif;
            font-size: 11px;
        }

        body {
            margin: 0;
            padding: 0;
            background: #fff;
        }

        /* Header Table */
        .header-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
        }

        .header-table td {
            padding: 2px 4px;
            vertical-align: top;
        }

        .bold { font-weight: bold; }
        .text-center { text-align: center; }
        .text-right { text-align: right; }

        /* Master Wrapper Table for 50/50 Layout */
        .layout-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;
        }

        .layout-table > tbody > tr > td {
            padding: 0;
            vertical-align: top;
        }

        /* Data Tables */
        .marks-table {
            width: 100%;
            border-collapse: collapse;
        }

        .marks-table th, 
        .marks-table td {
            border: 1px solid #444;
            padding: 4px 3px;
            text-align: left;
            height: 22px;
        }

        .marks-table th {
            font-weight: bold;
            background-color: #f0f0f0;
        }

        .col-sno { width: 12%; text-align: center; }
        .col-roll { width: 30%; }
        .col-words { width: 43%; }
        .col-fig { width: 15%; text-align: center; }
    </style>
</head>
<body>

    <!-- Header Details -->
    <table class=""header-table"">
        <tr>
            <td width=""33%"">
                <div>SCVT Examination (Yearly / First) July 2025</div>
                <div>Examiner Code: <span class=""bold"">E-0942</span></div>
                <div>Trade: <span class=""bold"">Mechanic Diesel</span></div>
            </td>
            <td width=""34%"" class=""text-center"">
                <div>Center Code: <span class=""bold"">G0127</span></div>
                <div style=""margin-top: 4px;"">Subject: <span class=""bold"">Paper-II: Employability Skills</span></div>
                <div>Maximum Marks: <span class=""bold"">50</span></div>
            </td>
            <td width=""33%"" class=""text-right"">
                <div>Date: <span class=""bold"">24-08-2026</span></div>
                <div>Subject Code: <span class=""bold"">SUB-102</span></div>
                <div>Semester/Year: <span class=""bold"">Sem-I</span></div>
            </td>
        </tr>
    </table>

    <!-- 50/50 Layout Wrapper Table -->
    <table class=""layout-table"">
        <tr>
            <!-- Left Side (50%) -->
            <td width=""49%"">
                <table class=""marks-table"">
                    <thead>
                        <tr>
                            <th class=""col-sno"">S.No.</th>
                            <th class=""col-roll"">Roll No</th>
                            <th colspan=""2"" class=""text-center"">Marks Obtained</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th></th>
                            <th class=""col-words"">In Words</th>
                            <th class=""col-fig"">In Fig.</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td class=""col-sno"">1</td><td>2507011007</td><td>forty two</td><td class=""col-fig"">42</td></tr>
                        <tr><td class=""col-sno"">2</td><td>2507011008</td><td>forty zero</td><td class=""col-fig"">40</td></tr>
                        <tr><td class=""col-sno"">3</td><td>2507011009</td><td>forty two</td><td class=""col-fig"">42</td></tr>
                        <tr><td class=""col-sno"">4</td><td>2507011010</td><td>thirty six</td><td class=""col-fig"">36</td></tr>
                        <tr><td class=""col-sno"">5</td><td>2507011011</td><td>forty four</td><td class=""col-fig"">44</td></tr>
                        <tr><td class=""col-sno"">6</td><td>2507011012</td><td>thirty eight</td><td class=""col-fig"">38</td></tr>
                        <tr><td class=""col-sno"">7</td><td>2507011013</td><td>forty zero</td><td class=""col-fig"">40</td></tr>
                        <tr><td class=""col-sno"">8</td><td>2507011014</td><td>thirty eight</td><td class=""col-fig"">38</td></tr>
                        <tr><td class=""col-sno"">9</td><td>2507011015</td><td>forty zero</td><td class=""col-fig"">40</td></tr>
                        <tr><td class=""col-sno"">10</td><td>2507011016</td><td>thirty eight</td><td class=""col-fig"">38</td></tr>
                        <tr><td class=""col-sno"">11</td><td>2507011017</td><td>forty two</td><td class=""col-fig"">42</td></tr>
                    </tbody>
                </table>
            </td>

            <!-- Spacer Gap (2%) -->
            <td width=""2%""></td>

            <!-- Right Side (50%) -->
            <td width=""49%"">
                <table class=""marks-table"">
                    <thead>
                        <tr>
                            <th class=""col-sno"">S.No.</th>
                            <th class=""col-roll"">Roll No</th>
                            <th colspan=""2"" class=""text-center"">Marks Obtained</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th></th>
                            <th class=""col-words"">In Words</th>
                            <th class=""col-fig"">In Fig.</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td class=""col-sno"">31</td><td>2507011037</td><td>forty two</td><td class=""col-fig"">42</td></tr>
                        <tr><td class=""col-sno"">32</td><td>2507011038</td><td>fifty zero</td><td class=""col-fig"">50</td></tr>
                        <tr><td class=""col-sno"">33</td><td>2507011039</td><td>forty six</td><td class=""col-fig"">46</td></tr>
                        <tr><td class=""col-sno"">34</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">35</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">36</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">37</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">38</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">39</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">40</td><td></td><td></td><td></td></tr>
                        <tr><td class=""col-sno"">41</td><td></td><td></td><td></td></tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>

</body>
</html>");

                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
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
                        FontSize = 8,
                        Left = "Printed on: " + DateTime.Now.ToString("dd-MM-yyyy"),
                        Right = "Page [page] of [toPage]",
                        Line = true
                    }
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



        [HttpPost("TeacherForExaminerReportDewnloadPdfNew")]
        public async Task<IActionResult> TeacherForExaminerReportDewnloadPdfNew([FromBody] ITITeacherForExaminerSearchModel body)
        {
            try
            {
                var streams_data = await _unitOfWork.ItiExaminerRepository.TeacherForExaminerReportDewnloadPdf(body);
                var dataList = CommonFuncationHelper.ConvertDataTable<List<ITITeacherForExaminerSearchModel>>(streams_data);

                // Grouping data dynamically
                var groupedData = dataList
                    .GroupBy(x => new { x.SubjectCode, x.StreamID, x.SemesterID, x.CenterCode })
                    .ToList();

                var sb = new StringBuilder();

                // ================= HTML HEADER & STYLES =================
                sb.Append(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <title>SCVT Examination Marks Sheet</title>

    <style>
        * {
            box-sizing: border-box;
            font-family: Arial, sans-serif;
            font-size: 11px;
        }

        body {
            margin: 0;
            padding: 0;
            background: #fff;
        }

        /* ================= PAGE ================= */

        .page {
            width: 100%;
            page-break-after: always;
            break-after: page;
            page-break-inside: avoid;
            break-inside: avoid;
        }

        .page:last-child {
            page-break-after: auto;
            break-after: auto;
        }

        /* ================= HEADER ================= */

        .header-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 12px;
        }

        .header-table td {
            padding: 2px 4px;
            vertical-align: top;
        }

        .bold {
            font-weight: bold;
        }

        .text-center {
            text-align: center;
        }

        .text-right {
            text-align: right;
        }

        /* ================= 50/50 LAYOUT ================= */

        .layout-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;

            /* Important */
            page-break-inside: avoid;
            break-inside: avoid;
        }

        .layout-table > tbody > tr > td {
            padding: 0;
            vertical-align: top;
        }

        /* ================= MARKS TABLE ================= */

        .marks-table {
            width: 100%;
            border-collapse: collapse;
            table-layout: fixed;

            /* Do not allow table to split */
            page-break-inside: avoid;
            break-inside: avoid;
        }

        .marks-table thead {
            display: table-row-group;
        }

        .marks-table tbody {
            display: table-row-group;
        }

        .marks-table tr {
            page-break-inside: avoid;
            break-inside: avoid;
        }

        .marks-table th,
        .marks-table td {
            border: 1px solid #444;
            padding: 4px 3px;
            text-align: left;
            height: 22px;
        }

        .marks-table th {
            font-weight: bold;
            background-color: #f0f0f0;
        }

        /* ================= COLUMN WIDTH ================= */

        .col-sno {
            width: 12%;
            text-align: center;
        }

        .col-roll {
            width: 30%;
        }

        .col-words {
            width: 43%;
        }

        .col-fig {
            width: 15%;
            text-align: center;
        }

        /* ================= PAGE BREAK ================= */

        .page-break {
            page-break-after: always;
            break-after: page;
        }
    </style>
</head>
<body>");
                var ExaminerName = String.Empty;
                var MobileNo = String.Empty;

                // ================= DYNAMIC DATA LOOP =================
                int pagebreackCount = 0;
                foreach (var group in groupedData)
                {
                    var students = group.ToList();
                    var firstItem = students.FirstOrDefault();

                    ExaminerName = firstItem.ExaminerName;
                    MobileNo = firstItem.MobileNo;

                    const int maxRowsPerColumn = 30; // Adjust row split count as needed
                    var leftList = students.Take(maxRowsPerColumn).ToList();
                    var rightList = students.Skip(maxRowsPerColumn).Take(maxRowsPerColumn).ToList();

                    if (leftList.Count > 30)
                    {

                        sb.Append($@"
<div class=""page"">
    <!-- 50/50 Layout Table Wrapper -->
    <table class=""layout-table"">
        <tr>
            <!-- Left Side Column (1 to 10) -->
            <td width=""49%"">
                <!-- Left Header -->
                <table style=""width:100%; border:none; margin-bottom:10px;"">
                    <tr>
                        <td style=""border:none; text-align:left;""><b>{firstItem.ExamName}</b></td>
                        <td style=""border:none; text-align:right;"">Center Code: <b>{firstItem.CenterCode}</b></td>
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Examiner Code: <b>{firstItem.ExaminerCode}</b></td>
                        <td style=""border:none; text-align:right;"">Subject: <b>{firstItem.SubjectName}</b></td>                        
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Trade: <b>{firstItem.StreamName}</b></td>
                        <td style=""border:none; text-align:right;"">Maximum Marks: <b>{firstItem.MaxMarks}</b></td>
                    </tr>
                </table>

                <!-- Left Marks Table -->
                <table class=""marks-table"">
                    <thead>
                        <tr>
                            <th class=""col-sno"">S.No.</th>
                            <th class=""col-roll"">Roll No</th>
                            <th colspan=""2"" class=""text-center"">Marks Obtained</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th></th>
                            <th class=""col-words"">In Words</th>
                            <th class=""col-fig"">In Fig.</th>
                        </tr>
                    </thead>
                    <tbody>");

                        for (int i = 0; i < maxRowsPerColumn; i++)
                        {
                            if (i < leftList.Count)
                            {
                                var item = leftList[i];
                                sb.Append($@"
                        <tr>
                            <td class=""col-sno"">{i + 1}</td>
                            <td class=""col-roll"">{item.RollNo}</td>
                            <td class=""col-words"">{item.ObtainedMarks_inWords}</td>
                            <td class=""col-fig"">{item.ObtainedMarks}</td>
                        </tr>");
                            }
                            //else
                            //{
                            //    sb.Append($@"
                            //<tr>
                            //    <td class=""col-sno"">{i + 1}</td>
                            //    <td></td>
                            //    <td></td>
                            //    <td></td>
                            //</tr>");
                            //}
                        }

                        sb.Append($@"
                    </tbody>
                </table>
            </td>


            <!-- 2% Horizontal Spacing Gap -->
            <td width=""2%""></td>

            <!-- Right Side Column (11 to 20) -->


            <td width=""49%"">

                <!-- Right Header -->
                <table style=""width:100%; border:none; margin-bottom:10px;"">
                    <tr>
                        <td style=""border:none; text-align:left;""><b>{firstItem.ExamName}</b></td>
                        <td style=""border:none; text-align:right;"">Center Code: <b>{firstItem.CenterCode}</b></td>
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Examiner Code: <b>{firstItem.ExaminerCode}</b></td>
                        <td style=""border:none; text-align:right;"">Subject: <b>{firstItem.SubjectName}</b></td>                        
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Trade: <b>{firstItem.StreamName}</b></td>
                        <td style=""border:none; text-align:right;"">Maximum Marks: <b>{firstItem.MaxMarks}</b></td>
                    </tr>
                </table>


                <!-- Right Marks Table -->
                <table class=""marks-table"">
                    <thead>
                        <tr>
                            <th class=""col-sno"">S.No.</th>
                            <th class=""col-roll"">Roll No</th>
                            <th colspan=""2"" class=""text-center"">Marks Obtained</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th></th>
                            <th class=""col-words"">In Words</th>
                            <th class=""col-fig"">In Fig.</th>
                        </tr>
                    </thead>
                    <tbody>");

                        for (int i = 0; i < maxRowsPerColumn; i++)
                        {
                            int sno = maxRowsPerColumn + i + 1;
                            if (i < rightList.Count)
                            {
                                var item = rightList[i];
                                sb.Append($@"
                        <tr>
                            <td class=""col-sno"">{sno}</td>
                            <td class=""col-roll"">{item.RollNo}</td>
                            <td class=""col-words"">{item.ObtainedMarks_inWords}</td>
                            <td class=""col-fig"">{item.ObtainedMarks}</td>
                        </tr>");
                            }
                            else
                            {
                                sb.Append($@"
                        <tr>
                            <td class=""col-sno"">{sno}</td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>");
                            }
                        }

                        sb.Append(@"
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</div>");
                    }
                    else
                    {
                        sb.Append($@"
<div >
    <!-- 50/50 Layout Table Wrapper -->
    <table class=""layout-table"">
        <tr>
            <!-- Left Side Column (1 to 10) -->
            <td width=""100%"">
                <!-- Left Header -->
                <table style=""width:100%; border:none; margin-bottom:10px; margin-top:10px;"">
                    <tr>
                        <td style=""border:none; text-align:left;""><b>{firstItem.ExamName}</b></td>
                        <td style=""border:none; text-align:right;"">Center Code: <b>{firstItem.CenterCode}</b></td>
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Examiner Code: <b>{firstItem.ExaminerCode}</b></td>
                        <td style=""border:none; text-align:right;"">Subject: <b>{firstItem.SubjectName}</b></td>                        
                    </tr>
                    <tr>
                        <td style=""border:none; text-align:left;"">Trade: <b>{firstItem.StreamName}</b></td>
                        <td style=""border:none; text-align:right;"">Maximum Marks: <b>{firstItem.MaxMarks}</b></td>
                    </tr>
                </table>

                <!-- Left Marks Table -->
                <table class=""marks-table"">
                    <thead>
                        <tr>
                            <th class=""col-sno"">S.No.</th>
                            <th class=""col-roll"">Roll No</th>
                            <th colspan=""2"" class=""text-center"">Marks Obtained</th>
                        </tr>
                        <tr>
                            <th></th>
                            <th></th>
                            <th class=""col-words"">In Words</th>
                            <th class=""col-fig"">In Fig.</th>
                        </tr>
                    </thead>
                    <tbody>");

                        for (int i = 0; i < maxRowsPerColumn; i++)
                        {
                            if (i < leftList.Count)
                            {
                                var item = leftList[i];
                                sb.Append($@"
                        <tr>
                            <td class=""col-sno"">{i + 1}</td>
                            <td class=""col-roll"">{item.RollNo}</td>
                            <td class=""col-words"">{item.ObtainedMarks_inWords}</td>
                            <td class=""col-fig"">{item.ObtainedMarks}</td>
                        </tr>");





                                pagebreackCount++;

                            }
                            else
                            {
                                sb.Append($@"
                            <tr>
                                <td class=""col-sno"">{i + 1}</td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>");
                            }

                        }


                        sb.Append($@"
                    </tbody>
                </table>
            </td>

            ");

                        if (pagebreackCount >= 30)
                        {
                            sb.Append("<div style='page-break-after: always;'></div>");
                            pagebreackCount = 0;
                        }






                    }




                }
                sb.Append($@"
<br/><br/>

<table width='100%' style='border:none; border-collapse:collapse; margin-top:30px;'>
    <tr>
        <td width='50%' style='border:none; padding:6px 20px 6px 0;'>
            <span style='font-weight:bold;'>Name:</span>
            <span style='margin-left:10px;'>{ExaminerName}</span>
        </td>

        <td width='50%' style='border:none; padding:6px 0 6px 20px;'>
            <span style='font-weight:bold;'>Date:</span>
            <span style='display:inline-block; width:160px; border-bottom:1px solid #000; margin-left:10px;'></span>
        </td>
    </tr>

    <tr>
        <td style='border:none; padding:6px 20px 6px 0;'>
            <span style='font-weight:bold;'>Post:</span>
            <span style='display:inline-block; width:180px; border-bottom:1px solid #000; margin-left:10px;'></span>
        </td>

        <td style='border:none; padding:6px 0 6px 20px;'>
            <span style='font-weight:bold;'>Signature:</span>
            <span style='display:inline-block; width:160px; border-bottom:1px solid #000; margin-left:10px;'></span>
        </td>
    </tr>

    <tr>
        <td style='border:none; padding:6px 20px 6px 0;'>
            <span style='font-weight:bold;'>Mobile No:</span>
            <span style='margin-left:10px;'>{MobileNo}</span>
        </td>

        <td style='border:none;'></td>
    </tr>
</table>
");


                // ================= PDF CONVERSION SETTINGS =================
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
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
                        FontSize = 8,
                        //Left = "Printed on: " + DateTime.Now.ToString("dd-MM-yyyy"),
                        //Right = "Page [page] of [toPage]",
                        //Line = true
                    }
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







        #endregion



        [HttpPost("ITIExaminerUploadFiles")]
        public async Task<ApiResult<int>> ITIExaminerUploadFiles([FromBody] ITIExaminerUploadFilesModel request)
        {
            ActionName = "ITIExaminerUploadFilesModel([FromBody])";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.ItiExaminerRepository.ITIExaminerUploadFiles(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Saved successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "There was an error updating data.!";
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

        [HttpPost("ITIExaminerUploadFilesByAction")]
        public async Task<ApiResult<DataTable>> ITIExaminerUploadFilesByAction([FromBody] ITIExaminerUploadFilesModel body)
        {
            ActionName = "GetTeacherForExaminerReport()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ItiExaminerRepository.ITIExaminerUploadFilesByAction(body));
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