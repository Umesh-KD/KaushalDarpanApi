using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class PayLevelMasterRepository: IPayLevelMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public PayLevelMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PayLevelMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SavePayLevelMasterData(PayLevelMasterDataModel request)
        {
            _actionName = "SavePayLevelMasterData(PayLevelMasterDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_PayLevelMaster_IU";
                    command.Parameters.AddWithValue("@PayLevelID", request.PayLevelID);
                    command.Parameters.AddWithValue("@PayLevel", request.PayLevel);
                    command.Parameters.AddWithValue("@UserID", request.UserID);

                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    command.Parameters.Add("@Return", SqlDbType.Int);
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;

                    _sqlQuery = command.GetSqlExecutableQuery();
                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        public async Task<DataTable> GetPayLevelMasterData(PayLevelMasterDataModel body)
        {
            _actionName = "GetPayLevelMasterData(PayLevelMasterDataModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //command.CommandText = "USP_BTER_EM_GetStaffList";
                    command.CommandText = "USP_PayLevelMaster_GetData";
                    command.Parameters.AddWithValue("@Action", body.Action);
                    command.Parameters.AddWithValue("@UserID", body.UserID);
                    command.Parameters.AddWithValue("@PayLevelID", body.PayLevelID);

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

        public async Task<bool> DeletePayLevel_ByID(PayLevelMasterDataModel request)
        {
            _actionName = "DeletePayLevel_ByID(PayLevelMasterDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PayLevelMaster_GetData";
                        command.Parameters.AddWithValue("@Action", "delete_byID");

                        command.Parameters.AddWithValue("@PayLevelID", request.PayLevelID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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
    }
}
