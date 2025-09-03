using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BterStudentJoinStatus;
using Kaushal_Darpan.Models.Attendance;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using Kaushal_Darpan.Models.StudentsJoiningStatusMarks;
using Newtonsoft.Json;
using System.Data;
using System.Windows.Input;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using Kaushal_Darpan.Models.ITIStudentMeritInfo;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;
using System.Globalization;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetStudentDashboard(StudentSearchModel filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_StudentDashboard";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@RoleId", filterModel.RoleId );
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID );
                        command.Parameters.AddWithValue("@SsoID", filterModel.SsoID ?? string.Empty);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<List<StudentDetailsModel>> GetAllData(StudentSearchModel searchModel)
        {
            _actionName = "GetAllData(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                    {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@RoleId", searchModel.RoleId);
                        command.Parameters.AddWithValue("@StudentID", searchModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", searchModel.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", searchModel.StreamID);
                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@action", searchModel.Action);
                        command.Parameters.AddWithValue("@SsoID", searchModel.SsoID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<StudentDetailsModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<StudentDetailsModel>>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<List<StudentDetailsModel>> ITIGetAllData(StudentSearchModel searchModel)
        {
            _actionName = "USP_ITI_StudentPendingFees(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudentPendingFees";
   
                        command.Parameters.AddWithValue("@StudentID", searchModel.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@action", searchModel.Action);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<StudentDetailsModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<StudentDetailsModel>>(dataTable);
                    }
                    return data;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetStudentDeatilsByAction(StudentSearchModel filterModel)
        {
            _actionName = "GetStudentDeatilsByAction()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@RoleId", filterModel.RoleId);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@SsoID", filterModel.SsoID ?? string.Empty);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetITIStudentDeatilsByAction(StudentSearchModel filterModel)
        {
            _actionName = "GetITIStudentDeatilsByAction()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetStudentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@RoleId", filterModel.RoleId);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@SsoID", filterModel.SsoID ?? string.Empty);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> UpdateStudentSsoMapping(StudentSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateStudentSsoMapping(StudentDetailsModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentSsoMapping";
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@SSOID", request.SsoID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<int> StudentPlacementMapping(StudentSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateStudentSsoMapping(StudentDetailsModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentPlacementMapping";
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@SSOID", request.SsoID);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetStudentDeatilsBySSOId(String ssoid,int DepartmentID=0)
        {
            _actionName = "GetStudentDeatilsBySSOId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetailsBySSO";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Action", "GetStudentDetailsBySSO");
                        command.Parameters.AddWithValue("@SsoID", ssoid);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<DataTable> GetProfileDashboard(StudentSearchModel filterModel)
        {
            _actionName = "GetProfileDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDetails";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Action", "GetProfileDashboard");
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@SsoID", filterModel.SsoID);
                        command.Parameters.AddWithValue("@StudentID", filterModel.StudentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetDataStudentBySSOId(String ssoid, int DepartmentID = 0)
        {
            _actionName = "GetDataStudentBySSOId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentApplicationBySSOID";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Action", "GetStudentApplicationBySSOID");
                        command.Parameters.AddWithValue("@SsoID", ssoid);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> AddStudentData(VerifierDataModel request)
        {
            _actionName = "AddStudentData(VerifierDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddStudent_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@VerifierID", request.VerifierID);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNumber);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAttendanceTimeTable(AttendanceTimeTableModal model)
        {
            _actionName = "GetDataStudentBySSOId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_AssignTeacherForSubject";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetStudentAttendance(AttendanceTimeTableModal model)
        {
            _actionName = "GetStudentAttendance()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_Get_StudentAttandance";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SectionID", model.SectionID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@ShiftNo", model.ShiftID);
                        command.Parameters.AddWithValue("@UnitNo", model.UnitID);
                        command.Parameters.AddWithValue("@AttendanceStartDate", model.AttendanceStartDate?.ToString("yyyy-MM-dd", new CultureInfo("en-GB"))); 
                        command.Parameters.AddWithValue("@AttendanceEndDate", model.AttendanceEndDate?.ToString("yyyy-MM-dd", new CultureInfo("en-GB")));
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<DataTable> GetHolidaysmaster(DateTime? start, DateTime? end)
        {
            _actionName = "GetHolidaysmaster()";
            DataTable dataTable = new DataTable();

            try
            {
                // Check if the start and end dates are valid
                if (!start.HasValue || !end.HasValue)
                {
                    throw new ArgumentException("Start and End dates must be provided.");
                }

                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Get_Holidaysmaster";

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@AttendanceStartDate", start.Value);
                    command.Parameters.AddWithValue("@AttendanceEndDate", end.Value);

              

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                var errorDesc = new ErrorDescription
                {
                    Message = ex.Message,
                    PageName = _pageName,
                    ActionName = _actionName,
                    SqlExecutableQuery = _sqlQuery
                };
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }


        public async Task<int> AddStudentAttendance(List<PostAttendanceTimeTableModal> model)
        {
            _actionName = "AddStudentAttendance()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEdit_StudentAttandance";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
          
        }
        public async Task<int> PostAttendanceTimeTable(PostAttendanceTimeTable model)
        {
            _actionName = "PostAttendanceTimeTable()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        if (model.DepartmentID==2)
                        {
                            command.CommandText = "USP_AddEdit_AssignTeacherForSubjectITI";
                        }
                        else
                        {
                            command.CommandText = "USP_AddEdit_AssignTeacherForSubject";
                        }
                 
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
          
        }

        public async Task<StudentMeritInfoModel> GetStudentMeritinfo(StudentSearchModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentMeritInfo";

                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@DOB", body.DOB);

                        command.Parameters.AddWithValue("@Action", body.Action);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new StudentMeritInfoModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<StudentMeritInfoModel>(dataSet.Tables[0]);
                            if (data != null)
                            {
                                if (dataSet.Tables[1].Rows.Count > 0)
                                {

                                    data.QualificationViewDetails = CommonFuncationHelper.ConvertDataTable<List<QualificationViewDetails>>(dataSet.Tables[1]);
                                }

                                if (dataSet.Tables[2].Rows.Count > 0)
                                {
                                    try
                                    {

                                        data.RecheckDocumentModel = CommonFuncationHelper.ConvertDataTable<List<RecheckDocumentModel>>(dataSet.Tables[2]);

                                    }
                                    catch (Exception ex) { }
                                }
                            }

                        }
                    }
                    return data;
                });
            }
            catch (Exception ex)
            {
                var errorDesc = new ErrorDescription
                {
                    Message = ex.Message,
                    PageName = _pageName,
                    ActionName = _actionName,
                    SqlExecutableQuery = _sqlQuery
                };
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }

        public async Task<int> SaveRecheckData(List<RecheckDocumentModel> entity)
        {
            _actionName = "SaveRevertData(List<VerificationDocumentDetailList> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Save_ReCheckMeritData";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling


                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);// out
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataSet> GetITIStudentMeritinfo(StudentSearchModel body)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIStudentMeritInfo";

                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentId", body.DepartmentID);
                        command.Parameters.AddWithValue("@DOB", body.DOB);

                        command.Parameters.AddWithValue("@Action", body.Action);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
                    }
                  
                    return dataTable;
                });
            }
            catch (Exception ex)
            {
                var errorDesc = new ErrorDescription
                {
                    Message = ex.Message,
                    PageName = _pageName,
                    ActionName = _actionName,
                    SqlExecutableQuery = _sqlQuery
                };
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }

        public async Task<DataTable> GetStudentApplication(StudentSearchModel body)
        {
            _actionName = "GetStudentApplication(iStudentSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentApplication";

                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@MobileNo", body.MobileNumber);
      
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                });
            }
            catch (Exception ex)
            {
                var errorDesc = new ErrorDescription
                {
                    Message = ex.Message,
                    PageName = _pageName,
                    ActionName = _actionName,
                    SqlExecutableQuery = _sqlQuery
                };
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }



        public async Task<DataTable> GetReverApplication(StudentSearchModel body)
        {
            _actionName = "GetStudentApplication(iStudentSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetRevertApplication";

                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@MobileNo", body.MobileNumber);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }

                    return dataTable;
                });
            }
            catch (Exception ex)
            {
                var errorDesc = new ErrorDescription
                {
                    Message = ex.Message,
                    PageName = _pageName,
                    ActionName = _actionName,
                    SqlExecutableQuery = _sqlQuery
                };
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }

        public async Task<int> ITIAddStudentAttendance(List<PostAttendanceTimeTableModal> model)
        {
            _actionName = "AddStudentAttendance()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_AddEdit_StudentAttandance";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });

        }

        public async Task<DataTable> ITIGetAttendanceTimeTable(AttendanceTimeTableModal model)
        {
            _actionName = "ITIGetDataStudentBySSOId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AssignTeacherForSubject";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<int> PostAttendanceTimeTableList(List<PostAttendanceTimeTable> model)
        {
            _actionName = "PostAttendanceTimeTable()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                       
                        command.CommandText = "USP_Bter_AddEdit_AssignTeacherForSubject";
                       
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();


                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });

        }


        public async Task<int> SetCalendarEventModel(List<CalendarEventModel> model)
        {
            _actionName = "SetCalendarEventModel(List<CalendarEventModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandText = "USP_GetEvents";

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Action", "GetEvents_IU");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });

        }

        public async Task<DataTable> getCalendarEventModel(CalendarEventModel model)
        {
            _actionName = "ITIGetDataStudentBySSOId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEventsList";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@Action", "View");
                        //command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        //command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        //command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        //command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        //command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        //command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
    }
}








