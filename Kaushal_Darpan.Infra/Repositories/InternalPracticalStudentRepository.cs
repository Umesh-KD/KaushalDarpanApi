using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TheoryMarks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class InternalPracticalStudentRepository : IInternalPracticalStudentRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public InternalPracticalStudentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "GroupMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(TheorySearchModel body)
        {
            _actionName = "GetAllData(TheorySearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (body.InternalPracticalID == 1)
                        {
                            command.CommandText = "USP_GetInternalPracticalStudent";
                        }
                        else if (body.InternalPracticalID == 2)
                        {
                            command.CommandText = "USP_GetInternalAssismentStudent";
                        }
                        else
                        {
                            throw new Exception(Constants.MSG_INVALID_REQUEST);
                        }

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                        command.Parameters.AddWithValue("@InternalPracticalID", body.InternalPracticalID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<int> UpdateSaveData(List<TheoryMarksModel> entity, int InternalPracticalID)
        {
            _actionName = "UpdateSaveData(List<TheoryMarksModel> entity, int InternalPracticalID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;

                        if (InternalPracticalID == 1)
                        {
                            command.CommandText = "USP_SaveInternalPracticalStudent";
                        }
                        else if (InternalPracticalID == 2)
                        {
                            command.CommandText = "USP_SaveInternalAssismentStudent";
                        }
                        else
                        {
                            throw new Exception(Constants.MSG_INVALID_REQUEST);
                        }

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.AddWithValue("@InternalPracticalID",InternalPracticalID);
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

        public async Task<DataTable> GetAllDataInternal_Admin(TheorySearchModel body)
        {
            _actionName = "GetAllData(TheorySearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (body.InternalPracticalID == 1)
                        {
                            command.CommandText = "USP_GetInternalPracticalStudent_Admin";
                        }
                        else if (body.InternalPracticalID == 2)
                        {
                            command.CommandText = "USP_GetInternalAssismentStudent_Admin";
                        }
                        else
                        {
                            throw new Exception(Constants.MSG_INVALID_REQUEST);
                        }

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                        command.Parameters.AddWithValue("@InternalPracticalID", body.InternalPracticalID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);

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

        public async Task<int> UpdateSaveDataInternal_Admin(List<TheoryMarksModel> entity, int InternalPracticalID)
        {
            _actionName = "UpdateSaveData(List<TheoryMarksModel> entity, int InternalPracticalID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;

                        if (InternalPracticalID == 1)
                        {
                            command.CommandText = "USP_SaveInternalPracticalStudent_Admin";
                        }
                        else if (InternalPracticalID == 2)
                        {
                            command.CommandText = "USP_SaveInternalAssismentStudent_Admin";
                        }
                        else
                        {
                            throw new Exception(Constants.MSG_INVALID_REQUEST);
                        }

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.AddWithValue("@InternalPracticalID", InternalPracticalID);
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

       



    }
}
