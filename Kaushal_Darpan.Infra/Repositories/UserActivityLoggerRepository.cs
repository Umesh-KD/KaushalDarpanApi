using System.Data;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.UserActivityLogger;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class UserActivityLoggerRepository : IUserActivityLoggerRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public UserActivityLoggerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "UserActivityLoggerRepository";
        }

        public async Task<int> SaveUserLogActivity(UserActivityLoggerModel model)
        {
            _actionName = "SaveUserLogActivity(UserActivityLoggerModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SaveUserActivityLogger";

                        command.Parameters.AddWithValue("@action", "_saveUserActivity");
                        command.Parameters.AddWithValue("@UserName", model.UserName);
                        command.Parameters.AddWithValue("@ActionName", model.ActionName);
                        command.Parameters.AddWithValue("@Controller", model.Controller);
                        command.Parameters.AddWithValue("@PageUrl", model.PageUrl);
                        command.Parameters.AddWithValue("@IPAddress", model.IpAddress);
                        command.Parameters.AddWithValue("@CreatedBy", 0);

                        command.Parameters.Add("@RetVal", SqlDbType.Int); // out
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@RetVal"].Value);// out
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
