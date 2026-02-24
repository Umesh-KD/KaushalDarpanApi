using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ITIHrMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ItiCompanyMasterRepository : I_ITICompanyMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ItiCompanyMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ItiCompanyMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ItiCompanyMasterSearchModel body)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetItiCompanyMaster";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@Status", body.Status);
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

        public async Task<bool> SaveData(ItiCompanyMasterModels request)
        {
            _actionName = "SaveData(ItiCompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PlacementItiCompanyMaster_IU";

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@CompanyRegNo", request.CompanyRegNo);
                        command.Parameters.AddWithValue("@CompanyTypeId", request.CompanyTypeId);
                        command.Parameters.AddWithValue("@Logo", request.CompanyPhoto);
                        command.Parameters.AddWithValue("@Dis_Name", request.Dis_CompanyName);
                        command.Parameters.AddWithValue("@UploadedDoc", request.UploadedDoc);
                        command.Parameters.AddWithValue("@Dis_UploadedDoc", request.Dis_UploadedDoc);

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@HrList", JsonConvert.SerializeObject(request.ListCompanyHRDetails));

                        //command.Parameters.AddWithValue("@HRName", request.HRName);
                        //command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        //command.Parameters.AddWithValue("@EmailId", request.EmailId);

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
        public async Task<ItiCompanyMasterModels> GetById(ItiCompanyMasterSearchModel request)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        //command.CommandText = " select pcm.*, hr.Name As HRName, hr.EmailId,hr.MobileNo from M_ITIPlacementCompanyMaster pcm left join M_ITIHRManagerMaster hr on pcm.ID=hr.PlacementCompanyID Where ID='" + PK_ID + "' ";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPlacement_Action";

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@Action", "_GetDataById");
                        command.Parameters.AddWithValue("@PK_ID", request.ID);
                        
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    var data = new ItiCompanyMasterModels();
                    if (ds != null)
                    {
                        //data = CommonFuncationHelper.ConvertDataTable<ItiCompanyMasterResponsiveModel>(dataTable);
                        if (ds.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ItiCompanyMasterModels>(ds.Tables[0]);
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                data.ListCompanyHRDetails = CommonFuncationHelper.ConvertDataTable<List<ItiHrMaster>>(ds.Tables[1]);
                            }
                        }
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

        public async Task<bool> DeleteDataByID(ItiCompanyMasterModels request)
        {
            _actionName = "DeleteDataByID(ItiCompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update [M_ITIPlacementCompanyMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";

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

        public async Task<bool> Save_CompanyValidation_NodalAction(ItiCompanyMaster_Action request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "Save_CompanyValidation_NodalAction(ItiCompanyMaster_Action request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCompanyValidation_NodalAction";
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@ActionRemarks", request.ActionRemarks);
                        command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<DataTable> CompanyValidationList(ItiCompanyMasterSearchModel body)
        {
            _actionName = "CompanyValidationList(ItiCompanyMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCompanyValidationList";
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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




        public async Task<DataTable> CompanyMasterReport(ItiCompanyMasterSearchModel body)
        {
            _actionName = "CompanyMasterReport(CompanyMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyMasterReport";
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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
