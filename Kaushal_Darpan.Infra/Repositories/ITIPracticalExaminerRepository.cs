using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.Student;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ITIPracticalExaminerRepository : IITIPracticalExaminerRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIPracticalExaminerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CenterAllocationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetPracticalExamCenter(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetPracticalExamCenter()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetPracticalExamCenter";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", '');
                        command.Parameters.AddWithValue("@StudentExamID ", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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

        public async Task<DataTable> GetPracticalExamCenter_Report(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetPracticalExamCenter()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetPracticalExamCenter_Report";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", '');
                        command.Parameters.AddWithValue("@StudentExamID ", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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


        public async Task<DataTable> GetCenterPracticalexaminer(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetPracticalExamCenter()";
            return await Task.Run(async () =>
            {

                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (filterModel.Eng_NonEng==1)
                        {
                            command.CommandText = "USP_ITI_NcvtGetCenterWisePracticalExaminer";
                        }
                        else
                        {
                            command.CommandText = "USP_ITI_GetCenterWisePracticalExaminer";
                        }
                    
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", '');
                        command.Parameters.AddWithValue("@StudentExamID ", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@Userid", filterModel.UserID);
                        command.Parameters.AddWithValue("@DistrictID", filterModel.DistrictID);
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


        public async Task<DataTable> GetCenterPracticalexaminerReliving(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetPracticalExamCenter()";
            return await Task.Run(async () =>
            {

                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetPracticalExaminerReliving";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", '');
                        command.Parameters.AddWithValue("@StudentExamID ", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@ExaminerName", filterModel.ExaminerName);
                        command.Parameters.AddWithValue("@ExaminerSSOID", filterModel.ExaminerSSOID);
                        command.Parameters.AddWithValue("@CenterName", filterModel.CenterName);

                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
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




        public async Task<DataTable> GetParcticalStudentCenterWise(ITIPracticalExaminerSearchFilter filterModel)
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
                        command.CommandText = "USP_ITI_GetParcticalStudentCenterWise";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@StudentExamID", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
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


        public async Task<DataTable> ParcticalExaminerDashboard(ITIPracticalExaminerSearchFilter filterModel)
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
                        command.CommandText = "USP_ITI_ParcticalExaminerDashboard";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
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

        public async Task<int> AssignPracticalExaminer(PracticalExaminerDetailsModel request)
        {
            _actionName = "UpdateCCCode(CenterCreationAddEditModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiPracticalExaminerDetails";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@CenterAssignedID", request.CenterAssignedID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@TimeTableID",request.TimeTableID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);

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


        public async Task<DataTable> Getstaffpractical(ItiPracticalExaminerDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiPracticalExaminer";
                        command.Parameters.AddWithValue("@Action", "GetUser_InstituteWise");
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@TimeTable", body.TimeTable);

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
        public async Task<DataSet> DownloadItiPracticalExaminer(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetPracticalExamCenter";
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", '');
                        command.Parameters.AddWithValue("@StudentExamID ", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    return ds;
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


        public async Task<DataTable> GetUndertakingExaminerDetailsByIdAsync(int id)
        {
            _actionName = "GetUndertakingExaminerDetailsByIdAsync(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterExaminerUndertakingDetails";
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Action", "GetPraticalExaminerDetailbyID");

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


        public async Task<DataTable> GetStudentExamReportAsync(ITIPracticalExaminerSearchFilter filterModel , string subjectCode)
        {
            _actionName = "GetStudentExamReportAsync()";

            return await Task.Run(async () =>
            {           
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Get_StudentExam_Report_With_Examiner_Timetable";

                        // Add required parameters
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@SubjectCode", subjectCode);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
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

        public async Task<DataTable> GetStudentExamReportGetStudentExamReportForITIAsync(ITIExaminerDataModel filterModel)
        {
            _actionName = "GetStudentExamReportGetStudentExamReportForITIAsync()";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Get_StudentExam_Report_With_Examiner_Timetable";

                        // Add required parameters
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@SubjectCode", "");
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
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


        public async Task<DataTable> GetAssignedCentersAndTimetableAsync(PracticalExaminerDetailsModel model)
        {
            _actionName = "GetAssignedCentersAndTimetableAsync";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_AssignedCentersAndTimeTable_ForExaminer";
                            
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery(); // Optional: for logging

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


        public async Task<int> UpdateStudentExamMarks(StudentExamMarksUpdateModel model)
        {
            _actionName = "UpdateStudentExamMarks(StudentExamMarksUpdateModel model)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Update_StudentExam_Marks";

                        command.Parameters.AddWithValue("@StudentExamPaperMarksID", model.StudentExamPaperMarksID);
                        command.Parameters.AddWithValue("@ObtainedMarks", model.ObtainedMarks);
                        command.Parameters.AddWithValue("@IsPresent", model.IsPresent);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@Latitude", model.Latitude);
                        command.Parameters.AddWithValue("@Longitude", model.Longitude);
                        command.Parameters.AddWithValue("@FileName", model.FileName);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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


        public async Task<int> UpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList)
        {
            _actionName = "UpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Update_StudentExam_Marks_Json";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entityList));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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


        public async Task<int> NcvtUpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList)
        {
            _actionName = "UpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                        
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_NcvtUpdate_StudentExam_Marks_Json";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entityList));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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




        public async Task<DataTable> GetPracticalExaminerRelivingByUserId(ITIPracticalExaminerSearchFilter filterModel)
        {
            _actionName = "GetPracticalExaminerRelivingByUserId()";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetPracticalExaminerReliving_ByUserID";

                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", filterModel.FinancialYearID);
                        //command.Parameters.AddWithValue("@Action", filterModel.Action ?? string.Empty);
                        command.Parameters.AddWithValue("@StudentExamID", filterModel.StudentExamID);
                        command.Parameters.AddWithValue("@ExamType", filterModel.ExamType);
                        command.Parameters.AddWithValue("@SemesterID", filterModel.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@ExaminerName", filterModel.ExaminerName ?? string.Empty);
                        command.Parameters.AddWithValue("@ExaminerSSOID", filterModel.ExaminerSSOID ?? string.Empty);
                        command.Parameters.AddWithValue("@CenterName", filterModel.CenterName ?? string.Empty);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode ?? string.Empty);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);

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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<DataTable> GetItiRemunerationExaminerDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_Get_ITIPracticalRemuneration";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@EndTermID", body.RoleID);
                        //command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        //command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@action", "RemunerationPracticalDetails");

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

        public async Task<DataTable> Iti_RemunerationGenerateAndViewPdf(ITI_AppointExaminerDetailsModel filterModel)
        {
            _actionName = "GetDataForGeneratePdf(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[USP_Get_ITIPracticalRemuneration]";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getDetailsOfPracticalPDF");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@ExaminerID", filterModel.ExaminerID);
                        command.Parameters.AddWithValue("@GroupCodeID", filterModel.Status);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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



        public async Task<int> SaveDataSubmitAndForwardToAdmin(ITI_AppointExaminerDetailsModel request)
        {
            _actionName = "SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Get_ITIPracticalRemuneration";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_saveSubmitAndForwardToAdmin");
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        command.Parameters.AddWithValue("@filename", request.filename);
                        //command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        //command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

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

        public async Task<DataTable> GetItiRemunerationAdminDetails(ITI_AppointExaminerDetailsModel body)
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
                        command.CommandText = "USP_Get_ITIPracticalRemuneration";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        //command.Parameters.AddWithValue("@EndTermID", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@EndTermID", body.RoleID);
                        //command.Parameters.AddWithValue("@ExaminerID", body.ExaminerID);
                        //command.Parameters.AddWithValue("@Status", body.Status);

                        command.Parameters.AddWithValue("@action", "AdminRemunerationPracticalDetails");

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


        public async Task<int> UpdateToApprove(ITI_AppointExaminerDetailsModel request)
        {
            _actionName = "SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Get_ITIPracticalRemuneration";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "UpdateToApprove");
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);

                        //command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        //command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

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


    }
}
