using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CheckListModel;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class CheckListRepository : ICheckListRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CheckListRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CheckListRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetCheckListQuestion(CheckListSearchModel searchModel)
        {
            _actionName = "GetAllData(GetCheckListQuestion searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCheckList_Questions";
                        command.Parameters.AddWithValue("@Action", "_GetCheckList_Questions");
                        command.Parameters.AddWithValue("@TypeID", searchModel.TypeID);
                        command.Parameters.AddWithValue("@UserID", searchModel.UserID);
                        command.Parameters.AddWithValue("@ID", searchModel.ID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@Remarks", searchModel.Remarks);
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
        public async Task<int> SaveChecklistAnswers(ChecklistAnswerRequest request)
        {
            _actionName = "SaveChecklistAnswers(ChecklistAnswerRequest request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request);
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "SaveChecklistAnswers";
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@JsonData", jsonData);
                        command.Parameters.Add("@JsonData", SqlDbType.NVarChar).Value = jsonData;
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
        public async Task<int> SaveChecklistAnswers_ITI(ChecklistAnswerRequest request)
        {
            _actionName = "SaveChecklistAnswers(ChecklistAnswerRequest request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request);
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_SaveChecklistAnswers_ITI";
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@JsonData", jsonData);
                        command.Parameters.Add("@JsonData", SqlDbType.NVarChar).Value = jsonData;
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








