using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AadhaarEsignAuth;
using Kaushal_Darpan.Models.Test;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class TestRepository : ITestRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public TestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "TestRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> Test_SaveHindiData(List<Test_SaveHindiData> model)
        {
            _actionName = "Test_SaveHindiData(Test_SaveHindiData model)";
            try
            {
                int result = 0;

                // Create the command and set up parameters
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "usp_testData";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@action", "_testHindiData");
                    //command.Parameters.Add("@row_json", SqlDbType.NVarChar, -1, JsonConvert.SerializeObject(model));
                    //command.Parameters.Add(new SqlParameter("@row_json", SqlDbType.NVarChar) { Value = JsonConvert.SerializeObject(model) });
                    command.Parameters.Add("@row_json", SqlDbType.NVarChar).Value = JsonConvert.SerializeObject(model);

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
                throw;
            }
        }

        public async Task<DataTable> Dummy_SendMessage(string Type)
        {
            _actionName = "Dummy_SendMessage(string Type)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Dummy_Test";

                        command.Parameters.AddWithValue("@action", "_Dummy_SendMessageSMS_NotifyBulk");
                        command.Parameters.AddWithValue("@Type", Type);

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
    }
}








