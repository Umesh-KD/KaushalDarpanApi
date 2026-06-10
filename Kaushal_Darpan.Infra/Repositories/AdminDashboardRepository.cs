using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AdminDashboard;
using Kaushal_Darpan.Models.StaffMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class AdminDashboardRepository : IAdminDashboardRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public AdminDashboardRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StaffDashboardRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAdminDashData(AdminDashboardSearchModel model)
        {
            _actionName = "GetAllData()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.CommandText = "USP_AdminDashboardIssueTracker";

                    //command.Parameters.AddWithValue("@ActionType", "GetAllData");

                    command.CommandText = "USP_AdminDashboard";

                    command.Parameters.AddWithValue("@action", _actionName);
                    command.Parameters.AddWithValue("@CommonID", model.CommonID);
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@RoleID", model.RoleID);
                    command.Parameters.AddWithValue("@IsYearly", model.IsYearly);
                    command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);

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

        public async Task<DataTable> GetAdminDashReportsData(AdminDashReportsModel model)
        {
            _actionName = "GetAdminDashReportsData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdminDashboardReports";
                        command.Parameters.AddWithValue("@action", "GetAdminDashReportsData");
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@Menu", model.Menu);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                   
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


        public async Task<DataTable> GetITI_TeacherDashboard(AdminDashboardSearchModel model)
        {
            _actionName = "GetITI_TeacherDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_TeacherDashboard";
                        //command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@CommonID", model.CommonID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);

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

        public async Task<DataTable> GetITI_TeacherDashboardNew(TeachearDashboardSearchModel model)
        {
            _actionName = "GetITI_TeacherDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetTeacherDashboard";
                        //command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Year", model.Year);
                        command.Parameters.AddWithValue("@Month", model.Month);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        //command.Parameters.AddWithValue("@StaffID", model.);

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



        public async Task<DataTable> GetBter_TeacherDashboardNew(TeachearDashboardSearchModel model)
        {
            _actionName = "GetITI_TeacherDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetTeacherDashboardBter";
                        //command.Parameters.AddWithValue("@action", _actionName);
                        command.Parameters.AddWithValue("@SSOID", model.SSOID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@Year", model.Year);
                        command.Parameters.AddWithValue("@Month", model.Month);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        //command.Parameters.AddWithValue("@StaffID", model.);

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




        public async Task<DataTable> GetEM_JDTEDashData(EM_JDTEDashboardSearchModel model)
        {
            _actionName = "GetEM_JDTEDashData()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.CommandText = "USP_AdminDashboardIssueTracker";

                    //command.Parameters.AddWithValue("@ActionType", "GetAllData");

                    command.CommandText = "USP_EM_JDTEDashboard";

                    command.Parameters.AddWithValue("@action", _actionName);
                    command.Parameters.AddWithValue("@CommonID", model.CommonID);
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@RoleID", model.RoleID);
                    command.Parameters.AddWithValue("@IsYearly", model.IsYearly);
                    command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                    command.Parameters.AddWithValue("@UserID", model.UserID);
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

        //TransferRelievingDash API
        public async Task<DataTable> GetTransferRelievingDashData(EM_TransferRelievingDashSearchModel model)
        {
            _actionName = "GetTransferRelievingDashData()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_EM_TransferSystemDashboard";
                    command.Parameters.AddWithValue("@action", _actionName);
                    command.Parameters.AddWithValue("@CommonID", model.CommonID);
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@RoleID", model.RoleID);
                    command.Parameters.AddWithValue("@IsYearly", model.IsYearly);
                    command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                    command.Parameters.AddWithValue("@UserID", model.UserID);
                    command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
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

        public async Task<DataTable> GetStaffTrainingDashboardData(EM_StaffTrainingDashboardSearchModel model)
        {
            _actionName = "GetStaffTrainingDashboardData()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StaffTrainingDashboard";
                    command.Parameters.AddWithValue("@action", _actionName);
                    command.Parameters.AddWithValue("@CommonID", model.CommonID);
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                    command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@RoleID", model.RoleID);
                    command.Parameters.AddWithValue("@IsYearly", model.IsYearly);
                    command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                    command.Parameters.AddWithValue("@UserID", model.UserID);
                    command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                    command.Parameters.AddWithValue("@ISNonGazetted", model.ISNonGazetted);
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
    }
}








