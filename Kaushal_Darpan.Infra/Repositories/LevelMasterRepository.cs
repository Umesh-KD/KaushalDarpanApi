using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.LevelMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class LevelMasterRepository : ILevelMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;

        public LevelMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "LevelMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> CreateLevel(LevelMasterModel request)
        {
            _actionName = "CreateLevel(LevelMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "usp_Trn_LevelMast";
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@action", "_addLevel");
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@LevelID", request.LevelID);
                        command.Parameters.AddWithValue("@LevelNameEnglish", request.LevelNameEnglish ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LevelNameHindi", request.LevelNameHindi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LevelNameShort", request.LevelNameShort ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);

                        // Use a default value or ensure CreatedBy is not null
                        //var createdBy = request.CreatedBy ?? 0; // Replace 0 with an appropriate default value
                        //command.Parameters.AddWithValue("@CreatedBy", createdBy);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<bool> UpdateLevelById(LevelMasterModel request)
        {
            _actionName = "UpdateLevelById(LevelMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "usp_Trn_LevelMast";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@LevelID", request.LevelID);
                        command.Parameters.AddWithValue("@LevelNameEnglish", request.LevelNameEnglish ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LevelNameHindi", request.LevelNameHindi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LevelNameShort", request.LevelNameShort ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);

                        var createdBy = request.CreatedBy ?? 0;
                        command.Parameters.AddWithValue("@CreatedBy", createdBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress ?? (object)DBNull.Value);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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


        public async Task<bool> DeleteLevelById(LevelMasterModel request)
        {
            _actionName = "DeleteLevelById(LevelMaster request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand())
                {
                    var Query = @"
                    UPDATE [dbo].[M_LevelMaster]
                    SET 
                        [DeleteStatus] = 1,
                        [ActiveStatus] = 0,
                        [ModifyBy] = @ModifyBy,
                        [ModifyDate] = GETDATE()
                    WHERE [LevelID] = @LevelID;
                    ";

                    command.CommandText = Query;
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                    command.Parameters.AddWithValue("@LevelID", request.LevelID);

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
        }

        public async Task<DataTable> GetAllData()
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "EXEC USP_LevelMaster ";

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
        public async Task<LevelMasterModel> GetLevelById(int levelId)
        {
            _actionName = "GetLevelById(int levelId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM M_LevelMaster WHERE LevelID = @LevelID";
                        command.Parameters.AddWithValue("@LevelID", levelId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    var data = new LevelMasterModel();
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<LevelMasterModel>(dataTable);
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
    }
}
