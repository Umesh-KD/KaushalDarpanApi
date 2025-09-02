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

        [HttpPost("UpdateStudentSsoMapping")]
        public async Task<ApiResult<int>> UpdateStudentSsoMapping([FromBody] StudentSearchModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try { 

                    var data = await _unitOfWork.StudentRepository.UpdateStudentSsoMapping(model);
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


        [HttpPost("StudentPlacementMapping")]
        public async Task<ApiResult<int>> StudentPlacementMapping([FromBody] StudentSearchModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    var data = await _unitOfWork.StudentRepository.StudentPlacementMapping(model);
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
                    _unitOfWork.SaveChanges();
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

        [HttpPost("AddStudentAttendance")]
        public async Task<ApiResult<int>> AddStudentAttendance([FromBody] List<PostAttendanceTimeTableModal> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.AddStudentAttendance(model);
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
        
        [HttpPost("PostAttendanceTimeTable")]
        public async Task<ApiResult<int>> PostAttendanceTimeTable([FromBody] PostAttendanceTimeTable model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.PostAttendanceTimeTable(model);
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


        [HttpPost("ITI_AddStudentAttendance")]
        public async Task<ApiResult<int>> ITI_AddStudentAttendance([FromBody] List<PostAttendanceTimeTableModal> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.ITIAddStudentAttendance(model);
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


        [HttpPost("PostAttendanceTimeTableList")]
        public async Task<ApiResult<int>> PostAttendanceTimeTableList([FromBody] List<PostAttendanceTimeTable> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.PostAttendanceTimeTableList(model);
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


        [HttpPost("SetCalendarEventModel")]
        public async Task<ApiResult<int>> SetCalendarEventModel([FromBody] List<CalendarEventModel> model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var data = await _unitOfWork.StudentRepository.SetCalendarEventModel(model);
                    _unitOfWork.SaveChanges();
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
