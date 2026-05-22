using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.StaffMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class AssignRoleRightsRepository : IAssignRoleRightsRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public AssignRoleRightsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "AssignRoleRightsRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> SaveData(List<AssignRoleRightsModel> request)
        {
            _actionName = "SaveData(List<AssignRoleRightsModel> request";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_AddEditAssignRole";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate null handling

                    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));


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
        }

        public async Task<List<AssignRoleRightsModel>> GetAssignedRoleById(int UserID)
        {
            _actionName = "GetAssignedRoleById(int UserID)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_UserMaster_AssignedRoleList";
                    command.Parameters.AddWithValue("@Action", "AssignedRoleList_GetAll");
                    command.Parameters.AddWithValue("@UserID", UserID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }
                var data = new List<AssignRoleRightsModel>();
                if (dataTable != null)
                {
                    data = CommonFuncationHelper.ConvertDataTable<List<AssignRoleRightsModel>>(dataTable);
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

        public async Task<List<AssignRoleRightsModel>> GetAssignedRole_USerWise(GetAssignedRoleDataModel model)
        {
            _actionName = "GetAssignedRole_USerWise(GetAssignedRoleDataModel model)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_UserMaster_AssignedRoleList";
                    command.Parameters.AddWithValue("@Action", "AssignedRole_UserWise");
                    command.Parameters.AddWithValue("@UserID", model.UserID);
                    command.Parameters.AddWithValue("@ParentRoleID", model.ParentRoleID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }
                var data = new List<AssignRoleRightsModel>();
                if (dataTable != null)
                {
                    data = CommonFuncationHelper.ConvertDataTable<List<AssignRoleRightsModel>>(dataTable);
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

        public async Task<int> SaveAssignedRole_UserWise(List<AssignRoleRightsModel> request)
        {
            _actionName = "SaveAssignedRole_UserWise(List<AssignRoleRightsModel> request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveSaveAssignedRole_UserWise";
                    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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
                var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errordetails, ex);
            }
        }
    }
}
