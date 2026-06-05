using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.GroupMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.TheoryMarks;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class TheoryMarksRepository : ITheoryMarksRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public TheoryMarksRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TheoryMarksRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetTheoryMarksDetailList(TheorySearchModel body)
        {
            _actionName = "GetTheoryMarksDetailList(TheorySearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TheoryMasterList";

                    command.Parameters.AddWithValue("@action", "_getTheoryDetail");
                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@StreamID", body.StreamID);
                    command.Parameters.AddWithValue("@StudentID", body.StudentID);
                    command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                    command.Parameters.AddWithValue("@ExaminerCode", body.ExaminerCode);
                    //command.Parameters.AddWithValue("@IsConfirmed", body.IsConfirmed);
                    command.Parameters.AddWithValue("@CheckedStatus", body.CheckedStatus);
                    command.Parameters.AddWithValue("@centersubmitstatus", body.centersubmitstatus);
                    command.Parameters.AddWithValue("@centerpresentstatus", body.centerpresentstatus);
                    command.Parameters.AddWithValue("@StudentStatus", body.StudentStatus);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<int> UpdateSaveData(List<TheoryMarksModel> entity)
        {
            _actionName = "UpdateSaveData(List<TheoryMarksModel> entity)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_UpdateTheoryMarksData";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));

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
        }

        public async Task<DataTable> GetTheoryMarksRptData(TheorySearchModel body)
        {
            _actionName = "GetTheoryMarksRptData(TheorySearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (body.IsReval == 1)
                    {
                        command.CommandText = "USP_TheoryMasterList_Reval";
                    }
                    else
                    {
                        command.CommandText = "USP_TheoryMasterList";
                    }

                    command.Parameters.AddWithValue("@action", "GetTheoryMarksRptData");
                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@StreamID", body.StreamID);
                    command.Parameters.AddWithValue("@StudentID", body.StudentID);
                    command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                    command.Parameters.AddWithValue("@ExaminerCode", body.ExaminerCode);
                    command.Parameters.AddWithValue("@SSOID", body.SSOID);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                    command.Parameters.AddWithValue("@CenterCode", body.CenterCode);
                    command.Parameters.AddWithValue("@isUFM", body.isUFM);
                    //command.Parameters.AddWithValue("@IsConfirmed", body.IsConfirmed);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<int> FeedbackSubmit(ExaminerFeedbackDataModel entity)
        {
            _actionName = "FeedbackSubmit(ExaminerFeedbackDataModel entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_TheoryMarks_Feedback_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "insert");
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", entity.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", entity.Eng_NonEng);

                        command.Parameters.AddWithValue("@GroupCodeID", entity.GroupCodeID);
                        command.Parameters.AddWithValue("@GroupCode", entity.GroupCode);
                        command.Parameters.AddWithValue("@ExaminerID", entity.ExaminerID);
                        command.Parameters.AddWithValue("@ExaminerCode", entity.ExaminerCode);
                        command.Parameters.AddWithValue("@CenterCode", entity.CenterCode);
                        command.Parameters.AddWithValue("@Feedback", entity.Feedback);
                        command.Parameters.AddWithValue("@UserID", entity.UserID);
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

        public async Task<DataTable> GetTheoryMarks_Admin(TheorySearchModel body)
        {
            _actionName = "GetTheoryMarks_Admin(TheorySearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetTheoryMarks_Admin";

                    command.Parameters.AddWithValue("@action", "_getTheoryDetail");
                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@StreamID", body.StreamID);
                    command.Parameters.AddWithValue("@StudentID", body.StudentID);
                    command.Parameters.AddWithValue("@SubjectID", body.SubjectID);
                    command.Parameters.AddWithValue("@RollNo", body.RollNo);
                    command.Parameters.AddWithValue("@MarkEnter", body.MarkEnter);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@GroupCodeID", body.GroupCodeID);
                    command.Parameters.AddWithValue("@ExaminerCode", body.ExaminerCode);
                    //command.Parameters.AddWithValue("@IsConfirmed", body.IsConfirmed);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<int> UpdateTheoryMarks_Admin(List<TheoryMarksModel> entity)
        {
            _actionName = "UpdateTheoryMarks_Admin(List<TheoryMarksModel> entity)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_UpdateTheoryMarks_Admin";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));

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
        }

        public async Task<UFMStudentExtraInfoSaveModel> GetUFMStudentExtraInfo(UFMStudentExtraInfoGetModel body)
        {
            _actionName = "GetUFMStudentExtraInfo(UFMStudentExtraInfoGetModel body)";
            try
            {
                UFMStudentExtraInfoSaveModel data = new UFMStudentExtraInfoSaveModel();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetUFMStuExtraInfo";

                    command.Parameters.AddWithValue("@action", "_getUFMLetterInfoById");
                    command.Parameters.AddWithValue("@StudentID", body.StudentID);
                    command.Parameters.AddWithValue("@UFMStuExtraInfoID", body.UFMStuExtraInfoID);
                    command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                    command.Parameters.AddWithValue("@StudentExamID", body.StudentExamID);
                    command.Parameters.AddWithValue("@StudentExamPaperID", body.StudentExamPaperID);

                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                    var dt = await command.FillAsync_DataTable();

                    data = CommonFuncationHelper.ConvertDataTable<UFMStudentExtraInfoSaveModel>(dt);
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
        }

        public async Task<int> SaveUFMStudentExtraInfo(UFMStudentExtraInfoSaveModel model)
        {
            _actionName = "SaveUFMStudentExtraInfo(UFMStudentExtraInfoSaveModel model)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_SaveUFMStuExtraInfo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@action", "_saveUFMLetterInfo");
                    command.Parameters.AddWithValue("@StudentID", model.StudentID);
                    command.Parameters.AddWithValue("@SerialNo", model.SerialNo);
                    command.Parameters.AddWithValue("@SerialNo2", model.SerialNo2);
                    command.Parameters.AddWithValue("@IssueDate", model.IssueDate);
                    command.Parameters.AddWithValue("@BundleSendDate", model.BundleSendDate);
                    command.Parameters.AddWithValue("@Date2", model.Date2);
                    command.Parameters.AddWithValue("@StudentExamType", model.StudentExamType);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                    command.Parameters.AddWithValue("@IPAddress", model.IPAddress);
                    command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                    command.Parameters.AddWithValue("@StudentExamPaperID", model.StudentExamPaperID);

                    command.Parameters.Add("@Ret_Val", SqlDbType.Int);// out
                    command.Parameters["@Ret_Val"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();

                    result = Convert.ToInt32(command.Parameters["@Ret_Val"].Value);// out
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
        }

        public async Task<int> SaveUFMExtraInfo(UFMExtraInfoSaveModel model)
        {
            _actionName = "SaveUFMExtraInfo(UFMExtraInfoSaveModel model)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_SaveUFMExtraInfo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@action", "_saveUFMLetterInfo");
                    command.Parameters.AddWithValue("@SerialNo", model.SerialNo);
                    command.Parameters.AddWithValue("@SerialNo2", model.SerialNo2);
                    command.Parameters.AddWithValue("@IssueDate", model.IssueDate);
                    command.Parameters.AddWithValue("@BundleSendDate", model.BundleSendDate);
                    command.Parameters.AddWithValue("@Date2", model.Date2);
                    command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                    command.Parameters.AddWithValue("@IPAddress", model.IPAddress);

                    command.Parameters.Add("@Ret_Val", SqlDbType.Int);// out
                    command.Parameters["@Ret_Val"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();

                    result = Convert.ToInt32(command.Parameters["@Ret_Val"].Value);// out
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
        }

        public async Task<int> UpdateUFMCategory(UFMCategoryUpdateModel model)
        {
            _actionName = "UpdateUFMCategory(UFMCategoryUpdateModel model)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_Update_UFMCategoryInfo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@action", "_updateUFMCategoryInfo");                    
                    command.Parameters.AddWithValue("@UFMCategory", model.UFMCategory);
                    command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                    command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                    command.Parameters.AddWithValue("@IPAddress", CommonFuncationHelper.GetIpAddress());

                    command.Parameters.Add("@Ret_Val", SqlDbType.Int);// out
                    command.Parameters["@Ret_Val"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();

                    result = Convert.ToInt32(command.Parameters["@Ret_Val"].Value);// out
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
        }

    }
}
