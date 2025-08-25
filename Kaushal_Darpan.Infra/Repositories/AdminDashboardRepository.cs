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
                using (var command = _dbContext.CreateCommand())
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
                    using (var command = _dbContext.CreateCommand())
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
                    using (var command = _dbContext.CreateCommand())
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
    }
}








