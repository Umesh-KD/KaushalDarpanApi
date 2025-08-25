using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AdminDashboardIssueTrackerSearchModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class AdminDashboardIssueTrackerRepository : IAdminDashboardIssueTrackerRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;

        public AdminDashboardIssueTrackerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "AdminDashboardIssueTrackerRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAdminDashData(AdminDashboardIssueTrackerSearchModel model)
        {
            _actionName = "GetAllData()";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                   // command.CommandText = "USP_AdminDashboard";
                    command.CommandText = "USP_AdminDashboardIssueTracker";

                    command.Parameters.AddWithValue("@ActionType", _actionName);
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

    }
}
