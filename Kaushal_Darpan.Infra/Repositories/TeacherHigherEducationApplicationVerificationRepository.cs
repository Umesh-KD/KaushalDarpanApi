using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.Test;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class TeacherHigherEducationApplicationVerificationRepository : ITeacherHigherEducationApplicationVerificationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public TeacherHigherEducationApplicationVerificationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TeacherHigherEducationApplicationVerificationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        #region teacher-higher-education-application-Verification
        public async Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)
        {
            _actionName = "GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEnrolledStudent_Promoted";

                        command.Parameters.AddWithValue("@action", "_getEnrolledStudent_Promoted");
                        command.Parameters.AddWithValue("@StudentName", model.StudentName);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);

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

        public async Task<int> SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model)
        {
            _actionName = "SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_saveEnrolledStudent_ReturnbyExamIncharge";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_saveEnrolledStudent_ReturnbyExamIncharge");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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
        #endregion

        public async Task<DataTable> ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)
        {
            _actionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationList_ForPrinciple";

                        command.Parameters.AddWithValue("@action", "GetApplicationList");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@status", model.status);

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

        public async Task<int> UpdateApplicationStatus_Principle_THTE(List<UpdateApplicationStatusDataModel_Principle> model)
        {
            _actionName = "UpdateApplicationStatus_Principle_THTE(List<UpdateApplicationStatusDataModel_Principle> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_THTE_UpdateApplicationStatus_Principle";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "UpdateStatus");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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
        #endregion


        public async Task<DataTable> ApplicationList_ForDTE_THTE(PrincipleApplicationListSearchModel model)
        {
            _actionName = "ApplicationList_ForPrinciple_THTE(PrincipleApplicationListSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationList_ForDte";

                        command.Parameters.AddWithValue("@action", "GetApplicationList");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@status", model.status);

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


        public async Task<int> UpdateApplicationStatus_DTE_THTE(List<UpdateApplicationStatusDataModel_Principle> model)
        {
            _actionName = "UpdateApplicationStatus_DTE_THTE(List<UpdateApplicationStatusDataModel_Principle> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_THTE_UpdateApplicationStatus_Dte";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "UpdateStatus");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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

        public async Task<DataTable> ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)
        {
            _actionName = "ApplicationList_ForCommittee_THTE(PrincipleApplicationListSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_THTE_ApplicationList_ForCommittee";

                        command.Parameters.AddWithValue("@action", "GetApplicationList_committee");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);
                        command.Parameters.AddWithValue("@status", model.status);

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

        public async Task<int> UpdateApplicationStatus_Committee_THTE(UpdateApplicationStatusDataModel_Committee model)
        {
            _actionName = "public async Task<int> UpdateApplicationStatus_Committee_THTE(UpdateApplicationStatusDataModel_Committee model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_THTE_UpdateApplicationStatus_Committee";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "UpdateStatus");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model.ApplicationListData));
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@status", model.status);
                        command.Parameters.AddWithValue("@Remark", model.Remark);
                        command.Parameters.AddWithValue("@CommitteeDocs", model.CommitteeDocs);
                        command.Parameters.AddWithValue("@Dis_CommitteeDocs", model.Dis_CommitteeDocs);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    }
                    return retval;
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


