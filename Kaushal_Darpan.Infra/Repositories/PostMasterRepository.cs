using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.PostMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class PostMasterRepository : IPostMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public PostMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PostMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        // ================= GET ALL =================
        public async Task<DataTable> GetAllData(PostMasterModel request)
        {
            _actionName = "GetAllData()";
            try
            {
                DataTable dataTable;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_PostMaster_GetData";

                    command.Parameters.AddWithValue("@Action", "GetAllData");
                    command.Parameters.AddWithValue("@PostID", request.PostID);
                    command.Parameters.AddWithValue("@PostName", request.PostName ?? "");
                    command.Parameters.AddWithValue("@ServiceID", request.ServiceID);

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

        // ================= GET BY ID =================
        public async Task<PostMasterModel> GetById(int postID)
        {
            _actionName = "GetById(int postID)";
            try
            {
                DataTable dataTable;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_PostMaster_GetData";

                    command.Parameters.AddWithValue("@Action", "GetByID");
                    command.Parameters.AddWithValue("@PostID", postID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }

                var data = new PostMasterModel();

                if (dataTable != null)
                {
                    data = CommonFuncationHelper.ConvertDataTable<PostMasterModel>(dataTable);
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

        // ================= SAVE (ADD / UPDATE) =================
        public async Task<bool> SaveData(PostMasterModel request)
        {
            _actionName = "SaveData(PostMasterModel request)";
            try
            {
                int result;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_PostMaster_AddUpdate";

                    command.Parameters.AddWithValue("@PostID", request.PostID);
                    command.Parameters.AddWithValue("@PostName", request.PostName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ServiceID", request.ServiceID);
                    command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                    command.Parameters.AddWithValue("@CreatedBy", request.UserID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
                }

                return result > 0;
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

        // ================= DELETE (SOFT DELETE STYLE) =================
        public async Task<bool> DeleteDataById(PostMasterModel request)
        {
            _actionName = "DeleteDataById(PostMasterModel request)";
            try
            {
                int result;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    var query = "UPDATE ITI_Govt_EM_Post SET DeleteStatus = 1, " +
                                "ModifyBy = @ModifyBy, ModifyDate = GETDATE(), IPAddress = @IPAddress " +
                                "WHERE ID = @PostID";

                    command.CommandText = query;

                    command.Parameters.AddWithValue("@PostID", request.PostID);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy ?? 0);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
                }

                return result > 0;
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