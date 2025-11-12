using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BterStudentJoinStatus;
using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Reflection;
using Kaushal_Darpan.Models.ITIStudentMeritInfo;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.BterCertificateReport;
using System.Text;
using iTextSharp.tool.xml.html;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class StudentController : BaseController
    {
        public override string PageName => "StudentController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("GetStudentDashboard")]
        public async Task<ApiResult<DataTable>> GetStudentDashboard([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentDashboard()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetStudentDashboard(body);
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

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<StudentDetailsModel>>> GetAllData([FromBody] StudentSearchModel body)
        {
            ActionName = "GetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<StudentDetailsModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetAllData(body);
                if (result.Data.Count > 0)
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

        [HttpPost("ITIGetAllData")]
        public async Task<ApiResult<List<StudentDetailsModel>>> ITIGetAllData([FromBody] StudentSearchModel body)
        {
            ActionName = "ITIGetAllData([FromBody] StudentSearchModel body)";
            var result = new ApiResult<List<StudentDetailsModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.ITIGetAllData(body);
                if (result.Data.Count > 0)
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

        [HttpPost("GetStudentDeatilsByAction")]
        public async Task<ApiResult<DataTable>> GetStudentDeatilsByAction([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentDeatilsByAction()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetStudentDeatilsByAction(body);
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

        [HttpPost("GetITIStudentDeatilsByAction")]
        public async Task<ApiResult<DataTable>> GetITIStudentDeatilsByAction([FromBody] StudentSearchModel body)
        {
            ActionName = "GetITIStudentDeatilsByAction()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetITIStudentDeatilsByAction(body);
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

        [HttpPost("UpdateStudentSsoMapping")]
        public async Task<ApiResult<int>> UpdateStudentSsoMapping([FromBody] StudentSearchModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try {

                    var data = await _unitOfWork.StudentRepository.UpdateStudentSsoMapping(model);
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


        [HttpPost("StudentPlacementMapping")]
        public async Task<ApiResult<int>> StudentPlacementMapping([FromBody] StudentSearchModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    var data = await _unitOfWork.StudentRepository.StudentPlacementMapping(model);
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


        [HttpPost("GetStudentDeatilsBySSOId/{ssoid}/{departmentid}")]
        public async Task<ApiResult<DataTable>> GetStudentDeatilsBySSOId(string ssoid, int departmentid = 0)
        {
            ActionName = "GetStudentDeatilsBySSOId()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetStudentDeatilsBySSOId(ssoid, departmentid);
                //var r = Path.Combine(ConfigurationHelper.StaticFileRootPath, Constants.StudentsFolder);
                //result.Path = r;
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



        [HttpPost("GetProfileDashboard")]
        public async Task<ApiResult<DataTable>> GetProfileDashboard([FromBody] StudentSearchModel body)
        {
            ActionName = "GetProfileDashboard()";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetProfileDashboard(body);
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


        [HttpPost("GetDataStudentBySSOId/{ssoid}/{departmentId}")]

        public async Task<ApiResult<DataTable>> GetDataStudentBySSOId(string ssoid, int departmentId = 0)
        {
            ActionName = "GetDataStudentBySSOId()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await _unitOfWork.StudentRepository.GetDataStudentBySSOId(ssoid, departmentId);
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


        [HttpPost("AddStudentData")]
        public async Task<ApiResult<int>> AddStudentData([FromBody] VerifierDataModel request)
        {
            ActionName = "AddStudentData([FromBody] VerifierDataModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.AddStudentData(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data > 0)
                    {
                        result.State = EnumStatus.Success;
                        if (request.VerifierID == 0)
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
                        if (request.VerifierID == 0)
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


        [HttpPost("GetStudentMeritinfo")]
        public async Task<ApiResult<StudentMeritInfoModel>> GetStudentMeritinfo([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentMeritinfo([FromBody] StudentSearchModel body)";
            var result = new ApiResult<StudentMeritInfoModel>();
            try
            {
                // Pass the entire model to the repository
                var data = await _unitOfWork.StudentRepository.GetStudentMeritinfo(body);
                if (data != null)
                {
                    var mappedData = _mapper.Map<StudentMeritInfoModel>(data);
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


        [HttpPost("SaveRecheckData")]
        public async Task<ApiResult<bool>> SaveRecheckData([FromBody] List<RecheckDocumentModel> request)
        {
            ActionName = "SaveAllData([FromBody] List<PlacementShortListStudentResponseModel> request)";
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
                    var isSave = await _unitOfWork.StudentRepository.SaveRecheckData(request);
                    await _unitOfWork.SaveChangesAsync();  // Commit changes if everything is successful

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

        [HttpPost("GetAttendanceTimeTable")]
        public async Task<ApiResult<DataTable>> GetAttendanceTimeTable([FromBody] AttendanceTimeTableModal request)
        {
            ActionName = "GetAttendanceTimeTable()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetAttendanceTimeTable(request);
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

        [HttpPost("GetStudentAttendance")]
        public async Task<ApiResult<DataTable>> GetStudentAttendance([FromBody] AttendanceTimeTableModal request)
        {
            ActionName = "GetStudentAttendance()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetStudentAttendance(request);
                    //var holidayData = await _unitOfWork.StudentRepository.GetHolidaysmaster(request.AttendanceStartDate, request.AttendanceEndDate);

                    // if (result.Data.Rows.Count > 0)
                    // {
                    //     // Iterate through each student attendance row
                    //     foreach (DataRow studentRow in result.Data.Rows)
                    //     {

                    //         // Check each holiday data to update attendance status
                    //         foreach (DataRow holidayRow in holidayData.Rows)
                    //         {
                    //             var holidayDate = Convert.ToDateTime(holidayRow.ItemArray[0]).ToString("yyyy-MM-dd");

                    //             if (!result.Data.Columns.Contains(holidayDate))
                    //             {
                    //                 result.Data.Columns.Add(holidayDate, typeof(string)); // Add new column to store holiday data
                    //                                                                       // Get the first item in the holidayRow
                    //                 string holidayValue = "A";
                    //                 // Example: Add the holidayValue to the studentRow's new column
                    //                 studentRow[holidayDate] = holidayValue;
                    //             }
                    //             else
                    //             {
                    //                 // Get the first item in the holidayRow
                    //                 string holidayValue = "P";
                    //                 // Example: Add the holidayValue to the studentRow's new column
                    //                 studentRow[holidayDate] = holidayValue;
                    //             }





                    //         }
                    //     }
                    // }




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

        [HttpPost("GetStudentAttendanceSubjectWise")]
        public async Task<ApiResult<DataTable>> GetStudentAttendanceSubjectWise([FromBody] AttendanceTimeTableModal request)
        {
            ActionName = "GetStudentAttendanceSubjectwise()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetStudentAttendanceSubjectwise(request);
                    
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

        [HttpPost("AddStudentAttendance")]
        public async Task<ApiResult<int>> AddStudentAttendance([FromBody] List<PostAttendanceTimeTableModal> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.AddStudentAttendance(model);
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

        [HttpPost("PostAttendanceTimeTable")]
        public async Task<ApiResult<int>> PostAttendanceTimeTable([FromBody] PostAttendanceTimeTable model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.PostAttendanceTimeTable(model);
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

        [HttpPost("GetITIStudentMeritinfo")]
        public async Task<ApiResult<System.Data.DataSet>> GetITIStudentMeritinfo([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentMeritinfo([FromBody] StudentSearchModel body)";
            var result = new ApiResult<System.Data.DataSet>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetITIStudentMeritinfo(body);
                if (result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
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


        [HttpPost("GetStudentApplication")]
        public async Task<ApiResult<DataTable>> GetStudentApplication([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentApplication([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetStudentApplication(body);
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





        [HttpPost("GetReverApplication")]
        public async Task<ApiResult<DataTable>> GetReverApplication([FromBody] StudentSearchModel body)
        {
            ActionName = "GetStudentApplication([FromBody] StudentSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.StudentRepository.GetReverApplication(body);
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


        [HttpPost("ITI_AddStudentAttendance")]
        public async Task<ApiResult<int>> ITI_AddStudentAttendance([FromBody] List<PostAttendanceTimeTableModal> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.ITIAddStudentAttendance(model);
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


        [HttpPost("ITI_GetAttendanceTimeTable")]
        public async Task<ApiResult<DataTable>> ITI_GetAttendanceTimeTable([FromBody] AttendanceTimeTableModal request)
        {
            ActionName = "ITI_GetAttendanceTimeTable()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.ITIGetAttendanceTimeTable(request);
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


        [HttpPost("PostAttendanceTimeTableList")]
        public async Task<ApiResult<int>> PostAttendanceTimeTableList([FromBody] List<PostAttendanceTimeTable> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.PostAttendanceTimeTableList(model);
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


        [HttpPost("SetCalendarEventModel")]
        public async Task<ApiResult<int>> SetCalendarEventModel([FromBody] List<CalendarEventModel> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.SetCalendarEventModel(model);
                    await _unitOfWork.SaveChangesAsync();
                    if (data > 0)
                    {
                        result.State = EnumStatus.Success;
                        result.Data = data;
                        result.Message = "Set Teacher Calendar Event Mapped Successfully";
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


        [HttpPost("getCalendarEventModel")]
        public async Task<ApiResult<DataTable>> getCalendarEventModel([FromBody] CalendarEventModel request)
        {
            ActionName = "getCalendarEventModel()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.getCalendarEventModel(request);
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

        [HttpPost("getdublicateCheckSection")]
        public async Task<ApiResult<DataTable>> getdublicateCheckSection([FromBody] SectionDataModel request)
        {
            ActionName = "getdublicateCheckSection()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.getdublicateCheckSection(request);
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


        [HttpPost("GetRosterDisplay_PDFTimeTable")]
        public async Task<ApiResult<DataTable>> GetRosterDisplay_PDFTimeTable([FromBody] RosterDisplayTimeTableDataModel request)
        {
            ActionName = "GetRosterDisplay_PDFTimeTable()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetRosterDisplay_PDFTimeTable(request);
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

        [HttpPost("GetRosterDisplay_PDFTimeTableDownload")]
        public async Task<ApiResult<string>> GetRosterDisplay_PDFTimeTableDownload([FromBody] RosterDisplayTimeTableDataModel model)
        {
            ActionName = "GetBterBridgeCourseReport(BterStatisticsReportDataModel)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.GetRosterDisplay_PDFTimeTableDownload(model);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        DataTable dt = data.Tables[0];
                        DataTable tempDt = dt.Clone();
                        DataRow prevRow = null;
                        Func<string, bool> IsTimeColumn = colName => colName.Contains(":") && colName.Contains("-");

                        foreach (DataRow row in dt.Rows)
                        {
                            DataRow newRow = tempDt.NewRow();
                            newRow.ItemArray = row.ItemArray.Clone() as object[];

                            if (prevRow != null)
                            {
                                foreach (DataColumn col in dt.Columns)
                                {
                                    string colName = col.ColumnName;


                                    if (IsTimeColumn(colName))
                                        continue;


                                    if (prevRow[colName]?.ToString() == row[colName]?.ToString())
                                        newRow[colName] = null;
                                }
                            }

                            tempDt.Rows.Add(newRow);
                            prevRow = row;
                        }

                        var dsTemp = new System.Data.DataSet();

                        // Copy DataTable into DataSet
                        var copiedTable = tempDt.Copy();
                        copiedTable.TableName = "GetRosterDisplay_PDFTimeTable";
                        dsTemp.Tables.Add(copiedTable);


                        DataTable headerTable = new DataTable();
                        headerTable.Columns.Add("Names", typeof(string));
                        foreach (DataColumn col in copiedTable.Columns)
                        {
                            string colName = col.ColumnName;
                            if (IsTimeColumn(colName))   // ✅ keep only time columns
                            {
                                DataRow row = headerTable.NewRow();
                                row["Names"] = colName;
                                headerTable.Rows.Add(row);
                            }
                        }

                        var headerTable1 = headerTable.Copy();
                        headerTable1.TableName = "GetRosterDisplay_PDFTimeTable_Header";
                        dsTemp.Tables.Add(headerTable1);

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        
                        string html = BuildTimeTableHtml(dsTemp);

                        //log 1
                        var ex = new Exception(html);
                        var nex = new NewException
                        {
                            PageName = "Deepak_1",
                            ActionName = ActionName,
                            Ex = ex,
                        };
                        await CreateErrorLog(nex, _unitOfWork);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
                        sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));


                        //log 2
                        var ex1 = new Exception(sb1.ToString());
                        var nex1 = new NewException
                        {
                            PageName = "Deepak_2",
                            ActionName = ActionName,
                            Ex = ex1,
                        };
                        await CreateErrorLog(nex1, _unitOfWork);



                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "landsacp", "");

                        result.Data = Convert.ToBase64String(pdfBytes);
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
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



        public static string BuildTimeTableHtml(System.Data.DataSet ds)
        {
            StringBuilder sb = new StringBuilder();

            // ===== Get unique teacher-class totals =====
            HashSet<string> uniqueTeachers = new HashSet<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string teacherClassTotal = row["TeacherClassTotal"]?.ToString();
                if (!string.IsNullOrWhiteSpace(teacherClassTotal))
                    uniqueTeachers.Add(teacherClassTotal);
            }
            string result = string.Join(", ", uniqueTeachers);

            // ===== Header Table (Institute + Info) =====
            sb.AppendLine("<table style='width:100%; font-size:14px; font-family:Arial, Helvetica, sans-serif;' cellpadding='5'>");
            sb.AppendLine($"  <tr><th colspan='2' style='text-align:center;'>{ds.Tables[0].Rows[0]["InstituteName"]}</th></tr>");
            sb.AppendLine($"  <tr><th colspan='2' style='text-align:center;'>TIME TABLE {ds.Tables[0].Rows[0]["FinancialYearName"]}</th></tr>");
            sb.AppendLine("  <tr>");
            sb.AppendLine($"    <th style='text-align:left; padding-left:20px;'>{ds.Tables[0].Rows[0]["StreamName"]}: {ds.Tables[0].Rows[0]["SemesterName"]}</th>");
            sb.AppendLine($"    <th style='text-align:right;'>W.E.F. {ds.Tables[0].Rows[0]["Date"]}</th>");
            sb.AppendLine("  </tr>");
            sb.AppendLine("</table>");

            // ===== Main Timetable Table =====
            sb.AppendLine("<table style='width:100%; border-collapse:collapse; font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; text-align:center; border:1px solid black;' cellpadding='5'>");

            // Header Row (Period Numbers)
            sb.AppendLine("<tr style='background-color:#f0f0f0;'>");
            sb.AppendLine("<td rowspan='2' style='border:1px solid black;'>Day</td>");
            sb.AppendLine("<td rowspan='2' style='border:1px solid black;'>Group</td>");
            for (int period = 1; period <= 6; period++)
            {
                sb.AppendLine($"<td style='border:1px solid black;'>{period}</td>");
            }
            sb.AppendLine("</tr>");

            // Header Row (Time Slots)
            sb.AppendLine("<tr style='background-color:#f9f9f9;'>");
            for (int i = 0; i < 6; i++)
            {
                string timeSlot = (i < ds.Tables[1].Rows.Count) ? ds.Tables[1].Rows[i]["Names"].ToString() : "";
                sb.AppendLine($"<td style='border:1px solid black;'>{timeSlot}</td>");
            }
            sb.AppendLine("</tr>");

            // Data Rows
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td style='border:1px solid black;'>{row["ClassDayName"]}</td>");
                sb.AppendLine($"<td style='border:1px solid black;'>{row["GroupName"]}</td>");

                for (int i = 0; i < 6; i++)
                {
                    string slotName = (i < ds.Tables[1].Rows.Count) ? ds.Tables[1].Rows[i]["Names"].ToString() : "";
                    string value = (!string.IsNullOrEmpty(slotName) && row.Table.Columns.Contains(slotName))
                                    ? row[slotName]?.ToString() ?? ""
                                    : "";
                    sb.AppendLine($"<td style='border:1px solid black;'>{value}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            // ===== Footer Section =====
            sb.AppendLine("<table style='width:100%; margin-top:20px; font-size:12px; font-family:Arial, Helvetica, sans-serif;'>");
            sb.AppendLine($"  <tr><td colspan='2'>Teacher Wise Count Class: {result}</td></tr>");
            sb.AppendLine("  <tr>");
            sb.AppendLine("    <td style='text-align:left; vertical-align:top;'>");
            sb.AppendLine("      <b>OIC TIME TABLE</b><br/><br/><br/>");
            sb.AppendLine("      <div style='margin-left:60px;'><b>COPY TO:</b></div>");
            sb.AppendLine("      <div style='margin-left:80px; display:block;'>");
            sb.AppendLine($"        1. HOD ({ds.Tables[0].Rows[0]["StreamName"]})<br/>");
            sb.AppendLine("        2. PA TO PRINCIPAL<br/>");
            sb.AppendLine("        3. Notice board<br/>");
            sb.AppendLine("        4. Student Section<br/>");
            sb.AppendLine("      </div>");
            sb.AppendLine("    </td>");
            sb.AppendLine("    <td style='text-align:right; vertical-align:top;'>PRINCIPAL</td>");
            sb.AppendLine("  </tr>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }






        //public static string BuildTimeTableHtml(System.Data.DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    // College Heading
        //    sb.AppendLine("<table style='width:100%; border-collapse:collapse; font-size:14px; font-family:Arial, Helvetica, sans-serif;' cellpadding='5' border='1'>");
        //    sb.AppendLine("  <tr><th colspan='2' style='border:1px solid black;'>GOVERNMENT POLYTECHNIC COLLEGE, JODHPUR</th></tr>");
        //    sb.AppendLine("  <tr><th colspan='2' style='border:1px solid black;'>TIME TABLE 2025-26</th></tr>");
        //    sb.AppendLine("  <tr><th style='border:1px solid black;'>Computer Science and Engineering</th><th style='border:1px solid black;'>W.E.F. 11/08/2025</th></tr>");
        //    sb.AppendLine("</table>");

        //    // Main TimeTable
        //    sb.AppendLine("<table style='width:100%; border-collapse:collapse; font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; text-align:center; border:1px solid black;' cellpadding='5'>");

        //    // Header Row
        //    sb.AppendLine("<tr style='background-color:#f0f0f0;'>");
        //    sb.AppendLine("<td style='border:1px solid black;'>Day</td>");
        //    sb.AppendLine("<td style='border:1px solid black;'>Semester</td>");
        //    sb.AppendLine("<td style='border:1px solid black;'>GroupName</td>");
        //    foreach (DataRow col in ds.Tables[1].Rows)
        //    {
        //        sb.AppendLine($"<td style='border:1px solid black;'>{col["Names"]}</td>");
        //    }
        //    sb.AppendLine("</tr>");

        //    // Data Rows
        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        sb.AppendLine("<tr>");
        //        sb.AppendLine($"<td style='border:1px solid black;'>{row["ClassDayName"]}</td>");
        //        sb.AppendLine($"<td style='border:1px solid black;'>{row["SemesterName"]}</td>");
        //        sb.AppendLine($"<td style='border:1px solid black;'>{row["GroupName"]}</td>");

        //        foreach (DataRow col in ds.Tables[1].Rows)
        //        {
        //            string slotName = col["Names"].ToString();
        //            string value = row[slotName]?.ToString();
        //            sb.AppendLine($"<td style='border:1px solid black;'>{value}</td>");
        //        }

        //        sb.AppendLine("</tr>");
        //    }

        //    sb.AppendLine("</table>");

        //    // Footer
        //    sb.AppendLine("<table style='width:100%; margin-top:20px; border-collapse:collapse;' border='1'>");
        //    sb.AppendLine("  <tr><th style='text-align:left; border:1px solid black;'>OIC TIME TABLE <br/> COPY TO: </th></tr>");
        //    sb.AppendLine("  <tr>");
        //    sb.AppendLine("    <td style='padding-left:30px; border:1px solid black;'>");
        //    sb.AppendLine("       1. HOD (CE,CS,EE,EL&EF,ME,PE,I YEAR)<br/>");
        //    sb.AppendLine("       2. PA TO PRINCIPAL<br/>");
        //    sb.AppendLine("       3. Notice board<br/>");
        //    sb.AppendLine("       4. Student Section");
        //    sb.AppendLine("    </td>");
        //    sb.AppendLine("    <td style='text-align:right; border:1px solid black;'>PRINCIPAL</td>");
        //    sb.AppendLine("  </tr>");
        //    sb.AppendLine("</table>");

        //    return sb.ToString();
        //}


        public static string BuildTimeTableHtmlOLD(System.Data.DataSet ds)
        {
            StringBuilder sb1 = new StringBuilder();

            sb1.AppendLine("<table style=\"width: 100%; border-collapse: collapse; font-size: 14px; font-family: Arial, Helvetica, sans-serif;\" cellpadding=\"5\">\n");
            sb1.AppendLine("     <tr>\n");
            sb1.AppendLine("         <th colspan=\"2\">GOVERNMENT POLYTECHNIC COLLEGE, JODHPUR</th>\n");
            sb1.AppendLine("     </tr>\n");
            sb1.AppendLine("     <tr>\n");
            sb1.AppendLine("         <th colspan=\"2\">TIME TABLE 2025-26</th>\n");
            sb1.AppendLine("     </tr>\n");
            sb1.AppendLine("     <tr>\n");
            sb1.AppendLine("         <th>Computer Science and Engineering </th>\n");
            sb1.AppendLine("         <th>W.E.F. 11/08/2025</th>\n");
            sb1.AppendLine("     </tr>\n");
            sb1.AppendLine(" </table>\n");




            sb1.AppendLine(" <table style=\"width: 100%; border-collapse: collapse; font-size: 12px; font-weight: bold; font-family: Arial, Helvetica, sans-serif; text-align: center; border-color: black;\" border=\"1\" cellpadding=\"5\">\n");
            sb1.AppendLine("     <tr>\n");
            sb1.AppendLine("         <td>PERIOD</td>\n");
            sb1.AppendLine("         <td rowspan=\"2\">SEM</td>\n");
            sb1.AppendLine("         <td rowspan=\"2\">GROUP</td>\n");







            int rowCounter = 1;
            foreach (DataRow row in ds.Tables[1].Rows)
            {

                sb1.AppendLine(string.Format( "<td colspan=\"3\">{0}</td>\n", rowCounter));

                rowCounter++;
            }









            //sb1.AppendLine("         <td colspan=\"3\">2</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">3</td>\n");
            //sb1.AppendLine("         <td style=\"transform: rotateZ(-89deg);\" rowspan=\"30\">LUNCH</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">4</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">5</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">6</td>\n");
            sb1.AppendLine("     </tr>\n");
            sb1.AppendLine("     <tr>\n");
            sb1.AppendLine("         <td>TIME</td>\n");

            foreach (DataRow row in ds.Tables[1].Rows)
            {

                sb1.AppendLine(string.Format("<td colspan=\"3\">{0}</td>\n", row["Names"].ToString()));
            }
            //sb1.AppendLine("         <td colspan=\"3\">11:15-12:15</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">12:15-01:15</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">01:45-02:45</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">02:45-03:45</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">03:45-04:45</td>\n");
            sb1.AppendLine("     </tr>\n");











            // Monday block
           sb1.AppendLine("<tr style=\"background-color: #FFFF99;\">\n");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sb1.AppendLine("<tr style=\"background-color: #FFFF99;\">\n");

                sb1.AppendLine(string.Format("<td >{0}</td>\n", row["ClassDayName"].ToString()));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", row["SemesterName"].ToString()));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", row["GroupName"].ToString()));
                sb1.AppendLine(string.Format("<td >{0}</td>\n", row["SubjectCode"].ToString()));
                sb1.AppendLine(string.Format("<td >{0}</td>\n", row["RoomNo"].ToString()));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", row["StaffName"].ToString()));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", ""));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", ""));
                sb1.AppendLine(string.Format("<td>{0}</td>\n",""));
                sb1.AppendLine(string.Format("<td>{0}</td>\n", ""));

            }

            sb1.AppendLine("     </tr>\n");







            //sb1.AppendLine("         <td rowspan=\"2\">IIIrd</td>\n");
            //sb1.AppendLine("         <td>CS1</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">3004</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">CC4</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">NK</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">3005</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">CC4</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">DJ</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">3002</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">CC4</td>\n");
            //sb1.AppendLine("         <td rowspan=\"2\">AV</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">SCA</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">CS1</td>\n");
            //sb1.AppendLine("         <td colspan=\"3\">RB</td>\n");




            // ... (continue appending the remaining rows exactly as in the provided HTML)

            sb1.AppendLine(" </table>\n");

            sb1.AppendLine(" <table style=\"width: 100%; margin-top: 20px;\">\n");
            sb1.AppendLine("     <thead>\n");
            sb1.AppendLine("         <tr>\n");
            sb1.AppendLine("             <th style=\"text-align: left;\">OIC TIME TABLE <br /> COPY TO: </th>\n");
            sb1.AppendLine("         </tr>\n");
            sb1.AppendLine("     </thead>\n");
            sb1.AppendLine("     <tbody>\n");
            sb1.AppendLine("         <tr>\n");
            sb1.AppendLine("             <td style=\"padding-left: 30px;\">\n");
            sb1.AppendLine("                 1. HOD (CE,CS,EE,EL&EF,ME,PE,I YEAR)<br />\n");
            sb1.AppendLine("                 2. PA TO PRINCIPAL<br />\n");
            sb1.AppendLine("                 3. Notice board 4. Student Section\n");
            sb1.AppendLine("             </td>\n");
            sb1.AppendLine("             <td style=\"text-align: right;\">PRINCIPAL</td>\n");
            sb1.AppendLine("         </tr>\n");
            sb1.AppendLine("     </tbody>\n");
            sb1.AppendLine(" </table>\n");

            // Return the built HTML
            return sb1.ToString();
        }

        //public static string BuildTimeTableHtml(System.Data.DataSet ds)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    // College Heading
        //    sb.AppendLine("<table style='width:100%; border-collapse:collapse; font-size:14px; font-family:Arial, Helvetica, sans-serif;' cellpadding='5'>");
        //    sb.AppendLine("  <tr><th colspan='2'>GOVERNMENT POLYTECHNIC COLLEGE, JODHPUR</th></tr>");
        //    sb.AppendLine("  <tr><th colspan='2'>TIME TABLE 2025-26</th></tr>");
        //    sb.AppendLine("  <tr><th>Computer Science and Engineering</th><th>W.E.F. 11/08/2025</th></tr>");
        //    sb.AppendLine("</table>");

        //    // Main TimeTable
        //    sb.AppendLine("<table style='width:100%; border-collapse:collapse; font-size:12px; font-weight:bold; font-family:Arial, Helvetica, sans-serif; text-align:center; border:1px solid black;' cellpadding='5'>");

        //    // Header Row
        //    sb.AppendLine("<tr style='background-color:#f0f0f0;'>");
        //    sb.AppendLine("<td>Day</td>");
        //    sb.AppendLine("<td>Semester</td>");
        //    sb.AppendLine("<td>GroupName</td>");
        //    foreach (DataRow col in ds.Tables[1].Rows)
        //    {
        //        sb.AppendLine($"<td>{col["Names"]}</td>");
        //    }
        //    sb.AppendLine("</tr>");

        //    // Data Rows
        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        sb.AppendLine("<tr>");
        //        sb.AppendLine($"<td>{row["ClassDayName"]}</td>");
        //        sb.AppendLine($"<td>{row["SemesterName"]}</td>");
        //        sb.AppendLine($"<td>{row["GroupName"]}</td>");
        //        sb.AppendLine($"<td>{row["SubjectCode"]}</td>");
        //        sb.AppendLine($"<td>{row["RoomNo"]}</td>");
        //        sb.AppendLine($"<td>{row["StaffName"]}</td>");
        //        sb.AppendLine("</tr>");
        //    }

        //    sb.AppendLine("</table>");

        //    // Footer
        //    sb.AppendLine("<table style='width:100%; margin-top:20px;'>");
        //    sb.AppendLine("  <tr><th style='text-align:left;'>OIC TIME TABLE <br/> COPY TO: </th></tr>");
        //    sb.AppendLine("  <tr>");
        //    sb.AppendLine("    <td style='padding-left:30px;'>");
        //    sb.AppendLine("       1. HOD (CE,CS,EE,EL&EF,ME,PE,I YEAR)<br/>");
        //    sb.AppendLine("       2. PA TO PRINCIPAL<br/>");
        //    sb.AppendLine("       3. Notice board<br/>");
        //    sb.AppendLine("       4. Student Section");
        //    sb.AppendLine("    </td>");
        //    sb.AppendLine("    <td style='text-align:right;'>PRINCIPAL</td>");
        //    sb.AppendLine("  </tr>");
        //    sb.AppendLine("</table>");

        //    return sb.ToString();
        //}


        [HttpPost("GetReAssignTeacher")]
        public async Task<ApiResult<DataTable>> GetReAssignTeacher([FromBody] ReAssignTeacherDataModel request)
        {
            ActionName = "GetReAssignTeacher()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetReAssignTeacher(request);
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

        [HttpPost("ReAssignTeacherForSaveLC")]
        public async Task<ApiResult<int>> ReAssignTeacherForSaveLC([FromBody] ReAssignTeacherSaveModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.ReAssignTeacherForSaveLC(model);
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



        [HttpPost("GetStudentAttendanceTLC")]
        public async Task<ApiResult<DataTable>> GetStudentAttendanceTLC([FromBody] AttendanceTimeTableTLCModal request)
        {
            ActionName = "GetStudentAttendanceTLC()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetStudentAttendanceTLC(request);
                    
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


        [HttpPost("SaveStudentAttendanceTLC")]
        public async Task<ApiResult<int>> SaveStudentAttendanceTLC([FromBody] List<PostAttendanceTimeTableTLCModal> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.SaveStudentAttendanceTLC(model);
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

        [HttpPost("GetReAttendanceTimeTable")]
        public async Task<ApiResult<DataTable>> GetReAttendanceTimeTable([FromBody] AttendanceTimeTableModal request)
        {
            ActionName = "GetReAttendanceTimeTable()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.StudentRepository.GetReAttendanceTimeTable(request);
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

    }


}
