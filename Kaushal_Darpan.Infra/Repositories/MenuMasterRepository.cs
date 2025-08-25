using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.MenuMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class MenuMasterRepository : IMenuMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public MenuMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "MenuMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        //public async Task<DataTable> GetAllData()
        //{
        //    _actionName = "GetAllData()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "MenuList";

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}

        public async Task<DataTable> GetAllData(MenuMasterSerchModel body)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "MenuList";
                        command.Parameters.AddWithValue("@action", "_getAllMenuData"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@MenuId", body.MenuId);
                        command.Parameters.AddWithValue("@MenuNameEn", body.MenuNameEn);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);


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



        public async Task<MenuMasterModel> GetById(int MenuId)
        {
            _actionName = "GetById(int MenuId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = " select * from M_MenuMaster Where MenuId='" + MenuId + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new MenuMasterModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<MenuMasterModel>(dataTable);
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
        public async Task<bool> SaveData(MenuMasterModel request)
        {
            _actionName = "SaveData(MenuMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "InsertMenu"; // Update to match the stored procedure name

                        // Add parameters based on the request object
                        //command.Parameters.AddWithValue("@MenuId", request.MenuId);
                        //command.Parameters.AddWithValue("@ParentId", request.ParentId);
                        //command.Parameters.AddWithValue("@MenuNameEn", request.MenuNameEn);
                        //command.Parameters.AddWithValue("@MenuNameHi", request.MenuNameHi);
                        //command.Parameters.AddWithValue("@MenuUrl", request.MenuUrl);
                        //command.Parameters.AddWithValue("@MenuIcon", request.MenuIcon);
                        //command.Parameters.AddWithValue("@MenuActionId", request.MenuActionId);
                        //command.Parameters.AddWithValue("@IsActive", request.IsActive);
                        //command.Parameters.AddWithValue("@Priority", request.Priority ?? 0); // Default to 0 if null
                        //command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        //command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy ?? 0); // Default to 0 if null
                        //command.Parameters.AddWithValue("@CreatedIP", request.CreatedIP);
                        //command.Parameters.AddWithValue("@UpdatedIP", request.UpdatedIP ?? ""); // Default to empty string if null
                        //command.Parameters.AddWithValue("@MenuLevel", request.MenuLevel ?? 0); // Default to 0 if null

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }

                    return result > 0; // Return true if the result indicates success
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<bool> SaveData_EditMenuDetails(MenuMasterModel request)
            {
            _actionName = "SaveData_EditMenuDetails(MenuMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InsertMenuMaster";

                        command.Parameters.AddWithValue("@MenuId", request.MenuId);
                        command.Parameters.AddWithValue("@ParentId", request.ParentId);
                        command.Parameters.AddWithValue("@MenuNameEn", request.MenuNameEn);
                        command.Parameters.AddWithValue("@MenuNameHi", request.MenuNameHi);
                        command.Parameters.AddWithValue("@MenuUrl", request.MenuUrl);
                        //command.Parameters.AddWithValue("@MenuLevel", request.MenuLevel);
                        command.Parameters.AddWithValue("@MenuIcon", request.MenuIcon);
                        command.Parameters.AddWithValue("@Priority", request.Priority);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        //command.Parameters.AddWithValue("@MenuActionId", 1);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy ", request.ModifyBy );
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        //command.Parameters.AddWithValue("@UpdationDate", request.UpdationDate);
                        //command.Parameters.AddWithValue("@UpdatedIP", _IPAddress);
                        //command.Parameters.AddWithValue("@IsShowInMenu", request.IsShowInMenu);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out


                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
        public async Task<bool> UpdateData(MenuMasterModel request)
        {
            _actionName = "UpdateData(MenuMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "USP_InsertMenuMaster";
                        //command.Parameters.AddWithValue("@ParentId", request.ParentId);
                        //command.Parameters.AddWithValue("@MenuNameEn", request.MenuNameEn);
                        //command.Parameters.AddWithValue("@MenuNameHi", request.MenuNameHi);
                        //command.Parameters.AddWithValue("@MenuUrl", request.MenuUrl);
                        //command.Parameters.AddWithValue("@MenuActionId", request.MenuActionId);
                        //command.Parameters.AddWithValue("@IsActive", request.IsActive);
                        //command.Parameters.AddWithValue("@Priority", request.Priority);
                        //command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        //command.Parameters.AddWithValue("@CreatedIP", request.CreatedIP);
                        //command.Parameters.AddWithValue("@MenuIcon", request.MenuIcon);
                        //command.Parameters.AddWithValue("@IsShowInMenu", request.IsShowInMenu);
                        command.Parameters.AddWithValue("@MenuLevel", request.MenuLevel);

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
            });
        }
        public async Task<bool> DeleteDataByID(MenuMasterModel request)
        {
            _actionName = "DeleteDataByID(MenuMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DeleteMenuByID";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MenuId", request.MenuId);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);


                        //command.Parameters.Add("@Return", SqlDbType.Int);// out
                        //command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

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
            });
        }

        public async Task<DataTable> MenuUserandRoleWise(MenuByUserAndRoleWiseModel model)
        {
            _actionName = "MenuUserandRoleWise(MenuByUserAndRoleWiseModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetMenuByUserAndRoleWise";//old=USP_MenuUserandRoleWise

                        command.Parameters.AddWithValue("@action", "_getMenuByUserAndRoleWise");
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteId);

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
        public async Task<bool> UpdateActiveStatusByID(MenuMasterModel request)
        {
            _actionName = "UpdateActiveStatusByID(MenuMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    var Query = @"
                UPDATE M_MenuMaster
                SET 
                    ActiveStatus = CASE 
                        WHEN ActiveStatus = 1 THEN 0
                        WHEN ActiveStatus = 0 THEN 1
                    END,

                    CreatedBy = '" + request.CreatedBy + "',ModifyDate = GETDATE() WHERE MenuId ='" + request.MenuId.ToString() + "' ";
                    command.CommandText = Query;

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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }
    }
}
