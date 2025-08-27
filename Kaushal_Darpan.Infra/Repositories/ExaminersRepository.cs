using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ExaminersRepository: IExaminersRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ExaminersRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ExaminersRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetTeacherForExaminer(TeacherForExaminerSearchModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Examiner_GetTeacherForExaminer";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", body.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@CommonSubjectID", body.CommonSubjectID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@IsYearly", body.IsYearly);
                        command.Parameters.AddWithValue("@IsReval", body.IsReval);


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

        public async Task<int> SaveExaminerData(ExaminerMaster request)
        {
            _actionName = "SaveExaminerData(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Examiner_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        //command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        //command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                    
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@ExamID", request.ExamID);
                        command.Parameters.AddWithValue("@GroupID", request.GroupID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExaminerCode", request.ExaminerCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsAppointed", request.IsAppointed);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<DataTable> GetExaminerData(TeacherForExaminerSearchModel body)
        {
            _actionName = "GetExaminerData(TeacherForExaminerSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Examiner_GetExaminerData";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                        //command.Parameters.AddWithValue("@ExamID", body.ExamID);
                        //command.Parameters.AddWithValue("@Name", body.Name);
                        //command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", body.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@CommonSubjectID", body.CommonSubjectID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@IsYearly", body.IsYearly);

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

        public async Task<bool> DeleteDataByID(ExaminerMaster request)
        {
            _actionName = "DeleteDataByID(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_ExaminerMaster set ActiveStatus=0, DeleteStatus=1, IsAppointed=0,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
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

        public async Task<DataTable> GetExaminerByCode(ExaminerCodeLoginModel model)
        {
            _actionName = "GetExaminerByCode(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetExaminerMasterData";

                        command.Parameters.AddWithValue("@action", "_getExaminerByCode");
                        command.Parameters.AddWithValue("@ExaminerID", model.ExaminerID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@ExaminerCode", model.ExaminerCode);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@GroupCodeID", model.GroupCodeID);

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


        public async Task<ExaminerMaster> GetById(int PK_ID, int StaffSubjectID, int DepartmentID,int EndTermID,int CourseTypeID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_Getexaminer_Edit";
                        command.Parameters.AddWithValue("@StaffID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@StaffSubjectID", StaffSubjectID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ExaminerMaster();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ExaminerMaster>(dataTable);
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




        public async Task<DataTable> ExaminerInchargeDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "ExaminerInchargeDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExaminerInchargeDashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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



        public async Task<DataTable> RegistrarDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "RegistrarDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RegistrarDashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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


        public async Task<DataTable> ITSupportDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "ITSupportDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITSupportDashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<DataTable> SectionInchargeDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "SectionInchargeDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SectionIncharge_Dashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<int> GetExaminerGroupTotalAsync(string examinerCode)
        {
            _actionName = "GetExaminerGroupTotalAsync(string examinerCode)";

            try
            {
                using var command = _dbContext.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GetExaminerGroupTotal";
                command.Parameters.AddWithValue("@ExaminerCode", examinerCode);

                _sqlQuery = command.GetSqlExecutableQuery();

                var dt = await command.FillAsync_DataTable();

                if (dt.Rows.Count > 0)
                {
                    var cell = dt.Rows[0][0]?.ToString();
                    if (int.TryParse(cell, out var totalInt))
                        return totalInt;
                    if (decimal.TryParse(cell, out var totalDec))
                        return (int)Math.Round(totalDec);
                }

                return 0;
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
                var err = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(err, ex);
            }
        }

        public async Task<DataTable> ACPDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "ACPDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ACPDashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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

        public async Task<DataTable> StoreKeeperDashboard(ExaminerDashboardSearchModel model)
        {
            _actionName = "StoreKeeperDashboard(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StoreKeeperDashboard";
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@UserID", model.UserID);

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

        public async Task<DataTable> GetRevalTeacherForExaminer(TeacherForExaminerSearchModel body)
        {
            _actionName = "GetRevalTeacherForExaminer(TeacherForExaminerSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetReval_Examiner_GetTeacherForExaminer";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", body.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@CommonSubjectID", body.CommonSubjectID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@IsYearly", body.IsYearly);

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

        //-----------------------------------------------------------------------REVAL---------------------------------------------------------------------------------------------

        public async Task<ExaminerMaster> Getexaminer_byID_Reval(int PK_ID, int StaffSubjectID, int DepartmentID, int EndTermID, int CourseTypeID)
        {
            _actionName = "Getexaminer_byID_Reval(int PK_ID, int StaffSubjectID, int DepartmentID, int EndTermID, int CourseTypeID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_Getexaminer_byID_Reval";
                        command.Parameters.AddWithValue("@StaffID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@StaffSubjectID", StaffSubjectID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ExaminerMaster();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ExaminerMaster>(dataTable);
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

        public async Task<int> SaveExaminerData_Reval(ExaminerMaster request)
        {
            _actionName = "SaveExaminerData_Reval(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Examiner_IU_Reval";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        //command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        //command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);

                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@ExamID", request.ExamID);
                        command.Parameters.AddWithValue("@GroupID", request.GroupID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ExaminerCode", request.ExaminerCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsAppointed", request.IsAppointed);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<DataTable> GetExaminerData_Reval(TeacherForExaminerSearchModel body)
        {
            _actionName = "GetExaminerData_Reval(TeacherForExaminerSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Examiner_GetData_Reval";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                        //command.Parameters.AddWithValue("@ExamID", body.ExamID);
                        //command.Parameters.AddWithValue("@Name", body.Name);
                        //command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CommonSubjectYesNo", body.CommonSubjectYesNo);
                        command.Parameters.AddWithValue("@CommonSubjectID", body.CommonSubjectID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@IsYearly", body.IsYearly);

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

        public async Task<bool> DeleteByID_Reval(ExaminerMaster request)
        {
            _actionName = "DeleteByID_Reval(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_RevalExaminerMaster set ActiveStatus=0, DeleteStatus=1, IsAppointed=0,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
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

        public async Task<DataTable> GetExaminerByCode_Reval(ExaminerCodeLoginModel model)
        {
            _actionName = "GetExaminerByCode_Reval(ExaminerCodeLoginModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetExaminerMasterData";

                        command.Parameters.AddWithValue("@action", "_getExaminerByCode_Reval");
                        command.Parameters.AddWithValue("@ExaminerID", model.ExaminerID);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@ExaminerCode", model.ExaminerCode);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@GroupCodeID", model.GroupCodeID);

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
    }
}
