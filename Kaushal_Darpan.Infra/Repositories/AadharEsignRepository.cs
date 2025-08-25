using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class AadharEsignRepository : IAadharEsignRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public AadharEsignRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "AadharEsignRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<EsignDataHistoryReponseModel> GetEsignDataHistory(EsignDataHistoryRequestModel model)
        {
            _actionName = "GetEsignDataHistory(EsignDataHistoryRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    EsignDataHistoryReponseModel data = new EsignDataHistoryReponseModel();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_getEsignedData_History";

                        command.Parameters.AddWithValue("@action", "_getEsignedDataByApiType");
                        command.Parameters.AddWithValue("@txn", model.Txn);
                        command.Parameters.AddWithValue("@ApiType", model.ApiType);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    data = CommonFuncationHelper.ConvertDataTable<EsignDataHistoryReponseModel>(dataTable);
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
        public async Task<int> SaveEsignDataHistory(EsignDataHistoryRequestModel model)
        {
            _actionName = "SaveEsignDataHistory(EsignDataHistoryRequestModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    // Create the command and set up parameters
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "usp_saveEsignedData_History";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@action", "_saveEsignedDataByApiType");
                        command.Parameters.AddWithValue("@txn", model.Txn);
                        command.Parameters.AddWithValue("@Response", model.Response);
                        command.Parameters.AddWithValue("@ApiType", model.ApiType);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                        command.Parameters.AddWithValue("@UserNameInAadhar", model.UserNameInAadhar);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@RetVal", SqlDbType.Int); // out
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output; // out

                        // Execute the command and get the result
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@RetVal"].Value); // out
                    }

                    // Return 
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








