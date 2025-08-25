using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.SetPaper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class SetPaperRepository : ISetPaperRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public SetPaperRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "SetPaperRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetSetPaper(GetSetPaperModal model)
        {
            _actionName = "GetSetPaper()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetSetPaper";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);

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

        public async Task<int> PostSetPaper(PostSetPaperModal model)
        {
            _actionName = "PostSetPaper()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditDelete_SetPaper";
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
        
        public async Task<int> PostAddQuestion(PostAddQuestionModal model)
        {
            _actionName = "PostSetPaper()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ManagePaperData";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OperationType", model.OperationType);
                        command.Parameters.AddWithValue("@QuestionText", model.QuestionText);
                        command.Parameters.AddWithValue("@QuestionId", model.QuestionId);
                        command.Parameters.AddWithValue("@PaperID", model.PaperID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@AnswerOptions", model.AnswerOptions);

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

        public async Task<DataTable> GetByIdQuestion(GetQuestionModal model)
        {
            _actionName = "GetAllQuestion()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetByIdQuestion";
                        command.Parameters.AddWithValue("@QuestionId", model.QuestionId);

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
        public async Task<DataTable> GetAllQuestion(GetQuestionModal model)
        {
            _actionName = "GetAllQuestion()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllQuestionPaperData";
                        command.Parameters.AddWithValue("@QuestionId", model.QuestionId);
                        command.Parameters.AddWithValue("@QuestionText", model.QuestionText);
                        command.Parameters.AddWithValue("@PaperID", model.PaperID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);

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

        public async Task<int> PostAddExamPaperAssignStaff(PostAddPaperAssignStaffModal model)
        {
            _actionName = "PostAddExamPaperAssignStaff()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ExamPaperAssignStaff";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OperationType", model.OperationType);
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@PaperID", model.PaperID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@QuestionLimit", model.QuestionLimit);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);

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

        public async Task<DataTable> GetByIdExamPaperAssignStaff(GetPaperAssignStaffModal model)
        {
            _actionName = "GetAllQuestion()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetByIdExamPaperAssignStaff";
                        command.Parameters.AddWithValue("@ID", model.ID);

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
       
        public async Task<DataTable> GetAllExamPaperAssignStaff(GetPaperAssignStaffModal model)
        {
            _actionName = "GetAllExamPaperAssignStaff()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllExamPaperAssignStaff";
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@PaperID", model.PaperID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);

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

        public async Task<int> PostPaperQuestionSetByStaff(List<PostAddPaperAssignStaffModal> modelList)
        {
            _actionName = "PostPaperQuestionSetByStaff()";

            return await Task.Run(async () =>
            {
                int totalResult = 0;

                try
                {
                    foreach (var model in modelList)
                    {
                        using (var command = _dbContext.CreateCommand(true))
                        {
                            command.CommandText = "USP_PaperQuestionSetByStaff";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@OperationType", model.ID==0?"POST":"UPDATE");
                            command.Parameters.AddWithValue("@ID", model.ID);
                            command.Parameters.AddWithValue("@PaperID", model.PaperID);
                            command.Parameters.AddWithValue("@ExamPaperAssignStaffID", model.ExamPaperAssignStaffID);
                            command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                            command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                            command.Parameters.AddWithValue("@StreamID", model.StreamID);
                            command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                            command.Parameters.AddWithValue("@StaffID", model.StaffID);
                            command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                            command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                            command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                            command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                            command.Parameters.AddWithValue("@isSelected", model.isSelected == false?0:1);
                            command.Parameters.AddWithValue("@DeleteStatus", model.DeleteStatus);
                            command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                            command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);

                            command.Parameters.Add("@Return", SqlDbType.Int).Direction = ParameterDirection.Output;

                            _sqlQuery = command.GetSqlExecutableQuery();

                            await command.ExecuteNonQueryAsync();
                            int result = Convert.ToInt32(command.Parameters["@Return"].Value);

                            totalResult += result;
                        }
                    }

                    return totalResult;
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

        public async Task<DataTable> GetAllPaperQuestionSetByStaff(GetPaperAssignStaffModal model)
        {
            _actionName = "GetAllPaperQuestionSetByStaff()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllPaperQuestionSetByStaff";
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@QuestionID", model.QuestionID);
                        command.Parameters.AddWithValue("@PaperID", model.PaperID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);

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
