using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.DesignationMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class DesignationMasterRepository : IDesignationMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;

        public DesignationMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "DesignationMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
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
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DesignationMaster_GetData";

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

        public async Task<DesignationMasterModel> GetById(int designationID)
        {
            _actionName = "GetById(int designationID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM M_DesignationMaster WHERE DesignationID = @DesignationID";
                        command.Parameters.AddWithValue("@DesignationID", designationID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    var data = new DesignationMasterModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DesignationMasterModel>(dataTable);
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

        public async Task<bool> SaveData(DesignationMasterModel request)
        {
            _actionName = "SaveData(DesignationMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type

                        command.CommandText = "USP_DesignationMaster_AddUpdate";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@DesignationNameEnglish", request.DesignationNameEnglish ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DesignationNameHindi", request.DesignationNameHindi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DesignationNameShort", request.DesignationNameShort ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);

                        // Use a default value or ensure CreatedBy is not null
                        var createdBy = request.CreatedBy ?? 0; // Replace 0 with an appropriate default value
                        command.Parameters.AddWithValue("@CreatedBy", request.UserID);
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


        public async Task<bool> UpdateData(DesignationMasterModel request)
        {
            _actionName = "UpdateData(DesignationMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_DesignationMaster_AddUpdate";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DesignationID", request.DesignationID);
                        command.Parameters.AddWithValue("@DesignationNameEnglish", request.DesignationNameEnglish ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DesignationNameHindi", request.DesignationNameHindi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DesignationNameShort", request.DesignationNameShort ?? (object)DBNull.Value);
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

        public async Task<bool> DeleteDataById(DesignationMasterModel request)
        {
            _actionName = "DeleteDataById(DesignationMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        var Query = " update M_DesignationMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='" + request.ModifyBy + "',ModifyDate=GETDATE(),IPAddress='" + CommonFuncationHelper.GetIpAddress() + "' ";
                        Query += " Where DesignationID='" + request.DesignationID.ToString() + "' ";
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
    }
}




