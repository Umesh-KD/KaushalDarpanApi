using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.LeaveMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class LeaveMasterRepository : ILeaveMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public LeaveMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "LeaveMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(LeaveMasterSearchModel body)
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
                        command.CommandText = "USP_GetLeaveMaster";
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@Status", body.Status);

                
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);


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
        public async Task<LeaveMaster> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        //command.CommandText = " select * from M_StaffLeave Where StaffLeaveID='" + PK_ID + "' ";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaffLeave_Action";
                        
                        command.Parameters.AddWithValue("@StaffLeaveID", PK_ID);
                        command.Parameters.AddWithValue("@Action", "GetLeaveRequestByID");

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                        
                    }
                    var data = new LeaveMaster();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<LeaveMaster>(dataTable);
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
        public async Task<int> SaveData(LeaveMaster request)
        {
            _actionName = "SaveData(CollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    { 
                        command.CommandText = "sp_InsertOrUpdate_StaffLeave";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@StaffLeaveID", request.StaffLeaveID);
                        command.Parameters.AddWithValue("@LeaveID", request.LeaveID);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@From_Date", request.From_Date);
                        command.Parameters.AddWithValue("@To_Date", request.To_Date);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        //command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
                        //command.Parameters.AddWithValue("@ActionDate", request.ActionDate);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        //command.Parameters.AddWithValue("@ActionRemark", request.ActionRemark);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@TotalDays", request.TotalDays);
                        command.Parameters.AddWithValue("@IsHeadQuarter", request.IsHeadQuarter);
                        command.Parameters.AddWithValue("@LeaveTypeID", request.LeaveTypeID);
                        command.Parameters.AddWithValue("@ActionBy", request.UserID);
                        command.Parameters.AddWithValue("@txtIsHeadQuarterAddress", request.txtIsHeadQuarterAddress);
                        command.Parameters.AddWithValue("@txtIsHeadQuarterMobileNo", request.txtIsHeadQuarterMobileNo);
                        command.Parameters.AddWithValue("@DisUploadDoc", request.DisUploadDoc);
                        command.Parameters.AddWithValue("@UploadDoc", request.UploadDoc);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        //command.Parameters.AddWithValue("@RemainingLeave", request.RemainingLeave);
                        command.Parameters.AddWithValue("@SessionTypeID", request.SessionTypeID);
                        command.Parameters.AddWithValue("@AppliedRoleID", request.RoleID);

                        command.Parameters.Add("@retval_ID", SqlDbType.Int);
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);
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
       
        
        public async Task<bool> DeleteDataByID(LeaveMaster request)
        {
            _actionName = "DeleteDataByID(HRMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $" update M_StaffLeave  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where StaffLeaveID={request.StaffLeaveID}";

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

        public async Task<bool> SaveStaffLeaveRequest(LeaveMaster request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveStaffLeaveRequest(LeaveMaster request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SaveLeavevalidation";
                        
                        command.Parameters.AddWithValue("@Action", request.Action);

                        command.Parameters.AddWithValue("@StaffLeaveID", request.StaffLeaveID);
                        command.Parameters.AddWithValue("@ActionBy", request.ActionBy);                    
                        command.Parameters.AddWithValue("@ActionRemark", request.ActionRemark);
                        command.Parameters.AddWithValue("@TotalDays", request.TotalDays);
                        command.Parameters.AddWithValue("@StaffID", request.StaffID);
                        command.Parameters.AddWithValue("@LeaveID", request.LeaveID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@SessionTypeID", request.SessionTypeID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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

        public async Task<DataTable> GetStaffLeaveRequest(LeaveMasterSearchModel body)
        {
            _actionName = "GetStaffLeaveRequest(LeaveMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetLeaveMasterValidation";
                        
                        command.Parameters.AddWithValue("@action", "_getPrincipalLeaveRequestData");

                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                       
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

        public async Task<DataTable> ByIDStaffLeaveList(LeaveMasterSearchModel body)
        {
            _actionName = "ByIDStaffLeaveList(LeaveMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetLeaveByID";
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@Status", body.Status);


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@To_Date", body.To_Date);
                        command.Parameters.AddWithValue("@From_Date", body.From_Date);
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        //command.Parameters.AddWithValue("@SSOID", body.SSOID);
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


        public async Task<DataTable> GetRemainingLeave(LeaveMasterSearchModel body)
        {
            _actionName = "GetRemainingLeave(LeaveMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StaffLeave_Action";
                        
                        command.Parameters.AddWithValue("@Action", body.Action);
                        command.Parameters.AddWithValue("@LeaveID", body.LeaveID);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
                        command.Parameters.AddWithValue("@StaffID", body.StaffID);
                        command.Parameters.AddWithValue("@SessionTypeID", body.SessionTypeID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);

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


        public async Task<DataTable> GetLeaveCreditStaffData(LeaveMasterSearchModel body)
        {
            _actionName = "GetLeaveCreditStaffData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffForLeaveCredit";

                        if (body.SessionTypeID == (int)SessionType.FinancialYear)
                        {
                        command.Parameters.AddWithValue("@Action", "_getLeaveCreditStaffData_FY");
                        }
                        else if (body.SessionTypeID == (int)SessionType.CalenderYear)
                        {
                            command.Parameters.AddWithValue("@Action", "_getLeaveCreditStaffData_CY");
                        }

                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        //command.Parameters.AddWithValue("@To_Date", body.To_Date);
                        //command.Parameters.AddWithValue("@From_Date", body.From_Date);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@SessionTypeID", body.SessionTypeID);

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

        public async Task<DataTable> GetStaffWithLeaveBalance(LeaveMasterSearchModel body)
        {
            _actionName = "GetStaffWithLeaveBalance()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffWithLeaveBalance";
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        //command.Parameters.AddWithValue("@To_Date", body.To_Date);
                        //command.Parameters.AddWithValue("@From_Date", body.From_Date);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@SessionTypeID", body.SessionTypeID);
                        command.Parameters.AddWithValue("@Action", "_getStaffWithLeaveBalanceData");

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

        public async Task<bool> Save_CreditStaffLeave(List<CreditLeaveModel> request)
        {
            _actionName = "Save_CreditStaffLeave(CreditLeaveModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveLeaveCreditForStaff";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@action", "_addLeaveCreditForStaffData");


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
