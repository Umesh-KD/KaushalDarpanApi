using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly DBContext _dbContext;
        public ErrorLogRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddErrorLog(Tbl_Trn_ErrorLog data)
        {
            return await Task.Run(async () =>
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_Trn_ErrorLog";
                    command.Parameters.AddWithValue("@action", "_adderrorlog");
                    command.Parameters.AddWithValue("@Errordescription", data.ErrorDescription);
                    command.Parameters.AddWithValue("@CreatedDate", data.CreatedDate);
                    command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                    command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                    result = await command.ExecuteNonQueryAsync();
                    var retval_ID = command.Parameters["@retval_ID"].Value;// out
                }
                return result;
            });
        }

        public async Task<List<Tbl_Trn_ErrorLog>> GetAllErrorLog(GenericPaginationSpecification specification)
        {
            return await Task.Run(async () =>
            {
                DataSet ds = null;
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_ErrorLog";
                    command.Parameters.AddWithValue("@action", "usp_ErrorLog");
                    // input pagination
                    command.Parameters.AddWithValue("@PageNumber", specification.PageNumber);
                    command.Parameters.AddWithValue("@PageSize", specification.PageSize);
                    ds = await command.FillAsync();
                }
                // class
                var data = new List<Tbl_Trn_ErrorLog>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<Tbl_Trn_ErrorLog>>(ds.Tables[0]);
                    }
                }
                return data;
            });
        }

        public async Task<Tbl_Trn_ErrorLog> GetErrorLogById(int errorLogId)
        {
            return await Task.Run(async () =>
            {
                DataSet ds = null;
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_ErrorLog";
                    command.Parameters.AddWithValue("@action", "_GetErrorLogById");
                    command.Parameters.AddWithValue("@Id", errorLogId);
                    ds = await command.FillAsync();
                }
                // class
                var data = new Tbl_Trn_ErrorLog();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<Tbl_Trn_ErrorLog>(ds.Tables[0]);
                    }
                }
                return data;
            });
        }
    }
}
